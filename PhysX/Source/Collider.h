#pragma once

class PhysicsScene;

class Collider : public ColliderBase
{
public:
	
	virtual PxRigidActor* CreateActor(PxPhysics* physics, PxMaterial* material, bool isDynamic) = 0;
	virtual void Release();
	//PxShape* GetShape();
protected:
	Collider(PhysicsScene* scene);
	virtual ~Collider();

	PxRigidActor* actor;
	//PxShape* shape;
};

