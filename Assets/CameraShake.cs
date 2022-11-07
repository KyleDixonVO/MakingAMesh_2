using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 360f)]
    float amplitude = 0.1f;
    [SerializeField]
    [Range(0f, 10f)]
    float frequency = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaPos;
        //deltaPos.x = Mathf.PerlinNoise(Time.time * frequency, 0) * amplitude;
        //deltaPos.y = Mathf.PerlinNoise(Time.time * frequency, 0) * amplitude;
        //deltaPos.z = Mathf.PerlinNoise(Time.time * frequency, 0) * amplitude;

        deltaPos.x = PerlinNoise(Time.time * frequency, 0.0f) * amplitude;
        deltaPos.y = PerlinNoise(Time.time * frequency, 1.0f) * amplitude;
        deltaPos.z = PerlinNoise(Time.time * frequency, 2.0f) * amplitude;
        gameObject.transform.localEulerAngles = deltaPos;
    }

    float PerlinNoise(float x, float y)
    {
        return ((Mathf.PerlinNoise(x, y) * 2.0f) - 1f);
    }
}
