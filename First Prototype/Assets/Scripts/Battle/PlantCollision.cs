using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantCollision : MonoBehaviour
{


    public GameObject levelChanger;
    public GameObject toBeDestroyed;


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.transform.position); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "plant_big")
        {   
            LevelChanger lc = levelChanger.GetComponent<LevelChanger>();
            //ToBeDestroyed tbd = toBeDestroyed.GetComponent<ToBeDestroyed>();
            //tbd.Add(collision.gameObject);
            lc.FadeToLevel(1);
            
        }
    }
}
