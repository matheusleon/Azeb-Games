using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb : MonoBehaviour
{
    public Animator animator;

    public float speed = 15f;
    public float timeToExplode;
    public int damage = 20;
    public Rigidbody2D rb;
    public LayerMask whatIsEnemy;
    public Transform explosionPos;
	public float explosionRange;
    private bool exploding = false;
    private float currentTime;

    void Start()
    {
        currentTime = timeToExplode;
    	float oneDirectionVelocity = (float) Math.Sqrt(1.0 / 2.0);
        rb.velocity = new Vector2(oneDirectionVelocity * transform.right.x, oneDirectionVelocity) * speed;
    }

    void Update()
    {
        animator.SetFloat("BombIntegrity", currentTime / timeToExplode);
        if (currentTime <= 0) {
            if (!exploding) {
                exploding = true;
        	    Explode(whatIsEnemy);
            }
        } else {
            currentTime -= Time.deltaTime;
    	}
    }

    void Explode(LayerMask attackLayer) {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(explosionPos.position, explosionRange, attackLayer);
		Debug.Log("Inimigos encontrados pela bomba: " + enemiesToDamage.Length);
		if (enemiesToDamage.Length > 0) {
			DoDamage(enemiesToDamage);
		}
        float explodingTime = 0.4f;
        Destroy(gameObject, explodingTime / 2.0f);
	}

    void DoDamage(Collider2D[] enemiesToDamage) {
		Debug.Log($"Damaging {enemiesToDamage.Length} enemies");
		for (int i = 0; i < enemiesToDamage.Length; i++) {
			enemiesToDamage[i].GetComponent<HasHealth>().TakeDamage(damage);
		}
    }
}
