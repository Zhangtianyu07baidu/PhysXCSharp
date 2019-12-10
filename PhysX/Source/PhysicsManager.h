#pragma once
#ifndef PHYSX_SCENE_MANAGER
#define PHYSX_SCENE_MANAGER

class PhysicsScene
{
public:
	PxScene* scene;
	PxControllerManager* controllerMgr;
	SceneQuerier* query;
	void Release();
};

class PhysicsManager
{
	PX_NOCOPY(PhysicsManager)

public:
	PhysicsManager(OnErrorCallback log);
	~PhysicsManager();

	static PhysicsManager* CreateInstance(OnErrorCallback log);
	static PhysicsManager& GetInstance();
	static void DestroyInstance();

	void Initialize();
	PxRigidActor* AddActor(PhysicsScene* scene, Collider* collider, bool isDynamic);
	PxRigidDynamic* AddController(CapsuleController* controller);
	void Release();
	PxPhysics* GetPhysics() const;
	PxCooking* GetCooking() const;
	void Update(PhysicsScene* scene, float deltaTime);
	Collider* GetCollider(PxRigidActor* actor);
	void RegistSceneUpdateCallback(OnSceneUpdateCallback cb);
	void RemoveCollider(PhysicsScene* scene, PxRigidActor& actor);
	PhysicsScene* CreateScene();

	void Print(PxErrorCode::Enum code, const char* messageFmt, ...);

	std::set<int>& GetIgnoreLayers();
private:
	PxFoundation* foundation;
	PxPhysics* physics;
	PxDefaultCpuDispatcher* globalDispatcher;
	PxCooking* cooking;
	PxMaterial* material;
	PxDefaultAllocator allocator;
	UserErrorCallback errorCallback;

	SceneQuerier* sceneQuerier;
	OnSceneUpdateCallback sceneUpdateCallback;
	std::set<int> ignoreLayers;

	static PhysicsManager* mInstance;
};


#endif
