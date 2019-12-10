#include "stdafx.h"

CapsuleCollider::CapsuleCollider(PhysicsScene* scene, CapsuleColliderData& data) : Collider(scene)
{
	this->colliderData = data;
}

CapsuleCollider::~CapsuleCollider()
{
}

PxRigidActor* CapsuleCollider::CreateActor(PxPhysics* physics, PxMaterial* material, bool isDynamic)
{
	CapsuleColliderData& capsule = this->colliderData;

	const PxTransform localTrans(capsule.position.ToPxVec3(), capsule.rotation.ToPxQuat());
	if (isDynamic)
	{
		this->actor = physics->createRigidDynamic(localTrans);
		//PxRigidBodyExt::updateMassAndInertia £¨* aCapsuleActor £¬ capsuleDensity £©;
	}
	else
	{
		this->actor = physics->createRigidStatic(localTrans);
		//this->actor->setActorFlag(PxActorFlag::eDISABLE_SIMULATION, true);
	}

	PxShape* shape = this->actor->createShape(PxCapsuleGeometry(capsule.radius, capsule.halfHeight), *material);
	if (capsule.direction == 1)
	{
		//Y-Axis
		const PxTransform relativePose(PxQuat(PxHalfPi, PxVec3(0, 0, 1)));
		shape->setLocalPose(relativePose);
	}
	else if (capsule.direction == 2)
	{
		//Z-Axis
		const PxTransform relativePose(PxQuat(PxHalfPi, PxVec3(0, 1, 0)));
		shape->setLocalPose(relativePose);
	}

	return this->actor;
}

ColliderType::Enum CapsuleCollider::GetType()
{
	return ColliderType::CAPSULE;
}

int CapsuleCollider::GetLayer()
{
	return this->colliderData.layer;
}