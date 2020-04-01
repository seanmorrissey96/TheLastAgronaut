using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public Animator animator;

    private int levelToLoad;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FadeToLevel(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FadeToLevel(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FadeToLevel(4);
        }
    }


    public void FadeToLevel(int levelIndex, string battleOutcome = "lost")
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    

    public void onFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
