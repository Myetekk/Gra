using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateEncounter : MonoBehaviour
{
    [SerializeField] private List<Sprite> PlayerClass;
    [SerializeField] private List<Sprite> EnemyType;
    [SerializeField] private List<Sprite> Background;
    [SerializeField] private GameObject bg, pl, en;
    [SerializeField] private GameObject canvas;
    public int bgNo, plNo, enNo;
    public float a, b;
    public void ajhsbckjh()
    { 
        //tutaj nadaæ wartoœci bgNo, plNo, enNo pochodz¹ce z zapisanych inforamcji mapy, aby stworzyæ odpowiedni encounter + zmieniæ inty na private
        //s¹ public zeby moc testowac w unity

        Image background = bg.GetComponent<Image>();
        Image player = pl.GetComponent<Image>();
        Image enemy = en.GetComponent<Image>();
        RectTransform rt = canvas.GetComponent<RectTransform>();
        background.sprite = Background[bgNo];
        background.rectTransform.sizeDelta = new Vector2(background.sprite.texture.width, background.sprite.texture.height);
        if (background.sprite.texture.width < rt.rect.width && background.sprite.texture.height < rt.rect.height)
        {
            float ratio = Mathf.Max((rt.rect.width + 30) / background.sprite.texture.width, (rt.rect.height + 30) / background.sprite.texture.height);
            a = bg.GetComponent<RectTransform>().rect.width;
            background.rectTransform.sizeDelta = new Vector2(bg.GetComponent<RectTransform>().rect.width * ratio, bg.GetComponent<RectTransform>().rect.height * ratio);
        }
        else if (background.sprite.texture.width < rt.rect.width)
        {
            float ratio = (rt.rect.width+30) / background.sprite.texture.width;
            a = bg.GetComponent<RectTransform>().rect.width;
            background.rectTransform.sizeDelta = new Vector2(bg.GetComponent<RectTransform>().rect.width*ratio, bg.GetComponent<RectTransform>().rect.height*ratio);
        }
        else if(background.sprite.texture.height < rt.rect.height)
        {
            float ratio = (rt.rect.height+30) / background.sprite.texture.height;
            b = bg.GetComponent<RectTransform>().rect.height * ratio;
            background.rectTransform.sizeDelta = new Vector2(bg.GetComponent<RectTransform>().rect.width * ratio, bg.GetComponent<RectTransform>().rect.height * ratio);
        }

        player.sprite = PlayerClass[plNo];
        enemy.sprite = EnemyType[enNo];
    }

}
