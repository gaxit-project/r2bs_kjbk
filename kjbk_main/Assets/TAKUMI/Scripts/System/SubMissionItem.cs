using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubMissionItem : MonoBehaviour
{
    [SerializeField] public GameObject[] subItem = new GameObject[9];
    [SerializeField] public GameObject[] subItemButton = new GameObject[9];

    public BlendSahder[] ShaderScripts = new BlendSahder[9];

    public ButtonChangeAYX ButtonAXY;

    //アイテムテキスト関連
    string ItemText;
    [SerializeField] public TMP_Text ItemTextMeshPro;
    public GameObject ItemTextObject;

    public Radio_ver4 Radio4;

    void Start()
    {
        ItemTextObject.SetActive(false);
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
            Debug.Log("Item ; " + num + "を消去");
            subItemButton[num].SetActive(false);
            ButtonAXY.DeactivateAllButtons();
            subItem[num].SetActive(false);
            Radio4.HMIUI(3);
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
