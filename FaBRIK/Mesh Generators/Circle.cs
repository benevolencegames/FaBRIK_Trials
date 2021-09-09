#region Circle Mesh Generator
using UnityEngine;

namespace MonkeyWrench.MeshGenerator
{

	public static class Circle
	{

		const int res = 100;

		public static void GenerateMesh(Mesh _mesh, float _angle)
		{

			int increments = (int)Mathf.Max(5, res * _angle / 360);

			float angleIncrement = _angle / (increments - 1f);
			var verts = new Vector3[increments + 1];
			var norms = new Vector3[increments + 1];
			var tris = new int[(increments - 1) * 3];
			verts[0] = Vector3.zero;
			norms[0] = Vector3.up;

			for (int i = 0; i < increments; i++)
			{
				float currAngle = (angleIncrement * i) * Mathf.Deg2Rad;
				Vector3 pos = new Vector3(Mathf.Sin(currAngle), 0, Mathf.Cos(currAngle));
				verts[i + 1] = pos;
				norms[i + 1] = Vector3.up;

				if (i < increments - 1)
				{
					tris[i * 3] = 0;
					tris[i * 3 + 1] = i + 1;
					tris[i * 3 + 2] = i + 2;
				}
			}

			_mesh.vertices = verts;
			_mesh.triangles = tris;
			_mesh.normals = norms;
		}
	}
}
#endregion