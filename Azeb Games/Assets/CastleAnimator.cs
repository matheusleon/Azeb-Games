using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleAnimator : MonoBehaviour
{
    public int healthyThreshold;
    public int midThreshold;
    public int lowThreshold;

    public enum Team {
        blue,
        red
    }

    public enum CastleStatus {
        Heathy,
        Mid,
        Low,
        Destroyed,
    }

    private CastleStatus currentStatus;
    public Team team;

    // Start is called before the first frame update
    void Start()
    {
        this.currentStatus = CastleStatus.Heathy;
    }

    // Update is called once per frame
    void Update()
    {
        float health = gameObject.GetComponent<HasHealth>().health;
        string name = this.currentStatus.ToString() + " " + this.team.ToString();
        switch(this.currentStatus) {
            case CastleStatus.Heathy:
                if (health < this.healthyThreshold) {
                    Debug.Log("Destroying healthy castle game object");
                    GameObject currentSprite = gameObject.transform.GetChild(0).gameObject;
                    Destroy(currentSprite);
                    this.currentStatus = CastleStatus.Mid;
                }
                break;
            case CastleStatus.Mid:
                if (health < this.midThreshold) {
                    Debug.Log("Destroying mid castle game object");
                    GameObject currentSprite = gameObject.transform.GetChild(0).gameObject;
                    Destroy(currentSprite);
                    this.currentStatus = CastleStatus.Low;
                }
                break;
            case CastleStatus.Low:
                if (health < this.lowThreshold) {
                    Debug.Log("Destroying low castle game object");
                    GameObject currentSprite = gameObject.transform.GetChild(0).gameObject;
                    Destroy(currentSprite);
                    this.currentStatus = CastleStatus.Destroyed;     
                }       
                break;
            case CastleStatus.Destroyed:
            default:
                break;
        }
    }
}
