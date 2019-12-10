// PhysXDll.cpp: 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"

static OnErrorCallback envlog;

static void Log(int categary, const char* msg)
{
	if (envlog)
	{
		envlog(categary, msg);
	}
}

EXPORT_DLL void SetErrorCallback(OnErrorCallback log)
{
	envlog = log;
}

#pragma region Controller
EXPORT_DLL void* AddCharacterController(PhysicsScene* scene, CapsuleControllerData data, 
	OnFilterCallback withWorld, OnFilterCallback withController,
	OnHitCallback hitShape, OnHitCallback hitController)
{
	CapsuleController* controller = new CapsuleController(data, scene, withWorld, withController, hitShape, hitController);
	return PhysicsManager::GetInstance().AddController(controller);
}

EXPORT_DLL void GetControllerPosition(void* p, PxVec3* pos)
{
	PxRigidDynamic* actor = static_cast<PxRigidDynamic*>(p);
	CapsuleController* controller = static_cast<CapsuleController*>(actor->userData);
	const PxExtendedVec3& position = controller->GetPosition();
	pos->x = position.x;
	pos->y = position.y;
	pos->z = position.z;
}

EXPORT_DLL void SetControllerPosition(void* p, Vector3 pos)
{
	PxRigidDynamic* actor = static_cast<PxRigidDynamic*>(p);
	CapsuleController* controller = static_cast<CapsuleController*>(actor->userData);
	controller->SetPosition(PxExtendedVec3(pos.x, pos.y, pos.z));
}

EXPORT_DLL void GetControllerFootPosition(PxRigidDynamic* actor, PxVec3* footPos)
{
	CapsuleController* controller = static_cast<CapsuleController*>(actor->userData);
	const PxExtendedVec3& position = controller->GetFootPosition();
	footPos->x = position.x;
	footPos->y = position.y;
	footPos->z = position.z;
}

EXPORT_DLL void SetControllerFootPosition(PxRigidDynamic* actor, Vector3 pos)
{
	CapsuleController* controller = static_cast<CapsuleController*>(actor->userData);
	controller->SetFootPosition(PxExtendedVec3(pos.x, pos.y, pos.z));
}

EXPORT_DLL void SetControllerRadius(void* p, float radius)
{
	PxRigidDynamic* actor = static_cast<PxRigidDynamic*>(p);
	CapsuleController* controller = static_cast<CapsuleController*>(actor->userData);
	controller->SetRadius(radius);
}

EXPORT_DLL void SetControllerHeight(void* p, float height)
{
	PxRigidDynamic* actor = static_cast<PxRigidDynamic*>(p);
	CapsuleController* controller = static_cast<CapsuleController*>(actor->userData);
	controller->SetHeight(height);
}


EXPORT_DLL void InvalidateCache(PxRigidDynamic* p)
{
	CapsuleController* controller = static_cast<CapsuleController*>(p->userData);
	controller->InvalidateCache();
}

EXPORT_DLL int MoveController(void* p, Vector3 vec, Vector3* footPos)
{
	PxRigidDynamic* actor = static_cast<PxRigidDynamic*>(p);
	CapsuleController* controller = static_cast<CapsuleController*>(actor->userData);
	PxVec3 motion = vec.ToPxVec3();

	int flag = (PxU8)controller->Move(motion);

	const PxExtendedVec3& position = controller->GetFootPosition();
	footPos->x = position.x;
	footPos->y = position.y;
	footPos->z = position.z;

	return flag;
}

EXPORT_DLL void IgnoreControllerLayerCollision(int layer1, int layer2, bool ignore)
{
	std::set<int>& layers = PhysicsManager::GetInstance().GetIgnoreLayers();
	const int result = (1 << layer1) | (1 << layer2);
	if (ignore)
	{
		layers.insert(result);
	}
	else
	{
		std::set<int>::iterator it = layers.find(result);
		layers.erase(it);
	}
}

EXPORT_DLL void SetFilterEnabled(void* p, bool enabled)
{
	PxRigidDynamic* actor = static_cast<PxRigidDynamic*>(p);
	CapsuleController* controller = static_cast<CapsuleController*>(actor->userData);
	controller->SetFilterEnabled(enabled);
}

