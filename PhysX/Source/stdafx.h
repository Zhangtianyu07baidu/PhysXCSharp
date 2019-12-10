// stdafx.h : 标准系统包含文件的包含文件，
// 或是经常使用但不常更改的
// 特定于项目的包含文件
//

#pragma once

#ifdef _WINDOWS

//#include "targetver.h"

//#define WIN32_LEAN_AND_MEAN             // 从 Windows 头中排除极少使用的资料

// Windows 头文件:
//#include <windows.h>

#define EXPORT_DLL extern "C" __declspec(dllexport)
#define STD_CALL __stdcall

#else
	
#define EXPORT_DLL extern "C"
//#define STD_CALL __attribute__((stdcall))
#define STD_CALL

#endif

#include "PxPhysicsAPI.h"
using namespace physx;
using namespace std;

#include "vector"
#include "stdint.h"
//#include "list"
#include "set"

#include "Common.h"
#include "PhysXDLL.h"

#include "ControllerFilterCallback.h"
#include "QueryFilterCallback.h"
#include "ColliderBase.h"
#include "CapsuleController.h"
#include "Collider.h"
#include "BoxCollider.h"
#include "SphereCollider.h"
#include "CapsuleCollider.h"
#include "MeshCollider.h"

#include "UserErrorCallback.h"
#include "RaycastCallback.h"
#include "SweepCastCallback.h"
#include "LayerFilterCallback.h"
#include "SceneQuerier.h"
#include "PhysicsManager.h"
