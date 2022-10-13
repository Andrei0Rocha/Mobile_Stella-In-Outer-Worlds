using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    public float ZOffset = -4;

    void Update()
    {
        if (Player)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, ZOffset);
        }
    }
}
