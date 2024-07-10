using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioText : MonoBehaviour
{
    [SerializeField] string text;   //表示されるテキスト
    [SerializeField] TextMeshProUGUI TMP;   //表示されるtextMesh
    public GameObject Rescued;

    private bool ActiveText = false;

    public bool RescueFlag = false;

    private void Start()
    {
        Rescued.SetActive(false);
    }
    void Update()
    {
        if (RescueFlag)
        {
            Invoke("ActiveRadio", 10f);
            Invoke("StopRadio", 13f);
            //SetActiveText(false);
            RescueFlag = false;
        }
    }

    public void ActiveRadio()
    {
        TMP.SetText(text);
        Rescued.SetActive(true);
    }

    public void StopRadio()
    {
        TMP.SetText("");
        Rescued.SetActive(false);
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
