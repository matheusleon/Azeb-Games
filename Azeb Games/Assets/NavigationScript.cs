using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class NavigationScript : MonoBehaviour
{
    GameObject mainMenu;
    GameObject optionsMenu;

    string[] mainMenuOptions = new string[2] {
        "Play2Button",
        "HowToPlayButton",
    };

    GameObject[] mainMenuSelectors;

    int mainMenuActiveOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize menu selectors (visual navigation)
        this.mainMenuSelectors = new GameObject[2] {
            GameObject.Find("PlaySelector"),
            GameObject.Find("HowToPlaySelector"),
        };
        for (int i = 1; i < 2; i++) {
            if (this.mainMenuSelectors[i] != null) {
                this.mainMenuSelectors[i].SetActive(false);
            }
        }

        // Initialize menu references
        mainMenu = GameObject.Find("MainMenu");
        optionsMenu = GameObject.Find("HowToPlayMenu");
        if (optionsMenu != null) {
            optionsMenu.SetActive(false);
        }
    }

    void Awake()
    {
        AirConsole.instance.onMessage += this.onMessage;
    }

    void onButtonClick(string buttonSelector)
    {
        var button = GameObject.Find(buttonSelector);
        if (button != null) {
            Debug.Log("entrando em: " + buttonSelector);
            var btnOnClick = button.GetComponent<Button>().onClick;
            btnOnClick.Invoke();
        } else {
            Debug.Log("Erro - botao nao encontrado");
        }
    }

    void setMainMenuSelectorVisibility(int option, bool isVisible) {
        var selector = this.mainMenuSelectors[option];
        if (selector != null) {
            selector.SetActive(isVisible);
        } else {
            Debug.Log("Nao consegui achar o GameObject do seletor do menu");
        }
    }

    void updateMainMenu(int add) {
        setMainMenuSelectorVisibility(this.mainMenuActiveOption, false);
        this.mainMenuActiveOption = (this.mainMenuActiveOption + add + this.mainMenuOptions.Length) % this.mainMenuOptions.Length;
        setMainMenuSelectorVisibility(this.mainMenuActiveOption, true);
    }

    void mainMenuHandler(int device_id, JToken data) {
        Debug.Log("Main menu menu");
        string element = data.Value<string>("element");
        JObject parsedData = data.Value<JObject>("data");
        if (element == "dpad") {
            if (parsedData["key"] == null) {
                return;
            }
            var key = parsedData["key"].ToString();
            var pressed = (int) parsedData["pressed"];
            if (pressed == 0) {
                return;
            }
            if (key == "up") {
                this.updateMainMenu(-1);
            } else if (key == "down") {
                this.updateMainMenu(1);
            }
        } else if (element == "attackButton") {
            var pressed = (int) parsedData["pressed"];
            if (pressed == 0) {
                return;
            }
            this.onButtonClick(this.mainMenuOptions[this.mainMenuActiveOption]);
        }
    }

    void optionsMenuHandler(int device_id, JToken data) {
        Debug.Log("OIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
        string element = data.Value<string>("element");
        JObject parsedData = data.Value<JObject>("data");
        if (element.ToString() == "attackButton") {
            var pressed = (int) parsedData["pressed"];
            if (pressed == 0) {
                return;
            }
            this.onButtonClick("HowToPlayBackButton");
        }
    }

    void onMessage(int device_id, JToken data)
    {
        Debug.Log("recebi: " + device_id.ToString());
        Debug.Log(data);
        
        if (this.mainMenu != null && this.mainMenu.activeInHierarchy) {
            mainMenuHandler(device_id, data);
        } else if (this.optionsMenu != null && this.optionsMenu.activeInHierarchy) {
            optionsMenuHandler(device_id, data);
        } else {
            Debug.Log("nao achei menu que preste");
        }
    }

    void onDisable()
    {
        if (AirConsole.instance != null) {
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
