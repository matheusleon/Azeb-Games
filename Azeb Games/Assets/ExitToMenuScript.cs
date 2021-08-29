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
        AirConsole.instance.onMessage += this.onMessage;
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
            SceneManager.LoadScene(0);
        }
    }

    void onDisable()
    {
        if (AirConsole.instance != null) {
            AirConsole.instance.onMessage -= this.onMessage;
        }
    }
}
