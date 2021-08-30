using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{
    public float attackDelay;
    private float timeLeftToAttack;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public LayerMask whatIsEnemy;
    public int id;
    private int isShooting;

    void Start()
    {
        timeLeftToAttack = 0;
        AirConsole.instance.onMessage += onMessage;
    }

    void onMessage(int fromDevice, JToken data)
    {
        Debug.Log("message from weapon " + fromDevice + ", data: " + data);

        if (fromDevice != this.id)
        {
            return;
        }

        JObject data2 = data.Value<JObject>("data");

        string element = data.Value<string>("element");

        if (element == "attackButton")
        {
            isShooting = (int)data2["pressed"];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting == 1 && timeLeftToAttack <= 0)
        {
            Shoot();
            isShooting = 0;
            timeLeftToAttack = attackDelay;
        } else {
            timeLeftToAttack -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        var bullet_instance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet_instance.GetComponent<Bullet>().whatIsEnemy = this.whatIsEnemy;
    }

    void OnDestroy()
    {
        AirConsole.instance.onMessage -= onMessage;
    }
}
