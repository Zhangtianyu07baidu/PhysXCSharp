#include "stdafx.h"

Collider::Collider(PhysicsScene* scene) : ColliderBase(scene)
{
}

Collider::~Collider()
{
}

/*PxShape* Collider::GetShape()
{
	return this->shape;
}*/

void Collider::Release()
{
	/*PxU32 shapeSize = this->actor->getNbShapes();
	PxShape** buffer = new PxShape*[shapeSize];
	PxU32 num = this->actor->getShapes(buffer, shapeSize);
	for (PxU32 index = 0; index < num; ++index)
	{
		PxShape* shape = buffer[index];
		this->actor->detachShape(*shape);
	}
	delete[] buffer;*/
	this->actor->userData = NULL;
	this->actor->release();
	this->actor = NULL;
}
