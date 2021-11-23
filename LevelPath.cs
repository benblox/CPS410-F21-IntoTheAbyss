using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
[ExecuteAlways]
public class LevelPath : MonoBehaviour
{

    // The points defined in editor that create a 3d path
    public int segments;
    public int length;
    public int obstacesPerSegment;
    public GameObject pf;

    [SerializeField]
    private Transform[] pathPoints;
    [SerializeField]
    public Mesh2D shape2D;
    [SerializeField]
    List<int> triIndices = new List<int>();
    [SerializeField]
    List<Vector2> uvs0 = new List<Vector2>();
    [SerializeField]
    Vector3[] tubePoints;
    public Transform[] PathPoints { get { return pathPoints; } }

    Mesh mesh;

    private void Awake()
    {

    }

    private void OnDrawGizmos()
    {

    }

    void extrudeTube()
    {
        mesh = new Mesh();
        mesh.name = "Tube";
        //The amount of faces basially, 2 triangles per face
        int runs = (pathPoints.Length - 1 - 1) * shape2D.vertices.Length;


        for (int i = 0; i < runs; i += 2)
        {

            // First Triangle of a face
            triIndices.Add(i);
            triIndices.Add(i + shape2D.vertices.Length);
            if (i % shape2D.vertices.Length == 0)
                triIndices.Add(i + shape2D.vertices.Length * 2 - 1);
            else
                triIndices.Add(i + shape2D.vertices.Length - 1);

            // Second Triangle of a face
            triIndices.Add(i);
            if (i % shape2D.vertices.Length == 0 )
            {
                triIndices.Add(i + shape2D.vertices.Length * 2 - 1);
                triIndices.Add(i + shape2D.vertices.Length - 1);
            }
            else
            {
                triIndices.Add(i + shape2D.vertices.Length - 1);
                triIndices.Add(i - 1);
            }
                
        }

        mesh.SetVertices(tubePoints);
        mesh.SetTriangles(triIndices, 0);
        mesh.SetUVs(0, uvs0);
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void GenerateLevel()
    {
        int x = Random.Range(-length/2, length/2);
        int y = Random.Range(-length/2, length/2);
        int z = length;
        //int rot = 0;


        GameObject gs = new GameObject("Start");
        gs.transform.position.Set(0f, 0f, 0f);
        gs.transform.parent = this.transform;

        for (int i = 0; i < segments; i++)
        {
            GameObject go = new GameObject("Path" + i);
            go.transform.position = new Vector3(x, y, z+=length);
            go.transform.parent = this.transform;
            //go.transform.Rotate(0, 0, rot += 15);
            x += Random.Range(-length/2, length/2);
            y += Random.Range(-length/2, length/2);
        }

        pathPoints = GetComponentsInChildren<Transform>();

        for (int i = 0; i < pathPoints.Length-1; i++)
            pathPoints[i].LookAt(pathPoints[i + 1]);

        GenTubePoints();
        extrudeTube();
        CreateObstacles();


    }

    public void DestroyLevel()
    {
        List<Transform> ac = new List<Transform>();

        foreach (Transform p in transform)
        {
            ac.Add(p);
        }
        foreach (Transform child in ac)
        {
            DestroyImmediate(child.gameObject);
        }
        if (mesh)
        {
            mesh.Clear();
        }
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var item in obs)
        {
            DestroyImmediate(item);
        }
        //mc.sharedMesh = null;
        //Array.Clear(tubePoints, 0, tubePoints.Length);
        triIndices.Clear();
        uvs0.Clear();
        //Array.Clear(pathPoints, 0, pathPoints.Length);
        tubePoints = new Vector3[0];
        pathPoints = new Transform[0];
    }


    private void GenTubePoints()
    {
        uvs0.Clear();
        tubePoints = new Vector3[(pathPoints.Length - 1) * shape2D.vertices.Length];
        int iamdumb = 0;

        for (int i = 1; i < pathPoints.Length; i++) // 1 .. 3
        {
            for (int j = 0; j < shape2D.vertices.Length; j++) // 0 .. 7
            {
                tubePoints[iamdumb] = (Vector3)shape2D.vertices[j].point + pathPoints[i].position;
                uvs0.Add(new Vector2(shape2D.vertices[j].u, .001F));
                iamdumb++;
            }

        }
    }

    private void CreateObstacles()
    {
        Quaternion rot;
        Vector3 offset;


        for (int i = 1; i < pathPoints.Length-1; i++)
        {
            for (int j = 1; j < obstacesPerSegment+1; j++)
            {
                int dir = Random.Range(0, 2);
                switch (dir)
                {
                    case 0:
                        rot = Quaternion.Euler(0, 0, 90);
                        offset = new Vector3(0f, Random.Range(-25f, 26f), 0f);
                        break;
                    case 1:
                        rot = Quaternion.identity;
                        offset = new Vector3(Random.Range(-25f, 26f), 0f, 0f);
                        break;
                    default:
                        rot = Quaternion.identity;
                        offset = new Vector3(0, 0, 0);
                        break;
                }
                var newPos = (pathPoints[i + 1].position - pathPoints[i].position) * (1 / ((float)obstacesPerSegment + 1) * j) + pathPoints[i].position;
                Instantiate(pf, newPos + offset, rot);
            }

        }
    }


}
