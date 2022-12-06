using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Mesh))]
public class MeshMaker : MonoBehaviour
{

    public Vector3[] newVerticies;
    Vector2[] newUV;
    int[] newTriangles;
    Mesh mesh;
    public Material material;
    int currentVertex;
    int currentTriangleIndex;

    public bool showGizmos;

    public float xSpacing = 1;
    public float zSpacing = 1;
    public int nodeAmountX = 10;
    public int nodeAmountZ = 10;
    public float frequency;
    private float amplitude;

    public float[,] Coordinates;

    // Start is called before the first frame update
    void Start()
    {
        Coordinates = new float[nodeAmountX, nodeAmountX];
        mesh = new Mesh();
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<MeshCollider>();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        MakeShape();
        ApplyPerlinNoise();
        UpdateMesh();
        gameObject.GetComponent<MeshRenderer>().material = material;
        showGizmos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Coordinates = new float[nodeAmountX, nodeAmountX];
            MakeShape();
            ApplyPerlinNoise();
            UpdateMesh();
        }

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    showGizmos = !showGizmos;
        //}
    }

    void MakeShape()
    {
        newVerticies = new Vector3[(nodeAmountX * nodeAmountZ)];
        currentVertex = 0;
        for (int x = 0; x <  nodeAmountX; x++)
        {
            for (int y = 0; y < nodeAmountZ; y++)
            {
                //Debug.Log("Assigning position to vertex: " + currentVertex);
                newVerticies[currentVertex] = new Vector3(y * xSpacing, 0, x * zSpacing);
                currentVertex++;
            }
        }

        newTriangles = new int[((nodeAmountX * nodeAmountZ) * 6)];

        currentTriangleIndex = 0;

        for (int x = 0; x < newVerticies.Length; x++)
        {
            //Debug.Log("Making triangles from node: " + x);
            if ((x + 1) % nodeAmountX == 0) continue;
            if (((x - (nodeAmountX - 2)) % nodeAmountX) == 0 && x != 0) // check if vertex is second to last on the right edge of plane
            {
                //Debug.Log("Right" + x);
                if (x >= (nodeAmountX * (nodeAmountZ - 1))) // check if vertex is on bottom edge of plane
                {
                    //Debug.Log("Bottom Row");
                    continue;
                }
                else
                {
                    newTriangles[currentTriangleIndex] = (x);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX - 1);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;

                    newTriangles[currentTriangleIndex] = (x);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX + 1);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;

                    newTriangles[currentTriangleIndex] = (x);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX + 1);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + 1);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;

                }
            }
            else if (x % (nodeAmountX) == 0 && x != 1 || x == 0) // check if vertex is on left edge of plane
            {
                //Debug.Log("Left" + x);
                if (x >= (nodeAmountX * (nodeAmountZ - 1))) // check if vertex is on bot
                {
                    //Debug.Log("Bottom Row");
                    continue;
                }
                else
                {
                    newTriangles[currentTriangleIndex] = (x);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + 1);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                }
            }
            else
            {
                //Debug.Log("Center");
                if (x >= nodeAmountX * ((nodeAmountZ - 1))) // check if vertex is on bottom edge of plane
                {
                    //Debug.Log("Bottom Row");
                    continue;
                }
                else
                {
                    newTriangles[currentTriangleIndex] = (x);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX -1 );
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;

                    newTriangles[currentTriangleIndex] = (x);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + nodeAmountX);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                    newTriangles[currentTriangleIndex] = (x + 1);
                    //Debug.Log(newTriangles[currentTriangleIndex] + " " + currentTriangleIndex);
                    currentTriangleIndex++;
                }
            }

        }
        //UpdateMesh();
    }

    public void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVerticies;

        mesh.triangles = newTriangles;

        newUV = new Vector2[newVerticies.Length];
        for (int i = 0; i < newVerticies.Length; i++)
        {
            newUV[i] = new Vector2(newVerticies[i].x, newVerticies[i].z);
        }

        mesh.uv = newUV;
    }

    void LogTriangles()
    {
        for (int i = 0; i < newTriangles.Length; i++)
        {
            if (newTriangles[i] == 0) continue;
            //Debug.Log(newTriangles[i]);
        }
    }

    void ApplyPerlinNoise()
    {
        for (int i = 0; i < newVerticies.Length; i++)
        {
            amplitude = (1 / frequency);
            newVerticies[i] = new Vector3(newVerticies[i].x, PerlinNoise(Time.time * frequency, Random.Range(0.0f, 10.0f)) * amplitude, newVerticies[i].z);
            i++;
        }
    }

    float PerlinNoise(float x, float z)
    {
        return ((Mathf.PerlinNoise(x, z) * 2.0f) - 1f);
    }

    float PerlinSpectral(float x, float z, float amplitude, float frequency, int numberOfIterations)
    {
        float result = 0.0f;
        for (int i = 0; i < numberOfIterations; i++)
        {
            result += PerlinNoise(x * frequency, z * amplitude) * (frequency/numberOfIterations);
        }
        return result;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos == false) return;
        for (int i = 0; i < newVerticies.Length; i++)
        {
            if (i == 0)
            {
                Gizmos.color = Color.cyan;
            }
            else if (i == 1)
            {
                Gizmos.color = Color.magenta;
            }
            else if (i % 10 == 0)
            {
                Gizmos.color = Color.yellow;
            }
            else if (i % 5 == 0)
            {
                Gizmos.color = Color.blue;
            }
            else if (i % 2 == 0)
            {
                Gizmos.color = Color.red;
            }

            else
            {
                Gizmos.color = Color.green;
            }

            Gizmos.DrawSphere(newVerticies[i], 0.05f);
        }

    }
}
