using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    // [SerializeField] private Transform movePositionTransform;
    [SerializeField] private List<Transform> navMeshAgents;
    private NavMeshAgent navMeshAgent;
    private Transform nextMoveTarget;

    private void Awake() {
        nextMoveTarget = navMeshAgents[0];
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void LateUpdate() {
        // Debug.Log(nextMoveTarget);
        navMeshAgent.destination = nextMoveTarget.position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "MoveTarget")
        {
            nextMoveTarget = other.gameObject.GetComponent<WayPoint>().nextPoint;
        }
    }
}
