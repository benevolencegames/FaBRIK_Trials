#region Utilities
using System.Collections.Generic;
using UnityEngine;

namespace MonkeyWrench.MeshGenerator
{
	public static class Utility
	{
		public static void MultiMesh(Mesh _mesh, List<Vector3>[] _vertexLists, List<int>[] _triList)
		{
			var vertices = new List<Vector3>();
			var tris = new List<int>();

			for (int i = 0; i < _vertexLists.Length; i++)
			{
				int vertSkipCount = vertices.Count;
				vertices.AddRange(_vertexLists[i]);

				for (int triIndex = 0; triIndex < _triList[i].Count; triIndex++)
				{
					tris.Add(_triList[i][triIndex] + vertSkipCount);
				}
			}

			_mesh.SetVertices(vertices);
			_mesh.SetTriangles(tris, 0);
			_mesh.RecalculateNormals();
			_mesh.RecalculateBounds();
		}

	}
}
#endregion