#include "stdafx.h"

SceneQuerier::SceneQuerier( PhysicsScene* scene)
{
	this->scene = scene;
	this->raycastCallback = new RaycastCallback();
	this->sweepCallback = new SweepCastCallback();
	this->layerFilterCallback = new LayerFilterCallback();
}

SceneQuerier::~SceneQuerier()
{
	delete this->raycastCallback;
	delete this->sweepCallback;
	delete this->layerFilterCallback;
}

#pragma region Raycast

PxRaycastBuffer SceneQuerier::Raycast(PxVec3 origin, PxVec3 direction, PxReal distance)
{
	PxRaycastBuffer buffer;
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::eANY_HIT;
	this->scene->scene->raycast(origin, direction.getNormalized(), distance, buffer, hitFlags, fd);
	return buffer;
}

RaycastHit* SceneQuerier::Raycast(PxVec3 origin, PxVec3 direction, PxReal distance, int layerMask)
{
	this->layerFilterCallback->SetLayerMask(layerMask);
	this->layerFilterCallback->SetIsOnlyGetFirst(true);
	this->Raycast(origin, direction, distance, *this->raycastCallback, this->layerFilterCallback);

	if (this->raycastCallback->GetHitCount() > 0)
	{
		RaycastHit* hit = &this->raycastCallback->GetHitArray()[0];
		this->raycastCallback->Clear();
		return hit;
	}
	else
	{
		return NULL;
	}
}

int SceneQuerier::RaycastAll(PxVec3 origin, PxVec3 direction, PxReal distance, RaycastHit* hitArray)
{
	this->RaycastAll(origin, direction, distance, *this->raycastCallback);
	int hitCount = this->raycastCallback->GetHitCount();
	if (hitCount > MAX_HIT)
	{
		hitCount = MAX_HIT;
		PhysicsManager::GetInstance().Print(PxErrorCode::eDEBUG_WARNING, "raycast hit num is %d, it has been more than %d", hitCount, MAX_HIT);
	}
	std::vector<RaycastHit>& hits = this->raycastCallback->GetHitArray();
	for (int i = 0; i < hitCount; i++)
	{
		hitArray[i] = hits[i];
	}
	this->raycastCallback->Clear();
	return hitCount;
}

int SceneQuerier::RaycastAll(PxVec3 origin, PxVec3 direction, PxReal distance, int layerMask, RaycastHit* hitArray)
{
	this->layerFilterCallback->SetLayerMask(layerMask);
	this->layerFilterCallback->SetIsOnlyGetFirst(false);
	this->Raycast(origin, direction, distance, *this->raycastCallback, this->layerFilterCallback);
	int hitCount = this->raycastCallback->GetHitCount();
	if (hitCount > MAX_HIT)
	{
		hitCount = MAX_HIT;
		PhysicsManager::GetInstance().Print(PxErrorCode::eDEBUG_WARNING, "raycast hit num is %d, it has been more than %d", hitCount, MAX_HIT);
	}
	std::vector<RaycastHit>& hits = this->raycastCallback->GetHitArray();
	for (int i = 0; i < hitCount; i++)
	{
		hitArray[i] = hits[i];
	}
	this->raycastCallback->Clear();
	return hitCount;
}

void SceneQuerier::RaycastAll(PxVec3 origin, PxVec3 direction, PxReal distance, PxRaycastCallback& hitCall)
{
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::eNO_BLOCK;
	this->scene->scene->raycast(origin, direction.getNormalized(), distance, hitCall, hitFlags, fd);
}

void SceneQuerier::Raycast(PxVec3 origin, PxVec3 direction, PxReal distance, PxRaycastCallback& hitCall, PxQueryFilterCallback* filterCall)
{
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::ePREFILTER;
	this->scene->scene->raycast(origin, direction.getNormalized(), distance, hitCall, hitFlags, fd, filterCall);
}

#pragma endregion

#pragma region SphereCast
PxSweepBuffer SceneQuerier::SphereCast(PxVec3 origin, PxVec3 direction, PxReal radius, PxReal distance)
{
	PxSphereGeometry sphereGeometry(radius);
	PxTransform initialPose(origin);  // [in] initial shape pose (at distance=0)
	PxSweepBuffer buffer;  // [out] Sweep results
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::eANY_HIT;
	this->scene->scene->sweep(sphereGeometry, initialPose, direction.getNormalized(), distance, buffer, hitFlags, fd);
	return buffer;
}

