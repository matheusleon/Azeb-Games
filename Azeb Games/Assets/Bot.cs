using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 150f;

    public bool direction;

    float horizontalMove = 60f;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {    	
    	if (direction) {
    		horizontalMove = runSpeed;
    	} else {
    		horizontalMove = -runSpeed;
    	}
        animator.SetFloat("Moving", Mathf.Abs(horizontalMove));
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
