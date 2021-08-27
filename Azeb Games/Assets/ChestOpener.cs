using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject ChestClosed, ChestOpened;
    public GameObject Bomb, Weapon;

    // Start is called before the first frame update
    void Start()
    {
        ChestClosed.SetActive(true);
        ChestOpened.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ChestClosed.SetActive(false);
        ChestOpened.SetActive(true);

        double item = Random.Range(0.0f, 1.0f);
        if (item <= 0.7)
        {
            Instantiate(Bomb, transform.position + new Vector3(0, 0 + 6, 0), Quaternion.identity);
        } 
        else
        {
            Instantiate(Weapon, transform.position + new Vector3(0, 0 + 6, 0), Quaternion.identity);
        }

        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        ChestClosed.SetActive(true);
        ChestOpened.SetActive(false);
    }

    void OnDestroy() {
        ChestSpawnerController chestController = Object.FindObjectOfType<ChestSpawnerController>();
        chestController.chestCount -= 1;
    }
}
