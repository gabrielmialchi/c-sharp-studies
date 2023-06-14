using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE, PATROL, CHASE
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE stateName;
    protected EVENT stage;
    protected GameObject Enemy;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Transform Player;
    protected Transform[] Waypoints;
    protected State nextState;

    float visDist = 10.0f;
    float visAngle = 45.0f;

    public State(GameObject _Enemy, NavMeshAgent _agent, Animator _animator, Transform _Player, Transform[] _Waypoints)
    {
        Enemy = _Enemy;
        agent = _agent;
        animator = _animator;
        Player = _Player;
        Waypoints = _Waypoints;
    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State Process()
    {
        if (stage == EVENT.ENTER)
        {
            Enter();
        }
        else if (stage == EVENT.UPDATE)
        {
            Update();
        }
        else 
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = Player.position - Enemy.transform.position;
        float angle = Vector3.Angle(direction, Enemy.transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            return true;
        }
        return false;
    }
}



public class Idle : State
{
    float timer;
    public Idle(GameObject _Enemy, NavMeshAgent _agent, Animator _animator, Transform _Player, Transform[] _Waypoints) : base(_Enemy, _agent, _animator, _Player, _Waypoints)
    {
        stateName = STATE.IDLE;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        animator.SetTrigger("idle");
        base.Enter();
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            nextState = new Patrol(Enemy, agent, animator, Player, Waypoints); 
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("idle");
        base.Exit();
    }
}

public class Patrol : State
{
    int currentIndex = -1;

    public Patrol(GameObject _Enemy, NavMeshAgent _agent, Animator _anim, Transform _Player, Transform[] _Waypoints) 
                : base(_Enemy, _agent, _anim, _Player, _Waypoints)
    {
        stateName = STATE.PATROL;
        agent.speed = 1.5f;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        float lastDist = Mathf.Infinity;


        for (int i = 0; i < Waypoints.Length; i++)
        {
            Transform thisWP = Waypoints[i];
            float distance = Vector3.Distance(Enemy.transform.position, thisWP.position); 
            if (distance < lastDist)
            {
                currentIndex = i - 1;
                lastDist = distance;
            }
        }
        animator.SetTrigger("walk");
        base.Enter();
    }

    public override void Update()
    {

        if (agent.remainingDistance < 1)
        {


            if (currentIndex >= Waypoints.Length - 1)
                currentIndex = 0;
            else
                currentIndex++;

            agent.SetDestination(Waypoints[currentIndex].position);
        }

        if (CanSeePlayer())
        {
            nextState = new Chase(Enemy, agent, animator, Player, Waypoints); 
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        animator.ResetTrigger("walk");
        base.Exit();
    }


}

public class Chase : State
{
    public Chase(GameObject _Enemy, NavMeshAgent _agent, Animator _animator, Transform _Player, Transform[] _Waypoints) : base(_Enemy, _agent, _animator, _Player, _Waypoints)
    {
        stateName = STATE.CHASE;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        animator.SetTrigger("run");
        base.Enter();
    }



    public override void Update()
    {
        agent.SetDestination(Player.position);
        if (agent.hasPath)
        {

            if (!CanSeePlayer())
            {
                nextState = new Idle(Enemy, agent, animator, Player, Waypoints);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("run");
        base.Exit();
    }
}