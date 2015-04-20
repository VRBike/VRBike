using System;
using System.Collections.Generic;
using UnityEngine;

public class Spline3D
{
	private readonly IList<Vector3> points;
	
	public Spline3D(IList<Vector3> controlPoints)
	{
		#region Asserts
		#if DEBUG
		if (controlPoints == null)
		{
			throw new ArgumentNullException("controlPoints");
		}
		if (controlPoints.Count < 2)
		{
			throw new ArgumentException("Too few control points");
		}
		#endif
		#endregion
		
		points = new List<Vector3>(controlPoints);
	}
	
	public int Length
	{
		get { return points.Count; }
	}
	
	public void RemoveAt(int index)
	{
		#region Asserts
		#if DEBUG
		if (index < 0 || index >= points.Count)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		#endif
		#endregion
		
		points.RemoveAt(index);
	}
	
	public Vector3 this[int index]
	{
		get
		{
			#region Asserts
			#if DEBUG
			if (index < 0 || index >= points.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			#endif
			#endregion
			
			return points[index];
		}
		set
		{
			#region Asserts
			#if DEBUG
			if (index < 0 || index >= points.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			#endif
			#endregion
			
			points[index] = value;
		}
	}
	public Vector3 GetValue(float x)
	{
		if (x <= 0)
		{
			return points[0] - (0 - x) * GetDerivative(0);
		}
		if (x >= points.Count - 1)
		{
			return points[points.Count - 1] + (x - (points.Count - 1)) * GetDerivative(points.Count - 1);
		}
		
		var k = (int)x;
		var t = x - k;
		
		var d0 = GetDerivative(k);
		var d1 = GetDerivative(k + 1);
		var f0 = points[k];
		var f1 = points[k + 1];
		
		var a = 2 * (f0 - f1) + d0 + d1;
		var b = 3 * (f1 - f0) - 2 * d0 - d1;
		var c = d0;
		var d = f0;
		
		return a * t * t * t + b * t * t + c * t + d;
	}
	
	private Vector3 GetDerivative(int k)
	{
		if (k <= 0)
		{
			return points[1] - points[0];
		}
		
		if (k >= points.Count - 1)
		{
			return points[points.Count - 1] - points[points.Count - 2];
		}
		
		return (points[k + 1] - points[k - 1]) * .5f;
	}
}