using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInsideWater : MonoBehaviour
{
    public GameObject Player, me;
    Vector3 start = transform.position - (transform.scale/2), end = transform.position + (transform.scale/2);

    void Update(){
        if(Player.transform.position.x >= start.x && Player.transform.position.x <= end.x){
            if(Player.transform.position.y >= start.y && Player.transform.position.y <= end.y){
                print("CANCERRR");
            }
        }
    }
}
