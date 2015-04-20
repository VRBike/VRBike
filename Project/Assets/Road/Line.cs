using System;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
	public readonly IList<Vector3> points;
	public readonly Color color;
	public readonly float width;
	public readonly int segments;
	
	public Line(IList<Vector3> points, Color color, float width)
		: this(points, color, width, 1)
	{
	}
	
	public Line(IList<Vector3> points, Color color, float width, int segments)
	{
		#region Asserts
		#if DEBUG
		if (points == null)
		{
			throw new ArgumentNullException("points");
		}
		if (points.Count < 2)
		{
			throw new ArgumentException("Line consists of less than 2 points");
		}
		if (width <= 0f)
		{
			throw new ArgumentException("Width must be positive");
		}
		if (segments <= 0)
		{
			throw new ArgumentException("Segments must be positive");
		}
		#endif
		#endregion
		
		this.points = points;
		this.color = color;
		this.width = width;
		this.segments = segments;
	}
}