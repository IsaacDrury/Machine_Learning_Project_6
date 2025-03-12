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
    private GameObject[] foods;

    public Transform target;

    private Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        forceMultiplier = 20;
    }

    public override void OnEpisodeBegin()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        if (trainingEnv.GetComponent<MoveAgents>().CheckAgent(this.gameObject))
        {
            trainingEnv.GetComponent<MoveAgents>().Reposition();
        }
        foods = trainingEnv.GetComponent<SpawnItems>().GetFoods();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Food and agent positions
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        //Agent velocity
        sensor.AddObservation(rb.linearVelocity.x);
        sensor.AddObservation(rb.linearVelocity.z);

        //Agent rotation
        sensor.AddObservation(rb.rotation.y);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        //Agent movement
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rb.AddForce(controlSignal * forceMultiplier);

        //Agent rotation
        Vector3 newRotation = Vector3.zero;
        newRotation.y = actionBuffers.ContinuousActions[2];
        rb.angularVelocity = newRotation;

        float targetDistance = Vector3.Distance(this.transform.localPosition, target.localPosition);

        if (targetDistance < 0.99)
        {
            SetReward(1.0f);
        }
    }
}
