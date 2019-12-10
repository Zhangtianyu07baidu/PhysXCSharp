#include "stdafx.h"
#include "UserAssertHandler.h"
#include <cstdarg>
//#include "PsFoundation.h"
#include "PsThread.h"

PhysicsManager* PhysicsManager::mInstance = NULL;

PhysicsManager::PhysicsManager(OnErrorCallback log) :
	foundation(NULL),
	physics(NULL),
	globalDispatcher(NULL),
	cooking(NULL),
	material(NULL),
	sceneQuerier(NULL),
	sceneUpdateCallback(NULL)
{
	this->errorCallback.SetErrorCallback(log);
}

PhysicsManager::~PhysicsManager()
{
	mInstance = NULL;
}

void PhysicsManager::Initialize()
{
	this->Print(PxErrorCode::eDEBUG_INFO, "PhysicsManager::Initialize");

	UserAssertHandler assertHandler;
	PxSetAssertHandler(assertHandler);

	//foundation and physics must be created at initialization stage
	this->foundation = PxCreateFoundation(PX_FOUNDATION_VERSION, this->allocator, this->errorCallback);
	//this->physics = PxCreatePhysics(PX_PHYSICS_VERSION, *this->foundation, PxTolerancesScale(), true);
	this->physics = PxCreateBasePhysics(PX_PHYSICS_VERSION, *this->foundation, PxTolerancesScale());

	PxTolerancesScale scale;
	PxCookingParams params(scale);
	// lower hierarchy for internal mesh
	params.meshCookingHint = PxMeshCookingHint::eCOOKING_PERFORMANCE;
	this->cooking = PxCreateCooking(PX_PHYSICS_VERSION, *this->foundation, params);
	
	/*
	 * Best performance is usually achieved if the number of threads is less than or
	 * equal to the available hardware threads of the platform you are running on,
	 * creating more worker threads than hardware threads will often lead to worse performance.
	 * For platforms with a single execution core, the CPU dispatcher can be created with zero
	 * worker threads (PxDefaultCpuDispatcherCreate(0)). In this case all work will be executed
	 * on the thread that calls PxScene::simulate(), which can be more efficient than using
	 * multiple threads.
	 */
	const int mNbThreads = PxMax(PxI32(physx::shdfnd::Thread::getNbPhysicalCores()) - 1, 0);
	this->globalDispatcher = PxDefaultCpuDispatcherCreate(mNbThreads);
	this->material = this->physics->createMaterial(0.5f, 1.0f, 0.0f);
}

PhysicsScene* PhysicsManager::CreateScene()
{
	PhysicsScene* s = new PhysicsScene();

	PxSceneDesc sceneDesc(this->physics->getTolerancesScale());
	sceneDesc.gravity = PxVec3(0.0f, -9.81f, 0.0f);

	sceneDesc.cpuDispatcher = this->globalDispatcher;
	sceneDesc.filterShader = PxDefaultSimulationFilterShader;
	sceneDesc.flags |= PxSceneFlag::eENABLE_ACTIVETRANSFORMS;

	//create scene and default material
	s->scene = this->physics->createScene(sceneDesc);

	//character controller
	s->controllerMgr = PxCreateControllerManager(*s->scene);

	s->query = new SceneQuerier(s);

	return s;
}

PxRigidActor* PhysicsManager::AddActor(PhysicsScene* scene, Collider* collider, bool isDynamic)
{
	PxRigidActor* actor = collider->CreateActor(this->physics, this->material, isDynamic);
	scene->scene->addActor(*actor);
	actor->userData = collider;
	
	if (isDynamic)
	{
		PxRigidDynamic* dynamic = (PxRigidDynamic*)actor;
		dynamic->setMass(1.0f);
		dynamic->setLinearDamping(5);
		dynamic->setAngularDamping(PX_MAX_F32);
	}
	return actor;
}

PxRigidDynamic* PhysicsManager::AddController(CapsuleController* controller)
{
	PxRigidDynamic* actor = controller->CreateController(this->material);
	actor->userData = controller;
	return actor;
}

