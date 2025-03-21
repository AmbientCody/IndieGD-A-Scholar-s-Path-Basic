using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Q000 : MonoBehaviour
{
    // Game Objects
    [SerializeField] GameObject loadingUI;

    // Script Objects
    int activeScene;

    // When quest complete & target destination
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            loadingUI.SetActive(true);
            SceneManager.LoadScene(activeScene + 1);
        }
    }

    // When Quest Game Object loads
    private void Awake()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
    }
}
