using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public void Settings()
    {
        
    }

    public void NewGame()
    {
        Debug.Log("Clicked!");
    }

    public void LoadGame()
    {
        Debug.Log("Clicked!");
    }

    public void Exit()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

}
