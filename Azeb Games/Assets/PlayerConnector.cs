using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;

public class PlayerConnector : MonoBehaviour
{
    public int id_left = LobbyPlayerConnector.PLAYER_LEFT_ID;
    public int id_right = LobbyPlayerConnector.PLAYER_RIGHT_ID;

    void Start()
    {
        // AirConsole.instance.onConnect += onConnect;
        GameObject player_left = GameObject.Find("PlayerSpawnerLeft");
        if (player_left != null)
        {
            player_left.GetComponent<PlayerSpawner>().id = LobbyPlayerConnector.PLAYER_LEFT_ID;
        }
        
        GameObject player_right = GameObject.Find("PlayerSpawnerRight");
        if (player_right != null)
        {
            player_right.GetComponent<PlayerSpawner>().id = LobbyPlayerConnector.PLAYER_RIGHT_ID;
        }

        Debug.Log("CARREGUEI!");
        Debug.Log(LobbyPlayerConnector.PLAYER_LEFT_ID);
        Debug.Log(LobbyPlayerConnector.PLAYER_RIGHT_ID);
    }

    /*
    void onConnect(int deviceId)
    {
        Debug.Log("Connecting device " + deviceId.ToString());
        if (this.id_left == -1)
        {
            this.id_left = deviceId;
            GameObject player_left = GameObject.Find("PlayerSpawnerLeft");
            if (player_left != null)
            {
                player_left.GetComponent<PlayerSpawner>().id = this.id_left;
            }
        }
        else if (id_right == -1)
        {
            this.id_right = deviceId;
            GameObject player_right = GameObject.Find("PlayerSpawnerRight");
            if (player_right != null)
            {
                player_right.GetComponent<PlayerSpawner>().id = this.id_right;
            }
        }
    }
    */
}
