using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    private NavMeshAgent enemy;
    private Transform point;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        point = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(point.position);
    }
}
