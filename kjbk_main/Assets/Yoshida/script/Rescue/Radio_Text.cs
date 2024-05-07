using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Radio_Text : MonoBehaviour
{
    [SerializeField] string mild_text;   //軽症者の時表示されるテキスト
    [SerializeField] string severe_text;   //重傷者の時表示されるテキスト
    [SerializeField] TextMeshProUGUI TMP;   //表示されるtextMesh

    private string text;   //表示するテキスト

    private bool ActiveText = false;
    private string preText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsItActiveText())
        {
            ActiveRadio();
            Invoke("StopRadio", 3f);
            SetActiveText(false);
        }
    }

    public void ActiveRadio()
    {
        TMP.SetText(text);
    }

    public void SetMildText()
    {
        text = mild_text;
    }

    public void SetSevereText()
    {
        text = severe_text;
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
