using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop_02 : MonoBehaviour
{
    // VARIABLES
    public int item_num = 0;
    public int item1_count = 0;

    // GAME OBJECTS
    public GameObject player;
    public GameObject item_detail;
    public GameObject prompt;

    // SCRIPT OBJECTS
    public P_Inventory P_InventoryObj;
    public PSettings PSettingsObj;
    public P_Stats P_Stats;


    // METHODS
    // Buys tomato
    public void btn_OSCD_detail()
    {
        item_num = 4;
        item_detail.gameObject.GetComponent<TMP_Text>().text = "Required for the quest to repair a PC.";
    }

    void Start()
    {
    }

    // Exeutes when the game object where the script is attached is enabled in the game runtime
    public void Awake()
    {
        PSettingsObj = player.GetComponent<PSettings>();
        P_InventoryObj = player.GetComponent<P_Inventory>();
        prompt = GameObject.FindGameObjectWithTag("Prompt");
    }

    void Update()
    {
        // Activates shop button only when near shopkeeper
        if (Input.GetKeyDown(KeyCode.E) && PSettingsObj.shoppable)
        {
            if (item_num == 4 && item1_count == 1)
            {
                prompt.gameObject.GetComponent<TMP_Text>().text = "Already got enough OS-CD";
            }

            else if (item_num == 4 && P_Stats.p_cash >= 1000 && item1_count == 0)
            {
                P_Stats.p_cash -= 1000;
                P_InventoryObj.items_in_inventory.Add(4, true);
                P_InventoryObj.items_loaded.Add(4, false);
                item1_count++;
            }
        }
    }
}
