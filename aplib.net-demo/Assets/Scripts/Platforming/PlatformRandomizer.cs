using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRandomizer : MonoBehaviour
{
    [SerializeField]
    private Vector3 _maxOffsetMagnitudes;

    [SerializeField]
    private Transform[] _platforms;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        _platforms = GetComponentsInChildren<Transform>();

        foreach (Transform t in _platforms)
        {
            t.localPosition += RandomOffsetVector3(_maxOffsetMagnitudes);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomOffsetVector3(Vector3 maxMagnitudes)
    {
        float x = RandomOffsetFloat(maxMagnitudes.x);
        float y = RandomOffsetFloat(maxMagnitudes.y);
        float z = RandomOffsetFloat(maxMagnitudes.z);
        return new Vector3(x, y, z);
    }


    float RandomOffsetFloat(float maxMagnitude)
    {
        return Random.Range(-maxMagnitude, maxMagnitude);
    }
}
