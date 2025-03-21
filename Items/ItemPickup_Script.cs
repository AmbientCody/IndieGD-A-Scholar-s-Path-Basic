using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "Item/Create Pickup Item")]
public class ItemPickup_Script : ScriptableObject
{
    public int id;
    public string names;
    public int score;
}