RaycastHit* SceneQuerier::SphereCast(PxVec3 origin, PxVec3 direction, PxReal radius, PxReal distance, int layerMask)
{
	this->layerFilterCallback->SetLayerMask(layerMask);
	this->layerFilterCallback->SetIsOnlyGetFirst(true);
	this->SphereCast(origin, direction, radius, distance, *this->sweepCallback, this->layerFilterCallback);
	if (this->sweepCallback->GetHitCount() > 0)
	{
		RaycastHit* hit = &this->sweepCallback->GetHitArray()[0];
		this->sweepCallback->Clear();
		return hit;
	}
	else
	{
		return NULL;
	}
}

int SceneQuerier::SphereCastAll(PxVec3 origin, PxVec3 direction, PxReal radius, PxReal distance, RaycastHit* hitArray)
{
	this->SphereCastAll(origin, direction, radius, distance, *this->sweepCallback);
	int hitCount = this->sweepCallback->GetHitCount();
	if (hitCount > MAX_HIT)
	{
		hitCount = MAX_HIT;
		PhysicsManager::GetInstance().Print(PxErrorCode::eDEBUG_WARNING, "spherecast hit num is %d, it has been more than %d", hitCount, MAX_HIT);
	}
	std::vector<RaycastHit>& hits = this->sweepCallback->GetHitArray();
	for (int i = 0; i < hitCount; i++)
	{
		hitArray[i] = hits[i];
	}
	this->sweepCallback->Clear();
	return hitCount;
}

int SceneQuerier::SphereCastAll(PxVec3 origin, PxVec3 direction, PxReal radius, PxReal distance, int layerMask, RaycastHit* hitArray)
{
	this->layerFilterCallback->SetLayerMask(layerMask);
	this->layerFilterCallback->SetIsOnlyGetFirst(false);
	this->SphereCast(origin, direction, radius, distance, *this->sweepCallback, this->layerFilterCallback);
	int hitCount = this->sweepCallback->GetHitCount();
	if (hitCount > MAX_HIT)
	{
		hitCount = MAX_HIT;
		PhysicsManager::GetInstance().Print(PxErrorCode::eDEBUG_WARNING, "spherecast hit num is %d, it has been more than %d", hitCount, MAX_HIT);
	}
	std::vector<RaycastHit>& hits = this->sweepCallback->GetHitArray();
	for (int i = 0; i < hitCount; i++)
	{
		hitArray[i] = hits[i];
	}
	this->sweepCallback->Clear();
	return hitCount;
}

void SceneQuerier::SphereCastAll(PxVec3 origin, PxVec3 direction, PxReal radius, PxReal distance, PxSweepCallback& hitCall)
{
	PxSphereGeometry sphereGeometry(radius);
	PxTransform initialPose(origin);  // [in] initial shape pose (at distance=0)
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::eNO_BLOCK;
	this->scene->scene->sweep(sphereGeometry, initialPose, direction.getNormalized(), distance, hitCall, hitFlags, fd);
}

void SceneQuerier::SphereCast(PxVec3 origin, PxVec3 direction, PxReal radius, PxReal distance, PxSweepCallback& hitCall, PxQueryFilterCallback* filterCall)
{
	PxSphereGeometry sphereGeometry(radius);
	PxTransform initialPose(origin);  // [in] initial shape pose (at distance=0)
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::ePREFILTER;
	this->scene->scene->sweep(sphereGeometry, initialPose, direction.getNormalized(), distance, hitCall, hitFlags, fd, filterCall);
}
#pragma endregion

#pragma region BoxCast

PxSweepBuffer SceneQuerier::BoxCast(PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxQuat orientation, PxReal distance)
{
	PxBoxGeometry boxGeometry(halfExtents);
	PxTransform initialPose(origin, orientation);  // [in] initial shape pose (at distance=0)
	PxSweepBuffer buffer;  // [out] Sweep results
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::eANY_HIT;
	this->scene->scene->sweep(boxGeometry, initialPose, direction.getNormalized(), distance, buffer, hitFlags, fd);
	return buffer;
}

RaycastHit* SceneQuerier::BoxCast(PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxQuat orientation, PxReal distance, int layerMask)
{
	this->layerFilterCallback->SetLayerMask(layerMask);
	this->layerFilterCallback->SetIsOnlyGetFirst(true);
	this->BoxCast(origin, direction, halfExtents, orientation, distance, *this->sweepCallback, this->layerFilterCallback);

	if (this->sweepCallback->GetHitCount() > 0)
	{
		RaycastHit* hit = &this->sweepCallback->GetHitArray()[0];
		this->sweepCallback->Clear();
		return hit;
	}
	else
	{
		return NULL;
	}
}

