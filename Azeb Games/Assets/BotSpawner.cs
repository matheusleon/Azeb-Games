using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
	public GameObject bot;
	public float spawnTime;
	public float spawnDelay;	

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBot", spawnTime, spawnDelay);
    }

    public void SpawnBot() {
    	Instantiate(bot, transform.position, transform.rotation);
    }    
}
