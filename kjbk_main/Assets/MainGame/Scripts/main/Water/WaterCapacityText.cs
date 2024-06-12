using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterCapacityText : MonoBehaviour
{
    public Text Capacity;
    void Start()
    {
        Capacity.text = "���Ί�̗e��:" + 0;
    }

    void Update()
    {
        if (PlayerPrefs.GetFloat("capacity") > 0)
        {
            Capacity.text = "���Ί�̗e��:" + PlayerPrefs.GetFloat("capacity");
        }
        else
        {
            Capacity.text = "���Ί�̗e��:" + "��[���Ă�������";
        }
        
    }
}