int SceneQuerier::BoxCastAll(PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxQuat orientation, PxReal distance, RaycastHit* hitArray)
{
	this->BoxCastAll(origin, direction, halfExtents, orientation, distance, *this->sweepCallback);
	int hitCount = this->sweepCallback->GetHitCount();
	if (hitCount > MAX_HIT)
	{
		hitCount = MAX_HIT;
		PhysicsManager::GetInstance().Print(PxErrorCode::eDEBUG_WARNING, "boxcast hit num is %d, it has been more than %d", hitCount, MAX_HIT);
	}
	std::vector<RaycastHit>& hits = this->sweepCallback->GetHitArray();
	for (int i = 0; i < hitCount; i++)
	{
		hitArray[i] = hits[i];
	}
	this->sweepCallback->Clear();
	return hitCount;
}

int SceneQuerier::BoxCastAll(PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxQuat orientation, PxReal distance, int layerMask, RaycastHit* hitArray)
{
	this->layerFilterCallback->SetLayerMask(layerMask);
	this->layerFilterCallback->SetIsOnlyGetFirst(false);
	this->BoxCast(origin, direction, halfExtents, orientation, distance, *this->sweepCallback, this->layerFilterCallback);
	int hitCount = this->sweepCallback->GetHitCount();
	if (hitCount > MAX_HIT)
	{
		hitCount = MAX_HIT;
		PhysicsManager::GetInstance().Print(PxErrorCode::eDEBUG_WARNING, "boxcast hit num is %d, it has been more than %d", hitCount, MAX_HIT);
	}
	std::vector<RaycastHit>& hits = this->sweepCallback->GetHitArray();
	for (int i = 0; i < hitCount; i++)
	{
		hitArray[i] = hits[i];
	}
	this->sweepCallback->Clear();
	return hitCount;
}

void SceneQuerier::BoxCastAll(PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxQuat orientation, PxReal distance, PxSweepCallback& hitCall)
{
	PxBoxGeometry boxGeometry(halfExtents);
	PxTransform initialPose(origin, orientation);  // [in] initial shape pose (at distance=0)
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::eNO_BLOCK;
	this->scene->scene->sweep(boxGeometry, initialPose, direction.getNormalized(), distance, hitCall, hitFlags, fd);
}

void SceneQuerier::BoxCast(PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxQuat orientation, PxReal distance, PxSweepCallback& hitCall, PxQueryFilterCallback* filterCall)
{
	PxBoxGeometry boxGeometry(halfExtents);
	PxTransform initialPose(origin, orientation);  // [in] initial shape pose (at distance=0)
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::ePREFILTER;
	this->scene->scene->sweep(boxGeometry, initialPose, direction.getNormalized(), distance, hitCall, hitFlags, fd, filterCall);
}
#pragma endregion

#pragma region CapsuleCast

PxSweepBuffer SceneQuerier::CapsuleCast(int axis, PxVec3 origin, PxVec3 direction, PxReal radius, PxReal halfHeight, PxQuat orientation, PxReal distance)
{
	orientation = this->GetQuatByAxis(axis, orientation);
	PxCapsuleGeometry capsuleGeometry(radius, halfHeight);
	PxTransform initialPose(origin, orientation);  // [in] initial shape pose (at distance=0)
	PxSweepBuffer buffer;  // [out] Sweep results
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::eANY_HIT;
	this->scene->scene->sweep(capsuleGeometry, initialPose, direction.getNormalized(), distance, buffer, hitFlags, fd);
	return buffer;
}

RaycastHit* SceneQuerier::CapsuleCast(int axis, PxVec3 origin, PxVec3 direction, PxReal radius, PxReal halfHeight, PxQuat orientation, PxReal distance, int layerMask)
{
	orientation = this->GetQuatByAxis(axis, orientation);
	this->layerFilterCallback->SetLayerMask(layerMask);
	this->layerFilterCallback->SetIsOnlyGetFirst(true);
	this->CapsuleCast(origin, direction, radius, halfHeight, orientation, distance, *this->sweepCallback, this->layerFilterCallback);
	if (this->sweepCallback->GetHitCount() > 0)
	{
		RaycastHit* hit = &this->sweepCallback->GetHitArray()[0];
		this->sweepCallback->Clear();
		return hit;
	}
	else
	{
		return NULL;
	}
}

