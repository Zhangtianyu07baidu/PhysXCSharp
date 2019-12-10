#include "stdafx.h"



LayerFilterCallback::LayerFilterCallback()
{
}

LayerFilterCallback::~LayerFilterCallback()
{
}

PxQueryHitType::Enum LayerFilterCallback::preFilter(const PxFilterData& filterData, const PxShape* shape, const PxRigidActor* actor, PxHitFlags& queryFlags)
{
	ColliderBase* collider = static_cast<ColliderBase*>(actor->userData);
	int mask = 1 << collider->GetLayer();
	int result = mask & this->layerMask;
	if (result != 0)
	{
		if (this->isOnlyGetFirst)
			return PxQueryHitType::eBLOCK;
		else
			return PxQueryHitType::eTOUCH;
	}
	else
	{
		return PxQueryHitType::eNONE;
	}
}

PxQueryHitType::Enum LayerFilterCallback::postFilter(const PxFilterData& filterData, const PxQueryHit& hit)
{
	return PxQueryHitType::eNONE;
}

void LayerFilterCallback::SetLayerMask(int layerMask)
{
	this->layerMask = layerMask;
}

void LayerFilterCallback::SetIsOnlyGetFirst(bool val)
{
	this->isOnlyGetFirst = val;
}