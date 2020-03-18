using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{


    public GameObject toBeDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "LevelWithEnemies")
        {
            ToBeDestroyed tbd = toBeDestroyed.GetComponent<ToBeDestroyed>();
            Destroy(tbd.toBeDestroyed[0]);
        }
        else if (sceneName == "Example 2")
        {
            // Do something...
        }

        // Retrieve the index of the scene in the project's build settings.
        int buildIndex = currentScene.buildIndex;

        // Check the scene name as a conditional.
        switch (buildIndex)
        {
            case 0:
                // Do something...
                break;
            case 1:
                // Do something...
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