void PhysicsManager::RemoveCollider(PhysicsScene* scene, PxRigidActor& actor)
{
	scene->scene->removeActor(actor);
	ColliderBase* collider = static_cast<ColliderBase*>(actor.userData);
	collider->Release();
	delete collider;
}

void PhysicsScene::Release()
{
	PxActorTypeFlags actorFlags = PxActorTypeFlag::eRIGID_STATIC | PxActorTypeFlag::eRIGID_DYNAMIC;
	PxU32 actorSize = this->scene->getNbActors(actorFlags);
	PxActor** buffer = new PxActor*[actorSize];
	PxU32 num = this->scene->getActors(actorFlags, buffer, actorSize);
	for (PxU32 index = 0; index < num; index++)
	{
		PxRigidActor* actor = (PxRigidActor*)buffer[index];
		ColliderBase* collider = static_cast<ColliderBase*>(actor->userData);
		collider->Release();
		delete collider;
	}
	delete[] buffer;

	this->controllerMgr->release();
	this->scene->release();
	delete this->query;
}

void PhysicsManager::Release()
{
	this->material->release();
	this->globalDispatcher->release();
	this->physics->release();
	this->cooking->release();
	this->foundation->release();
	this->sceneUpdateCallback = NULL;
	this->errorCallback.Release();
	this->ignoreLayers.clear();
}

Collider* PhysicsManager::GetCollider(PxRigidActor* actor)
{
	Collider* collider = static_cast<Collider*>(actor->userData);
	return collider;
}

void PhysicsManager::Update(PhysicsScene* scene, float deltaTime)
{
	scene->scene->simulate(deltaTime); //get the world state after 1/60.f second
	if (scene->scene->fetchResults(true)) //these two line must be used in pair
	{
		PxU32 nbActiveTransforms;// retrieve array of actors that moved
		const PxActiveTransform* activeTransforms = scene->scene->getActiveTransforms(nbActiveTransforms);

		// update each render object with the new transform
		for (PxU32 i = 0; i < nbActiveTransforms; ++i)
		{
			const PxActiveTransform& activeTrans = activeTransforms[i];
			ColliderBase* colliderBase = static_cast<ColliderBase*>(activeTrans.userData);
			if (colliderBase->GetType() != ColliderType::Controller)
			{
				PxRigidDynamic* actor = (PxRigidDynamic*)activeTrans.actor;
				Vector3 pos(actor->getGlobalPose().p);
				Vector4 rot(actor->getGlobalPose().q);

				if (this->sceneUpdateCallback != NULL)
				{
					this->sceneUpdateCallback(scene, actor, &pos, &rot);
				}
			}
		}
	}
}

void PhysicsManager::Print(PxErrorCode::Enum code, const char* messageFmt, ...)
{
	va_list va;
	va_start(va, messageFmt);
	static const size_t bufSize = 1024;
	char stringBuffer[bufSize];
#ifdef _WINDOWS
	vsnprintf_s(stringBuffer, bufSize, messageFmt, va);
#else
	vsnprintf(stringBuffer, bufSize, messageFmt, va);
#endif
	this->errorCallback.reportError(code, stringBuffer, __FILE__, __LINE__);
	va_end(va);
}

void PhysicsManager::RegistSceneUpdateCallback(OnSceneUpdateCallback cb)
{
	this->sceneUpdateCallback = cb;
}

PxPhysics* PhysicsManager::GetPhysics() const
{
	return this->physics;
}

PxCooking* PhysicsManager::GetCooking() const
{
	return this->cooking;
}

std::set<int>& PhysicsManager::GetIgnoreLayers()
{
	return this->ignoreLayers;
}

PhysicsManager* PhysicsManager::CreateInstance(OnErrorCallback log)
{
	PX_ASSERT(mInstance == NULL);
	if (!mInstance)
	{
		mInstance = new PhysicsManager(log);
	}
	return mInstance;
}

PhysicsManager& PhysicsManager::GetInstance()
{
	PX_ASSERT(mInstance != NULL);
	return *mInstance;
}

void PhysicsManager::DestroyInstance()
{
	PX_ASSERT(mInstance != NULL);
	mInstance->Release();
	delete mInstance;
}