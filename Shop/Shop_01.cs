using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Sequences;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop_01 : MonoBehaviour
{
    // VARIABLES
    public int item_num = 0;
    public int item1_count = 0;
    public int item2_count = 0;
    public int item3_count = 0;
    private int activeScene;

    // GAME OBJECTS
    public GameObject player;
    public GameObject item_detail;
    public GameObject prompt;

    // SCRIPT OBJECTS
    public P_Inventory P_InventoryObj;
    public PSettings PSettingsObj;
    //public P_Stats P_Stats;


    // METHODS
    // Buys tomato
    public void btn_tomato_detail()
    {
        item_num = 1;
        item_detail.gameObject.GetComponent<TMP_Text>().text = "Required for tutorial to cook a delicious meal.";
    }

    // Buys potato
    public void btn_potato_detail()
    {
        item_num = 2;
        item_detail.gameObject.GetComponent<TMP_Text>().text = "Required for tutorial to cook a delicious meal.";
    }

    // Buys onion
    public void btn_onion_detail()
    {
        item_num = 3;
        item_detail.gameObject.GetComponent<TMP_Text>().text = "Required for tutorial to cook a delicious meal.";
    }

    // Exeutes when the game object where the script is attached is enabled in the game runtime
    public void Awake()
    {
        PSettingsObj = player.GetComponent<PSettings>();
        P_InventoryObj = player.GetComponent<P_Inventory>();
        //P_Stats = player.GetComponent<P_Stats>();

        prompt = GameObject.FindGameObjectWithTag("Prompt");

        activeScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {

        // Activates shop button only when near shopkeeper
        if (Input.GetKeyDown(KeyCode.E) && PSettingsObj.shoppable && activeScene == 1)
        {
            if (item1_count == 1 && item_num == 1)
            {
                prompt.gameObject.GetComponent<TMP_Text>().text = "Already got enough tomatoes";
            }

            else if (item2_count == 1 && item_num == 2)
            {
                prompt.gameObject.GetComponent<TMP_Text>().text = "Already got enough potatoes";
            }

            else if (item3_count == 1 && item_num == 3)
            {
                prompt.gameObject.GetComponent<TMP_Text>().text = "Already got enough onions";
            }

            else if (item_num == 1 && P_Stats.p_cash >= 50 && item1_count == 0)
            {
                P_Stats.p_cash -= 50;
                P_InventoryObj.items_in_inventory.Add(1, true);
                P_InventoryObj.items_loaded.Add(1, false);
                item1_count++;
            }

            else if (item_num == 2 && P_Stats.p_cash >= 50 && item2_count == 0)
            {
                P_Stats.p_cash -= 50;
                P_InventoryObj.items_in_inventory.Add(2, true);
                P_InventoryObj.items_loaded.Add(2, false);
                item2_count++;
            }

            else if (item_num == 3 && P_Stats.p_cash >= 100 && item3_count == 0)
            {
                P_Stats.p_cash -= 100;
                P_InventoryObj.items_in_inventory.Add(3, true);
                P_InventoryObj.items_loaded.Add(3, false);
                item3_count++;
            }
        }
    }
}
