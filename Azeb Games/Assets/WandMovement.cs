using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandMovement : MonoBehaviour
{
    public bool Move = true;
    public Vector3 MoveVector = Vector3.up;
    public float MoveRange = 2.0f;
    public float MoveSpeed = 0.5f;

    private WandMovement wandMovement;

    Vector3 startPosition;
    void Start()
    {
        wandMovement = this;
        startPosition = wandMovement.transform.parent.position;
    }
    void Update()
    {
        if (Move)
        {
            Vector3 handPosition = wandMovement.transform.parent.position;
            handPosition.y -= 0.4f;
            handPosition.z = -5;
            wandMovement.transform.position = handPosition + MoveVector * (MoveRange * Mathf.Sin((Time.timeSinceLevelLoad + 0.1f) * MoveSpeed));
        }
    }
}