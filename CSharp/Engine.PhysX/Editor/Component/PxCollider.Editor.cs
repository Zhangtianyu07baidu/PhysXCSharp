
#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Engine.PhysX
{
	public partial class PxCollider
	{
		public string Name { get; protected set; }

		public virtual void DrawGizmos()
		{

		}
	}
}


#endif