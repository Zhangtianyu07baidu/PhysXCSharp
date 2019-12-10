using System.Runtime.InteropServices;

namespace Engine.PhysX
{
	[StructLayout(LayoutKind.Sequential)]
	public partial struct CapsuleColliderData : IColliderData
	{
		public int layer;
		public float radius;
		public float halfHeight;
		public PxVec3 position;
		public PxVec4 rotation;
		public int direction;

		public CapsuleColliderData(U3DPhysxCapsule capsule)
		{
			this.layer = capsule.Layer;
			this.radius = capsule.Radius;
			this.halfHeight = capsule.halfHeight;

			this.position.x = capsule.Position.X;
			this.position.y = capsule.Position.Y;
			this.position.z = capsule.Position.Z;

			this.rotation.x = capsule.Rotation.X;
			this.rotation.y = capsule.Rotation.Y;
			this.rotation.z = capsule.Rotation.Z;
			this.rotation.w = capsule.Rotation.W;

			this.direction = capsule.Direction;
		}

		public bool IsValid
		{
			get
			{
				if (float.IsInfinity(this.radius) || float.IsInfinity(this.halfHeight))
				{
					NLog.Log.Error("radius or halfHeight is invalid in CapsuleColliderData");
					return false;
				}

				if (this.radius <= 0f)
				{
					NLog.Log.Error("the radius is less then zero in CapsuleColliderData");
					return false;
				}

				if (this.halfHeight <= 0f)
				{
					NLog.Log.Error("the halfHeight is less then zero in CapsuleColliderData");
					return false;
				}

				if (this.rotation == PxVec4.Zero)
				{
					NLog.Log.Error("CapsuleColliderData rotation is invalid! Please Modify it");
					return false;
				}

				return true;
			}
		}

	}
}
