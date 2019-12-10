#pragma once
//class PhysicsManager;

class LayerFilterCallback : public PxQueryFilterCallback
{
public:
	LayerFilterCallback();
	~LayerFilterCallback();

	virtual PxQueryHitType::Enum preFilter(const PxFilterData& filterData, const PxShape* shape, const PxRigidActor* actor, PxHitFlags& queryFlags);

	virtual PxQueryHitType::Enum postFilter(const PxFilterData& filterData, const PxQueryHit& hit);

	void SetLayerMask(int layerMask);
	void SetIsOnlyGetFirst(bool val);
private:
	int layerMask;
	bool isOnlyGetFirst;
};

