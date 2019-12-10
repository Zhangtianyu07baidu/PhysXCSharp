using System.Runtime.InteropServices;


namespace Engine.PhysX
{
	[StructLayout(LayoutKind.Sequential)]
	public partial struct MeshColliderData : IColliderData
	{
		private const float PX_MESH_SCALE_MAX = 1e6f;
		private const float PX_MESH_SCALE_MIN = 1e-6f;

		public int layer;
		public PxVec3 position;
		public PxVec4 rotation;
		public PxVec3 scale;
		/// <summary>
		/// 是否是反面
		/// </summary>
		public bool IsReverseSide;
		public int verticesNum;
		public int trianglesNum;

		public float[] vertices;
		public int[] triangles;

		public MeshColliderData(U3DPhysxMesh mesh)
		{
			this.layer = mesh.Layer;
			this.IsReverseSide = false;
			this.position.x = mesh.Position.X;
			this.position.y = mesh.Position.Y;
			this.position.z = mesh.Position.Z;

			this.rotation.x = mesh.Rotation.X;
			this.rotation.y = mesh.Rotation.Y;
			this.rotation.z = mesh.Rotation.Z;
			this.rotation.w = mesh.Rotation.W;

			this.scale.x = mesh.Scale.X;
			this.scale.y = mesh.Scale.Y;
			this.scale.z = mesh.Scale.Z;

			this.verticesNum = mesh.Vertices.Count;
			this.vertices = new float[this.verticesNum * 3];
			for (int i = 0; i < this.verticesNum; ++i)
			{
				ProtoVec3 protoVec3 = mesh.Vertices[i];
				int index = i * 3;
				this.vertices[index] = protoVec3.X;
				this.vertices[index + 1] = protoVec3.Y;
				this.vertices[index + 2] = protoVec3.Z;
			}

			this.trianglesNum = mesh.Triangles.Length;
			this.triangles = new int[this.trianglesNum];
			for (int i = 0; i < this.trianglesNum; i++)
			{
				this.triangles[i] = mesh.Triangles[i];
			}
		}

		public bool IsValid
		{
			get
			{
				if (!this.scale.IsFinite())
				{
					NLog.Log.Error("scale param is invalid in MeshColliderData");
					return false;
				}

				if (this.rotation == PxVec4.Zero)
				{
					NLog.Log.Error("PxMeshCollider Rotation is invalid! Please Modify it");
					return false;
				}

				if (!this.IsValidForTriangleMesh())
				{
					NLog.Log.Error("scale is invalid for TriangleMesh");
					return false;
				}

				return true;
			}
		}

		private bool IsValidForTriangleMesh()
		{
			PxVec3 absXYZ = this.scale.Abs;
			return absXYZ.MaxElement <= PX_MESH_SCALE_MAX && absXYZ.MinElement >= PX_MESH_SCALE_MIN;
		}
	}
}
