using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GPSPatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int patrollingTo;

    public GameObject gpsDialoguePanel;
    public Text dialogue1;

    NavMeshAgent _navMeshAgent;
    void Start()
    {
        //dialogue1.GetComponent<Text>();
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
        print("Patrolling to: " + patrollingTo);
        if (patrollingTo >= 3)
        {
            dialogue1.text = "Some of the sheep seem a little down, go see if you can't cheer them up!";
            StartCoroutine("WaitForSec");
        }

        if (Vector3.Distance(transform.position, moveSpots[patrollingTo].position) < 0.5f)
        {
            if (waitTime <= 0)
            {
                if (patrollingTo < moveSpots.Length - 1)
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

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(6);
        gpsDialoguePanel.SetActive(false);
    }
}
