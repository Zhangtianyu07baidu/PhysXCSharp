#pragma once


class MeshCollider : public Collider
{
public:
	MeshCollider(MeshColliderData& data, PhysicsScene* scene, float* vertices, int* triangles);
	~MeshCollider();

	virtual PxRigidActor* CreateActor(PxPhysics* physics, PxMaterial* material, bool isDynamic);

	virtual ColliderType::Enum GetType();
	//void* GetId();
	virtual int GetLayer();

private:
	MeshColliderData colliderData;

	std::vector<PxVec3> pxVec3Array;
	std::vector<PxU32> intArray;
};