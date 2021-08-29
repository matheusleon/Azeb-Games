using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class LobbyPlayerConnector : MonoBehaviour
{
    public static int PLAYER_LEFT_ID = -1;
    public static int PLAYER_RIGHT_ID = -1;
    public static Image PLAYER_LEFT_IMAGE;
    public static Image PLAYER_RIGHT_IMAGE;
    public static bool LOBBY_ACTIVE = false;

    void Start()
    {
        AirConsole.instance.onMessage += this.onMessage;
        DontDestroyOnLoad(this.gameObject);
        
        GameObject playerLeftImg = GameObject.Find("Player1JoinButton");
        if (playerLeftImg != null) {
            LobbyPlayerConnector.PLAYER_LEFT_IMAGE = playerLeftImg.GetComponent<Image>();
        }
        GameObject playerRightImg = GameObject.Find("Player2JoinButton");
        if (playerRightImg != null) {
            LobbyPlayerConnector.PLAYER_RIGHT_IMAGE = playerRightImg.GetComponent<Image>();
        }

        LobbyPlayerConnector.LOBBY_ACTIVE = true;
    }

    void Awake()
    {
        LobbyPlayerConnector.LOBBY_ACTIVE = true;
    }

    public static void updateButtonColor(Image image, Color color) {
        if (image != null) {
            image.color = color;
        } else {
            Debug.Log("nunca que acha a imagem");
        }
    }

    void onPlayerJoin(int deviceId) {
        if (LobbyPlayerConnector.PLAYER_LEFT_ID == -1)
        {
            LobbyPlayerConnector.PLAYER_LEFT_ID = deviceId;
            LobbyPlayerConnector.updateButtonColor(LobbyPlayerConnector.PLAYER_LEFT_IMAGE, Color.green);
        }
        else if (LobbyPlayerConnector.PLAYER_RIGHT_ID == -1)
        {
            LobbyPlayerConnector.PLAYER_RIGHT_ID = deviceId;
            LobbyPlayerConnector.updateButtonColor(LobbyPlayerConnector.PLAYER_RIGHT_IMAGE, Color.green);
        } else {
            Debug.Log("entrei em lugar nenhum doido");
        }

        if (LobbyPlayerConnector.PLAYER_LEFT_ID != -1 && LobbyPlayerConnector.PLAYER_RIGHT_ID != -1) {
            Debug.Log("começando jogo");
            LobbyPlayerConnector.LOBBY_ACTIVE = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void onPlayerExit(int deviceId) {
        if (LobbyPlayerConnector.PLAYER_LEFT_ID == deviceId)
        {
            LobbyPlayerConnector.PLAYER_LEFT_ID = -1;
            LobbyPlayerConnector.updateButtonColor(LobbyPlayerConnector.PLAYER_LEFT_IMAGE, Color.red);
        }
        else if (LobbyPlayerConnector.PLAYER_RIGHT_ID == deviceId)
        {
            LobbyPlayerConnector.PLAYER_RIGHT_ID = -1;
            LobbyPlayerConnector.updateButtonColor(LobbyPlayerConnector.PLAYER_RIGHT_IMAGE, Color.red);
        }
    }

    bool hasPlayerJoined(int deviceId) {
        return LobbyPlayerConnector.PLAYER_LEFT_ID == deviceId || LobbyPlayerConnector.PLAYER_RIGHT_ID == deviceId;
    }

    void onMessage(int device_id, JToken data)
    {
        if (!LobbyPlayerConnector.LOBBY_ACTIVE) {
            return;
        }
        Debug.Log("RECEBI NO LOBBY: " + device_id.ToString());
        Debug.Log(data);

        string element = data.Value<string>("element");
        JObject parsedData = data.Value<JObject>("data");

        if (element.ToString() == "attackButton") {
            var pressed = (int) parsedData["pressed"];
            if (pressed == 0) {
                return;
            }
            if (this.hasPlayerJoined(device_id)) {
                this.onPlayerExit(device_id);
            } else {
                this.onPlayerJoin(device_id);
            }
        } else if (element.ToString() == "special") {
            // Go back to main menu
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    void onDisable()
    {
        if (AirConsole.instance != null) {
            Debug.Log("tirei o onMessage do lobby");
            AirConsole.instance.onMessage -= this.onMessage;
        }
    }

    void onDestroy()
    {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= this.onMessage;
        }
    }
}
