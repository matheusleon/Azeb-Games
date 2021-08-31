using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestSpawnerController : MonoBehaviour
{
    public GameObject chest;
    public double spawnDelay;
    private double timeLeftToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeLeftToSpawn = spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        var now = DateTime.Now;

		if (GameObject.Find("ChestOpener(Clone)") == null) {
			if (timeLeftToSpawn <= 0) {
				Instantiate(chest, chest.transform.position, chest.transform.rotation);
                timeLeftToSpawn = spawnDelay;
			} else {
				timeLeftToSpawn -= Time.deltaTime;
			}
		} else {
			timeLeftToSpawn = spawnDelay;
		}
    }
}
