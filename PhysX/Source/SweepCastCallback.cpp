#include "stdafx.h"

SweepCastCallback::SweepCastCallback()
{
}


SweepCastCallback::~SweepCastCallback()
{
}

PxAgain SweepCastCallback::processTouches(const PxSweepHit* buffer, PxU32 nbHits)
{
	for (PxU32 index = 0; index < nbHits; ++index)
	{
		PxSweepHit hit = buffer[index];
		this->AddHit(hit);
	}
	return true;
}

void SweepCastCallback::finalizeQuery()
{
	if (this->hasBlock)
	{
		this->AddHit(this->block);
	}
}

void SweepCastCallback::AddHit(PxSweepHit& hit)
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

int SweepCastCallback::GetHitCount() const
{
	return this->hitArray.size();
}

void SweepCastCallback::Clear()
{
	this->hitArray.clear();
}

std::vector<RaycastHit>& SweepCastCallback::GetHitArray()
{
	return this->hitArray;
}