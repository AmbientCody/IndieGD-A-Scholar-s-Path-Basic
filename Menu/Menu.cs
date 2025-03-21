using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Audio
    public AudioSource asMusic;
    public AudioSource asSound;
    GameObject musicGO;
    GameObject soundGO;

    // UI Loadscreen
    public GameObject loadingUI;

    int currentScene;

    
    // Asynchronous function
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingUI.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
    

    public void btnStart()
    {
        StartCoroutine(LoadSceneAsync(1));
        //SceneManager.LoadScene(1);
    }

    public void btnLoad()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void btnQuit()
    {
        Application.Quit();
    }

    public void btnSound0()
    {
        P_Stats.soundON = false;

        if (P_Stats.soundCount == 0)
        {
            P_Stats.soundCount++;
        }
    }

    public void btnSound1()
    {
        P_Stats.soundON = true;

        if (P_Stats.soundCount == 0)
        {
            P_Stats.soundCount++;
        }
    }

    public void btnMusic0()
    {
        P_Stats.musicON = false;
        asMusic.Stop();

        if (P_Stats.musicCount == 0)
        {
            P_Stats.musicCount++;
        }
    }

    public void btnMusic1()
    {
        P_Stats.musicON = true;

        if (asMusic.isPlaying == false)
        {
            asMusic.Play();
            asMusic.loop = true;
        }

        if (P_Stats.musicCount == 0)
        {
            P_Stats.musicCount++;
        }
    }

    public void btnOnClick()
    {
        if (P_Stats.soundON)
        {
            asSound.Play();
        }
    }

    void Start()
    {
        // Script Object
        P_Stats P_Stats = new P_Stats();

        // Audio
        musicGO = GameObject.Find("Menu_Music");
        asMusic = musicGO.GetComponent<AudioSource>();
        soundGO = GameObject.Find("Menu_Sound");
        asSound = soundGO.GetComponent<AudioSource>();

        currentScene = P_Stats.currentScene;

        // Auto musicON on first start
        if (P_Stats.musicCount == 0)
        {
            asMusic.playOnAwake = true;
            asMusic.loop = true;
            P_Stats.musicON = true;
        }

        // Auto soundON on first start
        if (P_Stats.soundCount == 0)
        {
            P_Stats.soundON = true;
        }

    }

    private void Update()
    {
        /*
        Debug.Log("MusicON: " + P_Stats.musicON);
        Debug.Log("SoundON: " + P_Stats.soundON);
        */
    }
}
