

namespace Engine.PhysX
{
	public abstract class ControllerFilterCallback
	{
		/// <summary>
		/// 和非Character Controller碰撞
		/// </summary>
		public virtual bool OnFilterWorld(PxCollider collider)
		{
			return true;
		}

		/// <summary>
		/// 和Character Controller碰撞
		/// </summary>
		/// <param name="controller"></param>
		/// <returns></returns>
		public virtual bool OnFilterController(PxCapsuleController controller)
		{
			return true;
		}
	}
}
