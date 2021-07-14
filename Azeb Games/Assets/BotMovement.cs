using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 150f;

    float horizontalMove = 60f;

    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
