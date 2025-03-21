using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class Menu_Game : MonoBehaviour
{
    public GameObject gos;

    public void btnQuit()
    {
        Application.Quit();
    }

    // Load Game Async Load Screen
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        gos.transform.GetChild(7).gameObject.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void btnLoad()
    {
        StartCoroutine(LoadSceneAsync(P_Stats.currentScene));
    }

    private void Awake()
    {
        P_Stats P_Stats = new P_Stats();
        gos = GameObject.FindGameObjectWithTag("PScreen");
    }
}
