
using Engine.Base;


namespace Engine.PhysX
{
	/// <summary>
	///   <para>Representation of rays.</para>
	/// </summary>
	public struct PxRay
	{
		/// <summary>
		///   <para>The origin point of the ray.</para>
		/// </summary>
		public Vector3 origin { get; }

		/// <summary>
		///   <para>The direction of the ray.</para>
		/// </summary>
		public Vector3 direction { get; }

		/// <summary>
		///   <para>Creates a ray starting at origin along direction.</para>
		/// </summary>
		/// <param name="origin"></param>
		/// <param name="direction"></param>
		public PxRay(Vector3 origin, Vector3 direction)
		{
			this.origin = origin;
			this.direction = direction;
		}

		/// <summary>
		///   <para>Returns a point at distance units along the ray.</para>
		/// </summary>
		/// <param name="distance"></param>
		public Vector3 GetPoint(float distance)
		{
			return this.origin + this.direction * distance;
		}

		/// <summary>
		///   <para>Returns a nicely formatted string for this ray.</para>
		/// </summary>
		/// <param name="format"></param>
		public override string ToString()
		{
			return string.Format("Origin: {0}, Dir: {1}", this.origin, this.direction);
		}
	}
}
