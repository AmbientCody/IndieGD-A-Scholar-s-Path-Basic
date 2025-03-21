using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Collection : MonoBehaviour
{
    public Item item1 = new Item();
    public Item item2 = new Item();
    public Item item3 = new Item();
    public Item item4 = new Item();
    public Item item5 = new Item();
    public Item item6 = new Item();

    // ITEM IMAGES
    public Sprite item1Sprite;
    public Sprite item2Sprite;
    public Sprite item3Sprite;
    public Sprite item4Sprite;
    public Sprite item5Sprite;
    public Sprite item6Sprite;

    public Dictionary<int , Item> item_find_dict = new Dictionary<int, Item>();

    // Start is called before the first frame update
    void Start()
    {
        item1.id = 1;
        item1.names = "Tomatoes";
        item1.description = "Required for tutorial to cook a delicious meal.";
        item1.price = 50;
        item1.image = item1Sprite;
        item1.acquired = false;

        item2.id = 2;
        item2.names = "Potatoes";
        item2.description = "Required for tutorial to cook a delicious meal.";
        item2.price = 50;
        item2.image = item2Sprite;
        item2.acquired = false;

        item3.id = 3;
        item3.names = "Onions";
        item3.description = "Required for tutorial to cook a delicious meal.";
        item3.price = 100;
        item3.image = item3Sprite;
        item3.acquired = false;

        item4.id = 4;
        item4.names = "OS-CD";
        item4.description = "Required for booting up a broken PC.";
        item4.price = 1000;
        item4.image = item4Sprite;
        item4.acquired = false;

        item5.id = 5;
        item5.names = "Office-Files";
        item5.description = "Required to help organize Anup dai's lumber mill.";
        item5.image = item5Sprite;
        item5.acquired = false;

        item6.id = 6;
        item6.names = "Certification";
        item6.description = "Required to help Sugyan apply for a job.";
        item6.image = item6Sprite;
        item6.acquired = false;

        item_find_dict[1] = item1;
        item_find_dict[2] = item2;
        item_find_dict[3] = item3;
        item_find_dict[4] = item4;
        item_find_dict[5] = item5;
        item_find_dict[6] = item6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
