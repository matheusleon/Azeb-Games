using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombCounterUI : MonoBehaviour
{
    public Text txt;

    // Start is called before the first frame update
    void Start()
    {
        txt.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        BombThrower bombThrower = gameObject.GetComponentInParent<BombThrower>();
        txt.text = bombThrower.getBombCount().ToString();
    }
}
