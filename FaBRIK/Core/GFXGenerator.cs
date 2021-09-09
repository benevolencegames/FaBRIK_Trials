#region Graphics Generation
using UnityEngine;
using MonkeyWrench;
using static MonkeyWrench.SystemManagement;

public static class GFXGenerator
{
	public static Color activeColor = Color.black;
	public static ShaderType activeStyle = ShaderType.Unlit;

	const float lineThicknessFactor = 1 / 30f;
	public static void DrawLine(Vector3 _start, Vector3 _end, float _thickness)
	{
		Vector3 center = (_start + _end) / 2;
		var rot = Quaternion.FromToRotation(Vector3.up, (_start - _end).normalized);
		Vector3 scale = new Vector3(_thickness * lineThicknessFactor, (_start - _end).magnitude, _thickness * lineThicknessFactor);
		MakeGFX(cylinderMesh, center, rot, scale);
	}
	public static void DrawSphere(Vector3 _center, float _radius)
	{
		MakeGFX(sphereMesh, _center, Quaternion.identity, Vector3.one * _radius);
	}
	public static void SetColor(Color _color)
	{
		activeColor = _color;
	}
}
#endregion