using System.Runtime.InteropServices;


namespace Engine.PhysX
{
	[StructLayout(LayoutKind.Sequential)]
	public partial struct SphereColliderData : IColliderData
	{
		public int layer;
		public float radius;
		public PxVec3 position;
		public PxVec4 rotation;

		public SphereColliderData(U3DPhysxSphere sphere)
		{
			this.layer = sphere.Layer;
			this.radius = sphere.Radius;

			this.position.x = sphere.Position.X;
			this.position.y = sphere.Position.Y;
			this.position.z = sphere.Position.Z;

			this.rotation.x = sphere.Rotation.X;
			this.rotation.y = sphere.Rotation.Y;
			this.rotation.z = sphere.Rotation.Z;
			this.rotation.w = sphere.Rotation.W;
		}

		public bool IsValid
		{
			get
			{
				if (float.IsInfinity(this.radius))
				{
					NLog.Log.Error("radius is invalid in SphereColliderData");
					return false;
				}

				if (this.radius <= 0f)
				{
					NLog.Log.Error("radius is less then zero in SphereColliderData");
					return false;
				}

				if (this.rotation == PxVec4.Zero)
				{
					NLog.Log.Error("SphereColliderData rotation is invalid! Please Modify it");
					return false;
				}

				return true;
			}
		}

	}
}
