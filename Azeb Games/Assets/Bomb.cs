using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb : MonoBehaviour
{
	public float speed = 15f;
    public float timeToExplode;
    public int damage = 20;
    public Rigidbody2D rb;
    public LayerMask whatIsEnemy;
    public Transform explosionPos;
	public float explosionRange;

    void Start()
    {
    	float oneDirectionVelocity = (float) Math.Sqrt(1.0 / 2.0);
        rb.velocity = new Vector2(oneDirectionVelocity * transform.right.x, oneDirectionVelocity) * speed;
    }

    void Update()
    {
        if (timeToExplode <= 0) {
        	Explode(whatIsEnemy);
    	} else {
        	timeToExplode -= Time.deltaTime;
    	}
    }

    void Explode(LayerMask attackLayer) {
		Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(explosionPos.position, explosionRange, attackLayer);
		Debug.Log("Inimigos encontrados pela bomba: " + enemiesToDamage.Length);
		if (enemiesToDamage.Length > 0) {
			DoDamage(enemiesToDamage);
		}
		// TODO: explosion animation
		Destroy(gameObject);
	}

    void DoDamage(Collider2D[] enemiesToDamage) {
		Debug.Log($"Damaging {enemiesToDamage.Length} enemies");
		for (int i = 0; i < enemiesToDamage.Length; i++) {
			enemiesToDamage[i].GetComponent<HasHealth>().TakeDamage(damage);
		}
    }
}
