using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMissionItem : MonoBehaviour
{
    [SerializeField] public GameObject[] subItem = new GameObject[9];
    [SerializeField] public GameObject[] subItemButton = new GameObject[9];

    public BlendSahder[] ShaderScripts = new BlendSahder[9];

    void Start()
    {
        for (int i = 0; i < subItem.Length; i++)
        {
            subItemButton[i].SetActive(false);
            ShaderScripts[i].enabled = false;

        }
    }


    public void ItemActive(int num)
    {
        if (0 <= num && num <= 9)
        {
            subItem[num].SetActive(false);
        }
    }
    public void ItemButtonActive(int num)
    {
        if (0 <= num && num <= 9)
        {
            subItemButton[num].SetActive(true);
        }
    }

    public void ShaderOn(int num)
    {
        if(0 <= num &&  num <= 9)
        {
            ShaderScripts[num].enabled = true;
        }
    }

}
