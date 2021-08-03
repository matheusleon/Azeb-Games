using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public float health;
    public bool triggersGameOver = false;
    public Team team;

    public enum Team {
        Blue,
        Red
    }

    // Start is called before the first frame update
    public void TakeDamage(int damage) {
    	health -= damage;
    	
        Debug.Log("damage taken");
        
        if (health <= 0) {
    		Destroy(gameObject);
            Debug.Log("Game object died");
    	}
    }

    public void OnDestroy() {
        if (triggersGameOver) {
            string sceneName = null;
            if (this.team == Team.Blue)
                sceneName = "GameOverRedWins";            
            if (this.team == Team.Red)
                sceneName = "GameOverBlueWins";
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}
