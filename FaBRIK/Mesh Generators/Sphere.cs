#region Sphere Mesh Generator
using System.Collections.Generic;
using UnityEngine;

namespace MonkeyWrench.MeshGenerator
{
    public static class Sphere
    {

        const int resolution = 20;
        static Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        #region Creation
        public static void CreateMesh(Mesh _mesh)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<int> triangles = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                Vector3 localUp = directions[i];
                var faceData = ConstructFace(localUp, resolution);

                int numVerts = vertices.Count;
                vertices.AddRange(faceData.verts);
                normals.AddRange(faceData.norms);

                for (int j = 0; j < faceData.tris.Length; j++)
                {
                    triangles.Add(faceData.tris[j] + numVerts);
                }
            }

            _mesh.SetVertices(vertices);
            _mesh.SetNormals(normals);
            _mesh.SetTriangles(triangles, 0);
        }
        #endregion

        #region Construction

        static ShapeFace ConstructFace(Vector3 _localUp, int _res)
        {
            Vector3 axisA = new Vector3(_localUp.y, _localUp.z, _localUp.x);
            Vector3 axisB = Vector3.Cross(_localUp, axisA);

            Vector3[] verts = new Vector3[_res * _res];
            Vector3[] norms = new Vector3[_res * _res];
            int[] tris = new int[(_res - 1) * (_res - 1) * 6];
            int triIndex = 0;

            for (int y = 0; y < _res; y++)
            {
                for (int x = 0; x < _res; x++)
                {
                    int i = x + y * _res;
                    Vector2 percent = new Vector2(x, y) / (_res - 1);
                    Vector3 pointOnCube = _localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                    Vector3 pointOnSphere = pointOnCube.normalized;
                    verts[i] = pointOnSphere;
                    norms[i] = pointOnSphere;

                    if (x != _res - 1 && y != _res - 1)
                    {
                        tris[triIndex] = i;
                        tris[triIndex + 1] = i + _res + 1;
                        tris[triIndex + 2] = i + _res;

                        tris[triIndex + 3] = i;
                        tris[triIndex + 4] = i + 1;
                        tris[triIndex + 5] = i + _res + 1;
                        triIndex += 6;
                    }
                }
            }

            return new ShapeFace() { tris = tris, verts = verts, norms = norms };
        }

        public class ShapeFace
        {
            public int[] tris;
            public Vector3[] verts;
            public Vector3[] norms;
        }
        #endregion
    }
}
#endregion