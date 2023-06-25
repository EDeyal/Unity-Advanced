using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Material winMetarial;
    [SerializeField] private Material loseMetarial;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    [SerializeField] private Transform targetTransform;
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveSpeed = 1f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }
    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //foreach (var idiot in idiots) {give us 100 }
        ActionSegment<float> continueousActions = actionsOut.ContinuousActions;
        continueousActions[0] = Input.GetAxisRaw("Horizontal");
        continueousActions[1] = Input.GetAxisRaw("Vertical");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            floorMeshRenderer.material = loseMetarial;
            EndEpisode();
        }
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            floorMeshRenderer.material = winMetarial;
            EndEpisode();
        }
    }
}
