using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class hpPool : MonoBehaviour
{

    [SerializeField] private Image HP;
    public float hpMax = 100;
    public float hpCurr = 43;
    Image img;
    private void Start()
    {
        img = GetComponent<Image>();
    }
    public void UpdateHpValue()
    {
        HP.transform.localPosition += new Vector3(0, (float)((-1 * HP.transform.localPosition.y) - (img.rectTransform.rect.height * ((hpMax - hpCurr) / hpMax))), 0);
    }
}
