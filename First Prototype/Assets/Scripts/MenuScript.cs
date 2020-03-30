using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Button play_button, setting_button, exit_button; //button gameObjects

    // Start is called before the first frame update
    void Start()
    { 
        //when buttons are clicked
        play_button.onClick.AddListener(playGame);
        setting_button.onClick.AddListener(OpenSettingsMenu);
        exit_button.onClick.AddListener(exitGame);
    }

    void playGame()
    {
        SceneManager.LoadScene(0);
    }

    void OpenSettingsMenu()
    {
        SceneManager.LoadScene(2);
    }

    void exitGame()
    {
        Application.Quit();
    }
}