int SceneQuerier::CapsuleCastAll(int axis, PxVec3 origin, PxVec3 direction, PxReal radius, PxReal halfHeight, PxQuat orientation, PxReal distance, RaycastHit* hitArray)
{
	orientation = this->GetQuatByAxis(axis, orientation);
	this->CapsuleCastAll(origin, direction, radius, halfHeight, orientation, distance, *this->sweepCallback);
	int hitCount = this->sweepCallback->GetHitCount();
	if (hitCount > MAX_HIT)
	{
		hitCount = MAX_HIT;
		PhysicsManager::GetInstance().Print(PxErrorCode::eDEBUG_WARNING, "capsulecast hit num is %d, it has been more than %d", hitCount, MAX_HIT);
	}
	std::vector<RaycastHit>& hits = this->sweepCallback->GetHitArray();
	for (int i = 0; i < hitCount; i++)
	{
		RaycastHit& resultHit = hitArray[i];
		resultHit.actor = hits[i].actor;
		resultHit.point = hits[i].point;
		resultHit.normal = hits[i].normal;
		resultHit.distance = hits[i].distance;
		resultHit.layer = hits[i].layer;
	}
	this->sweepCallback->Clear();
	return hitCount;
}

int SceneQuerier::CapsuleCastAll(int axis, PxVec3 origin, PxVec3 direction, PxReal radius, PxReal halfHeight, PxQuat orientation, PxReal distance, int layerMask, RaycastHit* hitArray)
{
	orientation = this->GetQuatByAxis(axis, orientation);
	this->layerFilterCallback->SetLayerMask(layerMask);
	this->layerFilterCallback->SetIsOnlyGetFirst(false);
	this->CapsuleCast(origin, direction, radius, halfHeight, orientation, distance, *this->sweepCallback, this->layerFilterCallback);
	int hitCount = this->sweepCallback->GetHitCount();
	if (hitCount > MAX_HIT)
	{
		hitCount = MAX_HIT;
		PhysicsManager::GetInstance().Print(PxErrorCode::eDEBUG_WARNING, "capsulecast hit num is %d, it has been more than %d", hitCount, MAX_HIT);
	}
	std::vector<RaycastHit>& hits = this->sweepCallback->GetHitArray();
	for (int i = 0; i < hitCount; i++)
	{
		hitArray[i] = hits[i];
	}
	this->sweepCallback->Clear();
	return hitCount;
}

void SceneQuerier::CapsuleCastAll(PxVec3 origin, PxVec3 direction, PxReal radius, PxReal halfHeight, PxQuat orientation, PxReal distance, PxSweepCallback& hitCall)
{
	PxCapsuleGeometry capsuleGeometry(radius, halfHeight);
	PxTransform initialPose(origin, orientation);  // [in] initial shape pose (at distance=0)
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::eNO_BLOCK;
	this->scene->scene->sweep(capsuleGeometry, initialPose, direction.getNormalized(), distance, hitCall, hitFlags, fd);
}

void SceneQuerier::CapsuleCast(PxVec3 origin, PxVec3 direction, PxReal radius, PxReal halfHeight, PxQuat orientation, PxReal distance, PxSweepCallback& hitCall, PxQueryFilterCallback* filterCall)
{
	PxCapsuleGeometry capsuleGeometry(radius, halfHeight);
	PxTransform initialPose(origin, orientation);  // [in] initial shape pose (at distance=0)
	PxHitFlags hitFlags = PxHitFlags(PxHitFlag::eDEFAULT);
	PxQueryFilterData fd;
	fd.flags |= PxQueryFlag::ePREFILTER;
	this->scene->scene->sweep(capsuleGeometry, initialPose, direction.getNormalized(), distance, hitCall, hitFlags, fd, filterCall);
}

PxQuat& SceneQuerier::GetQuatByAxis(int axis, PxQuat& rotation)
{
	if (axis == 1)
	{
		//Y-Axis
		rotation *= PxQuat(PxHalfPi, PxVec3(0, 0, 1));
	}
	else if (axis == 2)
	{
		//Z-Axis
		rotation *= PxQuat(PxHalfPi, PxVec3(0, 1, 0));
	}
	return rotation;
}
#pragma endregion

RaycastCallback* SceneQuerier::GetRaycastCallback()
{
	return this->raycastCallback;
}
