using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GPSPatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int patrollingTo;

    NavMeshAgent _navMeshAgent;
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        waitTime = startWaitTime;
        patrollingTo = 0;
    }

    void Update()
    {
        Vector3 targetVector = moveSpots[patrollingTo].transform.position;
        _navMeshAgent.SetDestination(targetVector);
        //transform.position = Vector3.MoveTowards(transform.position, moveSpots[patrollingTo].position, speed * Time.deltaTime);
        print("Distance to other: " + Vector3.Distance(transform.position, moveSpots[patrollingTo].position));
        if (Vector3.Distance(transform.position, moveSpots[patrollingTo].position) < 0.5f)
        {
            if(waitTime <= 0)
            {
                if(patrollingTo < moveSpots.Length - 1)
                {
                    patrollingTo++;
                }
                
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
