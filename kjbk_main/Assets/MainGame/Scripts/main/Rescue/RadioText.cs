using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioText : MonoBehaviour
{
    [SerializeField] string text;   //�\�������e�L�X�g
    [SerializeField] TextMeshProUGUI TMP;   //�\�������textMesh

    private bool ActiveText = false;

    void Update()
    {
        if (IsItActiveText())
        {
            Invoke("ActiveRadio", 10f);
            Invoke("StopRadio", 13f);
            SetActiveText(false);
        }
    }

    public void ActiveRadio()
    {
        TMP.SetText(text);
    }

    public void StopRadio()
    {
        TMP.SetText("");
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
