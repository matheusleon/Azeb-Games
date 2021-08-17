using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public float health;
    public bool triggersGameOver = false;
    public Team team;

    public Animator animator;
    private bool died = false;
    public bool animateDeath;

    public enum Team {
        Blue,
        Red
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
    	
        Debug.Log("damage taken, current health is " + health.ToString());

        float dyingTime = 0;
        if (health <= 0 && !died)
        {
            if (animateDeath) {
                died = true;
                Destroy(gameObject.GetComponent<BoxCollider2D>());
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                if (HasParameter("Dying", animator))
                {
                    animator.SetTrigger("Dying");
                    dyingTime = 1.5f;
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
                sceneName = "GameOverRedWins";            
            if (this.team == Team.Red)
                sceneName = "GameOverBlueWins";
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}
