#include "stdafx.h"



QueryFilterCallback::QueryFilterCallback(CapsuleController* controller, OnFilterCallback callBack)
{
	this->controller = controller;
	this->collideCallback = callBack;
}


QueryFilterCallback::~QueryFilterCallback()
{
	this->collideCallback = NULL;
	this->controller = NULL;
}

PxQueryHitType::Enum QueryFilterCallback::preFilter(const PxFilterData& filterData, const PxShape* shape, const PxRigidActor* actor, PxHitFlags& queryFlags)
{
	if(this->collideCallback == NULL)
		return PxQueryHitType::eBLOCK;

	//actor可能是controller本身，也可能是其他controller，但是在该方法里控制与controller的过滤逻辑是无效的，
	//需要在ControllerFilterCallback中处理controller之间的碰撞过滤逻辑
	ColliderBase* collider = static_cast<ColliderBase*>(actor->userData);
	if (collider->GetType() == (int)ColliderType::Controller)
	{
		return PxQueryHitType::eNONE;
	}

	if (this->collideCallback(this->controller->GetScene(), this->controller->GetActor(), actor))
	{
		return PxQueryHitType::eBLOCK;//命中阻止
	}
	return PxQueryHitType::eNONE;//丢弃命中
}

PxQueryHitType::Enum QueryFilterCallback::postFilter(const PxFilterData& filterData, const PxQueryHit& hit)
{
	//后过滤发生在窄相位测试之后，因此可以使用测试结果（例如PxRaycastHit.position）来确定是否应该丢弃命中
	return PxQueryHitType::eNONE;
}