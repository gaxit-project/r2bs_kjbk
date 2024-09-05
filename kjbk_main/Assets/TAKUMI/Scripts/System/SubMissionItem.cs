using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMissionItem : MonoBehaviour
{
    [SerializeField] public GameObject[] subItem = new GameObject[9];

    public BlendSahder[] ShaderScripts = new BlendSahder[9];
    void Start()
    {
        for (int i = 0; i < subItem.Length; i++)
        {
            Debug.Log("ここまで　1");
            //ShaderScripts[i] = subItem[i].GetComponent<BlendSahder>();
            Debug.Log("ここまで　2");
            ShaderScripts[i].enabled = false;
            Debug.Log("ここまで　3");
        }
    }

    public void ItemActive(int num)
    {
        if (0 <= num && num <= 9)
        {
            subItem[num].SetActive(false);
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
