#region System Management
using System.Collections.Generic;
using UnityEngine;
using MonkeyWrench.MeshGenerator;

namespace MonkeyWrench
{

    public enum ShaderType { Diffuse, Unlit, UnlitAlpha }

    public static class SystemManagement
    {
        #region Variables

        static readonly string[] shaderPaths = {
            "GFX/Diffuse",
            "GFX/Unlit",
            "GFX/UnlitColorAlpha"
        };

        static Material[] materials;
        static MaterialPropertyBlock materialProperties;

        public static Mesh sphereMesh;
        public static Mesh cylinderMesh;

        static Queue<Mesh> inactiveMeshes;
        static List<GFX> drawList;

        static int lastFrameInputReceived;

        #endregion  

        #region System Initilization
        static SystemManagement()
        {
            Camera.onPreCull -= Draw;
            Camera.onPreCull += Draw;

            Initialization();
        }

        static void Initialization()
        {
            if (sphereMesh == null)
            {
                inactiveMeshes = new Queue<Mesh>();
                materialProperties = new MaterialPropertyBlock();
                drawList = new List<GFX>();

                sphereMesh = new Mesh();
                cylinderMesh = new Mesh();
                Sphere.CreateMesh(sphereMesh);
                Cylinder.GenerateMesh(cylinderMesh);

                materials = new Material[shaderPaths.Length];
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = new Material(Shader.Find(shaderPaths[i]));
                }
            }

            if (lastFrameInputReceived != Time.frameCount)
            {
                lastFrameInputReceived = Time.frameCount;

                var usedMeshes = new HashSet<Mesh>();

                usedMeshes.Add(sphereMesh);
                usedMeshes.Add(cylinderMesh);

                for (int i = 0; i < drawList.Count; i++)
                {
                    if (!usedMeshes.Contains(drawList[i].mesh))
                    {
                        usedMeshes.Add(drawList[i].mesh);
                        inactiveMeshes.Enqueue(drawList[i].mesh);
                    }
                }

                drawList.Clear();
            }
        }

        #endregion

        #region Draw

        public static void MakeGFX(Mesh _mesh, Vector3 _position, Quaternion _rotation, Vector3 _scale)
        {
            Initialization();
            drawList.Add(new GFX(_mesh, _position, _rotation, _scale, GFXGenerator.activeColor, GFXGenerator.activeStyle));
        }

        static void Draw(Camera _camera)
        {
            if (_camera && Time.frameCount == lastFrameInputReceived)
            {
                for (int i = 0; i < drawList.Count; i++)
                {
                    GFX drawData = drawList[i];
                    Matrix4x4 matrix = Matrix4x4.TRS(drawData.position, drawData.rotation, drawData.scale);

                    materialProperties.SetColor("_Color", drawData.color);
                    Material activeMaterial = materials[(int)drawData.shaderType];
                    Graphics.DrawMesh(drawData.mesh, matrix, activeMaterial, 0, _camera, 0, materialProperties);
                }
            }
        }
    }
}
#endregion
#endregion