/*EXPORT_DLL bool CanStandUp(void* p)
{
	try
	{
		if (sceneManager->IsInitialize())
		{
			PxRigidDynamic* actor = static_cast<PxRigidDynamic*>(p);
			CapsuleController* controller = static_cast<CapsuleController*>(actor->userData);
			return controller->CanStandUp(sceneManager->GetScene());
		}
		else
		{
			sceneManager->Output(PxErrorCode::eINVALID_OPERATION, "sceneManager is not init");
			return false;
		}
	}
	catch (char *error)
	{
		sceneManager->Output(PxErrorCode::eINVALID_OPERATION, error);
	}
}*/
#pragma endregion

EXPORT_DLL void Initialize()
{
	PhysicsManager* manager = PhysicsManager::CreateInstance(Log);
	manager->Initialize();
}

EXPORT_DLL void* CreateScene()
{
	return PhysicsManager::GetInstance().CreateScene();
}

EXPORT_DLL void* AddBoxCollider(PhysicsScene* scene, BoxColliderData data, bool isDynamic)
{
	Collider* collider = new BoxCollider(scene, data);
	return PhysicsManager::GetInstance().AddActor(scene, collider, isDynamic);
}

EXPORT_DLL void* AddSphereCollider(PhysicsScene* scene, SphereColliderData data, bool isDynamic)
{
	Collider* collider = new SphereCollider(scene, data);
	return PhysicsManager::GetInstance().AddActor(scene, collider, isDynamic);
}

EXPORT_DLL void* AddCapsuleCollider(PhysicsScene* scene, CapsuleColliderData data, bool isDynamic)
{
	Collider* collider = new CapsuleCollider(scene, data);
	return PhysicsManager::GetInstance().AddActor(scene, collider, isDynamic);
}

EXPORT_DLL PxRigidActor* AddMeshCollider(PhysicsScene* scene, MeshColliderData data, float* vertices, int* triangles)
{
	Collider* collider = new MeshCollider(data, scene, vertices, triangles);
	return PhysicsManager::GetInstance().AddActor(scene, collider, false);
}

EXPORT_DLL void Update(PhysicsScene* scene, float deltaTime)
{
	PhysicsManager::GetInstance().Update(scene, deltaTime);
}

EXPORT_DLL void SetActorTransform(void* p, Vector3 position, Vector4 rotation)
{
	PxRigidActor* actor = static_cast<PxRigidActor*>(p);
	Collider* collider = PhysicsManager::GetInstance().GetCollider(actor);
	actor->setGlobalPose(PxTransform(position.ToPxVec3(), rotation.ToPxQuat()));
}

EXPORT_DLL void SetShapeInSceneQueryEnabled(void* p, bool enabled)
{
	PxRigidActor* actor = static_cast<PxRigidActor*>(p);
	PxShape* shape;
	actor->getShapes(&shape, 1);
	shape->setFlag(PxShapeFlag::eSCENE_QUERY_SHAPE, enabled);
}

EXPORT_DLL void RemoveCollider(PhysicsScene* scene, void* p)
{
	PxRigidActor* actor = static_cast<PxRigidActor*>(p);
	PhysicsManager::GetInstance().RemoveCollider(scene, *actor);
}

EXPORT_DLL void RegistSceneUpdateCallback(OnSceneUpdateCallback cb)
{
	PhysicsManager::GetInstance().RegistSceneUpdateCallback(cb);
}

EXPORT_DLL void ReleaseScene(PhysicsScene* scene)
{
	scene->Release();
	delete scene;
}

EXPORT_DLL void Release()
{
	PhysicsManager::DestroyInstance();
}

EXPORT_DLL void SetActorSimulationEnabled(void* p, bool enable)
{
	PxRigidActor* actor = static_cast<PxRigidActor*>(p);
	actor->setActorFlag(PxActorFlag::eDISABLE_SIMULATION, !enable);
}

EXPORT_DLL void SetActorGravity(void* p, bool useGravity)
{
	PxRigidActor* actor = static_cast<PxRigidActor*>(p);
	actor->setActorFlag(PxActorFlag::eDISABLE_GRAVITY, !useGravity);
	PxRigidDynamic* dynamicActor = actor->is<PxRigidDynamic>();
	if (useGravity)
	{
		if (dynamicActor->isSleeping())
		{
			dynamicActor->wakeUp();
		}
	}
	else
	{
		if (!dynamicActor->isSleeping())
		{
			dynamicActor->putToSleep();
		}
	}
}

