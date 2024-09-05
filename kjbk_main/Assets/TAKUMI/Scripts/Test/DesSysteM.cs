using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesSysteM : MonoBehaviour
{
    public Slider slider;
    bool DesSystemStatus = false;
    public static bool test = true;
    bool test2 = false;

    private void OnEnable()
    {
        Debug.Log("îjâÛÇÃÉQÅ[ÉÄ");
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = 0f;
    }
    void Start()
    {
        DesSystemStatus = true;
    }

    void Update()
    {
        if (DesSystemStatus)
        {
            test = true;
            Debug.Log("îjâÛÇÃë±Ç´");
            if(test2 == false)
            {
                slider.value += 100f * Time.deltaTime;
            }
        }
        if (Input.GetKeyDown("h"))
        {
            float Value = slider.value;
            test2 = true;
        }
        if(slider.value >= 100f)
        {
            test = false;
        }
    }
}
