using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInputs : MonoBehaviour
{
    private Animator _animator;


    void Start() //gets the animator component of the game object its attached to
    {//nie wiem czemu opis tego jest po ang 
        _animator = gameObject.GetComponent<Animator>();
    }


    void Update() //dodaje funkcjonalnoœæ guzika escape, mozna tutaj dodawaæ wiêcej guzików, obecnie dzia³¹ tylko w WelcomeScreen (chyba)
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Detects every game updater if the escape key was pressed, used for menu navigation
        {
            _animator.SetTrigger("RTPS"); //Return To Previous State
        }
    }
}
