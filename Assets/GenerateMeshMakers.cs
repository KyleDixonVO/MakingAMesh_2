using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMeshMakers : MonoBehaviour
{
    public int numberOfMeshMakers;
    public GameObject MeshMakerPrefab;
    public WorldStitcher worldStitcher;
    public int nodeAmountX;
    public int nodeAmountZ;
    public float nodeSpacingX;
    public float nodeSpacingZ;
    public float frequency;
    public float amplitude;
    public int numberOfSpectralIterations;
    public float spectralMultiplier;

    private void Awake()
    {
        numberOfMeshMakers = worldStitcher.meshesPerRow * worldStitcher.numberOfRows;
        for (int i = 0; i < numberOfMeshMakers; i++)
        {
            GameObject temp = Instantiate(MeshMakerPrefab);
            temp.transform.parent = transform;
            temp.GetComponent<MeshMaker>().nodeAmountX = nodeAmountX + 1;
            temp.GetComponent<MeshMaker>().nodeAmountZ = nodeAmountZ + 1;
            temp.GetComponent<MeshMaker>().xSpacing = nodeSpacingX;
            temp.GetComponent<MeshMaker>().zSpacing = nodeSpacingZ;
            temp.GetComponent<MeshMaker>().frequency = frequency;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
