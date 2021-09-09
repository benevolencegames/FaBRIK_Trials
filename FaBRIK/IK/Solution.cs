#region FaBRIK Solver
using UnityEngine;
using System;

public class Solution
{

	const int timesToIterate = 150;
	const float acceptableDistance = 0.01f;

	public void Execute(Vector3[] _points, Vector3 _target)
	{
		Vector3 start = _points[0];
		float[] stickLengths = new float[_points.Length - 1];
		for (int i = 0; i < _points.Length - 1; i++)
		{
			stickLengths[i] = (_points[i + 1] - _points[i]).magnitude;
		}

		for (int iteration = 0; iteration < timesToIterate; iteration++)
		{
			bool workingBackwards = iteration % 2 == 0;

			Array.Reverse(_points);
			Array.Reverse(stickLengths);
			_points[0] = (workingBackwards) ? _target : start;

			for (int i = 1; i < _points.Length; i++)
			{
				Vector3 dir = (_points[i] - _points[i - 1]).normalized;
				_points[i] = _points[i - 1] + dir * stickLengths[i - 1];
			}

			var distanceFromTarget = (_points[_points.Length - 1] - _target).magnitude;
			if (!workingBackwards && distanceFromTarget <= acceptableDistance)
			{
				return;
			}
		}
	}
}
#endregion