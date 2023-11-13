using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public void NewGame() //nic
    {
        Debug.Log("Clicked!");
    }

    public void LoadGame() //nic
    {
        Debug.Log("Clicked!");
    }

    public void Exit() //wy³¹cza program
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

}
