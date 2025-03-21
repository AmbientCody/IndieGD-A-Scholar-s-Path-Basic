using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using static UnityEngine.ParticleSystem;
using System;
using UnityEngine.Rendering;
using UnityEngine.Animations.Rigging;

public class Quest_Log : MonoBehaviour
{
    // Introducing Objects & vars
    public P_Inventory P_Inventory;
    public PMove PMove;
    public PSettings PSettings;
    public P_Meter_Bars P_Meter_Bars;

    public GameObject goalsGO;
    [SerializeField] private GameObject qDialogueGO;
    [SerializeField] private GameObject qName;
    [SerializeField] private GameObject qDiag1;
    [SerializeField] private GameObject qDiag2;
    [SerializeField] private GameObject qDiag3;
    [SerializeField] private GameObject qDiag4;
    [SerializeField] private GameObject questIconGO1;
    [SerializeField] private GameObject questIconGO2;
    [SerializeField] private GameObject questTxtGO1;
    [SerializeField] private GameObject questIconGO3;
    [SerializeField] private GameObject questIconGO4;
    [SerializeField] private GameObject loadingUI;
    public List<GameObject> voices;

    public int activeScene;
    private int qDiagNo = 0;
    private int qDiagB1 = 0;
    private int qDiagB2 = 0;
    private int audioCount = 0;

    public bool goal_000 = false;
    private bool goal_203 = false;
    private bool goal_204 = false;
    private bool goal_205 = false;
    private bool goal_215 = false;
    private bool goal_reached1 = false;
    private bool goal_inst2 = false;
    private bool onQuest;
    private bool onDialogue;


    private void OnCollisionEnter(Collision collision)
    {
        // Quest1 -> Contract Needs Attracts
        if (collision.gameObject.tag == "Goals" && (activeScene == 5 || activeScene == 6) && !goal_203 && !goal_204) 
        {
            Time.timeScale = 0f;
            onQuest = true;
            PMove.isStopPanel = true;
            qDialogueGO.SetActive(true);
        }

        if (collision.gameObject.tag == "Goals" && !goal_205 && activeScene == 7 && P_Inventory.items_in_inventory != null)
        {
            Time.timeScale = 0f;
            onQuest = true;
            PMove.isStopPanel = true;
            qDialogueGO.SetActive(true);
        }

        if (collision.gameObject.tag == "Goals" && activeScene == 10)
        {
            P_Inventory.items_in_inventory.Add(6, true);
            P_Inventory.items_loaded.Add(6, false);
            questIconGO1.SetActive(false);
            questIconGO2.SetActive(true);
            questIconGO3.SetActive(false);
            questIconGO4.SetActive(true);
            goalsGO.transform.GetChild(0).gameObject.SetActive(false);
            goalsGO.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (collision.gameObject.tag == "Goals" && activeScene == 15)
        {
            goalsGO.transform.GetChild(0).gameObject.SetActive(false);
            goalsGO.transform.GetChild(1).gameObject.SetActive(true);
            goalsGO.transform.GetChild(2).gameObject.SetActive(true);
            goalsGO.transform.GetChild(3).gameObject.SetActive(true);
            goal_215 = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Goals" && activeScene == 5)
        {
            Time.timeScale = 1f;
            onQuest = false;
            PMove.isStopPanel = false;
            qDialogueGO.SetActive(false);
        }
    }

    private void qDiag_Progress1()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            qDiagNo++;
            audioCount++;
        }
    }

