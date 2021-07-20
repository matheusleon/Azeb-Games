using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    public void TakeDamage(int damage) {
    	health -= damage;
    	
        Debug.Log("damage taken");
        
        if (health <= 0) {
    		Destroy(gameObject);
            Debug.Log("Game object died");
    	}
    }
}
