using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public Transform Player;
    State currentState;
    public Transform[] Waypoints;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = new Idle(gameObject, agent, animator, Player, Waypoints);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
