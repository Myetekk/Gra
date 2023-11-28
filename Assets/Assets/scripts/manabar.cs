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
    private void Start() //je�eli b�dzie potrzeba sprawdzania max ilo�ci kryszta��w poza tworzeniem sceny (idk jakies mechaniki czy cos) to przeniesc to do UpdateManaBar()
    { //je�eli obiekt "Mana Bar" b�dzie mie� inne dzieci ni� kryszta�y, funkcje trzeba bedzie zmieni� na gameobject.find i wprowadzi� system nazywania kryszta��w (najlepiej po porstu etgo nie robi�)
        for(int i = 0; i < crystals.Length; i++)
        {
            crystals[i] = this.gameObject.transform.GetChild(i).GetComponent<Image>();
        }
    }

    public void UpdateManaBar() //zape�nia lub oproznia kryszta�y w zale�no�ci od intagera
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
