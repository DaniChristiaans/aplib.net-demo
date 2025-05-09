using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Assertions.Must;

public class MLAgentAplib : Agent
{
    Rigidbody _rigidbody;
    private float[] _rayHits = new float[15];
    private float _rayDistance = 10f;
    private Vector3 _destination;
    private Movement _movement;
    private float _distanceToTarget;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _jumpInputGiven;

    int _layerMask;

    public RLEnvironmentManager environmentManager;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movement = GetComponent<Movement>();
        GameObject target = GameObject.FindWithTag("Target");
        if (target != null)
        {
            SetTarget(GameObject.FindWithTag("Target").transform.position);
            Debug.Log("Target pos = " + _destination);
        }
        // Set initiall distance to target to prevent null reference exception
        _distanceToTarget = Vector3.Distance(transform.position, _destination);
        _startPosition = transform.position;
        _startRotation = transform.rotation;

        _layerMask = ~(1 << LayerMask.NameToLayer("Player")); // set up layermask for raycasts to detect everything except "Player"
    }

    public void SetTarget(Vector3 destination)
    {
        _destination = destination;
    }

    public override void OnEpisodeBegin()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        environmentManager.OnEnvironmentReset();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((_destination - transform.position).normalized); //relative direction to goal
        sensor.AddObservation(_distanceToTarget / 100f);
        //sensor.AddObservation(Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad));
        //sensor.AddObservation(Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad));
        sensor.AddObservation(_rigidbody.velocity / 100f);
        sensor.AddObservation(transform.rotation);
        sensor.AddObservation(_movement.IsGrounded());

        foreach (var rayHit in _rayHits)
        {
            sensor.AddObservation(rayHit);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var discreteActions = actions.DiscreteActions;

        // Branch 0: Left / Right / None
        int leftRight = discreteActions[0];
        Vector2 move = Vector2.zero;
        if (leftRight == 1)
            move += Vector2.left;
        else if (leftRight == 2)
            move += Vector2.right;

        // Branch 1: Forward / Backward / None
        int forwardBack = discreteActions[1];
        if (forwardBack == 1)
            move += Vector2.up;
        else if (forwardBack == 2)
            move += Vector2.down;

        // Apply movement using the horizontal input on the Movement script
        _movement.ReceiveHorizontalInput(move);

        // Branch 2: Jump / Stay
        int jump = discreteActions[2];
        if (jump == 1 && !_jumpInputGiven)
        {
            _movement.OnJumped(); // Trigger the jump input on the Movement component
            _jumpInputGiven = true;
        }
        else if (jump == 0 && _jumpInputGiven)
        {
            _movement.OnStoppedJump();
            _jumpInputGiven = false;
        }

        CalculateReward();
    }

    void CalculateReward()
    {
        // Reward shaping:
        _distanceToTarget = Vector3.Distance(transform.position, _destination);

        // Small reward for getting closer
        float distanceReward = -_distanceToTarget / 500f;
        AddReward(distanceReward);

        JumpReward();
    }

    private bool _wasGrounded = false;
    private Vector3 _jumpStartPosition;
    private Vector3 _jumpEndPosition;

    private bool _overGap = false;
    private float _deepestPoint;

    public void JumpReward()
    {
        if (_wasGrounded && !_movement.IsGrounded())
        {
            _jumpStartPosition = transform.position;
            _overGap = false;
        }
        else if (!_movement.IsGrounded())
        {
            float point = ClampedNormalizedRaycast(transform.position, -Vector3.up);
            if(point > _deepestPoint)
            {
                _deepestPoint = point;
            }
        }
        
        if (!_wasGrounded && _movement.IsGrounded())
        {
            _jumpEndPosition = transform.position;
            Vector3 jumpDelta = _jumpEndPosition - _jumpStartPosition;
            if (_overGap && _deepestPoint > 1f)
            {
                Debug.Log("Jumped a gap! It was " + _deepestPoint + " units deep and I jumped " + jumpDelta + " units far!");
                AddReward(Vector3.SqrMagnitude(jumpDelta));
            }
        }
    }

    public void Win()
    {
        Debug.Log("GOALLLLLLLLLLLLLLLLL!!!");
        SetReward(500.0f);
        EndEpisode();
    }

    public void Die()
    {
        // Penalize falling and end the episode
        AddReward(-10.0f);   
        EndEpisode();
    }

    public void Treat()
    {
        // Small reward for reaching a specific place
        //AddReward(1.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateRaycasts();
    }

    void UpdateRaycasts()
    {
        (Vector3, Vector3)[] rays = new (Vector3, Vector3)[]
        {
            (transform.position, transform.forward),                                 // front
            (transform.position, -transform.right),                                  // left
            (transform.position, transform.right),                                   // right
            (transform.position, -transform.forward),                                // back
            (transform.position - transform.right, transform.forward),               // front from "left shoulder"
            (transform.position + transform.right, transform.forward),               // front from "right shoulder"
            (transform.position, -transform.up),                                     // down
            (transform.position + transform.forward, -transform.up),                 // down "in front of the agent's feet"
            (transform.position + (transform.forward * 2), -transform.up),           // more in front
            (transform.position + (transform.forward * 3), -transform.up),           // even more in front
            (transform.position + (transform.forward * 4), -transform.up),           // more forward again
            (transform.position + (transform.forward * 5), -transform.up),           // furthest forward
            (transform.position + transform.right, - transform.up),                  // down to the right
            (transform.position - transform.right, - transform.up),                  // down to the left
            (transform.position - transform.forward, - transform.up)                 // down behind
        };

        for (int i = 0; i < rays.Length; i++)
        {
            _rayHits[i] = ClampedNormalizedRaycast(rays[i].Item1, rays[i].Item2);
        }
    }

    

    float ClampedNormalizedRaycast(Vector3 position, Vector3 direction)
    {
        if (Physics.Raycast(position, direction, out RaycastHit hit, _rayDistance, _layerMask)) //
        {
            return hit.distance / _rayDistance;  // Normalize distance to [0, 1]
        }
        else
        {
            return 1.0f;  // No hit, so we set it to max distance
        }
    }

    //A heuristic for manual input, used for recording expert demonstrations
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;

        // Branch 0: Left / Right / None
        if (Input.GetKey(KeyCode.A))
            discreteActionsOut[0] = 1;
        else if (Input.GetKey(KeyCode.D))
            discreteActionsOut[0] = 2;
        else
            discreteActionsOut[0] = 0;

        // Branch 1: Forward / Backward / None
        if (Input.GetKey(KeyCode.W))
            discreteActionsOut[1] = 1;
        else if (Input.GetKey(KeyCode.S))
            discreteActionsOut[1] = 2;
        else
            discreteActionsOut[1] = 0;

        // Branch 2: Jump / Stay
        if (Input.GetKey(KeyCode.Space))
            discreteActionsOut[2] = 1;
        else
            discreteActionsOut[2] = 0;

        //float turnInput = actionsOut.ContinuousActions[0];

    }

}
