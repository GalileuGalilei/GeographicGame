using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int resolution; //resolution of each "face" of the cube sphere
    public float radius = 1.0f;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    private Color32[] cubeUV;

    private void Awake()
    {
        Generate();
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Sphere";
        CreateVertices();
        CreateTriangles();
        CreateColliders();
        RecalculateUVs();
    }

    private void CreateColliders()
    {
        gameObject.AddComponent<SphereCollider>();
    }

    private void SetVertex(int i, int x, int y, int z)
    {
        Vector3 v = new Vector3(x, y, z) * 2f / resolution - Vector3.one;
        float x2 = v.x * v.x;
        float y2 = v.y * v.y;
        float z2 = v.z * v.z;
        Vector3 s;
        s.x = v.x * Mathf.Sqrt(1f - y2 / 2f - z2 / 2f + y2 * z2 / 3f);
        s.y = v.y * Mathf.Sqrt(1f - x2 / 2f - z2 / 2f + x2 * z2 / 3f);
        s.z = v.z * Mathf.Sqrt(1f - x2 / 2f - y2 / 2f + x2 * y2 / 3f);
        normals[i] = s;
        vertices[i] = normals[i] * radius;
        cubeUV[i] = new Color32((byte)x, (byte)y, (byte)z, 0);
    }

    private void CreateVertices()
    {
        int cornerVertices = 8;
        int edgeVertices = (resolution + resolution + resolution - 3) * 4;
        int faceVertices = (
            (resolution - 1) * (resolution - 1) +
            (resolution - 1) * (resolution - 1) +
            (resolution - 1) * (resolution - 1)) * 2;

        vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
        normals = new Vector3[cornerVertices + edgeVertices + faceVertices];
        cubeUV = new Color32[cornerVertices + edgeVertices + faceVertices];

        int v = 0;

        for (int y = 0; y <= resolution; y++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                SetVertex(v++, x, y, 0);
            }
            for (int z = 1; z <= resolution; z++)
            {
                SetVertex(v++, resolution, y, z);
            }
            for (int x = resolution - 1; x >= 0; x--)
            {
                SetVertex(v++, x, y, resolution);
            }
            for (int z = resolution - 1; z > 0; z--)
            {
                SetVertex(v++, 0, y, z);
            }
        }

        for (int z = 1; z < resolution; z++)
        {
            for (int x = 1; x < resolution; x++)
            {
                SetVertex(v++, x, resolution, z);
            }
        }

        for (int z = 1; z < resolution; z++)
        {
            for (int x = 1; x < resolution; x++)
            {
                SetVertex(v++, x, 0, z);
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.colors32 = cubeUV;
    }

    private void CreateTriangles()
    {
        int quads = resolution * resolution * 6;
        int[] triangles = new int[quads * 6];
        int ring = resolution * 4;
        int t = 0, v = 0;

        for (int y = 0; y < resolution; y++, v++)
        {
            for (int q = 0; q < ring - 1; q++, v++)
            {
                t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
            }
            t = SetQuad(triangles, t, v, v - ring + 1, v + ring, v + 1);
        }


        t = CreateTopFace(triangles, t, ring);
        t = CreateBottomFace(triangles, t, ring);

        mesh.triangles = triangles;
    }

    private int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
    {
        triangles[i] = v00;
        triangles[i + 1] = triangles[i + 4] = v01;
        triangles[i + 2] = triangles[i + 3] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }

    private int CreateTopFace(int[] triangles, int t, int ring)
    {
        int v = ring * resolution;
        for (int x = 0; x < resolution - 1; x++, v++)
        {
            t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + ring);
        }

        t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + 2);

        int vMin = ring * (resolution + 1) - 1;
        int vMid = vMin + 1;
        int vMax = v + 2;

        for (int z = 1; z < resolution - 1; z++, vMin--, vMid++, vMax++)
        {
            t = SetQuad(triangles, t, vMin, vMid, vMin - 1, vMid + resolution - 1);
            for (int x = 1; x < resolution - 1; x++, vMid++)
            {
                t = SetQuad(
                    triangles, t,
                    vMid, vMid + 1, vMid + resolution - 1, vMid + resolution);
            }
            t = SetQuad(triangles, t, vMid, vMax, vMid + resolution - 1, vMax + 1);
        }

        int vTop = vMin - 2;
        t = SetQuad(triangles, t, vMin, vMid, vTop + 1, vTop);

        for (int x = 1; x < resolution - 1; x++, vTop--, vMid++)
        {
            t = SetQuad(triangles, t, vMid, vMid + 1, vTop, vTop - 1);
        }

        t = SetQuad(triangles, t, vMid, vTop - 2, vTop, vTop - 1);

        return t;
    }


    private int CreateBottomFace(int[] triangles, int t, int ring)
    {
        int v = 1;
        int vMid = vertices.Length - (resolution - 1) * (resolution - 1);
        t = SetQuad(triangles, t, ring - 1, vMid, 0, 1);
        for (int x = 1; x < resolution - 1; x++, v++, vMid++)
        {
            t = SetQuad(triangles, t, vMid, vMid + 1, v, v + 1);
        }
        t = SetQuad(triangles, t, vMid, v + 2, v, v + 1);

        int vMin = ring - 2;
        vMid -= resolution - 2;
        int vMax = v + 2;

        for (int z = 1; z < resolution - 1; z++, vMin--, vMid++, vMax++)
        {
            t = SetQuad(triangles, t, vMin, vMid + resolution - 1, vMin + 1, vMid);
            for (int x = 1; x < resolution - 1; x++, vMid++)
            {
                t = SetQuad(
                    triangles, t,
                    vMid + resolution - 1, vMid + resolution, vMid, vMid + 1);
            }
            t = SetQuad(triangles, t, vMid + resolution - 1, vMax + 1, vMid, vMax);
        }

        int vTop = vMin - 1;
        t = SetQuad(triangles, t, vTop + 1, vTop, vTop + 2, vMid);
        for (int x = 1; x < resolution - 1; x++, vTop--, vMid++)
        {
            t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vMid + 1);
        }
        t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vTop - 2);

        return t;
    }
    
    private void RecalculateUVs()
    {
        //calculate normalized uv map following the traingles order

        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i] = PointToCoordinate(vertices[i]);
        }
        
        mesh.uv = uvs;
    }

    private Vector2 PointToCoordinate(Vector3 point)
    {
        point = (point - transform.position).normalized;

        Vector2 coord = new Vector2(
            (Mathf.Atan2(point.x, point.z) / (2f * Mathf.PI)) + 0.5f,
            point.y * 0.5f + 0.5f
        );

        return coord;
    }

}
