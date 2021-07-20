using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
	public float attackDelay;
	private float timeLeftToAttack;
	public bool alwaysAttacking;
	public Transform attackPos;
	public LayerMask whatIsEnemy;
	public LayerMask finalAttackLayer;
	public float attackRange;
	public int damage;

    // Update is called once per frame
    void Update() {
        if (timeLeftToAttack <= 0) {
            if (alwaysAttacking) {
							PerformAttack(this.whatIsEnemy);
            } else if (Input.GetKey(KeyCode.E)) {
							PerformAttack(this.whatIsEnemy);
            }
        } else {
        	timeLeftToAttack -= Time.deltaTime;
        }
    }

		void PerformAttack(LayerMask attackLayer) {
			Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, attackLayer);
			if (enemiesToDamage.Length > 0) {
					DoDamage(enemiesToDamage, damage);
					timeLeftToAttack = attackDelay;
			}
		}

    void DoDamage(Collider2D[] enemiesToDamage, int damage) {
			Debug.Log($"Damaging {enemiesToDamage.Length} enemies");
			for (int i = 0; i < enemiesToDamage.Length; i++) {
					enemiesToDamage[i].GetComponent<HasHealth>().TakeDamage(damage);
			}
    }

		void OnDestroy() {
			if (alwaysAttacking)
				PerformAttack(this.finalAttackLayer);
		}

    void OnDrawGizmosSelected() {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
