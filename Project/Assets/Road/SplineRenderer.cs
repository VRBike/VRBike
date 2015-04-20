using System;
using System.Collections.Generic;
using UnityEngine;

public class SplineRenderer : MonoBehaviour
{
	public Material material;
	
	private IDictionary<object, IList<Line>> data;
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private bool isChanged;
	
	public static SplineRenderer FindInstance()
	{
		return (SplineRenderer)FindObjectOfType(typeof(SplineRenderer));
	}
	
	private void Awake()
	{
		data = new Dictionary<object, IList<Line>>();
		meshFilter = gameObject.AddComponent<MeshFilter>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.sharedMaterial = material;
	}
	
	private void Update()
	{
		if (isChanged)
		{
			isChanged = false;
			RegenerateMesh();
		}
	}
	
	public void AddLine(object key, Line line)
	{
		#region Asserts
		#if DEBUG
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		if (line == null)
		{
			throw new ArgumentNullException("line");
		}
		#endif
		#endregion
		
		if (data.ContainsKey(key) == false)
		{
			data.Add(key, new List<Line>());
		}
		data[key].Add(line);
		
		isChanged = true;
	}
	
	public void RemoveLines(object key)
	{
		#region Asserts
		#if DEBUG
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		#endif
		#endregion
		
		if (data.Remove(key))
		{
			isChanged = true;
		}
	}
	
	private IEnumerable<Line> GetLines()
	{
		foreach (var lines in data.Values)
		{
			foreach (var line in lines)
			{
				yield return line;
			}
		}
	}
	
	private void RegenerateMesh()
	{
		if (meshFilter.sharedMesh != null)
		{
			Destroy(meshFilter.sharedMesh);
		}
		
		var geometry = new Geometry();
		foreach (var line in GetLines())
		{
			AppendLine(geometry, line);
		}
		
		meshFilter.sharedMesh = geometry.ConvertToMesh();
	}
	
	private void AppendLine(Geometry geometry, Line line)
	{
		var spline = new Spline3D(line.points);
		
		var forward = (spline[1] - spline[0]).normalized;
		var left = Vector3.Cross(forward, Vector3.up);
		var point = spline.GetValue(0);
		
		geometry.vertices.Add(point + left * line.width / 2);
		geometry.vertices.Add(point - left * line.width / 2);
		geometry.normals.Add(Vector3.up);
		geometry.normals.Add(Vector3.up);
		geometry.uvs.Add(new Vector2(0, 0));
		geometry.uvs.Add(new Vector2(1, 0));
		geometry.colors.Add(line.color);
		geometry.colors.Add(line.color);
		
		var previousPoint = point;
		
		for (int i = 1; i < line.segments * spline.Length; i++)
		{
			var t = i / (float)line.segments;
			point = spline.GetValue(t);
			
			forward = (point - previousPoint).normalized;
			previousPoint = point;
			
			left = Vector3.Cross(forward, Vector3.up);
			
			var vertexCount = geometry.vertices.Count;
			
			geometry.vertices.Add(point + left * line.width / 2);
			geometry.vertices.Add(point - left * line.width / 2);
			geometry.normals.Add(Vector3.up);
			geometry.normals.Add(Vector3.up);
			geometry.uvs.Add(new Vector2(0, t));
			geometry.uvs.Add(new Vector2(1, t));
			geometry.colors.Add(line.color);
			geometry.colors.Add(line.color);
			
			geometry.triangles.Add(vertexCount + 0);
			geometry.triangles.Add(vertexCount + 1);
			geometry.triangles.Add(vertexCount - 2);
			geometry.triangles.Add(vertexCount + 1);
			geometry.triangles.Add(vertexCount - 1);
			geometry.triangles.Add(vertexCount - 2);
		}
	}
}