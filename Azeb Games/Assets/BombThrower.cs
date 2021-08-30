using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class BombThrower : MonoBehaviour
{
	public float attackDelay;
    private float timeLeftToAttack;
    public Transform bombPoint;
    public GameObject bombPrefab;
    public LayerMask whatIsEnemy;
    public int id;
    private int isThrowing;
    private int bombCount;

    void Start()
    {
        timeLeftToAttack = 0;
        bombCount = 0;
        AirConsole.instance.onMessage += onMessage;
    }

    void onMessage(int fromDevice, JToken data)
    {
        Debug.Log("message from bomb " + fromDevice + ", data: " + data);

        if (fromDevice != this.id)
        {
            return;
        }

        JObject data2 = data.Value<JObject>("data");

        string element = data.Value<string>("element");

        if (element == "special" && bombCount > 0)
        {
            isThrowing = (int)data2["pressed"];
        }
    }

    void Update()
    {
        if (isThrowing == 1 && timeLeftToAttack <= 0)
        {
            Throw();
            isThrowing = 0;
            bombCount--;
            timeLeftToAttack = attackDelay;
        } else {
            timeLeftToAttack -= Time.deltaTime;
        }
    }

    void Throw() {
    	var bomb_instance = Instantiate(bombPrefab, bombPoint.position, bombPoint.rotation);
        bomb_instance.GetComponent<Bomb>().whatIsEnemy = this.whatIsEnemy;
    }

    public void addBomb()
    {
        bombCount++;
        Debug.Log("bombCount = " + bombCount);
    }

    void OnDestroy()
    {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= this.onMessage;
        }
    }
}
