using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLEnvironmentManager : MonoBehaviour
{
    //A simple platform randomizer for robust training and robustness testing
    private PlatformRandomizer[] _platformRandomizers;

    public void OnEnvironmentReset()
    {
        GetPlatformRandomizers();
        foreach(PlatformRandomizer pr in _platformRandomizers)
        {
            pr.Randomize();
        }
    }

    public void GetPlatformRandomizers()
    {
        _platformRandomizers = GetComponentsInChildren<PlatformRandomizer>();
    }
}
