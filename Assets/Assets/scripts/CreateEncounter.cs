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
    public int bgNo, plNo, enNo, boNo; //bg number (numer sprite t³a), player number (klasa postaci), enemy number (rodzaj przeciwnika), boss number (rodzaj bossa)
    public bool isBoss;
    public void ajhsbckjh()
    { 
        //tutaj nadaæ wartoœci bgNo, plNo, enNo pochodz¹ce z zapisanych inforamcji mapy, aby stworzyæ odpowiedni encounter + zmieniæ inty na private
        //s¹ public zeby moc testowac w unity

        Image background = bg.GetComponent<Image>();
        Image player = pl.GetComponent<Image>();
        Image enemy = en.GetComponent<Image>();
        Image boss = bo.GetComponent<Image>();
        RectTransform rt = canvas.GetComponent<RectTransform>();
        background.sprite = Background[bgNo];
        background.rectTransform.sizeDelta = new Vector2(background.sprite.texture.width, background.sprite.texture.height);
        if (background.sprite.texture.width < rt.rect.width && background.sprite.texture.height < rt.rect.height) //ten ca³y syf poni¿ej rozci¹ga odpowiednio t³o, tak ¿eby mo¿na by³o wrzucic jakikolwiek +/- prostok¹tny obraz na t³o i bedzie dzia³aæ
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
        //a¿ do t¹d, poni¿ej tego ustawia sprite gracza i przeciwnika (chyba nie sprawdza³em czy boss dzia³a dobrze)
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
