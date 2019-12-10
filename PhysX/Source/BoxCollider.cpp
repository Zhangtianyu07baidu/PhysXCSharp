#include "stdafx.h"


BoxCollider::BoxCollider(PhysicsScene* scene, BoxColliderData& data) : Collider(scene)
{
	this->colliderData = data;

}

BoxCollider::~BoxCollider()
{
}


PxRigidActor* BoxCollider::CreateActor(PxPhysics* physics, PxMaterial* material, bool isDynamic)
{
	BoxColliderData& box = this->colliderData;
	const PxTransform localTrans(box.position.ToPxVec3(), box.rotation.ToPxQuat());
	if (isDynamic)
		this->actor = physics->createRigidDynamic(localTrans);
	else
	{
		this->actor = physics->createRigidStatic(localTrans);
		//this->actor->setActorFlag(PxActorFlag::eDISABLE_SIMULATION, true);
	}
	
	this->actor->createShape(PxBoxGeometry(box.x_extents * 0.5f, box.y_extents * 0.5f, box.z_extents * 0.5), *material);
	return this->actor;
}

ColliderType::Enum BoxCollider::GetType()
{
	return ColliderType::BOX;
}

int BoxCollider::GetLayer()
{
	return this->colliderData.layer;
}