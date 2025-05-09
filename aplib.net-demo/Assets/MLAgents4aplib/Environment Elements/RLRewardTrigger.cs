using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class RLRewardTrigger : MonoBehaviour
{
    //Script for use on environment objects with a trigger that need to reward the agent or act as win condition
    private bool _claimed = false;
    [SerializeField]
    private bool isWinCondition = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_claimed)
            {
                return;
            }
            MLAgentAplib agent = other.GetComponent<MLAgentAplib>();
            if (agent != null)
            {
                if (isWinCondition)
                {
                    agent.Win();
                }
                else
                {
                    agent.Treat();
                    _claimed = true;
                }
                    
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
