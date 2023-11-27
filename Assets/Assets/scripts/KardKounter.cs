using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class KardKounter : MonoBehaviour
{
    [SerializeField] GameObject card;
    public List<GameObject> hand = new List<GameObject>();
    public float angleBetweenCards = 5;

    public void updateHand() //rodziela po r�wno karty w r�ce
    {
        float angle = (hand.Count - 1) * -(angleBetweenCards/2);
        for(int i = 0; i < hand.Count; i++)
        {
            
            hand[i].transform.Rotate(0, 0, -1* hand[i].transform.localRotation.eulerAngles.z + angle);
            angle += angleBetweenCards;
        }
    }

    public void drawCard() //TA FUNKCJA JEST PRZYPI�TA DO GUZIKA ZNAJDUJ�CEGO SI� W PIERWSZYM KRYSZTALE MANY W CELU TESTOWANIA. USUN�� GUZIK PO SKO�CZENIU TEST�W!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        if(hand.Count < 10) //max ilosc kart na r�ce, mozna zmienic jak uwazacie
        {
            var obj = Instantiate(card, transform.position, Quaternion.identity, gameObject.transform);
            //tutaj dodawa� wszystkie zminy do podstawowej karty (sprite, i takie tam)
            hand.Add(obj);
        }
        updateHand();
    }

    public void playCard(GameObject a) //no wiadomo raczej 
    {
        hand.Remove(a);
        updateHand();
    }
}
