using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerStart : MonoBehaviour
{
    void spawnPlayer(int id, string gameObject) {
		GameObject player = GameObject.Find(gameObject);
		if (player != null)
		{
			player.GetComponent<PlayerSpawner>().id = id;
		} else
		{
			Debug.Log("Falha para spawnar player de id " + id.ToString());
		}
	}

	void Start()
	{
		spawnPlayer(LobbyPlayerConnector.PLAYER_LEFT_ID, "PlayerSpawnerLeft");
		spawnPlayer(LobbyPlayerConnector.PLAYER_RIGHT_ID, "PlayerSpawnerRight");
	}
}
