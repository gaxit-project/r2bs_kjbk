using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunaarashi_ON_OFF : MonoBehaviour
{
    //public GameObject GlitchCamera;
    //GlitchFx GlitchFxSc;
    // Start is called before the first frame update
    void Start()
    {
        //GlitchCamera = transform.Find("GlitchCam").gameObject;
        GetComponent<GlitchFx>().enabled = false;
        //GlitchCamera.SetActive(false);
        //GlitchFxSc = GlitchCamera.GetComponent<GlitchFx>();
        //GlitchFxSc.enabled = false;
    }

    // Update is called once per frame
    public void SunaONOFF()
    {
        //GlitchCamera.SetActive(true);
        GetComponent<GlitchFx>().enabled = true;
        Invoke(nameof(SunaOFF), 2f);
    }

    public void SunaOFF()
    {
        //GlitchCamera.SetActive(false);
        GetComponent<GlitchFx>().enabled = false;
    }
}
