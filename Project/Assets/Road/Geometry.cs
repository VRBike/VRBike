using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Geometry
{
	public readonly List<Vector3> vertices;
	public readonly List<Vector3> normals;
	public readonly List<Color> colors;
	public readonly List<Vector2> uvs;
	public readonly List<int> triangles;
	
	public Geometry()
	{
		vertices = new List<Vector3>();
		normals = new List<Vector3>();
		colors = new List<Color>();
		uvs = new List<Vector2>();
		triangles = new List<int>();
	}
	
	public Geometry(Mesh mesh)
	{
		#region Asserts
		#if DEBUG
		if (mesh == null)
		{
			throw new ArgumentNullException("mesh");
		}
		#endif
		#endregion
		
		vertices = new List<Vector3>(mesh.vertices);
		normals = new List<Vector3>(mesh.normals);
		colors = new List<Color>(mesh.colors);
		uvs = new List<Vector2>(mesh.uv);
		triangles = new List<int>(mesh.triangles);
	}
	
	public bool IsEmpty()
	{
		return vertices.Count == 0;
	}
	
	public void Clear()
	{
		vertices.Clear();
		normals.Clear();
		colors.Clear();
		uvs.Clear();
		triangles.Clear();
	}
	
	public static Mesh ConvertToSubmeshes(List<Geometry> geometries)
	{
		var vertices = new List<Vector3>();
		var uvs = new List<Vector2>();
		var normals = new List<Vector3>();
		var colors = new List<Color>();
		var tangents = new List<Vector4>();
		
		foreach (var geometry in geometries)
		{
			vertices.AddRange(geometry.vertices);
			uvs.AddRange(geometry.uvs);
			normals.AddRange(geometry.normals);
			colors.AddRange(geometry.colors);
			tangents.AddRange(geometry.CalculateTangents());
		}
		
		var mesh = new Mesh
		{
			vertices = vertices.ToArray(),
			uv = uvs.ToArray(),
			normals = normals.ToArray(),
			colors = colors.ToArray(),
			tangents = tangents.ToArray(),
			subMeshCount = geometries.Count,
		};
		
		int vertexShift = 0;
		for (int submeshIndex = 0; submeshIndex < geometries.Count; submeshIndex++)
		{
			var geometry = geometries[submeshIndex];
			
			var triangles = geometry.triangles.Select(t => t + vertexShift).ToArray();
			mesh.SetTriangles(triangles, submeshIndex);
			
			vertexShift += geometry.vertices.Count;
		}
		
		return mesh;
	}
	
	public Mesh ConvertToMesh()
	{
		if (triangles.Count % 3 != 0)
		{
			Debug.Log(string.Format("Vertices: {0}, Tris: {1}, Colors: {2}, Normals: {3}, Uvs: {4}", vertices.Count, triangles.Count, colors.Count, normals.Count, uvs.Count));
		}
		
		var mesh = new Mesh
		{
			vertices = vertices.ToArray(),
			triangles = triangles.ToArray(),
		};
		
		if (uvs != null)
		{
			mesh.normals = normals.ToArray();
			mesh.uv = uvs.ToArray();
			mesh.colors = colors.ToArray();
			if (uvs.Count == vertices.Count)
			{
				mesh.tangents = CalculateTangents();
			}
		}
		
		return mesh;
	}
	
	public int AppendGeometry(Vector3 position, Geometry geometry)
	{
		#region Asserts
		#if DEBUG
		if (geometry == null)
		{
			throw new ArgumentNullException("geometry");
		}
		#endif
		#endregion
		
		return AppendGeometry(position, geometry.vertices, geometry.normals, geometry.uvs, geometry.colors, geometry.triangles);
	}
	
	#region Private
	
	private Vector4[] CalculateTangents()
	{
		int triangleCount = triangles.Count;
		int vertexCount = vertices.Count;
		
		var tan1 = new Vector3[vertexCount];
		var tan2 = new Vector3[vertexCount];
		
		var tangents = new Vector4[vertexCount];
		
		for (int a = 0; a < triangleCount; a += 3)
		{
			int i1 = triangles[a + 0];
			int i2 = triangles[a + 1];
			int i3 = triangles[a + 2];
			
			var v1 = vertices[i1];
			var v2 = vertices[i2];
			var v3 = vertices[i3];
			
			var w1 = uvs[i1];
			var w2 = uvs[i2];
			var w3 = uvs[i3];
			
			float x1 = v2.x - v1.x;
			float x2 = v3.x - v1.x;
			float y1 = v2.y - v1.y;
			float y2 = v3.y - v1.y;
			float z1 = v2.z - v1.z;
			float z2 = v3.z - v1.z;
			
			float s1 = w2.x - w1.x;
			float s2 = w3.x - w1.x;
			float t1 = w2.y - w1.y;
			float t2 = w3.y - w1.y;
			
			float r = 1.0f / (s1 * t2 - s2 * t1);
			
			var sdir = new Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
			var tdir = new Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);
			
			tan1[i1] += sdir;
			tan1[i2] += sdir;
			tan1[i3] += sdir;
			
			tan2[i1] += tdir;
			tan2[i2] += tdir;
			tan2[i3] += tdir;
		}
		
		for (int a = 0; a < vertexCount; ++a)
		{
			var n = normals[a];
			var t = tan1[a];
			
			var tmp = (t - n * Vector3.Dot(n, t)).normalized;
			tangents[a] = new Vector4(tmp.x, tmp.y, tmp.z);
			tangents[a].w = Vector3.Dot(Vector3.Cross(n, t), tan2[a]) < 0.0f ? -1.0f : 1.0f;
		}
		
		return tangents;
	}
	
	private int AppendGeometry(Vector3 position, IEnumerable<Vector3> vertices, IEnumerable<Vector3> normals, IEnumerable<Vector2> uvs, IEnumerable<Color> colors, IEnumerable<int> triangles)
	{
		var v = this.vertices.Count;
		
		foreach (var vertex in vertices)
		{
			this.vertices.Add(position + vertex);
		}
		
		this.colors.AddRange(colors);
		this.normals.AddRange(normals);
		this.uvs.AddRange(uvs);
		
		foreach (var triangle in triangles)
		{
			this.triangles.Add(v + triangle);
		}
		
		return v;
	}
	
	#endregion
}