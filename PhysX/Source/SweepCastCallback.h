#pragma once

class SweepCastCallback : public PxSweepBufferN<10>
{
public:
	SweepCastCallback();
	~SweepCastCallback();

	virtual PxAgain processTouches(const PxSweepHit* buffer, PxU32 nbHits);

	virtual void finalizeQuery();
	int GetHitCount() const;
	std::vector<RaycastHit>& GetHitArray();
	void Clear();
private:
	void AddHit(PxSweepHit& hit);

	std::vector<RaycastHit> hitArray;
};

