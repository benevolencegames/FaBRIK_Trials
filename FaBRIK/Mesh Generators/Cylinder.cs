#region Cylinder Mesh Generator
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonkeyWrench.MeshGenerator
{
    public static class Cylinder
    {

        const int res = 20;

        public static void GenerateMesh(Mesh _mesh)
        {

            float radius = .5f;

            var bottomVerts = new List<Vector3>();
            var bottomTris = new List<int>();

            var topVerts = new List<Vector3>();
            var topTris = new List<int>();

            var sideVerts = new List<Vector3>();
            var sideTris = new List<int>();

            // Top/bottom face
            Vector3 bottomCentre = Vector3.down * .5f;
            Vector3 topCentre = Vector3.up * .5f;

            for (int i = 0; i < res; i++)
            {
                float angle = i / (float)(res) * Mathf.PI * 2;
                Vector3 offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius;
                bottomVerts.Add(bottomCentre + offset);
                bottomTris.AddRange(new int[] { res, (i + 1) % res, i % res });

                topVerts.Add(topCentre + offset);
                topTris.AddRange(new int[] { res, i % res, (i + 1) % res });
            }

            sideVerts.AddRange(bottomVerts);
            sideVerts.AddRange(topVerts);

            bottomVerts.Add(bottomCentre);
            topVerts.Add(topCentre);

            // Sides
            for (int i = 0; i < res; i++)
            {
                sideTris.Add(i);
                sideTris.Add((i + 1) % res + res);
                sideTris.Add(i + res);

                sideTris.Add(i);
                sideTris.Add((i + 1) % res);
                sideTris.Add((i + 1) % res + res);
            }
            var allVertLists = new List<Vector3>[] { topVerts, bottomVerts, sideVerts };
            var allTriLists = new List<int>[] { topTris, bottomTris, sideTris };
            Utility.MultiMesh(_mesh, allVertLists, allTriLists);
        }

    }
}
#endregion