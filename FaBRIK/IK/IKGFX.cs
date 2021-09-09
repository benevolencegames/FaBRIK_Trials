#region FaBRIK Graphics Generator
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class IKGFX : MonoBehaviour
{

	public Transform[] pointsT;
	public Transform target;
	public float radius;
	public float lineThickness;
	Solution solution;

	void Start()
	{
		solution = new Solution();
	}

	void Update()
	{
		Vector3[] points = pointsT.Select((p) => p.position).ToArray();

		if (Application.isPlaying)
		{
			solution.Execute(points, target.position);
		}

		for (int i = 0; i < points.Length; i++)
		{
			GFXGenerator.SetColor(Color.white);
			GFXGenerator.DrawSphere(points[i], radius);
			if (i < points.Length - 1)
			{
				GFXGenerator.SetColor(Color.white);
				GFXGenerator.DrawLine(points[i], points[i + 1], lineThickness);
			}
		}

		GFXGenerator.SetColor(Color.red);
		GFXGenerator.DrawSphere(target.position + Vector3.forward * 2, radius * 1.5f);

		for (int i = 0; i < pointsT.Length; i++)
		{
			pointsT[i].position = points[i];
		}
	}
}
#endregion