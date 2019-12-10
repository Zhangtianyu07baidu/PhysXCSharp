#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Engine.PhysX
{
	public partial struct SphereColliderData
	{
		public SphereColliderData(SphereCollider sphere)
		{
			if (sphere.transform.lossyScale.x <= 0 || sphere.transform.lossyScale.y <= 0 ||
			    sphere.transform.lossyScale.z <= 0)
			{
				Debug.LogError("scale param is less then zero!");
			}

			float scaleX = Mathf.Abs(sphere.transform.lossyScale.x);
			float scaleY = Mathf.Abs(sphere.transform.lossyScale.y);
			float scaleZ = Mathf.Abs(sphere.transform.lossyScale.z);

			this.layer = sphere.gameObject.layer;
			//scale
			float maxScale = Math.Max(Math.Max(scaleX, scaleY), scaleZ);
			this.radius = sphere.radius * maxScale;

			//world pos
			UnityEngine.Vector3 position = sphere.transform.position;
			position += sphere.transform.right.normalized * sphere.center.x * scaleX;
			position += sphere.transform.up.normalized * sphere.center.y * scaleY;
			position += sphere.transform.forward.normalized * sphere.center.z * scaleZ;

			this.position.x = position.x;
			this.position.y = position.y;
			this.position.z = position.z;

			//rotation considered
			this.rotation.x = sphere.transform.rotation.x;
			this.rotation.y = sphere.transform.rotation.y;
			this.rotation.z = sphere.transform.rotation.z;
			this.rotation.w = sphere.transform.rotation.w;

#if PHYSX_DEBUG
			Debug.Log("=============Pos================");
			Debug.Log("Sphere Position X: " + this.position.x);
			Debug.Log("Sphere Position Y: " + this.position.y);
			Debug.Log("Sphere Position Z: " + this.position.z);
			Debug.Log("==============Rot================");
			Debug.Log("Sphere Rotation X: " + this.rotation.x);
			Debug.Log("Sphere Rotation Y: " + this.rotation.y);
			Debug.Log("Sphere Rotation Z: " + this.rotation.z);
			Debug.Log("Sphere Rotation W: " + this.rotation.w);
			Debug.Log("===========Radius=============");
			Debug.Log("Sphere Radius: " + this.radius);
#endif
		}
	}
}

#endif