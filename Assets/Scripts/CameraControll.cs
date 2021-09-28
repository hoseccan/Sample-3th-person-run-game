using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public PlayerControll player;
    private Vector3 offset;

    void Start()
    {
        offset = player.transform.position - transform.position;
    }
    void LateUpdate()
    {
        transform.position = player.transform.position - offset;
    }
}
