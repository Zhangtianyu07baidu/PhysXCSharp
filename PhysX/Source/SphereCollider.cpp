#include "stdafx.h"


SphereCollider::SphereCollider(PhysicsScene* scene, SphereColliderData& data) : Collider(scene)
{
	this->colliderData = data;
}

SphereCollider::~SphereCollider()
{
	//delete this->colliderData.id;
}

PxRigidActor* SphereCollider::CreateActor(PxPhysics* physics, PxMaterial* material, bool isDynamic)
{
	SphereColliderData& sphere = this->colliderData;

	PxTransform localTrans(sphere.position.ToPxVec3(), sphere.rotation.ToPxQuat());
	if (isDynamic)
		this->actor = physics->createRigidDynamic(localTrans);
	else
	{
		this->actor = physics->createRigidStatic(localTrans);
		//this->actor->setActorFlag(PxActorFlag::eDISABLE_SIMULATION, true);
	}

	this->actor->createShape(PxSphereGeometry(sphere.radius), *material);
	return this->actor;
}

ColliderType::Enum SphereCollider::GetType()
{
	return ColliderType::SPHERE;
}

int SphereCollider::GetLayer()
{
	return this->colliderData.layer;
}