#pragma region Raycast C链接不允许重载
EXPORT_DLL bool Raycast(PhysicsScene* scene, Vector3 origin, Vector3 direction, float maxDistance)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_REAL;
	PxRaycastBuffer buffer = scene->query->Raycast(origin.ToPxVec3(), direction.ToPxVec3(), maxDistance);
	return buffer.hasBlock;
}

EXPORT_DLL bool RaycastWithInfo(PhysicsScene* scene, Vector3 origin, Vector3 direction, float maxDistance, RaycastHit* hitInfo)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_REAL;
	PxRaycastBuffer buffer = scene->query->Raycast(origin.ToPxVec3(), direction.ToPxVec3(), maxDistance);
	if (buffer.hasBlock)
	{
		Collider* collider = PhysicsManager::GetInstance().GetCollider(buffer.block.actor);
		hitInfo->actor = buffer.block.actor;
		hitInfo->point = buffer.block.position;
		hitInfo->normal = buffer.block.normal;
		hitInfo->distance = buffer.block.distance;
		hitInfo->layer = collider->GetLayer();
	}
	return buffer.hasBlock;
}

EXPORT_DLL bool RaycastWithMask(PhysicsScene* scene, Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_REAL;
	RaycastHit* hit = scene->query->Raycast(origin.ToPxVec3(), direction.ToPxVec3(), maxDistance, layerMask);
	return hit != NULL;
}

EXPORT_DLL bool RaycastWithMaskAndInfo(PhysicsScene* scene, Vector3 origin, Vector3 direction, float maxDistance, int layerMask, RaycastHit* hitInfo)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_REAL;
	RaycastHit* hit = scene->query->Raycast(origin.ToPxVec3(), direction.ToPxVec3(), maxDistance, layerMask);
	if (hit != NULL)
	{
		hitInfo->actor = hit->actor;
		hitInfo->point = hit->point;
		hitInfo->normal = hit->normal;
		hitInfo->distance = hit->distance;
		hitInfo->layer = hit->layer;
	}
	return hit != NULL;
}

EXPORT_DLL int RaycastAll(PhysicsScene* scene, Vector3 origin, Vector3 direction, float maxDistance, RaycastHit* hitArray)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_REAL;
	return scene->query->RaycastAll(origin.ToPxVec3(), direction.ToPxVec3(), maxDistance, hitArray);
}

EXPORT_DLL int RaycastAllWithMask(PhysicsScene* scene, Vector3 origin, Vector3 direction, float maxDistance, int layerMask, RaycastHit* hitArray)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_REAL;
	return scene->query->RaycastAll(origin.ToPxVec3(), direction.ToPxVec3(), maxDistance, layerMask, hitArray);
}
#pragma endregion

#pragma region SphereCast

EXPORT_DLL bool SphereCast(PhysicsScene* scene, Vector3 origin, Vector3 direction, float radius, float maxDistance)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	PxSweepBuffer buffer = scene->query->SphereCast(origin.ToPxVec3(), direction.ToPxVec3(), radius, maxDistance);
	return buffer.hasBlock;
}

EXPORT_DLL bool SphereCastWithInfo(PhysicsScene* scene, Vector3 origin, Vector3 direction, float radius, float maxDistance, RaycastHit* hitInfo)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	PxSweepBuffer buffer = scene->query->SphereCast(origin.ToPxVec3(), direction.ToPxVec3(), radius, maxDistance);
	if (buffer.hasBlock)
	{
		Collider* collider = PhysicsManager::GetInstance().GetCollider(buffer.block.actor);
		hitInfo->actor = buffer.block.actor;
		hitInfo->point = buffer.block.position;
		hitInfo->normal = buffer.block.normal;
		hitInfo->distance = buffer.block.distance;
		hitInfo->layer = collider->GetLayer();
	}
	return buffer.hasBlock;
}

EXPORT_DLL bool SphereCastWithMask(PhysicsScene* scene, Vector3 origin, Vector3 direction, float radius, float maxDistance, int layerMask)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	RaycastHit* hit = scene->query->SphereCast(origin.ToPxVec3(), direction.ToPxVec3(), radius, maxDistance, layerMask);
	return hit != NULL;
}

