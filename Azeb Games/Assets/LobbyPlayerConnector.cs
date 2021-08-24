using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyPlayerConnector : MonoBehaviour
{
    public static int PLAYER_LEFT_ID = -1;
    public static int PLAYER_RIGHT_ID = -1;
    public static Button PLAYER_LEFT_BUTTON;
    public static Button PLAYER_RIGHT_BUTTON;

    void Start()
    {
        AirConsole.instance.onConnect += this.onConnect;
        DontDestroyOnLoad(this.gameObject);
        
        GameObject playerLeftBtn = GameObject.Find("Player1JoinButton");
        if (playerLeftBtn != null) {
            LobbyPlayerConnector.PLAYER_LEFT_BUTTON = playerLeftBtn.GetComponent<Button>();
        }
        GameObject playerRightBtn = GameObject.Find("Player2JoinButton");
        if (playerRightBtn != null) {
            LobbyPlayerConnector.PLAYER_RIGHT_BUTTON = playerRightBtn.GetComponent<Button>();
        }
    }

    public static void updateButtonColor(Button button, Color color) {
        if (button != null) {
            var colors = button.colors;
            colors.normalColor = color;
            button.colors = colors;
        }
    }

    void onPlayerJoin(int deviceId) {
        if (LobbyPlayerConnector.PLAYER_LEFT_ID == -1)
        {
            LobbyPlayerConnector.PLAYER_LEFT_ID = deviceId;
            LobbyPlayerConnector.updateButtonColor(LobbyPlayerConnector.PLAYER_LEFT_BUTTON, Color.green);
        }
        else if (LobbyPlayerConnector.PLAYER_RIGHT_ID == -1)
        {
            LobbyPlayerConnector.PLAYER_RIGHT_ID = deviceId;
            LobbyPlayerConnector.updateButtonColor(LobbyPlayerConnector.PLAYER_RIGHT_BUTTON, Color.green);
        }

        if (LobbyPlayerConnector.PLAYER_LEFT_ID != -1 && LobbyPlayerConnector.PLAYER_RIGHT_ID != -1) {
            Debug.Log("começando jogo");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void onPlayerExit(int deviceId) {
        if (LobbyPlayerConnector.PLAYER_LEFT_ID == deviceId)
        {
            LobbyPlayerConnector.PLAYER_LEFT_ID = -1;
            LobbyPlayerConnector.updateButtonColor(LobbyPlayerConnector.PLAYER_LEFT_BUTTON, Color.red);
        }
        else if (LobbyPlayerConnector.PLAYER_RIGHT_ID == deviceId)
        {
            LobbyPlayerConnector.PLAYER_RIGHT_ID = -1;
            LobbyPlayerConnector.updateButtonColor(LobbyPlayerConnector.PLAYER_RIGHT_BUTTON, Color.red);
        }
    }

    void onConnect(int deviceId)
    {
        Debug.Log("CONECTOU NO LOBBY " + deviceId.ToString());
        if (LobbyPlayerConnector.PLAYER_LEFT_ID == -1)
        {
            LobbyPlayerConnector.PLAYER_LEFT_ID = deviceId;
        }
        else if (LobbyPlayerConnector.PLAYER_RIGHT_ID == -1)
        {
            LobbyPlayerConnector.PLAYER_RIGHT_ID = deviceId;
        }

        if (LobbyPlayerConnector.PLAYER_LEFT_ID != -1 && LobbyPlayerConnector.PLAYER_RIGHT_ID != -1) {
            Debug.Log("começando jogo");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void onDisconnect(int deviceId)
    {
        Debug.Log("DEVICE " + deviceId.ToString() + " SAIU DO LOBBY");
        if (LobbyPlayerConnector.PLAYER_LEFT_ID == deviceId) {
            LobbyPlayerConnector.PLAYER_LEFT_ID = -1;
        } else {
            LobbyPlayerConnector.PLAYER_RIGHT_ID = -1;
        }
    }

    void onDestroy()
    {
        if (AirConsole.instance != null) {
            AirConsole.instance.onConnect -= this.onConnect;
        }
    }
}
