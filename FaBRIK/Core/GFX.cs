#region Graphic
using UnityEngine;

namespace MonkeyWrench
{

    public class GFX
    {
        public Mesh mesh;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public Color color;
        public ShaderType shaderType;

        public GFX(Mesh _mesh, Vector3 _position, Quaternion _rotation, Vector3 _scale, Color _color, ShaderType _shaderType)
        {
            this.mesh = _mesh;
            this.position = _position;
            this.rotation = _rotation;
            this.scale = _scale;
            this.color = _color;
            this.shaderType = _shaderType;
        }
    }
}
#endregion