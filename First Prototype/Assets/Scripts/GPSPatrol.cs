using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GPSPatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int patrollingTo;
    private string sceneName;
    public GameObject gpsDialoguePanel;
    public Text dialogue1;
    public bool showReminderText;

    public AudioSource gpsSound;
    //public Animator anim;

    NavMeshAgent _navMeshAgent;
    void Start()
    {
        //dialogue1.GetComponent<Text>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        waitTime = startWaitTime;
        patrollingTo = 0;
        showReminderText = false;

        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "AnimatedSheepLevel")
        {
            Vector3 targetVector = moveSpots[patrollingTo].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            //transform.position = Vector3.MoveTowards(transform.position, moveSpots[patrollingTo].position, speed * Time.deltaTime);
            if (patrollingTo >= 3)
            {
                //anim.SetBool("isWalking", true);
                dialogue1.text = "Some of the sheep seem a little down, go see if you can cheer them up!\nPress 'E' when near a sheep to feed it.";
                gpsSound.Play();
                StartCoroutine("WaitForSec");
                
            }
            //print("DISTANCE TO DEST: " + Vector3.Distance(transform.position, moveSpots[patrollingTo].position));
            //print("WAITTIME: " + waitTime);
            if (Vector3.Distance(transform.position, moveSpots[patrollingTo].position) < 1.2f)
            {
                //anim.SetBool("isWalking", true);

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
        else
        {
            dialogue1.text = "Penelope! You won!";
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(6);
        gpsDialoguePanel.SetActive(false);
        showReminderText = true;
    }
}
