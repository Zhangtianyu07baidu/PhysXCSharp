
namespace Engine.PhysX
{
	public partial class PhysicsScene
	{
		public bool BoxCast(PxVec3 center, PxVec3 halfExtents, PxVec3 direction)
		{
			return BoxCast(center, halfExtents, direction, PxVec4.Identity);
		}

		public bool BoxCast(PxVec3 center, PxVec3 halfExtents, PxVec3 direction, out PxRaycastHit hitInfo)
		{
			return BoxCast(center, halfExtents, direction, out hitInfo, PxVec4.Identity);
		}

		public bool BoxCast(PxVec3 center, PxVec3 halfExtents, PxVec3 direction, PxVec4 orientation)
		{
			return BoxCast(center, halfExtents, direction, orientation, -1);
		}

		public bool BoxCast(PxVec3 center, PxVec3 halfExtents, PxVec3 direction, out PxRaycastHit hitInfo, PxVec4 orientation)
		{
			return BoxCast(center, halfExtents, direction, out hitInfo, orientation, -1);
		}

		public bool BoxCast(PxVec3 center, PxVec3 halfExtents, PxVec3 direction, PxVec4 orientation, float maxDistance)
		{
			return PhysXDll.BoxCast(this.scenePtr, center, direction, halfExtents, orientation, maxDistance);
		}

		public bool BoxCast(PxVec3 center, PxVec3 halfExtents, PxVec3 direction, out PxRaycastHit hitInfo, PxVec4 orientation, float maxDistance)
		{
			return PhysXDll.BoxCast(this.scenePtr, center, direction, halfExtents, orientation, maxDistance, out hitInfo);
		}

		public bool BoxCast(PxVec3 center, PxVec3 halfExtents, PxVec3 direction, PxVec4 orientation, float maxDistance, int layerMask)
		{
			return PhysXDll.BoxCast(this.scenePtr, center, direction, halfExtents, orientation, maxDistance, layerMask);
		}

		public bool BoxCast(PxVec3 center, PxVec3 halfExtents, PxVec3 direction, out PxRaycastHit hitInfo, PxVec4 orientation, float maxDistance, int layerMask)
		{
			return PhysXDll.BoxCast(this.scenePtr, center, direction, halfExtents, orientation, maxDistance, layerMask, out hitInfo);
		}

		public int BoxCastAll(PxVec3 center, PxVec3 halfExtents, PxVec3 direction)
		{
			return BoxCastAll(center, halfExtents, direction, PxVec4.Identity);
		}

		public int BoxCastAll(PxVec3 center, PxVec3 halfExtents, PxVec3 direction, PxVec4 orientation)
		{
			return BoxCastAll(center, halfExtents, direction, orientation, -1);
		}

		public int BoxCastAll(PxVec3 center, PxVec3 halfExtents, PxVec3 direction, PxVec4 orientation,
			float maxDistance)
		{
			return PhysXDll.BoxCastAll(this.scenePtr, center, direction, halfExtents, orientation, maxDistance, this.RaycastHits);
		}

		public int BoxCastAll(PxVec3 center, PxVec3 halfExtents, PxVec3 direction, PxVec4 orientation,
			float maxDistance, int layermask)
		{
			return PhysXDll.BoxCastAll(this.scenePtr, center, direction, halfExtents, orientation, maxDistance, layermask, this.RaycastHits);
		}
	}
}
