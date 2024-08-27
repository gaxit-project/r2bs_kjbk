using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioText : MonoBehaviour
{
    [SerializeField] string text;   //表示されるテキスト
    [SerializeField] TextMeshProUGUI TMP;   //表示されるtextMesh
    public GameObject Rescued;
    public GameObject Rescued2;
    public GameObject Rescued3;

    private bool ActiveText = false;

    public bool RescueFlag = false;
    int Tmp=0;

    private void Start()
    {
        Rescued.SetActive(false);
        Rescued2.SetActive(false);
        Rescued3.SetActive(false);
    }
    void Update()
    {
        if (RescueFlag)
        {
            Invoke("ActiveRadio", 1f);
            Invoke("StopRadio", 2f);
            //SetActiveText(false);
            RescueFlag = false;
        }
    }

    public void ActiveRadio()
    {
        Tmp = Random.Range(0,2);
        TMP.SetText(text);
        if(Tmp == 0)
        {
            Rescued.SetActive(true);
        }
        else if(Tmp == 1)
        {
            Rescued2.SetActive(true);
        }
        else
        {
            Rescued3.SetActive(true);
        }
        
    }

    public void StopRadio()
    {
        TMP.SetText("");
        Rescued.SetActive(false);
        Rescued2.SetActive(false);
        Rescued3.SetActive(false);
    }

    public void SetActiveText(bool b)
    {
        ActiveText = b;
    }

    public bool IsItActiveText()
    {
        return ActiveText;
    }
}
