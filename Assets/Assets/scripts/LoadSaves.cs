using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSaves : MonoBehaviour
{
    GameObject Save1, Save2, Save3, Save4, Save5, Save6;
    [SerializeField] private Sprite ChipBlack;
    [SerializeField] private Sprite ChipCyan;
    [SerializeField] private Sprite ChipPink;
    [SerializeField] private Sprite ChipYellow;
    [SerializeField] private Sprite ChipBrown;
    [SerializeField] private Sprite ChipOrange;
    [SerializeField] private Sprite ChipWhite;
    [SerializeField] private Sprite ChipBlue;
    [SerializeField] private Sprite ChipGreen;
    [SerializeField] private Sprite ChipRed;
    [SerializeField] private int sceneId; // 0 - WelcomeScreen, 1 - Fight Encounter, 2 - Rest Encounter, 3 - Endscreen, 4 - Map, 5 - Pick a card 


    void assignSprite(Image chip) //najlepiej to random by³oby zmieniæ na jakas zapamietana w bazie wartosc, zeby chip by³ tego samego koloru dla konkretnego save
    {
        switch (Random.Range(0, 9))
        {
            case 0: { chip.sprite = ChipBlack; } break;
            case 1: { chip.sprite = ChipCyan; } break;
            case 2: { chip.sprite = ChipPink; } break;
            case 3: { chip.sprite = ChipYellow; } break;
            case 4: { chip.sprite = ChipBrown; } break;
            case 5: { chip.sprite = ChipOrange; } break;
            case 6: { chip.sprite = ChipWhite; } break;
            case 7: { chip.sprite = ChipBlue; } break;
            case 8: { chip.sprite = ChipGreen; } break;
            case 9: { chip.sprite = ChipRed; } break;

        }
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
        //...albo tutaj 
        Save1 = this.gameObject.transform.GetChild(0).gameObject;
        Save2 = this.gameObject.transform.GetChild(1).gameObject;
        Save3 = this.gameObject.transform.GetChild(2).gameObject;
        Save4 = this.gameObject.transform.GetChild(3).gameObject;
        Save5 = this.gameObject.transform.GetChild(4).gameObject;
        Save6 = this.gameObject.transform.GetChild(5).gameObject;

        assignSprite(Save1.GetComponent<Image>());
        assignSprite(Save2.GetComponent<Image>());
        assignSprite(Save3.GetComponent<Image>());
        assignSprite(Save4.GetComponent<Image>());
        assignSprite(Save5.GetComponent<Image>());
        assignSprite(Save6.GetComponent<Image>());
        //Save1.GetComponent<Button>().onClick.AddListener(chuj);
        //w ten sposob mozna przypisac na startupie do przyciskow funkcje, z tym problemem
        //¿e nie mog¹ mieæ ¿adnych parametrów, jeœli znajdziecie lepszy sposób zapraszam do zmiany
    }
}
