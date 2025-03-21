using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Magic_Idol : MonoBehaviour
{
    // GAME OBJECTS & VARS
    public GameObject gos;                  // GameObj with Pscreen tag
    public GameObject wsPanelGO;            // GameObj with MagicIdolUI
    public GameObject prompt;
    public GameObject wsTxt01;              // Worship txt option [A]
    public GameObject wsTxt02;              // Worship txt option [D]

    public float timer;                     // Timer for disappearance of nptxt
    public bool isTimer;
    public int wsSeq;                       // For worship option sequencing

    public GameObject starPrefab;            
    public Transform loc1;
    public Transform loc2;

    // LOCK / UNLOCK
    public int wsKey = 0;                   // Key for worship only once at an idol
    public bool wsEnabled = false;          // To close logic for worship operation                             

    // SCRIPT OBJECTS
    public PMove PMoveObj;
    public P_Meter_Bars P_Meter_Bars_Obj;

    //
    // When player arrives at idol
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && wsKey == 0)
        {
            wsKey++;

            wsPanelGO.SetActive(true);
            wsEnabled = true;

            Time.timeScale = 0f;
            PMoveObj.isStopPanel = true;
        }
    }

    //
    // To turn off positive or negative outcome txt
    public void timerText()
    {
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            prompt.gameObject.GetComponent<TMP_Text>().text = string.Empty;
            isTimer = false;
        }
    }

    void Start()
    {
        // INITIALIZATION

        gos = GameObject.FindGameObjectWithTag("PScreen");             
        wsPanelGO = gos.transform.GetChild(6).gameObject;               
        wsTxt01 = wsPanelGO.transform.GetChild(2).gameObject;           
        wsTxt02 = wsPanelGO.transform.GetChild(3).gameObject;

        wsSeq = 0;

        PMoveObj = FindObjectOfType<PMove>();
        P_Meter_Bars_Obj = FindObjectOfType<P_Meter_Bars>();
    }

    void Update()
    {
        //
        // To start worship sequence if enabled
        if (wsEnabled == true)
        {
            prompt = GameObject.FindGameObjectWithTag("Prompt");

            //
            // Iter 00
            if (Input.GetKeyDown(KeyCode.A) && wsSeq==0)
            {
                wsTxt01.SetActive(false);
                wsSeq = 1;
            }

            //
            // Iter 01
            else if (Input.GetKeyDown(KeyCode.D) && wsSeq==1)
            {
                wsTxt01.SetActive(true);
                wsTxt02.SetActive(true);
                wsPanelGO.SetActive(false);
                PMoveObj.isStopPanel = false;                                                   // Stops Player Movement & Rotation
                prompt.gameObject.GetComponent<TMP_Text>().text = "Oops! Wrong sequence";
                isTimer = true;                                                                 // Starts 5s timer
                wsSeq = 0;
                Time.timeScale = 1f;                                                            // Stops time
                wsEnabled = false;
            }

            //
            // Iter 10
            else if (Input.GetKeyDown(KeyCode.D) && wsSeq==0)
            {
                wsTxt02.SetActive(false);
                wsSeq = 1;
            }

            //
            // Iter 11
            else if (Input.GetKeyDown(KeyCode.A) && wsSeq == 1)
            {
                wsTxt01.SetActive(true);
                wsTxt02.SetActive(true);
                wsPanelGO.SetActive(false);
                PMoveObj.isStopPanel = false;                                                   // Stops Player Movement & Rotation
                prompt.gameObject.GetComponent<TMP_Text>().text = "Way to go!";
                isTimer = true;                                                                 // Starts 5s timer
                wsSeq = 0;
                Time.timeScale = 1f;                                                            // Stops time

                Instantiate(starPrefab, loc1.position, loc1.rotation);                          // Instantiates stars for score
                Instantiate(starPrefab, loc2.position, loc2.rotation);

                P_Meter_Bars_Obj.moodCurrent = 100;
                wsEnabled = false;
            }
        }

        //
        // Deactivate negative / positive outcome texts 
        if (isTimer == true)
        {
            if (prompt.gameObject.GetComponent<TMP_Text>().text != string.Empty)
            {
                timerText();
            }
        }
        
    }
}
