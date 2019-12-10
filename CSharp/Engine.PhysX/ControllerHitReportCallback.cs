
namespace Engine.PhysX
{
	public abstract class ControllerHitReportCallback
	{
		/// <summary>
		/// This is called when the CCT moves and hits a shape. This will not be called when a moving shape hits a non-moving CCT.
		/// </summary>
		/// <param name="collider">非Controller</param>
		public abstract void OnHitShape(PxCollider collider);

		/// <summary>
		/// Called when current controller hits another controller.
		/// </summary>
		/// <param name="controller"></param>
		public abstract void OnHitController(PxCapsuleController controller);

		/*/// <summary>
		/// Called when current controller hits a user-defined obstacle.
		/// </summary>
		/// <param name="obstacle"></param>
		public abstract void OnHitObstacle(PxCollider obstacle);*/
	}
}
