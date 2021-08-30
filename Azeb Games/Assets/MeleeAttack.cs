using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

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
	public int isAttacking;
	public int id;

	public Animator animator;

	void Start()
	{
		AirConsole.instance.onMessage += onMessage;
	}

	// Update is called once per frame
	void Update() {
        if (timeLeftToAttack <= 0) {
            if (alwaysAttacking) {
				PerformAttack(this.whatIsEnemy);
            } else if (isAttacking == 1) {
				PerformAttack(this.whatIsEnemy);
            }
        } else {
        	timeLeftToAttack -= Time.deltaTime;
        }
    }

    void onMessage(int fromDevice, JToken data)
    {
        Debug.Log("message from attack " + fromDevice + ", data: " + data);

		if (fromDevice != this.id)
		{
			return;
		}

		JObject data2 = data.Value<JObject>("data");

        string element = data.Value<string>("element");

		if (element == "attackButton")
        {
			isAttacking = (int)data2["pressed"];
		}
    }

    void PerformAttack(LayerMask attackLayer) {
		Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, attackLayer);
		if (enemiesToDamage.Length > 0) {
			DoDamage(enemiesToDamage);
			timeLeftToAttack = attackDelay;
		}
		animator.SetBool("Attacking", enemiesToDamage.Length > 0);
	}

    void DoDamage(Collider2D[] enemiesToDamage) {
		Debug.Log($"Damaging {enemiesToDamage.Length} enemies");
		for (int i = 0; i < enemiesToDamage.Length; i++) {
			enemiesToDamage[i].GetComponent<HasHealth>().TakeDamage(damage);
		}
    }

	void OnDestroy() {
		if (alwaysAttacking)
			PerformAttack(this.finalAttackLayer);

		if (AirConsole.instance != null)
		{
			AirConsole.instance.onMessage -= onMessage;
		}
	}

	void OnDrawGizmosSelected() {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
