#if UNITY_EDITOR

using System;
using UnityEngine;


namespace Engine.PhysX
{
	public partial class PxMeshCollider
	{
		private Mesh mesh;

		public PxMeshCollider(PhysicsScene scene, MeshColliderData ColliderData, string name) : this(scene, ColliderData)
		{
			this.Name = name;
		}

		public override void DrawGizmos()
		{
			if (this.mesh == null)
			{
				this.mesh = new Mesh();
				this.mesh.Clear();

				UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[this.ColliderData.verticesNum];
				for (int i = 0; i < this.ColliderData.verticesNum; i++)
				{
					int index = i * 3;
					Vector3 vertice = new Vector3();
					vertice.x = this.ColliderData.vertices[index] * this.ColliderData.scale.x;
					vertice.y = this.ColliderData.vertices[index + 1] * this.ColliderData.scale.y;
					vertice.z = this.ColliderData.vertices[index + 2] * this.ColliderData.scale.z;
					vertices[i] = vertice;
				}
				this.mesh.vertices = vertices;

				int[] triangles = new int[this.ColliderData.trianglesNum];
				for (int i = 0; i < this.ColliderData.trianglesNum; i++)
				{
					triangles[i] = this.ColliderData.triangles[i];
				}
				this.mesh.triangles = triangles;

				this.mesh.RecalculateNormals();
			}

			if(this.mesh != null)
			{
				Color color = Gizmos.color;
				Gizmos.color = Color.green;
				Gizmos.DrawWireMesh(this.mesh, this.position, this.rotation);
				Gizmos.color = color;
			}
		}
	}
}

#endif