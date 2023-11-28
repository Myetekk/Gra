using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSaves : MonoBehaviour
{
    GameObject Save1, Save2, Save3, Save4, Save5, Save6;
    [SerializeField] private List<Sprite> chips;
    [SerializeField] private int sceneId; // 0 - WelcomeScreen, 1 - Fight Encounter, 2 - Rest Encounter, 3 - Endscreen, 4 - Map, 5 - Pick a card 


    void assignSprite(Image chip) //najlepiej to random by³oby zmieniæ na jakas zapamietana w bazie wartosc, zeby chip by³ tego samego koloru dla konkretnego savea
    {
        chip.sprite = chips[Random.Range(0, 9)];
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(sceneId); //zmienia scene, numer do przypisania w obiekcie LoadMenu
        GetComponentInParent<CGFV>().CreateGameFromValues();
    }
    public void LoadFromDatabase()//tutaj mo¿na te¿ dodaæ wczytywanie z bazy danych zapisanych saveów...
    {

    }
    void Start()
    {
        List<GameObject> saves = new List<GameObject>() { Save1, Save2, Save3, Save4, Save5, Save6 };
        //...albo tutaj 
        for (int i = 0; i < saves.Count; i++)
        {
            saves[i] = this.gameObject.transform.GetChild(i).gameObject;
            assignSprite(saves[i].GetComponent<Image>());
        }
        //Save1.GetComponent<Button>().onClick.AddListener(chuj);
        //w ten sposob mozna przypisac na startupie do przyciskow funkcje, z tym problemem
        //¿e nie mog¹ mieæ ¿adnych parametrów, jeœli znajdziecie lepszy sposób zapraszam do zmiany
    }
}
