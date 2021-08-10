using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;

public class PlayerSpawner : MonoBehaviour
{
	public GameObject player;
	public float spawnDelay;
	private float timeLeftToSpawn = 0;
	private string cloneSuffix = "(Clone)";
	public int id = -1;

    void Start() 
    {
        AirConsole.instance.onConnect += onConnect;
    }

    void onConnect(int deviceId) {
        Debug.Log("Connecting device " + deviceId.ToString());
        if (this.id == -1) {
            this.id = deviceId;
        }
    }

	// Update is called once per frame
	void Update()
	{
		var playerName = player.name + cloneSuffix;
		if (GameObject.Find(playerName) == null) {
			if (timeLeftToSpawn <= 0) {
					Instantiate(player, transform.position, transform.rotation);
					player.GetComponent<PlayerMovement>().id = this.id;
                    timeLeftToSpawn = spawnDelay;
				} else {
					timeLeftToSpawn -= Time.deltaTime;
				}
		} else {
			timeLeftToSpawn = spawnDelay;
		}
	}
}
