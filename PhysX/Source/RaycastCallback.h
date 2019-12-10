#pragma once
struct RaycastCallback : PxRaycastBufferN<10>
{
public:
	RaycastCallback();
	~RaycastCallback();

	virtual PxAgain processTouches(const PxRaycastHit* buffer, PxU32 nbHits);

	virtual void finalizeQuery();
	int GetHitCount() const;
	std::vector<RaycastHit>& GetHitArray();
	void Clear();
	
private:
	void AddHit(PxRaycastHit& hit);

	std::vector<RaycastHit> hitArray;
};
