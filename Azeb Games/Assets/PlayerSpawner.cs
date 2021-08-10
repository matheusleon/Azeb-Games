using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	public GameObject player;
	public float spawnDelay;
	private float timeLeftToSpawn = 0;
	private string cloneSuffix = "(Clone)";
	public int id = -10;
	GameObject player_instance;

	// Update is called once per frame
	void Update()
	{
		var playerName = player.name + cloneSuffix;
		if (GameObject.Find(playerName) == null) {
			if (timeLeftToSpawn <= 0) {
				player_instance = Instantiate(player, transform.position, transform.rotation);

                timeLeftToSpawn = spawnDelay;
			} else {
				timeLeftToSpawn -= Time.deltaTime;
			}
		} else {
			timeLeftToSpawn = spawnDelay;
		}

		if (player_instance != null)
        {
			player_instance.GetComponent<PlayerMovement>().id = this.id;
			player_instance.GetComponent<MeleeAttack>().id = this.id;

			Debug.Log("Setando player = " + playerName);
			Debug.Log(player_instance.GetComponent<PlayerMovement>().id + " O QUE = " + this.id);
		}

	}
}
