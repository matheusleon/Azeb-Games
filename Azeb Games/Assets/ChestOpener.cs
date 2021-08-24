using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject ChestClosed, ChestOpened;
    public GameObject Bomb;

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

        Instantiate(Bomb, transform.position + new Vector3(0, 0 + 6, 0), Quaternion.identity);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        ChestClosed.SetActive(true);
        ChestOpened.SetActive(false);
    }
}
