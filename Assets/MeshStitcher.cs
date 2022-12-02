using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshStitcher : MonoBehaviour
{
    public MakerParent makerParent;
    public static MeshStitcher stitcher;
    // Start is called before the first frame update
    void Start()
    {
        stitcher = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StitchCorners()
    {
        for (int mesh = 0; mesh < makerParent.meshMakers.Length; mesh++)
        {
            Debug.Log("Inside for loop mesh");

            for (int x = 0; x < makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies.Length; x++)
            {
                float BR = 0f;
                float BL = 0f;
                float TR = 0f;
                float TL = 0f;
                if (x == 0) //bottom left
                {
                    if (mesh < makerParent.meshesPerRow && mesh % (makerParent.meshesPerRow) == 0) continue; // mesh is bottom left corner
                    else if (mesh < makerParent.meshesPerRow) //mesh is on bottom edge of array
                    {
                        BL += makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y;
                        BL += makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().newVerticies[x + (makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().nodeAmountX - 1)].y;
                        BL = BL / 2;
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y = BL;
                        makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().newVerticies[x + (makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().nodeAmountX - 1)].y = BL;

                    }
                    else if (mesh % (makerParent.meshesPerRow) == 0) // mesh on left edge of array
                    {
                        BL += makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y;
                        BL += makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().newVerticies[x + (makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountZ - 1))].y;
                        BL = BL / 2;
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y = BL;
                        makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().newVerticies[x + (makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountZ - 1))].y = BL;
                    }
                    else
                    {
                        BL += makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y;
                        BL += makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().newVerticies[x + (makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX - 1))].y;
                        BL += makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().newVerticies[x + (makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().nodeAmountX - 1)].y;
                        BL += BL += makerParent.meshMakers[mesh - (makerParent.meshesPerRow + 1)].GetComponent<MeshMaker>().newVerticies[x + ((makerParent.meshMakers[mesh - (makerParent.meshesPerRow + 1)].GetComponent<MeshMaker>().nodeAmountX * makerParent.meshMakers[mesh - (makerParent.meshesPerRow + 1)].GetComponent<MeshMaker>().nodeAmountZ) -1)].y;
                        BL = BL / 4;
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y = BL;
                        makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().newVerticies[x + (makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountZ - 1))].y = BL;
                        makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().newVerticies[x + (makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().nodeAmountX - 1)].y = BL;
                        makerParent.meshMakers[mesh - (makerParent.meshesPerRow + 1)].GetComponent<MeshMaker>().newVerticies[x + ((makerParent.meshMakers[mesh - (makerParent.meshesPerRow + 1)].GetComponent<MeshMaker>().nodeAmountX * makerParent.meshMakers[mesh - (makerParent.meshesPerRow + 1)].GetComponent<MeshMaker>().nodeAmountZ) - 1)].y = BL;
                    }
                }
                else if (x == (makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountX * makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountZ) - 1) // top right
                {
                    if (mesh >= makerParent.meshesPerRow * (makerParent.numberOfRows - 1) && (mesh + 1) % makerParent.meshesPerRow == 0) continue; // mesh is top right corner
                    else if (mesh >= makerParent.meshesPerRow * (makerParent.numberOfRows - 1)) // mesh is on top edge of array
                    {
                        TR += makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y;
                        TR += makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().newVerticies[makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().nodeAmountZ - 1)].y;
                        TR = TR / 2;
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y = TR;
                        makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().newVerticies[makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().nodeAmountZ - 1)].y = TR;
                    }
                    else if ((mesh + 1) % makerParent.meshesPerRow == 0) // mesh on right edge of array
                    {
                        TR += makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y;
                        TR += makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().newVerticies[makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX - 1].y;
                        TR = TR / 2;
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y = TR;
                        makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().newVerticies[makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX - 1].y = TR;
                    }
                    else
                    {
                        TR += makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y;
                        TR += makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().newVerticies[makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().nodeAmountZ - 1)].y;
                        TR += makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().newVerticies[makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX - 1].y;
                        TR += makerParent.meshMakers[mesh + makerParent.meshesPerRow + 1].GetComponent<MeshMaker>().newVerticies[0].y;
                        TR = TR / 4;
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y = TR;
                        makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().newVerticies[makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().nodeAmountZ - 1)].y = TR;
                        makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().newVerticies[makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX - 1].y = TR;
                        makerParent.meshMakers[mesh + makerParent.meshesPerRow + 1].GetComponent<MeshMaker>().newVerticies[0].y = TR;
                    }
                }
                else if (x == (makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountZ - 1) -1)) // top left
                {
                    if (mesh >= makerParent.meshesPerRow * (makerParent.numberOfRows - 1) && mesh % (makerParent.meshesPerRow) == 0) continue; // mesh is top left corner
                    else if (mesh >= makerParent.meshesPerRow * (makerParent.numberOfRows - 1)) // mesh is on top edge of array
                    {
                        TL += makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y;
                        TL += makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().newVerticies[(makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountZ) - 1)].y;
                        TL = TL / 2;
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].y = TL;
                        makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().newVerticies[(makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountZ) - 1)].y = TL;

                    }
                    else if (mesh % (makerParent.meshesPerRow) == 0) // mesh on left edge of array
                    {

                    }
                    else
                    {

                    }
                }
                else if (x == makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountX - 1) // bottom right
                {
                    if (mesh < makerParent.meshesPerRow && (mesh + 1) % makerParent.meshesPerRow == 0) continue; //mesh is bottom right corner
                    else if (mesh < makerParent.meshesPerRow) //mesh is on bottom edge of array
                    {
                        
                    }
                    else if ((mesh + 1) % makerParent.meshesPerRow == 0) // mesh on right edge of array
                    {

                    }
                    else
                    {

                    }

                }
                else continue;
            }
            makerParent.meshMakers[mesh].GetComponent<MeshMaker>().UpdateMesh();
        }   
    }

    public void StitchMeshes()
    {
        Debug.Log("Stitching Meshes");
        Debug.Log(makerParent.meshMakers.Length);
        for (int mesh = 0; mesh < makerParent.meshMakers.Length; mesh++)
        {
            Debug.Log("Inside for loop mesh");
            Debug.Log(makerParent.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshMaker>().newVerticies.Length);
            
            if (mesh == 0)
            {
                continue;
            }
            else
            {
                for (int x = 0; x < makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies.Length; x++)
                {
                    if ((x + 1) % makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountX == 0) // check if on right edge of plane
                    {
                        if ((mesh + 1) % makerParent.meshesPerRow == 0) continue;
                        Debug.Log("Mesh: " + mesh + " Vertex: " + x + "Position: Right");
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x] = new Vector3(makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].x, makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().newVerticies[x - (makerParent.meshMakers[mesh + 1].GetComponent<MeshMaker>().nodeAmountX -1)].y, makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].z);
                    }
                    else if (x % (makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountX) == 0 && x != 1 || x == 0) // check if vertex is on left edge of plane
                    {
                        if (mesh % (makerParent.meshesPerRow) == 0 && mesh != 1) continue;
                        Debug.Log("Mesh: " + mesh + " Vertex: " + x + "Position: Left");
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x] = new Vector3(makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].x, makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().newVerticies[x + (makerParent.meshMakers[mesh - 1].GetComponent<MeshMaker>().nodeAmountX - 1)].y, makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].z);
                    }
                    else if (x >= makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountX * ((makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountZ - 1))) // check if on top edge of plane
                    {
                        if (mesh >= makerParent.meshesPerRow * (makerParent.numberOfRows - 1)) continue;
                        Debug.Log("Mesh: " + mesh + " Vertex: " + x + "Position: Top");
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x] = new Vector3 (makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].x, makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().newVerticies[x - (makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh + makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountZ - 1))].y, makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].z);
                    }
                    else if (x < makerParent.meshMakers[mesh].GetComponent<MeshMaker>().nodeAmountX) // check if vertex is on bottom edge of plane
                    {
                        if (mesh < makerParent.meshesPerRow) continue;
                        Debug.Log("Mesh: " + mesh + " Vertex: " + x + "Position: Bottom");
                        makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x] = new Vector3(makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].x, makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().newVerticies[x + (makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountX * (makerParent.meshMakers[mesh - makerParent.meshesPerRow].GetComponent<MeshMaker>().nodeAmountZ - 1))].y, makerParent.meshMakers[mesh].GetComponent<MeshMaker>().newVerticies[x].z);
                    }
                    else continue;     
                }
            }
            makerParent.meshMakers[mesh].GetComponent<MeshMaker>().UpdateMesh();
        } 
    }
}
