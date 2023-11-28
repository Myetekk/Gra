using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class StartGame : MonoBehaviour
{
    [SerializeField] int sceneId;
    public void startNewGame() //doda� warto�ci losowe i sta�e przy tworzeniu nowej gry
    {               
        SceneManager.LoadScene(sceneId);
        GetComponentInParent<CGFV>().CreateGameFromValues();
    }

}
