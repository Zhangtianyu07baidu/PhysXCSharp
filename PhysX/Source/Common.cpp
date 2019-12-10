#include "stdafx.h"

Common::Common()
{
	
}

Common::~Common()
{
	
}

Vector3::Vector3(float x, float y, float z)
{
	this->x = x;
	this->y = y;
	this->z = z;
}

Vector3::Vector3(const PxVec3& vec3)
{
	this->x = vec3.x;
	this->y = vec3.y;
	this->z = vec3.z;
}

Vector3& Vector3::operator=(PxVec3& p)
{
	x = p.x;
	y = p.y;
	z = p.z;
	return *this;
}

PxVec3 Vector3::ToPxVec3()
{
	return PxVec3(this->x, this->y, this->z);
}

Vector4::Vector4(float x, float y, float z, float w)
{
	this->x = x;
	this->y = y;
	this->z = z;
	this->w = w;
}


Vector4::Vector4(const PxQuat& q)
{
	this->x = q.x;
	this->y = q.y;
	this->z = q.z;
	this->w = q.w;
}

PxQuat Vector4::ToPxQuat()
{
	return PxQuat(this->x, this->y, this->z, this->w);
}

Vector4 Vector4::Clone()
{
	return Vector4(this->x, this->y, this->z, this->w);
}
