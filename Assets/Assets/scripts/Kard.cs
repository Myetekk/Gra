using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class Kard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] GameObject CardDisplay;
    private Sprite DefaultSprite;
    private GameObject PickedUpCard;
    private RectTransform PickedUpRect;
    [SerializeField] Sprite TransparentSprite;
    [SerializeField] GameObject HandField;
    [SerializeField] GameObject CastSelfField;
    [SerializeField] GameObject CastEnemyField;
    private RectTransform HandFieldRect;
    private RectTransform CastSelfRect;
    private RectTransform CastEnemyRect;
    void Awake() //wykonuje si� przy powstaniu obiektu, czyli gdy gracz dobiera karte
    {
        DefaultSprite = gameObject.GetComponent<Image>().sprite;
        HandFieldRect = HandField.GetComponent<RectTransform>();
        CastSelfRect = CastSelfField.GetComponent<RectTransform>();
        CastEnemyRect = CastEnemyField.GetComponent<RectTransform>();
    }
    void Update() //wykonuje si� co update, nie za�mieca� tej fuknji bo bedzie lagowa�
    {
        if(PickedUpCard != null)
        {
            Vector2 mp = Input.mousePosition;
            PickedUpRect.transform.position = mp;          
        }
    }
    public void OnPointerDown(PointerEventData eventData) //lewy: podnosi karte, prawy: przykl�da si� karcie
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            PickedUpCard = Instantiate(gameObject, new Vector2(0, 0), Quaternion.identity, gameObject.transform);
            PickedUpCard.GetComponent<Image>().sprite = DefaultSprite;
            PickedUpRect = PickedUpCard.GetComponent<RectTransform>();
            PickedUpRect.pivot = new Vector2(0.5f, 0.5f);
            gameObject.GetComponent<Image>().sprite = TransparentSprite;
            if (CardDisplay.activeSelf)
            {
                CardDisplay.SetActive(false);
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            CardDisplay.GetComponent<Image>().sprite = DefaultSprite;
            CardDisplay.SetActive(true);
            gameObject.GetComponent<Image>().sprite = TransparentSprite;
        }
    }
    public void OnPointerUp(PointerEventData eventData) //lewy: zagrywa karte je�li wyci�gnie si� j� z r�ki, prawy: chowa przygl�danie si� karcie
    {   
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(!CardNotPlayed())
            {
                GetComponentInParent<KardKounter>().playCard(gameObject);
                Destroy(gameObject);
                //tutaj chyba tez efekty kart jakos musicie wcisnac, chyba ze ktos m lepszy pomys�
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = DefaultSprite;
            }
            Destroy(PickedUpCard);
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            gameObject.GetComponent<Image>().sprite = DefaultSprite;
            CardDisplay.SetActive(false);
        }

    }
    private bool CardNotPlayed()
    {

        if (HandFieldRect != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            return RectTransformUtility.RectangleContainsScreenPoint(HandFieldRect, mousePosition, null);
        }
        return false;
    }
    private bool CardCastOnSelf() //te dwa boole dodaje w razie jakbyscie chcieli wykozysta� mechanike wyboru celu, je�eli jej nie u�yjecie usu�cie to i usu�cie potrzebne to tego czesci w deklaracji zmiennych i Awake(), i usu�cie te dwa GameObjecty ze sceny Encounter, dzieci HandHandlera
    {

        if (CastSelfRect != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            return RectTransformUtility.RectangleContainsScreenPoint(CastSelfRect, mousePosition, null);
        }
        return false;
    }
    private bool CardCastOnEnemy()
    {

        if (CastEnemyRect != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            return RectTransformUtility.RectangleContainsScreenPoint(CastEnemyRect, mousePosition, null);
        }
        return false;
    }

    //WA�NE!!!!!!!!!!! ten skrypt kopiuje si� razem ze swoimi wszyskimi zmiennymi ka�dorazowo gdy gracz dobiera kart� i niszczy si� gdy j� zagrywa.
    //nie wiem jak mo�e to wp�ywa� na wydajno�� programu, je�eli kto� wpadnie na lepszy spos�b �eby rozwi�za� to (idk robi� to wszsytko w skrypcie na HandHandlerze jakos)
    //to zach�cam do zmiany
}
