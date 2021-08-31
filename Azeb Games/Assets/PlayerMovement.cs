using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public CharacterController2D controller;

    public float runSpeed = 80f;
    float horizontalMove = 0f;

    bool jump = false;
    bool crouch = false;

    int isMovingRight = 0;
    int isMovingLeft = 0;
    int isJumping = 0;
    int isCrouching = 0;
    public int id = -10;

    public int bombCount = 0;

    void Start() 
    {
        AirConsole.instance.onMessage += onMessage;
    }

    void onMessage(int fromDevice, JToken data)
    {

        if (fromDevice != this.id) {
            return;
        }

        Debug.Log("message from " + fromDevice + ", my id " + this.id);

        Debug.Log("message from " + fromDevice + ", data: " + data);

        JObject data2 = data.Value<JObject>("data");

        string element = data.Value<string>("element");

        if (element == "dpad")
        {
            if (data2["key"] != null && data2["key"].ToString() == "right")
            {
                isMovingRight = (int)data2["pressed"];
            }
            if (data2["key"] != null && data2["key"].ToString() == "left")
            {
                isMovingLeft = (int)data2["pressed"];
            }
            if (data2["key"] != null && data2["key"].ToString() == "up")
            {
                isJumping = (int)data2["pressed"];
            }
            if (data2["key"] != null && data2["key"].ToString() == "down")
            {
                isCrouching = (int)data2["pressed"];
            }
        } 
        else if (element == "jump")
        {
            Debug.Log("Entrou no pular, � isso" + (int)data2["pressed"]);
            isJumping = (int)data2["pressed"];
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = (isMovingRight - isMovingLeft) * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (isJumping == 1 && !jump)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (isCrouching == 1)
        {
            crouch = true;
        } else if (isCrouching == 0)
        {
            crouch = false;
        }
    }

    public void onLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); 
        jump = false;
    }

    void OnDestroy()
    {
        if (AirConsole.instance != null){
            AirConsole.instance.onMessage -= onMessage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            string item = collision.gameObject.GetComponent<CollectableScript>().itemType;
            if (item == "Bomb")
            {
                // Debug.Log("Pegou");
                bombCount = bombCount + 1;
                BombThrower bombThrower = GetComponent<BombThrower>();
                bombThrower.addBomb();
            } 
            else if (item == "Weapon")
            {
                transform.Find("Wand").gameObject.SetActive(true);
                Weapon weapon = GetComponent<Weapon>();
                weapon.updateWeapon();
                gameObject.GetComponent<MeleeAttack>().enabled = false;
                gameObject.GetComponent<Weapon>().enabled = true;
            }
        }
    }
}
