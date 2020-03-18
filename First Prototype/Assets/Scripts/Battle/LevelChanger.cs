using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public Animator animator;

    private int levelToLoad;

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    FadeToLevel(1);
        //}


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
