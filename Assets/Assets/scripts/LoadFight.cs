using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFight : MonoBehaviour
{
    [SerializeField] private int sceneId; // 0 - WelcomeScreen, 1 - Fight Encounter, 2 - Rest Encounter, 3 - Endscreen, 4 - Map, 5 - Pick a card 
   
    public void LoadGame()
    {
        SceneManager.LoadScene(sceneId); //zmienia scene, numer do przypisania w obiekcie LoadMenu
        GetComponentInParent<CGFV>().CreateGameFromValues();
    }
}
