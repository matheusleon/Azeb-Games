using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestSpawnerController : MonoBehaviour
{
    private DateTime lastChestSpawned = DateTime.Now;
    public static int chestCount = 0;
    public int spawnTimeInSeconds = 30;
    public GameObject chest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var now = DateTime.Now;

        if ((now - lastChestSpawned).TotalSeconds > spawnTimeInSeconds && chestCount == 0) {
            Instantiate(chest, chest.transform.position, chest.transform.rotation);
            lastChestSpawned = now;
            chestCount++;
        }
    }
}
