using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class manabar : MonoBehaviour
{
    public int manaleft;
    [SerializeField] Image[] crystals;
    [SerializeField] private Sprite full, empty;
    public void UpdateManaBar()
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
