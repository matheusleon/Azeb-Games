using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;

public class PlayerConnector : MonoBehaviour
{
    public int id_left = -10;
    public int id_right = -10;

    void Start()
    {
        AirConsole.instance.onConnect += onConnect;
    }

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
}
