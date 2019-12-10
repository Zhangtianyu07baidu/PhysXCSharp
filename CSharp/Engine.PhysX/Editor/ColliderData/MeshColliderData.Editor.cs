
#if UNITY_EDITOR

using UnityEngine;

namespace Engine.PhysX
{
	public partial struct MeshColliderData
	{
		public MeshColliderData(MeshCollider collider)
		{
			if (collider.transform.lossyScale.x <= 0 || collider.transform.lossyScale.y <= 0 ||
			    collider.transform.lossyScale.z <= 0)
			{
				Debug.LogError("scale param is less then zero!");
			}
			this.layer = collider.gameObject.layer;
			this.IsReverseSide = false;
			this.scale.x = UnityEngine.Mathf.Abs(collider.transform.lossyScale.x);
			this.scale.y = UnityEngine.Mathf.Abs(collider.transform.lossyScale.y);
			this.scale.z = UnityEngine.Mathf.Abs(collider.transform.lossyScale.z);

			this.position.x = collider.transform.position.x;
			this.position.y = collider.transform.position.y;
			this.position.z = collider.transform.position.z;

			this.rotation.x = collider.transform.rotation.x;
			this.rotation.y = collider.transform.rotation.y;
			this.rotation.z = collider.transform.rotation.z;
			this.rotation.w = collider.transform.rotation.w;

			Mesh sharedMesh = collider.sharedMesh;
			if (sharedMesh == null)
			{
				Debug.LogError("sharedMesh is null in " + collider.name);
			}

			if (sharedMesh.vertexCount <= 0)
			{
				Debug.LogError("sharedMesh vertexCount is zero in " + collider.name);
			}

			this.verticesNum = sharedMesh.vertexCount;
			this.vertices = new float[this.verticesNum * 3];

			for (int i = 0; i < this.verticesNum; ++i)
			{
				Vector3 vec3 = sharedMesh.vertices[i];
				int index = i * 3;
				this.vertices[index] = vec3.x * this.scale.x;
				this.vertices[index + 1] = vec3.y * this.scale.y;
				this.vertices[index + 2] = vec3.z * this.scale.z;
			}

			this.trianglesNum = sharedMesh.triangles.Length;
			this.triangles = new int[this.trianglesNum];
			for (int i = 0; i < this.trianglesNum; i++)
			{
				this.triangles[i] = sharedMesh.triangles[i];
			}
		}
	}
}


#endif