EXPORT_DLL bool SweepCastWithMaskAndInfo(PhysicsScene* scene, Vector3 origin, Vector3 direction, float radius, float maxDistance, int layerMask, RaycastHit* hitInfo)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	RaycastHit* hit = scene->query->SphereCast(origin.ToPxVec3(), direction.ToPxVec3(), radius, maxDistance, layerMask);
	if (hit != NULL)
	{
		hitInfo->actor = hit->actor;
		hitInfo->point = hit->point;
		hitInfo->normal = hit->normal;
		hitInfo->distance = hit->distance;
		hitInfo->layer = hit->layer;
	}
	return hit != NULL;
}

EXPORT_DLL int SphereCastAll(PhysicsScene* scene, Vector3 origin, Vector3 direction, float radius, float maxDistance, RaycastHit* hitArray)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	return scene->query->SphereCastAll(origin.ToPxVec3(), direction.ToPxVec3(), radius, maxDistance, hitArray);
}

EXPORT_DLL int SphereCastAllWithMask(PhysicsScene* scene, Vector3 origin, Vector3 direction, float radius, float maxDistance, int layerMask, RaycastHit* hitArray)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	return scene->query->SphereCastAll(origin.ToPxVec3(), direction.ToPxVec3(), radius, maxDistance, layerMask, hitArray);
}
#pragma endregion

#pragma region BoxCast

EXPORT_DLL bool BoxCast(PhysicsScene* scene, Vector3 origin, Vector3 direction, Vector3& halfExtents, Vector4& orientation, float maxDistance)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	PxSweepBuffer buffer = scene->query->BoxCast(origin.ToPxVec3(), direction.ToPxVec3(), halfExtents.ToPxVec3(), orientation.ToPxQuat(), maxDistance);
	return buffer.hasBlock;
}

EXPORT_DLL bool BoxCastWithInfo(PhysicsScene* scene, Vector3 origin, Vector3 direction, Vector3 halfExtents, Vector4 orientation, float maxDistance, RaycastHit* hitInfo)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	PxSweepBuffer buffer = scene->query->BoxCast(origin.ToPxVec3(), direction.ToPxVec3(), halfExtents.ToPxVec3(), orientation.ToPxQuat(), maxDistance);
	if (buffer.hasBlock)
	{
		Collider* collider = PhysicsManager::GetInstance().GetCollider(buffer.block.actor);
		hitInfo->actor = buffer.block.actor;
		hitInfo->point = buffer.block.position;
		hitInfo->normal = buffer.block.normal;
		hitInfo->distance = buffer.block.distance;
		hitInfo->layer = collider->GetLayer();
	}
	return buffer.hasBlock;
}

EXPORT_DLL bool BoxCastWithMask(PhysicsScene* scene, Vector3 origin, Vector3 direction, Vector3 halfExtents, Vector4 orientation, float maxDistance, int layerMask)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	RaycastHit* hit = scene->query->BoxCast(origin.ToPxVec3(), direction.ToPxVec3(), halfExtents.ToPxVec3(), orientation.ToPxQuat(), maxDistance, layerMask);
	return hit != NULL;
}

EXPORT_DLL bool BoxCastWithMaskAndInfo(PhysicsScene* scene, Vector3 origin, Vector3 direction, Vector3 halfExtents, Vector4 orientation, float maxDistance, int layerMask, RaycastHit* hitInfo)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	RaycastHit* hit = scene->query->BoxCast(origin.ToPxVec3(), direction.ToPxVec3(), halfExtents.ToPxVec3(), orientation.ToPxQuat(), maxDistance, layerMask);
	if (hit != NULL)
	{
		hitInfo->actor = hit->actor;
		hitInfo->point = hit->point;
		hitInfo->normal = hit->normal;
		hitInfo->distance = hit->distance;
		hitInfo->layer = hit->layer;
	}
	return hit != NULL;
}

EXPORT_DLL int BoxCastAll(PhysicsScene* scene, Vector3 origin, Vector3 direction, Vector3 halfExtents, Vector4 orientation, float maxDistance, RaycastHit* hitArray)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	return scene->query->BoxCastAll(origin.ToPxVec3(), direction.ToPxVec3(), halfExtents.ToPxVec3(), orientation.ToPxQuat(), maxDistance, hitArray);
}

