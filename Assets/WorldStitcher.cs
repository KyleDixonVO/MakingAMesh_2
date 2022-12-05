using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStitcher : MonoBehaviour
{
    public GameObject makerParent;
    public int meshesPerRow;
    public int numberOfRows;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        int j = 0;
            for (int k = 0; k < numberOfRows; k++)
            {
                for (int l = 0; l < meshesPerRow; l++)
                {
                    makerParent.transform.GetChild(j).gameObject.GetComponent<MeshMaker>().transform.position = new Vector3(l * makerParent.transform.GetChild(j).gameObject.GetComponent<MeshMaker>().xSpacing * (makerParent.transform.GetChild(j).gameObject.GetComponent<MeshMaker>().nodeAmountX - 1), 0, k * makerParent.transform.GetChild(j).gameObject.GetComponent<MeshMaker>().zSpacing * (makerParent.transform.GetChild(j).gameObject.GetComponent<MeshMaker>().nodeAmountZ - 1));
                    j++;
                }
            }
        StitchWorld();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            makerParent.transform.GetChild(0).gameObject.transform.position = new Vector3(7, 7, 7);
        }
    }

    void StitchWorld()
    {
        for (int i = 0; i < makerParent.transform.childCount; i++)
        {
            Debug.Log("mesh: " + i);
            Debug.Log(i + "number of verticies: " + (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX) * (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountZ));
            for (int x = 0; x < 99; x++)
            { 
                if (((x - (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX - 1)) % makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX) == 0 && x != 0) // check if vertex is on the right edge of plane
                {
                    Debug.Log("i: " + i + " x: " + x);
                    if (i == 0 || ((i  + 1) % (meshesPerRow)) == 0) continue;
                    makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x] = new Vector3(makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x].x, makerParent.transform.GetChild(i + 1).gameObject.GetComponent<MeshMaker>().newVerticies[x - (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX -1)].y, makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x].z);

                }
                else if (x <= makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX && x != 0) // check if vertex is on top edge of plane
                {
                    Debug.Log("i: " + i + " x: " + x);
                    if (i == 0 || i < meshesPerRow) continue;
                    makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x] = new Vector3(makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x].x, makerParent.transform.GetChild(i + meshesPerRow).gameObject.GetComponent<MeshMaker>().newVerticies[x - (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX  * (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountZ - 1))].y, makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x].z);
                    
                }
                else if (x % (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX) == 0 && x != 1 || x == 0) // check if vertex is on left edge of plane
                {
                    Debug.Log("i: " + i + " x: " + x);
                    if (i == 0 || i % meshesPerRow == 0) continue;
                    Debug.Log("Targeted vertex: " + x + (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX - 1));
                    //Debug.Log(meshMakers[i - 1].newVerticies[x + (meshMakers[i].nodeAmountX - 1)].y);
                    Debug.Log("Length: " + makerParent.transform.GetChild(0).gameObject.GetComponent<MeshMaker>().newVerticies.Length);
                    Debug.Log(makerParent.transform.GetChild(i -1).gameObject.GetComponent<MeshMaker>().newVerticies[x + makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX - 1].y);
                    makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x] = new Vector3(makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x].x, makerParent.transform.GetChild(i - 1).gameObject.GetComponent<MeshMaker>().newVerticies[x + (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX - 1)].y, makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x].z);
                }
                else if (x >= makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX * ((makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountZ - 1))) // check if vertex is on bottom edge of plane
                {

                    Debug.Log("i: " + i + " x: " + x);
                    if (i == 0 || i >= (meshesPerRow * (numberOfRows - 1))) continue;
                    makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x] = new Vector3(makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x].x, makerParent.transform.GetChild(i - meshesPerRow).gameObject.GetComponent<MeshMaker>().newVerticies[x + (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountX * (makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().nodeAmountZ - 1))].y, makerParent.transform.GetChild(i).gameObject.GetComponent<MeshMaker>().newVerticies[x].z);

                }
                else
                {
                    Debug.Log("not an edge");
                }
            }

        }


    }
}
