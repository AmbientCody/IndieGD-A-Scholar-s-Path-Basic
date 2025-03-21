using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
//using UnityEditor.Recorder;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using UnityEngine.SceneManagement;
using UnityEditor.Sequences;

public class P_Inventory : MonoBehaviour
{
    // VARIABLES

    public int count = 0;

    public Dictionary<int, bool> items_in_inventory = new Dictionary<int, bool>();
    public Dictionary<int, bool> items_loaded = new Dictionary<int, bool>();

    // GAME OBJECTS
    public GameObject UI_Score;
    public GameObject UI_Cash;
    public List<GameObject> listItemsGO;
    public GameObject gos;

    // SCRIPT OBJECTS
    public PSettings pScreen;
    public PSettings PSettingsObj;
    public Item_Collection Item_CollectionObj;
    public Quest_Log Quest_Log;


    // When star is collected
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ItemPickup_Star")
        {
            P_Stats.p_score += collision.gameObject.GetComponent<Item_Controller>().score;
            Destroy(collision.gameObject);
        }
    }

    // Automation of inventory management
    public void write_Image_Inventory()
    {
        foreach (int iter in items_in_inventory.Keys)
        {
            // For item image writing
            if (items_in_inventory[iter] == true && items_loaded[iter] == false)
            {
                foreach (GameObject gos2 in listItemsGO)
                {
                    if (gos2.gameObject.GetComponent<UnityEngine.UI.Image>().sprite == null)
                    {
                        gos2.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Item_CollectionObj.item_find_dict[iter].image;
                        items_loaded[iter] = true;
                        //Debug.Log("Item image: " + Item_CollectionObj.item_find_dict[iter].image);
                        break;
                    }
                }
            }
            // For item image removing
            else if (items_in_inventory[iter] == false && items_loaded[iter] == true)
            {
                foreach (GameObject gos2 in listItemsGO)
                {
                    if (gos2.gameObject.GetComponent<UnityEngine.UI.Image>().sprite == Item_CollectionObj.item_find_dict[iter].image)
                    {
                        gos2.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = null;
                        items_loaded[iter] = false;
                        //Debug.Log("Item image: " + Item_CollectionObj.item_find_dict[iter].image);
                        break;
                    }
                }
            }
        }
    }

    void Start()
    {
        PSettingsObj = gameObject.GetComponent<PSettings>();
        Item_CollectionObj = GetComponent<Item_Collection>();
        P_Stats P_Stats1 = new P_Stats();
        Quest_Log = GetComponent<Quest_Log>();

        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 1)
        {
            P_Stats.p_cash = 1500;
            P_Stats.p_score = 0;
        }

        if (Quest_Log.activeScene == 6)
        {
            items_in_inventory.Add(5, true);
            items_loaded.Add(5, false);
        }

        if (Quest_Log.activeScene == 7)
        {
            P_Stats.p_cash = 1200;
        }
    }

    void Update()
    {
        // Executes only when Inventory panel is on
        try
        {
            if (GameObject.FindGameObjectWithTag("P_Items").activeSelf == true)
            {
                // Creates a list of all item game object in inventory
                gos = GameObject.FindGameObjectWithTag("P_Items");

                for (int i = 0; i < gos.transform.childCount; i++)
                {
                    listItemsGO.Add(gos.transform.GetChild(i).gameObject);
                }   
            }
        }
        catch { }


        write_Image_Inventory();

        // Test for removal of items
        if (Input.GetKeyDown(KeyCode.T))
        {
            items_in_inventory[1] = false;
            items_loaded[1] = true;
        }

        //Debug.Log(items_in_inventory.Count);

        
    }
}
