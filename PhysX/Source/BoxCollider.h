#pragma once
class BoxCollider : public Collider
{
public:
	BoxCollider(PhysicsScene* scene, BoxColliderData& data);
	~BoxCollider();

	virtual PxRigidActor* CreateActor(PxPhysics* physics, PxMaterial* material, bool isDynamic);

	virtual ColliderType::Enum GetType();
	virtual int GetLayer();
private:
	BoxColliderData colliderData;
	
};

