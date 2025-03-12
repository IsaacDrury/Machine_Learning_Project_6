using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;

public class MLAgent : Agent
{
    [SerializeField]
    private GameObject trainingEnv;
    [SerializeField]
    private int forceMultiplier;
    [SerializeField]
    private int rotationForceMultiplier;

    public Transform target;

    private Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        forceMultiplier = 20;
        rotationForceMultiplier = 4;
    }

    public override void OnEpisodeBegin()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        if (trainingEnv.GetComponent<MoveAgents>().CheckAgent(this.gameObject))
        {
            trainingEnv.GetComponent<MoveAgents>().Reposition();
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        SetReward(-0.0001f);
        //Agent movement
        float dir = actionBuffers.ContinuousActions[0];
        rb.linearVelocity = this.transform.forward * forceMultiplier * dir;

        //Agent rotation
        Vector3 newRotation = Vector3.zero;
        newRotation.y = actionBuffers.ContinuousActions[1];
        this.transform.Rotate(newRotation);
    }

    private void OnTriggerEnter(Collision other)
    {
        if (other.gameObject.tag == "Food")
        {
            SetReward(1.0f);
            trainingEnv.GetComponent<SpawnItems>().SetFoodAmount(other.gameObject);
        }
    }
}
