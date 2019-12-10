using System;

namespace Engine.PhysX
{
	/// <summary>
	/// halfHeight表示的是胶囊体的圆柱部分一半的高度
	/// </summary>
	public partial class PhysicsScene
	{
		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeight, PxVec3 direction)
		{
			return CapsuleCast(axis, center, radius, halfHeight, direction, PxVec4.Identity);
		}

		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeight, PxVec3 direction, PxVec4 orientation)
		{
			return CapsuleCast(axis, center, radius, halfHeight, direction, -1, orientation);
		}

		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, int layerMask)
		{
			return CapsuleCast(axis, center, radius, halfHeigh, direction, -1, layerMask);
		}

		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, out PxRaycastHit hitInfo)
		{
			return CapsuleCast(axis, center, radius, halfHeigh, direction, out hitInfo, -1);
		}

		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeight, PxVec3 direction, float maxDistance, PxVec4 orientation)
		{
			return PhysXDll.CapsuleCast(this.scenePtr, (int)axis, center, direction, radius, halfHeight, orientation, maxDistance);
		}

		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, out PxRaycastHit hitInfo, PxVec4 orientation)
		{
			return CapsuleCast(axis, center, radius, halfHeigh, direction, out hitInfo, -1, orientation);
		}

		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, float maxDistance, int layerMask)
		{
			return CapsuleCast(axis, center, radius, halfHeigh, direction, maxDistance, layerMask, PxVec4.Identity);
		}

		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, out PxRaycastHit hitInfo, float maxDistance)
		{
			return CapsuleCast(axis, center, radius, halfHeigh, direction, out hitInfo, maxDistance, PxVec4.Identity);
		}

		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, float maxDistance, int layerMask, PxVec4 orientation)
		{
			return PhysXDll.CapsuleCast(this.scenePtr, (int)axis, center, direction, radius, halfHeigh, orientation, maxDistance, layerMask);
		}

		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, out PxRaycastHit hitInfo, float maxDistance, PxVec4 orientation)
		{
			return PhysXDll.CapsuleCast(this.scenePtr, (int)axis, center, direction, radius, halfHeigh, orientation, maxDistance, out hitInfo);
		}

		public bool CapsuleCast(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, out PxRaycastHit hitInfo, float maxDistance, int layerMask, PxVec4 orientation)
		{
			return PhysXDll.CapsuleCast(this.scenePtr, (int)axis, center, direction, radius, halfHeigh, orientation, maxDistance, layerMask, out hitInfo);
		}

		public int CapsuleCastAll(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction)
		{
			return CapsuleCastAll(axis, center, radius, halfHeigh, direction, -1);
		}

		public int CapsuleCastAll(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, PxVec4 orientation)
		{
			return CapsuleCastAll(axis, center, radius, halfHeigh, direction, -1, orientation);
		}

		public int CapsuleCastAll(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, float maxDistance)
		{
			return CapsuleCastAll(axis, center, radius, halfHeigh, direction, maxDistance, PxVec4.Identity);
		}

		public int CapsuleCastAll(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, float maxDistance, PxVec4 orientation)
		{
			return PhysXDll.CapsuleCastAll(this.scenePtr, (int)axis, center, direction, radius, halfHeigh, orientation, maxDistance, this.RaycastHits);
		}

		public int CapsuleCastAll(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, int layerMask)
		{
			return CapsuleCastAll(axis, center, radius, halfHeigh, direction, -1, layerMask);
		}

		public int CapsuleCastAll(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, int layerMask, PxVec4 orientation)
		{
			return CapsuleCastAll(axis, center, radius, halfHeigh, direction, -1, layerMask, orientation);
		}

		public int CapsuleCastAll(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, float maxDistance, int layerMask)
		{
			return CapsuleCastAll(axis, center, radius, halfHeigh, direction, maxDistance, layerMask, PxVec4.Identity);
		}

		public int CapsuleCastAll(CapsuleAxis axis, PxVec3 center, float radius, float halfHeigh, PxVec3 direction, float maxDistance, int layerMask, PxVec4 orientation)
		{
			return PhysXDll.CapsuleCastAll(this.scenePtr, (int)axis, center, direction, radius, halfHeigh, orientation, maxDistance, layerMask, this.RaycastHits);
		}
	}
}
