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
    [SerializeField] private List<Sprite> Boss;
    [SerializeField] private GameObject bg, pl, en, bo;
    [SerializeField] private GameObject canvas;
    public int bgNo, plNo, enNo, boNo;
    public bool isBoss;
    public void ajhsbckjh()
    { 
        //tutaj nada� warto�ci bgNo, plNo, enNo pochodz�ce z zapisanych inforamcji mapy, aby stworzy� odpowiedni encounter + zmieni� inty na private
        //s� public zeby moc testowac w unity

        Image background = bg.GetComponent<Image>();
        Image player = pl.GetComponent<Image>();
        Image enemy = en.GetComponent<Image>();
        Image boss = bo.GetComponent<Image>();
        RectTransform rt = canvas.GetComponent<RectTransform>();
        background.sprite = Background[bgNo];
        background.rectTransform.sizeDelta = new Vector2(background.sprite.texture.width, background.sprite.texture.height);
        if (background.sprite.texture.width < rt.rect.width && background.sprite.texture.height < rt.rect.height)
        {
            float ratio = Mathf.Max((rt.rect.width + 30) / background.sprite.texture.width, (rt.rect.height + 30) / background.sprite.texture.height);
            background.rectTransform.sizeDelta = new Vector2(bg.GetComponent<RectTransform>().rect.width * ratio, bg.GetComponent<RectTransform>().rect.height * ratio);
        }
        else if (background.sprite.texture.width < rt.rect.width)
        {
            float ratio = (rt.rect.width+30) / background.sprite.texture.width;
            background.rectTransform.sizeDelta = new Vector2(bg.GetComponent<RectTransform>().rect.width*ratio, bg.GetComponent<RectTransform>().rect.height*ratio);
        }
        else if(background.sprite.texture.height < rt.rect.height)
        {
            float ratio = (rt.rect.height+30) / background.sprite.texture.height;
            background.rectTransform.sizeDelta = new Vector2(bg.GetComponent<RectTransform>().rect.width * ratio, bg.GetComponent<RectTransform>().rect.height * ratio);
        }

        player.sprite = PlayerClass[plNo];
        if(isBoss == false)
        {
            en.SetActive(true);
            enemy.sprite = EnemyType[enNo];
        }
        else
        {
            bo.SetActive(true);
            boss.sprite = EnemyType[boNo];
        }
    }

}
