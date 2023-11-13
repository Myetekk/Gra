using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInputs : MonoBehaviour
{
    private Animator _animator;


    void Start() //gets the animator component of the game object its attached to
    {
        _animator = gameObject.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Detects every game updater if the escape key was pressed, used for menu navigation
        {
            Debug.Log("escape pressed key was pressed");
            _animator.SetTrigger("RTPS"); //Return To Previous State
        }
    }
}
