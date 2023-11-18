using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class StartGame : MonoBehaviour
{
    [SerializeField] int sceneId;     //Random.Range(x, y) -> losowa liczba od x do y, 0.0f - 10.0f losowy float od 0 do 10 w³¹cznie, 0 - 10 losowy int od 0 do 10 w³¹cznie
    public void startNewGame() //dodaæ wartoœci losowe i sta³e przy tworzeniu nowej gry
    {               
        SceneManager.LoadScene(sceneId);
        GetComponentInParent<CGFV>().CreateGameFromValues();

    }

}
