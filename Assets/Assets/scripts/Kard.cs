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
    void Awake() //wykonuje siê przy powstaniu obiektu, czyli gdy gracz dobiera karte
    {
        DefaultSprite = gameObject.GetComponent<Image>().sprite;
        HandFieldRect = HandField.GetComponent<RectTransform>();
        CastSelfRect = CastSelfField.GetComponent<RectTransform>();
        CastEnemyRect = CastEnemyField.GetComponent<RectTransform>();
    }
    void Update() //wykonuje siê co update, nie zaœmiecaæ tej fuknji bo bedzie lagowaæ
    {
        if(PickedUpCard != null)
        {
            Vector2 mp = Input.mousePosition;
            PickedUpRect.transform.position = mp;          
        }
    }
    public void OnPointerDown(PointerEventData eventData) //lewy: podnosi karte, prawy: przykl¹da siê karcie
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
    public void OnPointerUp(PointerEventData eventData) //lewy: zagrywa karte jeœli wyci¹gnie siê j¹ z rêki, prawy: chowa przygl¹danie siê karcie
    {   
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(!CardNotPlayed())
            {
                GetComponentInParent<KardKounter>().playCard(gameObject);
                Destroy(gameObject);
                //tutaj chyba tez efekty kart jakos musicie wcisnac, chyba ze ktos m lepszy pomys³
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
    private bool CardCastOnSelf() //te dwa boole dodaje w razie jakbyscie chcieli wykozystaæ mechanike wyboru celu, je¿eli jej nie u¿yjecie usuñcie to i usuñcie potrzebne to tego czesci w deklaracji zmiennych i Awake(), i usuñcie te dwa GameObjecty ze sceny Encounter, dzieci HandHandlera
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

    //WA¯NE!!!!!!!!!!! ten skrypt kopiuje siê razem ze swoimi wszyskimi zmiennymi ka¿dorazowo gdy gracz dobiera kartê i niszczy siê gdy j¹ zagrywa.
    //nie wiem jak mo¿e to wp³ywaæ na wydajnoœæ programu, je¿eli ktoœ wpadnie na lepszy sposób ¿eby rozwi¹zaæ to (idk robiæ to wszsytko w skrypcie na HandHandlerze jakos)
    //to zachêcam do zmiany
}
