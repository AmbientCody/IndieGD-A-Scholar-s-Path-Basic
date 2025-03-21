using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Controller : MonoBehaviour
{
    public int id;
    public int score;
    public ItemPickup_Script item;

    void Start()
    {
        id = item.id;
        score = item.score;
    }
}
