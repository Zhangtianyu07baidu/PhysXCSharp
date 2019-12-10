#include "stdafx.h"

RaycastCallback::RaycastCallback()
{
}

RaycastCallback::~RaycastCallback()
{
}

PxAgain RaycastCallback::processTouches(const PxRaycastHit* buffer, PxU32 nbHits)
{
	for (PxU32 index = 0; index < nbHits; ++index)
	{
		PxRaycastHit hit = buffer[index];
		this->AddHit(hit);
	}
	return true;
}

void RaycastCallback::finalizeQuery()
{
	if (this->hasBlock)
	{
		this->AddHit(this->block);
	}
}

void RaycastCallback::AddHit(PxRaycastHit& hit)
{
	ColliderBase* collider = static_cast<ColliderBase*>(hit.actor->userData);
	RaycastHit hitInfo;
	hitInfo.actor = hit.actor;
	hitInfo.point = hit.position;
	hitInfo.normal = hit.normal;
	hitInfo.distance = hit.distance;
	hitInfo.layer = collider->GetLayer();
	this->hitArray.push_back(hitInfo);
}

int RaycastCallback::GetHitCount() const
{
	return this->hitArray.size();
}

void RaycastCallback::Clear()
{
	this->hitArray.clear();
}

std::vector<RaycastHit>& RaycastCallback::GetHitArray()
{
	return this->hitArray;
}