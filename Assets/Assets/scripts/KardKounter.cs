using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class KardKounter : MonoBehaviour
{
    [SerializeField] GameObject card;
    public List<GameObject> hand;
    public float angleBetweenCards = 5;

    public void updateHand()
    {
        float angle = (hand.Count - 1) * -(angleBetweenCards/2);
        for(int i = 0; i < hand.Count; i++)
        {
            
            hand[i].transform.Rotate(0, 0, -1* hand[i].transform.localRotation.eulerAngles.z + angle);
            angle += angleBetweenCards;
        
        
        
        
        
        }
    }


}
