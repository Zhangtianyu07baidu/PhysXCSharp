using System;
using System.Runtime.InteropServices;

namespace Engine.PhysX
{
	public class PhysXDll
	{
#if UNITY_IPHONE && !UNITY_EDITOR
		const string PHYSX = "__Internal";
		const string PREFIX = "X_";
#else
		const string PHYSX = "physx";
		const string PREFIX = "";
#endif

		[UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
		public delegate void OnSceneUpdateCallback(IntPtr scenePtr, IntPtr id, PxVec3 position, PxVec4 rotation);

		[UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
		public delegate void OnErrorCallback(int type, [MarshalAs(UnmanagedType.LPStr)]string message);

		[UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
		public delegate bool OnFilterCallback(IntPtr scenePtr, IntPtr p1, IntPtr p2);

		[UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
		public delegate void OnHitCallback(IntPtr scenePtr, IntPtr controllerPtr, IntPtr colliderPtr);

		#region Common

		[DllImport(PHYSX, EntryPoint = PREFIX + "Initialize", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Initialize();

		[DllImport(PHYSX, EntryPoint = PREFIX + "CreateScene", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr CreateScene();

		[DllImport(PHYSX, EntryPoint = PREFIX + "IgnoreControllerLayerCollision", CallingConvention = CallingConvention.Cdecl)]
		public static extern void IgnoreControllerLayerCollision(int layer1, int layer2, bool ignore);

		[DllImport(PHYSX, EntryPoint = PREFIX + "AddCharacterController", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr AddCharacterController(IntPtr scene, CapsuleControllerData controller, 
			OnFilterCallback withWorld, OnFilterCallback withController,
			OnHitCallback onHitShape, OnHitCallback onHitController);

		[DllImport(PHYSX, EntryPoint = PREFIX + "GetControllerFootPosition", CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetControllerFootPosition(IntPtr p, ref PxVec3 pos);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SetControllerFootPosition", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetControllerFootPosition(IntPtr p, PxVec3 pos);

		[DllImport(PHYSX, EntryPoint = PREFIX + "GetControllerPosition", CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetControllerPosition(IntPtr p, ref PxVec3 pos);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SetControllerPosition", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetControllerPosition(IntPtr p, PxVec3 pos);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SetControllerRadius", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetControllerRadius(IntPtr p, float radius);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SetControllerHeight", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetControllerHeight(IntPtr p, float height);

		[DllImport(PHYSX, EntryPoint = PREFIX + "MoveController", CallingConvention = CallingConvention.Cdecl)]
		public static extern int MoveController(IntPtr p, PxVec3 motion, ref PxVec3 footPos);

		[DllImport(PHYSX, EntryPoint = PREFIX + "InvalidateCache", CallingConvention = CallingConvention.Cdecl)]
		public static extern int InvalidateCache(IntPtr p);

		[DllImport(PHYSX, EntryPoint = PREFIX + "AddBoxCollider", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr AddBoxCollider(IntPtr scene, BoxColliderData boxData, bool isDynamic);

		[DllImport(PHYSX, EntryPoint = PREFIX + "AddSphereCollider", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr AddSphereCollider(IntPtr scene, SphereColliderData sphere, bool isDynamic);

		[DllImport(PHYSX, EntryPoint = PREFIX + "AddCapsuleCollider", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr AddCapsuleCollider(IntPtr scene, CapsuleColliderData capsule, bool isDynamic);

		[DllImport(PHYSX, EntryPoint = PREFIX + "AddMeshCollider", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr AddMeshCollider(IntPtr scene, MeshColliderData mesh, float[] vertices, int[] triangles);

		[DllImport(PHYSX, EntryPoint = PREFIX + "Update", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Update(IntPtr scene, float deltaTime);

		[DllImport(PHYSX, EntryPoint = PREFIX + "ReleaseScene", CallingConvention = CallingConvention.Cdecl)]
		public static extern void ReleaseScene(IntPtr scene);

		[DllImport(PHYSX, EntryPoint = PREFIX + "Release", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Release();

		[DllImport(PHYSX, EntryPoint = PREFIX + "GetHitInfoArray", CallingConvention = CallingConvention.Cdecl)]
		public static extern void GetHitInfoArray(IntPtr p);
		
		[DllImport(PHYSX, EntryPoint = PREFIX + "SetActorTransform", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetActorTransform(IntPtr p, PxVec3 position, PxVec4 rotation);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SetActorGravity", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetActorGravity(IntPtr p, bool useGravity);

		[DllImport(PHYSX, EntryPoint = PREFIX + "RegistSceneUpdateCallback", CallingConvention = CallingConvention.Cdecl)]
		public static extern void RegistSceneUpdateCallback(OnSceneUpdateCallback opeDelegate);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SetErrorCallback", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetErrorCallback(OnErrorCallback cb);

		[DllImport(PHYSX, EntryPoint = PREFIX + "RemoveCollider", CallingConvention = CallingConvention.Cdecl)]
		public static extern void RemoveCollider(IntPtr manager, IntPtr p);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SetActorSimulationEnabled", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetActorSimulationEnabled(IntPtr p, bool enabled);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SetShapeInSceneQueryEnabled", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetShapeInSceneQueryEnabled(IntPtr p, bool enabled);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SetFilterEnabled", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetFilterEnabled(IntPtr p, bool enabled);

		#endregion

		#region Raycast

		[DllImport(PHYSX, EntryPoint = PREFIX + "Raycast", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool Raycast(IntPtr scene, PxVec3 origin, PxVec3 direction, float maxDistance);

		[DllImport(PHYSX, EntryPoint = PREFIX + "RaycastWithMask", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool Raycast(IntPtr scene, PxVec3 origin, PxVec3 direction, float maxDistance, int layerMask);

		[DllImport(PHYSX, EntryPoint = PREFIX + "RaycastWithInfo", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool Raycast(IntPtr scene, PxVec3 origin, PxVec3 direction, float maxDistance, out PxRaycastHit hitInfo);

		[DllImport(PHYSX, EntryPoint = PREFIX + "RaycastWithMaskAndInfo", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool Raycast(IntPtr scene, PxVec3 origin, PxVec3 direction, float maxDistance, int layerMask, out PxRaycastHit hitInfo);

		[DllImport(PHYSX, EntryPoint = PREFIX + "RaycastAll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int RaycastAll(IntPtr scene, PxVec3 origin, PxVec3 direction, float maxDistance, [In, Out]PxRaycastHit[] hits);

		[DllImport(PHYSX, EntryPoint = PREFIX + "RaycastAllWithMask", CallingConvention = CallingConvention.Cdecl)]
		public static extern int RaycastAll(IntPtr scene, PxVec3 origin, PxVec3 direction, float maxDistance, int layerMask, [In, Out]PxRaycastHit[] hits);

		#endregion

		#region SphereCast

		[DllImport(PHYSX, EntryPoint = PREFIX + "SphereCast", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool SphereCast(IntPtr scene, PxVec3 origin, PxVec3 direction, float radius, float maxDistance);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SphereCastWithMask", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool SphereCast(IntPtr scene, PxVec3 origin, PxVec3 direction, float radius, float maxDistance, int layerMask);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SphereCastWithInfo", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool SphereCast(IntPtr scene, PxVec3 origin, PxVec3 direction, float radius, float maxDistance, out PxRaycastHit hitInfo);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SweepCastWithMaskAndInfo", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool SphereCast(IntPtr scene, PxVec3 origin, PxVec3 direction, float radius, float maxDistance, int layerMask, out PxRaycastHit hitInfo);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SphereCastAll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SphereCastAll(IntPtr scene, PxVec3 origin, PxVec3 direction, float radius, float maxDistance, [In, Out]PxRaycastHit[] hits);

		[DllImport(PHYSX, EntryPoint = PREFIX + "SphereCastAllWithMask", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SphereCastAll(IntPtr scene, PxVec3 origin, PxVec3 direction, float radius, float maxDistance, int layerMask, [In, Out]PxRaycastHit[] hits);
		#endregion

		#region BoxCast

		[DllImport(PHYSX, EntryPoint = PREFIX + "BoxCast", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool BoxCast(IntPtr scene, PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxVec4 orientation, float maxDistance);

		[DllImport(PHYSX, EntryPoint = PREFIX + "BoxCastWithMask", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool BoxCast(IntPtr scene, PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxVec4 orientation, float maxDistance, int layerMask);

		[DllImport(PHYSX, EntryPoint = PREFIX + "BoxCastWithInfo", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool BoxCast(IntPtr scene, PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxVec4 orientation, float maxDistance, out PxRaycastHit hitInfo);

		[DllImport(PHYSX, EntryPoint = PREFIX + "BoxCastWithMaskAndInfo", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool BoxCast(IntPtr scene, PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxVec4 orientation, float maxDistance, int layerMask, out PxRaycastHit hitInfo);

		[DllImport(PHYSX, EntryPoint = PREFIX + "BoxCastAll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BoxCastAll(IntPtr scene, PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxVec4 orientation, float maxDistance, [In, Out]PxRaycastHit[] hits);

		[DllImport(PHYSX, EntryPoint = PREFIX + "BoxCastAllWithMask", CallingConvention = CallingConvention.Cdecl)]
		public static extern int BoxCastAll(IntPtr scene, PxVec3 origin, PxVec3 direction, PxVec3 halfExtents, PxVec4 orientation, float maxDistance, int layerMask, [In, Out]PxRaycastHit[] hits);
		#endregion

		#region CapsuleCast

		[DllImport(PHYSX, EntryPoint = PREFIX + "CapsuleCast", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CapsuleCast(IntPtr scene, int axis, PxVec3 origin, PxVec3 direction, float radius, float halfHeight, PxVec4 orientation, float maxDistance);

		[DllImport(PHYSX, EntryPoint = PREFIX + "CapsuleCastWithMask", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CapsuleCast(IntPtr scene, int axis, PxVec3 origin, PxVec3 direction, float radius, float halfHeight, PxVec4 orientation, float maxDistance, int layerMask);

		[DllImport(PHYSX, EntryPoint = PREFIX + "CapsuleCastWithInfo", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CapsuleCast(IntPtr scene, int axis, PxVec3 origin, PxVec3 direction, float radius, float halfHeight, PxVec4 orientation, float maxDistance, out PxRaycastHit hitInfo);

		[DllImport(PHYSX, EntryPoint = PREFIX + "CapsuleCastWithMaskAndInfo", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool CapsuleCast(IntPtr scene, int axis, PxVec3 origin, PxVec3 direction, float radius, float halfHeight, PxVec4 orientation, float maxDistance, int layerMask, out PxRaycastHit hitInfo);

		[DllImport(PHYSX, EntryPoint = PREFIX + "CapsuleCastAll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int CapsuleCastAll(IntPtr scene, int axis, PxVec3 origin, PxVec3 direction, float radius, float halfHeight, PxVec4 orientation, float maxDistance, [In, Out]PxRaycastHit[] hits);

		[DllImport(PHYSX, EntryPoint = PREFIX + "CapsuleCastAllWithMask", CallingConvention = CallingConvention.Cdecl)]
		public static extern int CapsuleCastAll(IntPtr scene, int axis, PxVec3 origin, PxVec3 direction, float radius, float halfHeight, PxVec4 orientation, float maxDistance, int layerMask, [In, Out]PxRaycastHit[] hits);
		#endregion

	}

}


