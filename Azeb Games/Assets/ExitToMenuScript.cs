using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class ExitToMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += this.OnSceneLoaded;
        SceneManager.sceneUnloaded += this.OnSceneUnloaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOverBlueWins" || scene.name == "GameOverRedWins") {
            AirConsole.instance.onMessage += this.onMessage;
        }
    }

    void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == "GameOverBlueWins" || scene.name == "GameOverRedWins") {
            AirConsole.instance.onMessage -= this.onMessage;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= this.OnSceneLoaded;
        SceneManager.sceneUnloaded -= this.OnSceneUnloaded;
    }

    void onMessage(int device_id, JToken data)
    {
        string element = data.Value<string>("element");
        JObject parsedData = data.Value<JObject>("data");

        if (element.ToString() == "attackButton") {
            var pressed = (int) parsedData["pressed"];
            if (pressed == 0) {
                return;
            }
            if (AirConsole.instance != null) {
                AirConsole.instance.onMessage -= this.onMessage;
            }
            SceneManager.LoadScene(0);
        }
    }
}
