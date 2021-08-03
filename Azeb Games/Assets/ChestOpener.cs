using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject ChestClosed, ChestOpened;


    // Start is called before the first frame update
    void Start()
    {
        ChestClosed.SetActive(true);
        ChestOpened.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ChestClosed.SetActive(false);
        ChestOpened.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        ChestClosed.SetActive(true);
        ChestOpened.SetActive(false);
    }
}
