using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Mesh))]
public class MeshMaker : MonoBehaviour
{

    Vector3[] newVerticies;
    //Vector2[] newUV;
    int[] newTriangles;
    Mesh mesh;
    int currentVertex;
    int currentTriangleIndex;

    public float xSpacing = 1;
    public float zSpacing = 1;
    public int nodeAmountX = 10;
    public int nodeAmountZ = 10;

    public float[,] Coordinates;

    // Start is called before the first frame update
    void Start()
    {
        Coordinates = new float[nodeAmountX, nodeAmountX];
        mesh = new Mesh();
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        MakeShape();
        UpdateMesh();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeShape()
    {
        newVerticies = new Vector3[(nodeAmountX * nodeAmountZ)];
        currentVertex = 0;
        for (int x = 0; x <  nodeAmountX; x++)
        {
            for (int y = 0; y < nodeAmountZ; y++)
            {
                Debug.Log("Assigning position to vertex: " + currentVertex);
                newVerticies[currentVertex] = new Vector3(y * xSpacing, 0, x * zSpacing);
                currentVertex++;
            }
        }

        //newTriangles = new int[] 
        //{ 
        //    0, 1, 2,
        //    1, 10, 9,
        //    1, 2, 3,
        //    2, 11, 10,
        //    2, 3, 4,

        //};


        newTriangles = new int[((nodeAmountX * nodeAmountZ) * 3)];

        currentTriangleIndex = 0;

        for (int x = 0; x < newVerticies.Length; x++)
        {
            Debug.Log("Making triangles from node: " + x);
            if (x % nodeAmountX == 0 || x != 0) // check if vertex is on right edge of plane
            {
                if (x > nodeAmountX * (nodeAmountZ - 1)) // check if vertex is on bottom edge of plane
                {
                    Debug.Log("Bottom Row");
                    continue;
                }
                else
                {
                    newTriangles[currentTriangleIndex] = (x);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX - 1);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX - 2);
                    currentTriangleIndex++;
                }
            }
            else if (x % nodeAmountX == 1) // check if vertex is on left edge of plane
            {
                if (x > nodeAmountX * (nodeAmountZ - 1)) // check if vertex is on bot
                {
                    Debug.Log("Bottom Row");
                    continue;
                }
                else
                {
                    newTriangles[currentTriangleIndex] = (x);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + 1);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX -1);
                    currentTriangleIndex++;
                }
            }
            else
            {
                if (x > nodeAmountX * (nodeAmountZ - 1)) // check if vertex is on bottom edge of plane
                {
                    Debug.Log("Bottom Row");
                    continue;
                }
                else
                {
                    newTriangles[currentTriangleIndex] = (x);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX -1);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX - 2);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + 1);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX -1);
                    currentTriangleIndex++;
                }
            }

        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVerticies;
        //mesh.uv = newUV;
        mesh.triangles = newTriangles;
    }
}
