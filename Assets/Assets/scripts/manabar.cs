using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class manabar : MonoBehaviour
{
    public int manaleft;
    public const int maxmana = 10;
    public Image[] crystals = new Image[maxmana];
    [SerializeField] private Sprite full, empty;
    private void Start() //je¿eli bêdzie potrzeba sprawdzania max iloœci kryszta³ów poza tworzeniem sceny (idk jakies mechaniki czy cos) to przeniesc to do UpdateManaBar()
    { //je¿eli obiekt "Mana Bar" bêdzie mieæ inne dzieci ni¿ kryszta³y, funkcje trzeba bedzie zmieniæ na gameobject.find i wprowadziæ system nazywania kryszta³ów (najlepiej po porstu etgo nie robiæ)
        for(int i = 0; i < crystals.Length; i++)
        {
            crystals[i] = this.gameObject.transform.GetChild(i).GetComponent<Image>();
        }
    }

    public void UpdateManaBar() //zape³nia lub oproznia kryszta³y w zale¿noœci od intagera
    {
        if(manaleft <= 10)
        {
            for (int i = 0; i < manaleft; i++)
            {
                crystals[i].sprite = full;
            }
            for(int i = manaleft;i < 10; i++)
            {
                crystals[i].sprite = empty;
            }
        }
    }
}
