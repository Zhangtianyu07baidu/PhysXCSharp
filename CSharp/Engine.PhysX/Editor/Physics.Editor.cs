#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;


namespace Engine.PhysX
{
	public partial class Physics
	{
		private static readonly List<string> selectedName = new List<string>();

		public static void DrawGizmos()
		{
			selectedName.Clear();
			foreach (var go in UnityEditor.Selection.gameObjects)
			{
				selectedName.Add(go.name);
			}

			foreach (var scene in scenes)
			{
				foreach (var collider in scene.Value.Colliders)
				{
					if (!string.IsNullOrEmpty(collider.Value.Name) && selectedName.Any(n => n.Equals(collider.Value.Name)))
						collider.Value.DrawGizmos();
				}
			}
		}
	}
}

#endif