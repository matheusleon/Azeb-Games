using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
	public float attackDelay;
	private float timeLeftToAttack;

	public Transform attackPos;
	public LayerMask whatIsEnemy;
	public float attackRange;
	public int damage;

    // Update is called once per frame
    void Update() {
        if (timeLeftToAttack <= 0) {
        	if (Input.GetKey(KeyCode.E)) {
        		Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        		Debug.Log($"Found {enemiesToDamage.Length} enemies to damage.");
						for (int i = 0; i < enemiesToDamage.Length; i++) {
        			enemiesToDamage[i].GetComponent<HasHealth>().TakeDamage(damage);
        		}
        	    timeLeftToAttack = attackDelay;
            }
        } else {
        	timeLeftToAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected() {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
