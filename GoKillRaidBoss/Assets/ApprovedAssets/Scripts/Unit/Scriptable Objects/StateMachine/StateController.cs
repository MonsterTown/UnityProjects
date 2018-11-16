using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//using Complete;

public class StateController : MonoBehaviour {
    public State currentState;
    [HideInInspector] public UnitStats unitStats;
    public Transform eyes;
    public State remainState;


    //   [HideInInspector] public NavMeshAgent navMeshAgent;
    //   [HideInInspector] public Complete.TankShooting tankShooting;
    //   [HideInInspector] public List<Transform> wayPointList;
//    [HideInInspector] public int nextWayPoint;
    public GameObject targetForChaseOrAttack;
    [HideInInspector] public float stateTimeElapsed;
    public List<GameObject> enemies;

    private bool aiActive;


    void Awake() {
        //       tankShooting = GetComponent<Complete.TankShooting>();
        //       navMeshAgent = GetComponent<NavMeshAgent>();
        unitStats = GetComponent<UnitStats>();
    }

    private void Start() {
        SetupAI(true);
    }

    public void SetupAI(bool activeFromStart) {
        aiActive = activeFromStart;
        if (aiActive) {
//            navMeshAgent.enabled = true;
        } else {
            //          navMeshAgent.enabled = false;
        }
    }

    void Update() {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }


    void OnDrawGizmos() {
        if (currentState != null && eyes != null) {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, 5f);
        }
    }

    public void TransitionToState(State nextState) {
        if (nextState != remainState) {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration) {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState() {
        stateTimeElapsed = 0;
    }
}