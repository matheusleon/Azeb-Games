using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 150f;

    public bool direction;

    public float health;

    float horizontalMove = 60f;

    // Update is called once per frame
    void Update()
    {
    	if (health <= 0) {
    		Destroy(gameObject);
    	}
    	
    	if (direction) {
    		horizontalMove = runSpeed;
    	} else {
    		horizontalMove = -runSpeed;
    	}
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

    public void TakeDamage(int damage) {
    	health -= damage;
    	Debug.Log("damage taken");
    }
}
