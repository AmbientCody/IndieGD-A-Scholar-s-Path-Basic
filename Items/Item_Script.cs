using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Item/Create New Item")]
public class Item_Script : ScriptableObject
{
    public int id;
    public string names;
    public string description;
    public int price;
    public Sprite icon;
}
