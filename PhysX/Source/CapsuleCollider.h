#pragma once

class CapsuleCollider : public Collider
{
public:
	CapsuleCollider(PhysicsScene* scene, CapsuleColliderData& data);
	~CapsuleCollider();

	virtual PxRigidActor* CreateActor(PxPhysics* physics, PxMaterial* material, bool isDynamic);

	virtual ColliderType::Enum GetType();
	//void* GetId();
	virtual int GetLayer();
private:
	CapsuleColliderData colliderData;
};