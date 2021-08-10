using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public Rigidbody2D rb;
    public LayerMask whatIsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        if(((1<<hitInfo.gameObject.layer) & whatIsEnemy) != 0)
	    { 
	    	hitInfo.GetComponent<HasHealth>().TakeDamage(damage);
        	Destroy(gameObject);
	    }
    }
}
