#if UNITY_EDITOR
using UnityEngine;


namespace Engine.PhysX
{
	public partial struct BoxColliderData
	{
		public BoxColliderData(UnityEngine.BoxCollider boxCollider)
		{
			if (boxCollider.transform.lossyScale.x <= 0 || boxCollider.transform.lossyScale.y <= 0 ||
			    boxCollider.transform.lossyScale.z <= 0)
			{
				Debug.LogError("scale param is less then zero!");
			}

			float scaleX = Mathf.Abs(boxCollider.transform.lossyScale.x);
			float scaleY = Mathf.Abs(boxCollider.transform.lossyScale.y);
			float scaleZ = Mathf.Abs(boxCollider.transform.lossyScale.z);

			this.layer = boxCollider.gameObject.layer;

			//extents 考虑父节点的影响
			this.x_extents = boxCollider.size.x * scaleX;
			this.y_extents = boxCollider.size.y * scaleY;
			this.z_extents = boxCollider.size.z * scaleZ;

			//world pos
			UnityEngine.Vector3 position = boxCollider.transform.position;
			position += boxCollider.transform.right.normalized * boxCollider.center.x * scaleX;
			position += boxCollider.transform.up.normalized * boxCollider.center.y * scaleY;
			position += boxCollider.transform.forward.normalized * boxCollider.center.z * scaleZ;

			this.position.x = position.x;
			this.position.y = position.y;
			this.position.z = position.z;

			//rotation considered
			this.rotation.x = boxCollider.transform.rotation.x;
			this.rotation.y = boxCollider.transform.rotation.y;
			this.rotation.z = boxCollider.transform.rotation.z;
			this.rotation.w = boxCollider.transform.rotation.w;

#if PHYSX_DEBUG
			UnityEngine.Debug.Log("Box Layer: " + this.layer);
			UnityEngine.Debug.Log("=============Pos================");
			UnityEngine.Debug.Log("Box Position X: " + this.position.x);
			UnityEngine.Debug.Log("Box Position Y: " + this.position.y);
			UnityEngine.Debug.Log("Box Position Z: " + this.position.z);
			UnityEngine.Debug.Log("==============Rot================");
			UnityEngine.Debug.Log("Box Rotation X: " + this.rotation.x);
			UnityEngine.Debug.Log("Box Rotation Y: " + this.rotation.y);
			UnityEngine.Debug.Log("Box Rotation Z: " + this.rotation.z);
			UnityEngine.Debug.Log("Box Rotation W: " + this.rotation.w);
			UnityEngine.Debug.Log("===========Extents=============");
			UnityEngine.Debug.Log("Box X_Extents: " + this.x_extents);
			UnityEngine.Debug.Log("Box Y_Extents: " + this.y_extents);
			UnityEngine.Debug.Log("Box Z_Extents: " + this.z_extents);
#endif
		}
	}
}


#endif