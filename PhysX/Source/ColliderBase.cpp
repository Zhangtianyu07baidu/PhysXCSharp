#include "stdafx.h"

ColliderBase::ColliderBase(PhysicsScene* scene)
{
	this->scene = scene;
}


ColliderBase::~ColliderBase()
{
	this->scene = NULL;
}

PhysicsScene* ColliderBase::GetScene() const
{
	return this->scene;
}