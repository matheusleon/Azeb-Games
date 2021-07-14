using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 80f;

    float horizontalMove = 0f;

    bool jump = false;

    void Awake() 
    {
        AirConsole.instance.onMessage += onMessage;
    }

    void onMessage(int fromDevice, JToken data)
    {
        Debug.Log("message from " + fromDevice + ", data: " + data);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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

    void onDestroy()
    {
        if (AirConsole.instance != null){
            AirConsole.instance.onMessage -= onMessage;
        }
    }
}
