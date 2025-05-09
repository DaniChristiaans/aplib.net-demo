using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRandomizer : MonoBehaviour
{
    [SerializeField]
    private Vector3 _maxOffsetMagnitudes;

    [SerializeField]
    private Transform[] _platforms;
    private Vector3[] _origins;
    
    // Start is called before the first frame update
    void Awake()
    {
        _platforms = GetComponentsInChildren<Transform>();
        _origins = new Vector3[_platforms.Length];

        for (int i = 0; i < _platforms.Length; i++)
        {
            _origins[i] = _platforms[i].position;
        }
    }

    void OnEnable()
    {

    }

    public void Randomize()
    {
        

        for(int i = 0; i < _platforms.Length; i++)
        {
            _platforms[i].position = _origins[i] + RandomOffsetVector3(_maxOffsetMagnitudes);
        }
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
