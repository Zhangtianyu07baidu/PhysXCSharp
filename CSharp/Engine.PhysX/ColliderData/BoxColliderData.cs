using System.Runtime.InteropServices;

namespace Engine.PhysX
{
	[StructLayout(LayoutKind.Sequential)]
	public partial struct BoxColliderData : IColliderData
	{
		public int layer;
		public PxVec3 position;
		public PxVec4 rotation;
		public float x_extents;
		public float y_extents;
		public float z_extents;

		public BoxColliderData(U3DPhysxBox box)
		{
			this.layer = box.Layer;

			//extents 考虑父节点的影响
			this.x_extents = box.xExtents;
			this.y_extents = box.yExtents;
			this.z_extents = box.zExtents;

			//world pos
			this.position.x = box.Position.X;
			this.position.y = box.Position.Y;
			this.position.z = box.Position.Z;

			//rotation considered
			this.rotation.x = box.Rotation.X;
			this.rotation.y = box.Rotation.Y;
			this.rotation.z = box.Rotation.Z;
			this.rotation.w = box.Rotation.W;
		}

		public bool IsValid
		{
			get
			{
				if (float.IsInfinity(this.x_extents) || float.IsInfinity(this.y_extents) ||
				    float.IsInfinity(this.z_extents))
				{
					NLog.Log.Error("PxBoxCollider extents is invalid! Please Modify it");
					return false;
				}

				if (this.x_extents <= 0f || this.y_extents <= 0f || this.z_extents <= 0f)
				{
					NLog.Log.Error("PxBoxCollider extents is less then zero! Please Modify it");
					return false;
				}

				if (this.rotation == PxVec4.Zero)
				{
					NLog.Log.Error("PxBoxCollider Rotation is invalid! Please Modify it");
					return false;
				}

				return true;
			}
		}
	}
}