EXPORT_DLL int BoxCastAllWithMask(PhysicsScene* scene, Vector3 origin, Vector3 direction, Vector3 halfExtents, Vector4 orientation, float maxDistance, int layerMask, RaycastHit* hitArray)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	return scene->query->BoxCastAll(origin.ToPxVec3(), direction.ToPxVec3(), halfExtents.ToPxVec3(), orientation.ToPxQuat(), maxDistance, layerMask, hitArray);
}

#pragma endregion

#pragma region CapsuleCast

EXPORT_DLL bool CapsuleCast(PhysicsScene* scene, int axis, Vector3 origin, Vector3 direction, float radius, float halfHeight, Vector4 orientation, float maxDistance)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	PxSweepBuffer buffer = scene->query->CapsuleCast(axis, origin.ToPxVec3(), direction.ToPxVec3(), radius, halfHeight, orientation.ToPxQuat(), maxDistance);
	return buffer.hasAnyHits();
}

EXPORT_DLL bool CapsuleCastWithInfo(PhysicsScene* scene, int axis, Vector3 origin, Vector3 direction, float radius, float halfHeight, Vector4 orientation, float maxDistance, RaycastHit* hitInfo)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	PxSweepBuffer buffer = scene->query->CapsuleCast(axis, origin.ToPxVec3(), direction.ToPxVec3(), radius, halfHeight, orientation.ToPxQuat(), maxDistance);
	if (buffer.hasBlock)
	{
		Collider* collider = PhysicsManager::GetInstance().GetCollider(buffer.block.actor);
		hitInfo->actor = buffer.block.actor;
		hitInfo->point = buffer.block.position;
		hitInfo->normal = buffer.block.normal;
		hitInfo->distance = buffer.block.distance;
		hitInfo->layer = collider->GetLayer();
	}
	return buffer.hasBlock;
}

EXPORT_DLL bool CapsuleCastWithMask(PhysicsScene* scene, int axis, Vector3 origin, Vector3 direction, float radius, float halfHeight, Vector4 orientation, float maxDistance, int layerMask)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	RaycastHit* hit = scene->query->CapsuleCast(axis, origin.ToPxVec3(), direction.ToPxVec3(), radius, halfHeight, orientation.ToPxQuat(), maxDistance, layerMask);
	return hit != NULL;
}

EXPORT_DLL bool CapsuleCastWithMaskAndInfo(PhysicsScene* scene, int axis, Vector3 origin, Vector3 direction, float radius, float halfHeight, Vector4 orientation, float maxDistance, int layerMask, RaycastHit* hitInfo)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	RaycastHit* hit = scene->query->CapsuleCast(axis, origin.ToPxVec3(), direction.ToPxVec3(), radius, halfHeight, orientation.ToPxQuat(), maxDistance, layerMask);
	if (hit != NULL)
	{
		hitInfo->actor = hit->actor;
		hitInfo->point = hit->point;
		hitInfo->normal = hit->normal;
		hitInfo->distance = hit->distance;
		hitInfo->layer = hit->layer;
	}
	return hit != NULL;
}

EXPORT_DLL int CapsuleCastAll(PhysicsScene* scene, int axis, Vector3 origin, Vector3 direction, float radius, float halfHeight, Vector4 orientation, float maxDistance, RaycastHit* hitArray)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	return scene->query->CapsuleCastAll(axis, origin.ToPxVec3(), direction.ToPxVec3(), radius, halfHeight, orientation.ToPxQuat(), maxDistance, hitArray);
}

EXPORT_DLL int CapsuleCastAllWithMask(PhysicsScene* scene, int axis, Vector3 origin, Vector3 direction, float radius, float halfHeight, Vector4 orientation, float maxDistance, int layerMask, RaycastHit* hitArray)
{
	if (maxDistance < 0)
		maxDistance = PX_MAX_SWEEP_DISTANCE;
	return scene->query->CapsuleCastAll(axis, origin.ToPxVec3(), direction.ToPxVec3(), radius, halfHeight, orientation.ToPxQuat(), maxDistance, layerMask, hitArray);
}

#pragma endregion
