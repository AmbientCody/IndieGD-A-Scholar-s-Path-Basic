using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.SocialPlatforms;
//using UnityEngine.SocialPlatforms.Impl;


public class PSettings : MonoBehaviour
{
    /* GameObjects for screen
     * [0] = Trackers
     * [1] = Inventory
     * [2] = Shop
     * [3] = MapQuest
     * [4] = QuestDialogue
     * */

    // Screens
    private int currentScene;
    private GameObject currentScreen;
    public GameObject gos;
    public List<GameObject> listScreens;

    // Bools for panels
    public bool onInventory;
    public bool onShop;
    public bool onMapQuest;
    public bool onTrackers;
    public bool onMenu;

    // Script Objects
    public PMove PMoveObj;
    public P_Inventory PInventoryObj;

    // Shop
    public GameObject shop01;
    public GameObject shop02;
    Shop_01 Shop01Obj;
    public int shopId;

    // Inventory
    public GameObject UI_Score;             // For score display
    public GameObject UI_Cash;              // For cash display
    public GameObject prompt;               // Prompt for panel
    public bool shoppable;                  // Bool for shopability check

    // Audio
    AudioSource asrc;

    public int getCurrentScene()
    {
        return currentScene;
    }

    // Shop NPCs
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NPC_Cashier1")
        {
            prompt.gameObject.GetComponent<TMP_Text>().text = "Press 'Z' for shop";
            shoppable = true;
            shopId = 1;
        }

        else if (collision.gameObject.tag == "NPC_Cashier2")
        {
            prompt.gameObject.GetComponent<TMP_Text>().text = "Press 'Z' for shop";
            shoppable = true;
            shopId = 2;
        }
        /*
        else if (collision.gameObject.tag == "NPC_Cashier3")
        {
            prompt.gameObject.GetComponent<TMP_Text>().text = "Press 'Z' for shop";
            shoppable = true;
            shopId = 3;
        }
        */
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "NPC_Cashier1" || collision.gameObject.tag == "NPC_Cashier2" || collision.gameObject.tag == "NPC_Cashier3")
        {
            prompt.gameObject.GetComponent<TMP_Text>().text = string.Empty;
            shoppable = false;
            shopId = 0;
        }
    }

    void Start()
    {
        // Script Initialization
        P_Stats P_Stats = new P_Stats();
        PMoveObj = GetComponent<PMove>();
        PInventoryObj = GetComponent<P_Inventory>();

        // Audio
        asrc = GetComponent<AudioSource>();

        // Game Objects
        prompt = GameObject.FindGameObjectWithTag("Prompt");
        gos = GameObject.FindGameObjectWithTag("PScreen");

        // Variable Initialization
        onInventory = false;
        onShop = false;
        onMapQuest = false;
        shoppable = false;

        // Creating a list of all on-screen UIs
        for (int i=0; i<gos.transform.childCount; i++)
        {
            listScreens.Add(gos.transform.GetChild(i).gameObject);
        }

        // Music
        if (P_Stats.musicON == true)
        {
            GetComponent<AudioSource>().volume = 0.10f;
            GetComponent<AudioSource>().Play();
        }
        /*
        Debug.Log("Music status: " + P_Stats.musicON);
        Debug.Log("Volume: " + GetComponent<AudioSource>().volume);
        */
    }

    void Update()
    {
        // For load game feature
        //currentScene = SceneManager.GetActiveScene().buildIndex;

        // Inventory Update
        if (onInventory)
        {
            UI_Score = GameObject.FindGameObjectWithTag("UI_Score");
            UI_Score.GetComponent<TMP_Text>().text = P_Stats.p_score.ToString();                // Displays score in Inventory UI

            UI_Cash = GameObject.FindGameObjectWithTag("UI_Cash");
            UI_Cash.GetComponent<TMP_Text>().text = P_Stats.p_cash.ToString();
        }

        // For Inventory
        if (Input.GetKeyDown(KeyCode.C) && !onInventory && !onShop && !onMapQuest)
        {
            //Debug.Log("C key pressed.");

            onInventory = true;
            listScreens[1].SetActive(true);

            Time.timeScale = 0f;
            PMoveObj.isStopPanel = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && onInventory && !onShop)
        {
            listScreens[1].SetActive(false);
            onInventory = false;
            Time.timeScale = 1f;
            PMoveObj.isStopPanel = false;
        }

        // For Shop
        else if (Input.GetKeyDown(KeyCode.Z) && !onShop && !onInventory && !onMapQuest && shoppable)
        {
            Debug.Log("Z key pressed.");
            listScreens[1].SetActive(true);
            onShop = true;
            onInventory = true;
            Time.timeScale = 0f;
            PMoveObj.isStopPanel = true;
            if (shopId == 1)
            {
                listScreens[2].SetActive(true);
                shop01.GetComponent<Shop_01>().enabled = true;
            }
            else if (shopId == 2)
            {
                listScreens[3].SetActive(true);
                shop02.GetComponent<Shop_02>().enabled = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z) && onShop && onInventory)
        {
            listScreens[1].SetActive(false);
            
            if (shopId == 1)
            {
                listScreens[2].SetActive(false);
                shop01.GetComponent<Shop_01>().enabled = false;
            }
            else if (shopId == 2)
            {
                listScreens[3].SetActive(false);
                shop02.GetComponent<Shop_02>().enabled = false;
            }

            onShop = false;
            onInventory = false;
            Time.timeScale = 1f;
            PMoveObj.isStopPanel = false;
            prompt.gameObject.GetComponent<TMP_Text>().text = string.Empty;
        }

        // For Map & Quest
        else if (Input.GetKeyDown(KeyCode.Q) && !onMapQuest && !onInventory && !onShop)
        {
            Debug.Log("Q key pressed.");
            listScreens[4].SetActive(true);
            onMapQuest = true;
            Time.timeScale = 0f;
            PMoveObj.isStopPanel = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && onMapQuest)
        {
            listScreens[4].SetActive(false);
            onMapQuest = false;
            Time.timeScale = 1f;
            PMoveObj.isStopPanel = false;
        }

        // For Menu 
        if (Input.GetKeyDown(KeyCode.Escape) && !onMenu)
        {
            listScreens[9].SetActive(true);
            PMoveObj.isStopPanel = true;
            Time.timeScale = 0f;
            onMenu = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && onMenu)
        {
            listScreens[9].SetActive(false);
            PMoveObj.isStopPanel = false;
            Time.timeScale = 1f;
            onMenu = false;
        }
    
        // Immideate Next
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(P_Stats.currentScene + 1);
        }
    }
}
