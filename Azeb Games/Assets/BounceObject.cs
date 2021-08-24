using UnityEngine;
using System.Collections;

public class BounceObject : MonoBehaviour
{
    public bool Move = true;
    public Vector3 MoveVector = Vector3.up;
    public float MoveRange = 2.0f;
    public float MoveSpeed = 0.5f;

    private BounceObject bounceObject;

    Vector3 startPosition;
    void Start()
    {
        bounceObject = this;
        startPosition = bounceObject.transform.position;
    }
    void Update()
    {
        if (Move)
        {
            bounceObject.transform.position = startPosition + MoveVector * (MoveRange * Mathf.Sin(Time.timeSinceLevelLoad * MoveSpeed));
        }
    }
}