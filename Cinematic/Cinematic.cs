using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using UnityEngine.Device;

public class Cinematic : MonoBehaviour
{
    public PlayableDirector pDir;
    private int activeScene;
    public GameObject loadingUI;

    /*
    // Mission pass Load Screen
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Single);
        operation.allowSceneActivation = false;

        loadingUI.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        pDir = GetComponent<PlayableDirector>();
        
        if (pDir.state != PlayState.Playing)
        {
            //StartCoroutine(LoadSceneAsync(activeScene + 1));
            loadingUI.SetActive(true);
            SceneManager.LoadScene(activeScene + 1);
        }
    }
}
