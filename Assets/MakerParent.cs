using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakerParent : MonoBehaviour
{
    public GameObject meshMakerPrefab;
    public int squaresPerMeshLength;
    public float frequency;
    public float xSpacing;
    public float zSpacing;
    public int meshesPerRow;
    public int numberOfRows;
    public GameObject[] meshMakers;
    //public List<GameObject> meshMakers;
    public GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        meshMakers = new GameObject[meshesPerRow * numberOfRows];
        //meshMakers = new List<GameObject>();
        PopulateMeshMakers();
        SetMakerProperties();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MeshStitcher.stitcher.StitchMeshes();
            MeshStitcher.stitcher.StitchCorners();
        }
    }

    void PopulateMeshMakers()
    {
        for (int i = 0; i < (meshesPerRow * numberOfRows); i++)
        {
            meshMakers[i] = Instantiate<GameObject>(meshMakerPrefab);
            //meshMakers.Add(Instantiate(meshMakerPrefab));
            meshMakers[i].transform.parent = gameObject.transform;
        }
    }

    void SetMakerProperties()
    {
        int meshNum = 0;
        for (int i = 0; i < numberOfRows; i++)
        {
            for(int j = 0; j < meshesPerRow; j++)
            {
                meshMakers[meshNum].GetComponent<MeshMaker>().nodeAmountX = squaresPerMeshLength + 1;
                meshMakers[meshNum].GetComponent<MeshMaker>().nodeAmountZ = squaresPerMeshLength + 1;
                meshMakers[meshNum].GetComponent<MeshMaker>().xSpacing = xSpacing;
                meshMakers[meshNum].GetComponent<MeshMaker>().zSpacing = zSpacing;
                meshMakers[meshNum].transform.position = new Vector3(j * xSpacing * (squaresPerMeshLength), 0, i * zSpacing * (squaresPerMeshLength));
                meshNum++;
            }
        }
    }
}