    private void qDiag_Progress2()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            qDiagNo++;
            qDiagB1++;
        }
    }

    private void qDiag_Progress3()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            qDiagNo++;
            qDiagB2++;
        }
    }

    private void reset_Bdiag()
    {
        qDiagB1 = 0;
        qDiagB2 = 0;
    }

    private void reset_Qdiag()
    {
        qDiag1.gameObject.GetComponent<TMP_Text>().text = string.Empty;
        qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;
    }

    public void Awake()
    {
        // Initialization
        P_Inventory = GetComponent<P_Inventory>();
        PMove = GetComponent<PMove>();
        P_Stats P_Stats = new P_Stats();
        PSettings = GetComponent<PSettings>();
        P_Meter_Bars = GetComponent<P_Meter_Bars>();

        activeScene = SceneManager.GetActiveScene().buildIndex; 
        P_Stats.currentScene = activeScene;

        qDiagNo = 0;

        if (activeScene == 5 || activeScene == 6 || activeScene == 7 || activeScene == 17)
        {
            for (int i = 2; i < goalsGO.transform.childCount; i++)
            {
                voices.Add(goalsGO.transform.GetChild(i).gameObject);
            }

            for (int i = 2; i < goalsGO.transform.childCount; i++)
            {
                Debug.Log("Voices: " + voices[i - 2]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (P_Inventory.items_in_inventory.Count == 3 && activeScene == 1)
        {
            goal_000 = true;
            //goalsGO = GameObject.FindGameObjectWithTag("Goals");
            goalsGO.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if ((goal_203 && activeScene == 5) || (goal_204 && activeScene == 6))
        {
            goalsGO.transform.GetChild(0).gameObject.SetActive(false);
            goalsGO.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (goal_205 && activeScene == 7)
        {
            questIconGO1.SetActive(false);
            questIconGO2.SetActive(false);
            questTxtGO1.gameObject.GetComponent<TMP_Text>().text = "Get back home.";
        }
        
        Debug.Log("QDiagMain: " + qDiagNo);
        Debug.Log("QDiagB1: " + qDiagB1);
        Debug.Log("QDiagB2: " + qDiagB2);
        Debug.Log("AudioCount: " + audioCount);
        Debug.Log("Items in Inventory: " + P_Inventory.items_in_inventory.Count);

        // ---------------------- Quest "Contract Needs Attracts" ----------- //
        // ---------------------- Scene203 ---------------------------------- //
        if (onQuest && activeScene == 5 && !goal_203)
        {
            if (qDiagNo == 0)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Ah… You’re here. Good morning!";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[0].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 0)
                {
                    voices[0].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[0].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 1)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 1)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Good morning! So, this is your wood workshop…";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[1].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 2)
                {
                    voices[1].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[1].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 3)
                {
                    qDiag_Progress1();
                }

            }

            else if (qDiagNo == 2)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "See, I am trying to expand the business. If I could bring in some new type of woods, that’d be great.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[2].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 4)
                {
                    voices[2].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[2].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 5)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 3)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "[A] Wait, so you bring tree logs from a local forestry. You saw them down, even create furniture and sell them.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = "[D] Ok.";

                if (Input.GetKeyDown(KeyCode.A) && onDialogue == false && audioCount == 6)
                {
                    onDialogue = true;

                    if (voices[3].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 6)
                    {
                        voices[3].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D) && onDialogue == false && audioCount == 6)
                {
                    onDialogue = true;
                    P_Meter_Bars.moodCurrent -= 10;

                    if (voices[4].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 6)
                    {
                        voices[4].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                    }
                }
                else if (voices[4].gameObject.GetComponent<AudioSource>().isPlaying == false && voices[3].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 7)
                {
                    onDialogue = false;
                    audioCount++;
                    qDiagNo++;
                }
            }

            else if (qDiagNo == 4)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Yeah. There’s problem with workers though. They become rebellious and often leave work.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                reset_Bdiag();

                if (voices[5].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 8)
                {
                    voices[5].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[5].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 9)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 5)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "[A] Why are they rebellious?";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = "[D] So?";

                if (Input.GetKeyDown(KeyCode.A) && onDialogue == false && audioCount == 10)
                {
                    onDialogue = true;

                    if (voices[6].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[6].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                        qDiagB1++;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D) && onDialogue == false && audioCount == 10)
                {
                    onDialogue = true;
                    P_Meter_Bars.moodCurrent -= 10;

                    if (voices[7].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[7].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                        qDiagB2++;
                    }
                }
                else if (voices[6].gameObject.GetComponent<AudioSource>().isPlaying == false && voices[7].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 11)
                {
                    onDialogue = false;
                    reset_Qdiag();
                    audioCount++;
                    qDiagNo++;
                }
            }

            else if (qDiagNo == 6 && qDiagB1 == 1)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Because they demand more money if they feel like the task that was given to them does not balance the money they get.";

                if (voices[8].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 12)
                {
                    voices[8].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[8].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 13)
                {
                    Debug.Log("Code is pointing here");
                    qDiag_Progress1();
                }

                if (voices[5].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 8)
                {
                    voices[5].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[5].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 9)
                {
                    qDiag_Progress1();
                }

            }
            else if (qDiagNo == 6 && qDiagB2 == 1)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "So? Who’s gonna do the work if they leave? Maybe you’re only learning.";

                if (voices[9].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 12)
                {
                    voices[9].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[9].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 13)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 7)
            {
                reset_Bdiag();
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "What if you sign up contracts with them and go from there.";

                if (voices[10].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 14)
                {
                    voices[10].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[10].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 15)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 8)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "That’s brilliant. Would you do that for me then?";

                if (voices[11].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 16)
                {
                    voices[11].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[11].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 17)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 9)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "[S] No, I have other work to do.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = "[D] Sure, that’s what Im here for. I will also work on management documents.";

                if (Input.GetKeyDown(KeyCode.S) && onDialogue == false && audioCount == 18)
                {
                    onDialogue = true;
                    P_Meter_Bars.energyCurrent -= 50;

                    if (voices[12].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[12].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                        qDiagB1++;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D) && onDialogue == false && audioCount == 18)
                {
                    onDialogue = true;

                    if (voices[13].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[13].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                        qDiagB2++;
                    }
                }
                else if (voices[12].gameObject.GetComponent<AudioSource>().isPlaying == false && voices[13].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 19 && qDiagB1 == 1)
                {
                    onDialogue = false;
                    audioCount++;
                    qDiagNo++;

                    Time.timeScale = 1f;
                    PMove.isStopPanel = false;
                    qDialogueGO.SetActive(false);

                    qDiagNo = 0;
                    audioCount = 0;
                    onQuest = false;

                    reset_Bdiag();
                }
                else if (voices[12].gameObject.GetComponent<AudioSource>().isPlaying == false && voices[13].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 19 && qDiagB2 == 1)
                {
                    onDialogue = false;
                    audioCount++;
                    qDiagNo++;
                }
            }

            else if (qDiagNo == 10)
            {
                reset_Bdiag();
                reset_Qdiag();
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "So, let’s talk money. Do I get anything in advance?";

                if (voices[14].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 20)
                {
                    voices[14].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[14].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 21)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 11)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Advance! Hold your horses boy. Let’s see what you’ve got first.";

                if (voices[15].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 22)
                {
                    voices[15].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[15].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 23)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 12)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "[A] How about we go on a weekly basis. And make 5k per week?";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = "[D] Ok. So, payment at the end then. Im looking for about 20k.";
                
                /*
                qDiag_Progress2();
                qDiag_Progress3();
                */

                if (Input.GetKeyDown(KeyCode.A) && onDialogue == false && audioCount == 24)
                {
                    onDialogue = true;
                    if (voices[17].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[17].gameObject.GetComponent<AudioSource>().Play();
                        qDiagB1++;
                        audioCount++;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D) && onDialogue == false && audioCount == 24)
                {
                    onDialogue = true;
                    P_Meter_Bars.moodCurrent -= 10;

                    if (voices[16].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[16].gameObject.GetComponent<AudioSource>().Play();
                        qDiagB2++;
                        audioCount++;
                    }
                }
                else if (voices[17].gameObject.GetComponent<AudioSource>().isPlaying == false && voices[16].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 25)
                {
                    onDialogue = false;
                    reset_Qdiag();
                    audioCount++;
                    qDiagNo++;
                }
            }

            else if (qDiagNo == 13 && qDiagB1 == 1)
            {
                
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Sounds good.";

                if (voices[18].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 26)
                {
                    voices[18].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[18].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 27)
                {
                    Time.timeScale = 1f;
                    PMove.isStopPanel = false;
                    qDialogueGO.SetActive(false);

                    qDiagNo = 0;
                    onQuest = false;
                    goal_203 = true;
                    
                    audioCount = 0;
                    Debug.Log("Goal Reached: " + goal_203);
                    reset_Qdiag();
                    reset_Bdiag();
                }
            }
            else if (qDiagNo == 13 && qDiagB2 == 1)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Its better if we stick to the payment at the end. Besides, I have upcoming revenue and that would be perfect timing for me, setting you up with money then.";

                if (voices[19].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 26)
                {
                    voices[19].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }
                else if (voices[19].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 27)
                {
                    Time.timeScale = 1f;
                    PMove.isStopPanel = false;

                    qDiagNo = 0;
                    onQuest = false;
                    goal_203 = true;
                    qDialogueGO.SetActive(false);

                    audioCount = 0;
                    reset_Qdiag();
                    reset_Bdiag();
                    Debug.Log("Goal Reached: " + goal_203);
                }
            }
        }

        if (activeScene == 5 && goal_203)
        {
            if (questIconGO1.activeSelf == true)
            {
                questIconGO1.SetActive(false);
                questIconGO2.SetActive(false);
            }
            if (PSettings.onMapQuest)
            {
                questTxtGO1.GetComponent<TMP_Text>().text = "Get back home";
            }
        }

        // ----------------------------- Scene204 -------------------------------- //

        if (onQuest && activeScene == 6 && !goal_204)
        {
            if (qDiagNo == 0)
            {
                P_Inventory.items_in_inventory.Remove(0);
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "So, here’s the contract papers. And the other documents.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[0].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 0)
                {
                    voices[0].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[0].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 1)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 1)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Awesome. Let’s get that flash drive. Thank you.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[1].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 2)
                {
                    voices[1].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[1].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 3)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 2)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Do you have the payment ready?";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[2].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 4)
                {
                    voices[2].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[2].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 5)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 3)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Yeah, about that. I am receiving funds from my earlier client, and I will get to you soon after settling that down.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[3].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 6)
                {
                    voices[3].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[3].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 7)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 4)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Ok...";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[4].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 8)
                {
                    voices[4].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[4].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 9)
                {
                    Time.timeScale = 1f;
                    PMove.isStopPanel = false;

                    qDiagNo = 0;
                    onQuest = false;
                    goal_204 = true;
                    qDialogueGO.SetActive(false);

                    audioCount = 0;
                    reset_Qdiag();
                    reset_Bdiag();
                    Debug.Log("Goal Reached: " + goal_204);
                }
            }
        }

        if (activeScene == 6 && goal_204)
        {
            if (questIconGO1.activeSelf == true)
            {
                questIconGO1.SetActive(false);
                questIconGO2.SetActive(false);
            }
            if (PSettings.onMapQuest)
            {
                questTxtGO1.GetComponent<TMP_Text>().text = "Get back home";
            }
            if (P_Inventory.items_in_inventory[5] == true)
            {
                P_Inventory.items_in_inventory[5] = false;
                P_Inventory.items_loaded[5] = true;
            }
        }

        // ------------------------------   Scene205   -------------------------------

        if (P_Inventory.items_in_inventory.Count == 1 && activeScene == 7)
        {
            questIconGO1.SetActive(true);
            questIconGO2.SetActive(true);
            questTxtGO1.gameObject.GetComponent<TMP_Text>().text = "Visit Anup's wood workshop.";
        }

        if (onQuest && activeScene == 7 && !goal_205 && P_Inventory.items_in_inventory.Count == 1)
        {
            if (qDiagNo == 0)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Here’s the computer. Ask me if you need anything.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;
                qDiag3.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[0].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 0)
                {
                    voices[0].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[0].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 1)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 1)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Sure.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[1].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 2)
                {
                    voices[1].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[1].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 3)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 2)
            {
                if (audioCount == 4)
                {
                    qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                    qDiag1.gameObject.GetComponent<TMP_Text>().text = "[W] Try installing OS from a CD.";
                    qDiag2.gameObject.GetComponent<TMP_Text>().text = "[D] Check if the hardware is running.";
                    qDiag3.gameObject.GetComponent<TMP_Text>().text = "[A] Try opening computer.";
                }
                
                if (Input.GetKeyDown(KeyCode.A) && audioCount == 4)
                {
                    audioCount++;
                    qDiag3.gameObject.GetComponent <TMP_Text>().text = String.Empty;
                    P_Stats.p_score += 50;
                }
                else if (Input.GetKeyDown(KeyCode.D) && audioCount == 5)
                {
                    qDiag2.gameObject.GetComponent<TMP_Text>().text = String.Empty;
                    P_Stats.p_score += 50;
                    audioCount++;
                }
                else if (Input.GetKeyDown(KeyCode.W) && audioCount == 6)
                {
                    qDiag1.gameObject.GetComponent<TMP_Text>().text = String.Empty;
                    P_Stats.p_score += 50;
                    qDiagNo++;
                    audioCount++;
                }
                else if (Input.GetKeyDown(KeyCode.W) && audioCount == 4)
                {
                    P_Meter_Bars.moodCurrent -= 10;
                }
                else if (Input.GetKeyDown(KeyCode.D) && audioCount == 4)
                {
                    P_Meter_Bars.moodCurrent -= 10;
                }
            }

            else if (qDiagNo == 3)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Now with another HDD";
                qDiag_Progress1();
            }

            else if (qDiagNo == 4)
            {
                if (audioCount == 8)
                {
                    qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                    qDiag1.gameObject.GetComponent<TMP_Text>().text = "[W] Boot up computer from our HDD.";
                    qDiag2.gameObject.GetComponent<TMP_Text>().text = "[D] Fix another HDD on the computer";
                    qDiag3.gameObject.GetComponent<TMP_Text>().text = "[A] Clean install the OS on Anup’s HDD.";
                    qDiag4.gameObject.GetComponent<TMP_Text>().text = "[S] Boot up computer from Anup’s HDD.";
                }

                if (Input.GetKeyDown(KeyCode.D) && audioCount == 8)
                {
                    audioCount++;
                    qDiag2.gameObject.GetComponent<TMP_Text>().text = String.Empty;
                    P_Stats.p_score += 50;
                }
                else if (Input.GetKeyDown(KeyCode.W) && audioCount == 9)
                {
                    qDiag1.gameObject.GetComponent<TMP_Text>().text = String.Empty;
                    P_Stats.p_score += 50;
                    audioCount++;
                }
                else if (Input.GetKeyDown(KeyCode.A) && audioCount == 10)
                {
                    qDiag3.gameObject.GetComponent<TMP_Text>().text = String.Empty;
                    P_Stats.p_score += 50;
                    audioCount++;
                }
                else if (Input.GetKeyDown(KeyCode.S) && audioCount == 11)
                {
                    qDiag4.gameObject.GetComponent<TMP_Text>().text = String.Empty;
                    P_Stats.p_score += 50;
                    qDiagNo++;
                    audioCount++;
                }

                else if (Input.GetKeyDown(KeyCode.A) && audioCount == 8)
                {
                    P_Meter_Bars.moodCurrent -= 10;
                }
                else if (Input.GetKeyDown(KeyCode.S) && audioCount == 8)
                {
                    P_Meter_Bars.moodCurrent -= 10;
                }
                else if (Input.GetKeyDown(KeyCode.W) && audioCount == 8)
                {
                    P_Meter_Bars.moodCurrent -= 10;
                }
            }

            else if (qDiagNo == 5)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Anup";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Oh great! It’s working. Here’s 5k and that’s all I have right now. Come back later for more.";

                if (voices[2].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 12)
                {
                    voices[2].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[2].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 13)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 6)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Awesome!";

                if (voices[3].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 14)
                {
                    voices[3].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[3].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 15)
                {
                    Time.timeScale = 1f;
                    PMove.isStopPanel = false;

                    qDiagNo = 0;
                    onQuest = false;
                    goal_205 = true;
                    qDialogueGO.SetActive(false);

                    P_Inventory.items_in_inventory.Remove(4);
                    P_Stats.p_cash += 5000;
                    audioCount = 0;
                    reset_Qdiag();
                    reset_Bdiag();
                    Debug.Log("Goal Reached: " + goal_205);
                }
            }
        }

        // ------------------- Nepotism --------------------

        if (activeScene == 15 && goal_215 == true)
        {
            if (goalsGO.transform.GetChild(2).gameObject.GetComponent<AudioSource>().isPlaying == false)
            {
                goalsGO.transform.GetChild(1).gameObject.SetActive(false);
                questIconGO1.SetActive(false);
                questIconGO2.SetActive(true);
                questIconGO3.SetActive(false);
                questIconGO4.SetActive(true);
            }
        }

        // ------------------ Mind Detox --------------------

        if (activeScene == 17)
        {
            Time.timeScale = 0f;
            onQuest = true;
            PMove.isStopPanel = true;

            if (qDiagNo == 0 & !goal_reached1)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "I used to be fat as a kid.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[0].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 0)
                {
                    voices[0].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[0].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 1)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 1)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "[A] Everyone is built different. They have their own perks.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = "[D] I used to be such a blob.";

                if (Input.GetKeyDown(KeyCode.A) && onDialogue == false && audioCount == 2)
                {
                    onDialogue = true;

                    if (voices[1].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[1].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                        qDiagB1++;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D) && onDialogue == false && audioCount == 2)
                {
                    onDialogue = true;

                    if (voices[2].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[2].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                        qDiagB2++;
                    }
                }
                else if (voices[1].gameObject.GetComponent<AudioSource>().isPlaying == false && voices[2].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 3)
                {
                    onDialogue = false;
                    audioCount++;
                    qDiagNo++;
                }
            }

            else if(qDiagNo == 2)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Everything that I do is messed up.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[3].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 4)
                {
                    voices[3].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[3].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 5)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 3)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "[A] I am such a mess.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = "[D] I have the potential to learn to organize.";

                if (Input.GetKeyDown(KeyCode.A) && onDialogue == false && audioCount == 6)
                {
                    onDialogue = true;

                    if (voices[4].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[4].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                        qDiagB2++;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D) && onDialogue == false && audioCount == 6)
                {
                    onDialogue = true;

                    if (voices[5].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[5].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                        qDiagB1++;
                    }
                }
                else if (voices[4].gameObject.GetComponent<AudioSource>().isPlaying == false && voices[5].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 7)
                {
                    onDialogue = false;
                    audioCount++;
                    qDiagNo++;
                }
            }

            else if (qDiagNo == 4)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "There is nothing I can do to make things right.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[6].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 8)
                {
                    voices[6].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[6].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 9)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 5)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "[A] I am hopeless.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = "[D] I can start from the little things and work my way up.";

                if (Input.GetKeyDown(KeyCode.A) && onDialogue == false && audioCount == 10)
                {
                    onDialogue = true;

                    if (voices[7].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[7].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                        qDiagB2++;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D) && onDialogue == false && audioCount == 10)
                {
                    onDialogue = true;

                    if (voices[8].gameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        voices[8].gameObject.GetComponent<AudioSource>().Play();
                        audioCount++;
                        qDiagB1++;
                    }
                }
                else if (voices[7].gameObject.GetComponent<AudioSource>().isPlaying == false && voices[8].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 11)
                {
                    onDialogue = false;
                    audioCount++;
                    qDiagNo++;
                }
            }

            else if (qDiagNo == 6 && qDiagB1 > qDiagB2)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Drishti - The Psychiatrist";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Great! You have achieved self-respect. Try this at home and everywhere you go. Things will then slowly turn out right.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[9].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 12)
                {
                    voices[9].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[9].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 13)
                {
                    qDiag_Progress1();
                }
            }

            else if (qDiagNo == 6 && qDiagB1 < qDiagB2)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Drishti - The Psychiatrist";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Oh Boy! Try again.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[10].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 12)
                {
                    voices[10].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[10].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 13)
                {
                    qName.gameObject.GetComponent<TMP_Text>().text = string.Empty;
                    qDiag1.gameObject.GetComponent<TMP_Text>().text = string.Empty;
                    qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;
                    qDiagNo = 0;
                    reset_Bdiag();
                    audioCount = 0;
                }
            }

            else if (qDiagNo == 7)
            {
                qName.gameObject.GetComponent<TMP_Text>().text = "Biwek";
                qDiag1.gameObject.GetComponent<TMP_Text>().text = "Thanks! That was refreshing. I will be on my way.";
                qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;

                if (voices[11].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 14)
                {
                    voices[11].gameObject.GetComponent<AudioSource>().Play();
                    audioCount++;
                }

                else if (voices[11].gameObject.GetComponent<AudioSource>().isPlaying == false && audioCount == 15)
                {
                    qName.gameObject.GetComponent<TMP_Text>().text = string.Empty;
                    qDiag1.gameObject.GetComponent<TMP_Text>().text = string.Empty;
                    qDiag2.gameObject.GetComponent<TMP_Text>().text = string.Empty;
                    goal_reached1 = true;
                    qDiagNo = 0;
                    reset_Bdiag();
                    audioCount = 0;
                    loadingUI.SetActive(true);
                    SceneManager.LoadScene(activeScene + 1);
                }
            }
        }
    }
}
