using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class CardPicker : MonoBehaviour
{
    [SerializeField] GameObject card1;
    [SerializeField] GameObject card2;
    [SerializeField] Sprite card1spr;
    [SerializeField] Sprite card2spr;
    [SerializeField] int sceneId;
    private Animator anim;
    private int choice = 0;
    private void Start() //zgarnia animator z PickCardBG
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void SetCardSprites() //wo�a� te funkcje �eby przypisa� odpowiednie sprity do kard do wyboru
    {
        card1.GetComponent<Image>().sprite = card1spr;
        card2.GetComponent<Image>().sprite = card2spr;
    }
    public void playanim() //odpala animacje, te funkje wo�a� po zabiciu przeciwnika (+ doda� jaka blokade zeby reszta ekranu nie dzia�a�a)
    {
        anim.SetTrigger("trigger");
    }
    public void pick(int a) //przypisuje wyb�r
    {
        choice = a;
    }
    public void ConfirmChoice() //wraca do mapy i dodaje karte do talii
    {
        if (choice != 0)
        {
            //doda� dodawanie do talii
            SceneManager.LoadScene(sceneId);
        }
    }
}
