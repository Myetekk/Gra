using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    [SerializeField] GameObject mainmenu;


    void NewGameLogic()   
    {
        mainmenu.SetActive(false);

    }
    public void NewGame()
    {
        Invoke("NewGameLogic", 0.5f);
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
