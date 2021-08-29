using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public int health;
    public HealthBar healthBar;
    public bool triggersGameOver = false;
    public bool isBot = false;
    public Team team;

    public Animator animator;
    private bool died = false;
    public float dyingTime;
    public bool animateDeath;

    public enum Team {
        Blue,
        Red
    }

    void Start() {
        if (!isBot) healthBar.setMaxHealth(health);
    }

    public static bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }


    // Start is called before the first frame update
    public void TakeDamage(int damage) {
    	health -= damage;
        if (!isBot) healthBar.setHealth(health);
    	
        Debug.Log("damage taken, current health is " + health.ToString());

        if (health <= 0 && !died)
        {
            if (animateDeath) {
                died = true;
                Destroy(gameObject.GetComponent<BoxCollider2D>());
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                if (HasParameter("Dying", animator))
                {
                    animator.SetTrigger("Dying");
                }
                Debug.Log("Game object died");
            }

            Destroy(gameObject, dyingTime);
        }
    }

    public void OnDestroy() {
        if (triggersGameOver) {
            string sceneName = null;
            if (this.team == Team.Blue)
                sceneName = "GameOverBlueWins";            
            if (this.team == Team.Red)
                sceneName = "GameOverRedWins";
            SceneManager.LoadScene(sceneName);
        }
    }
}
