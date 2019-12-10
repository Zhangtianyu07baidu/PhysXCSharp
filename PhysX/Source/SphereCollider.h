#pragma once

class SphereCollider : public Collider
{
public:
	SphereCollider(PhysicsScene* scene, SphereColliderData& data);
	~SphereCollider();

	virtual PxRigidActor* CreateActor(PxPhysics* physics, PxMaterial* material, bool isDynamic);

	virtual ColliderType::Enum GetType();
	//void* GetId();
	virtual int GetLayer();

private:
	SphereColliderData colliderData;
};