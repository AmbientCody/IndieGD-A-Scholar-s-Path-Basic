using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MM_Brightness : MonoBehaviour
{
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    AutoExposure exposure;

    // Start is called before the first frame update
    void Start()
    {
        brightness.TryGetSettings(out exposure);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnBrightness0()
    {
        exposure.keyValue.value = 0.25f;
    }

    public void btnBrightness1()
    {
        exposure.keyValue.value = 1f;
    }
}
