#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Engine.PhysX
{
	public partial struct CapsuleColliderData
	{
		private const float MIN_HEIGHT = 0.00001f;

		public CapsuleColliderData(CapsuleCollider collider)
		{
			float scaleX = Mathf.Abs(collider.transform.lossyScale.x);
			float scaleY = Mathf.Abs(collider.transform.lossyScale.y);
			float scaleZ = Mathf.Abs(collider.transform.lossyScale.z);

			this.layer = collider.gameObject.layer;
			this.direction = collider.direction;

			if (collider.transform.lossyScale.x <= 0 || collider.transform.lossyScale.y <= 0 ||
				collider.transform.lossyScale.z <= 0)
			{
				Debug.LogError("scale param is less then zero!");
			}

			switch (collider.direction)
			{
				//X-Axis
				case 0:
					{
						//radius
						float maxScale = Math.Max(scaleY, scaleZ);
						this.radius = collider.radius * maxScale;

						//halfHeight
						this.halfHeight = collider.height * scaleX * 0.5f - this.radius;
						this.halfHeight = Math.Max(MIN_HEIGHT, this.halfHeight);
						break;
					}
				//Y-Axis
				case 1:
					{
						//radius
						float maxScale = Math.Max(scaleX, scaleZ);
						this.radius = collider.radius * maxScale;

						//halfHeight
						this.halfHeight = collider.height * scaleY * 0.5f - this.radius;
						this.halfHeight = Math.Max(MIN_HEIGHT, this.halfHeight);
						break;
					}
				//Z-Axis
				case 2:
					{
						//radius
						float maxScale = Math.Max(scaleX, scaleY);
						this.radius = collider.radius * maxScale;

						//halfHeight
						this.halfHeight = collider.height * scaleZ * 0.5f - this.radius;
						this.halfHeight = Math.Max(MIN_HEIGHT, this.halfHeight);
						break;
					}
				default:
					this.radius = 0f;
					this.halfHeight = 0f;
					NLog.Log.Assert(false);
					break;
			}

			//world pos
			UnityEngine.Vector3 position = collider.transform.position;
			position += collider.transform.right.normalized * collider.center.x * scaleX;
			position += collider.transform.up.normalized * collider.center.y * scaleY;
			position += collider.transform.forward.normalized * collider.center.z * scaleZ;

			this.position.x = position.x;
			this.position.y = position.y;
			this.position.z = position.z;

			//rotation considered
			Quaternion rotation = collider.transform.rotation;
			this.rotation.x = rotation.x;
			this.rotation.y = rotation.y;
			this.rotation.z = rotation.z;
			this.rotation.w = rotation.w;

#if PHYSX_DEBUG
			UnityEngine.Debug.Log("=============Pos================");
			UnityEngine.Debug.Log("Capsule Position X: " + this.position.x);
			UnityEngine.Debug.Log("Capsule Position Y: " + this.position.y);
			UnityEngine.Debug.Log("Capsule Position Z: " + this.position.z);
			UnityEngine.Debug.Log("==============Rot================");
			UnityEngine.Debug.Log("Capsule Rotation X: " + this.rotation.x);
			UnityEngine.Debug.Log("Capsule Rotation Y: " + this.rotation.y);
			UnityEngine.Debug.Log("Capsule Rotation Z: " + this.rotation.z);
			UnityEngine.Debug.Log("Capsule Rotation W: " + this.rotation.w);
			UnityEngine.Debug.Log("===========Radius=============");
			UnityEngine.Debug.Log("Capsule Radius: " + this.radius);
			UnityEngine.Debug.Log("Capsule HalfHeight: " + this.halfHeight);
#endif
		}
	}
}


#endif