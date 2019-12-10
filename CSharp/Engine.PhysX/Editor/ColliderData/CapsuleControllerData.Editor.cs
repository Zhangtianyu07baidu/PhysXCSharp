#if UNITY_EDITOR

using System;
using UnityEngine;


namespace Engine.PhysX
{
	public partial struct CapsuleControllerData
	{
		public CapsuleControllerData(CharacterController controller)
		{
			this.layer = controller.gameObject.layer;

			this.slopeLimit = controller.slopeLimit;
			this.stepOffset = controller.stepOffset;
			this.skinWidth = controller.skinWidth;
			this.minMoveDistance = controller.minMoveDistance;

			//radius
			float maxScale = Math.Max(controller.transform.lossyScale.x, controller.transform.lossyScale.z);
			this.radius = controller.radius * maxScale;

			//height
			this.height = controller.height * controller.transform.lossyScale.y - this.radius * 2;

			//world pos
			//this.position = new PxVec3();
			UnityEngine.Vector3 position = controller.transform.position;
			this.position.x = position.x + controller.center.x * controller.transform.lossyScale.x;
			this.position.y = position.y + controller.center.y * 10 * controller.transform.lossyScale.y;
			this.position.z = position.z + controller.center.z * controller.transform.lossyScale.z;

#if PHYSX_DEBUG
			Debug.Log("SlopeLimit: " + this.slopeLimit);
			Debug.Log("StepOffset: " + this.stepOffset);
			Debug.Log("SkinWidth: " + this.skinWidth);
			Debug.Log("MinMoveDistance: " + this.minMoveDistance);
			Debug.Log("Radius: " + this.radius);
			Debug.Log("Height: " + this.height);

			Debug.Log("=============Pos================");
			Debug.Log("Controller Position X: " + this.position.x);
			Debug.Log("Controller Position Y: " + this.position.y);
			Debug.Log("Controller Position Z: " + this.position.z);
#endif
		}
	}
}


#endif