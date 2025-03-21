//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode()]

public class P_Meter_Bars : MonoBehaviour
{
    public int moodMax;
    public int moodCurrent;
    public Image moodMask;

    public int energyMax;
    public int energyCurrent;
    public Image energyMask;

    public float timer;
    public int counter;

    void getM_CurrentFill()
    {
        float fillAmount = (float)moodCurrent / (float)moodMax;
        moodMask.fillAmount = fillAmount;
    }

    void getE_CurrentFill()
    {
        float fillAmount = (float)energyCurrent / (float)energyMax;
        energyMask.fillAmount = fillAmount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mood_Drainers")
        {
            if (moodCurrent >= 20)
            {
                moodCurrent -= 20;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        moodMax = 100;
        moodCurrent = 100;
        energyMax = 100;
        energyCurrent = 100;
        timer = 0;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        getM_CurrentFill();
        getE_CurrentFill();

        if (moodCurrent < 50 && counter == 0)
        {
            timer += Time.deltaTime;

            if (timer > 10)
            {
                energyCurrent -= 50;
                timer = 0;
                counter = 1;
            }
        }

        if (moodCurrent == 0 && counter == 1)
        {
            timer += Time.deltaTime;

            if (timer > 10)
            {
                energyCurrent -= 50;
                timer = 0;
                counter = 2;
            }
        }

        /*
        Debug.Log("Mood Current: " + moodCurrent);
        Debug.Log("Timer: " + timer);
        Debug.Log("Counter: " + counter);
        */
    }
}
