using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Follow : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 player_pos;
    int offset;

    void Start()
    {
        offset = 40;    
    }

    void Update()
    {
        player_pos = player.transform.position;
        gameObject.transform.position = player_pos + Vector3.up * offset;
    }
}
