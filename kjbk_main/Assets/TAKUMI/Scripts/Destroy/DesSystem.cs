using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesSystem : MonoBehaviour
{
    public Slider slider; // スライダー
    public static bool DesSystemStatus = false; // 
    public static bool DesSystemInput = false; // playerが入力したか

    private void OnEnable()
    {
        Debug.Log("破壊のゲーム");
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = 0f;
        DesSystemStatus = true;
        DesSystemInput = false;
    }

    void Update()
    {
        Debug.Log("破壊の続き");
        if (DesSystemInput == false)
        {
            slider.value += 100f * Time.deltaTime;
        }
        if (Input.GetKeyDown("h"))
        {
            float Value = slider.value;
            DesSystemInput = true;
            DesSystemStatus = false;
        }
        if (slider.value >= 100f)
        {
            DesSystemStatus = false;
        }
    }
}
