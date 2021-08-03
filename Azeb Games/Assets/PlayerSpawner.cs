using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	public GameObject player;
	public float spawnDelay;
	private float timeLeftToSpawn = 0;
	private string cloneSuffix = "(Clone)";

    // Update is called once per frame
    void Update()
    {
    	var playerName = player.name + cloneSuffix;
    	if (GameObject.Find(playerName) == null) {
    		if (timeLeftToSpawn <= 0) {
	        	Instantiate(player, transform.position, transform.rotation);
	        	timeLeftToSpawn = spawnDelay;
	        } else {
	        	timeLeftToSpawn -= Time.deltaTime;
	        }
    	} else {
    		timeLeftToSpawn = spawnDelay;
    	}
    }
}
