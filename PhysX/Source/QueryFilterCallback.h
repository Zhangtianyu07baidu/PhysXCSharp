#pragma once

class QueryFilterCallback : public PxQueryFilterCallback
{
public:
	QueryFilterCallback(class CapsuleController* controller, OnFilterCallback callBack);
	~QueryFilterCallback();

	virtual PxQueryHitType::Enum preFilter(const PxFilterData& filterData, const PxShape* shape, const PxRigidActor* actor, PxHitFlags& queryFlags);

	virtual PxQueryHitType::Enum postFilter(const PxFilterData& filterData, const PxQueryHit& hit);

private:
	CapsuleController* controller;
	OnFilterCallback collideCallback;
};