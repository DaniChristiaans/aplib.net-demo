using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HealthComponent healthComponent = other.GetComponent<HealthComponent>();
            if(healthComponent != null)
            {
                healthComponent.ReduceHealth(healthComponent.Health * 2);
            }
            else
            {
                Debug.LogWarning("Deadzone is trying to kill player, but no health component found!");
            }
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
