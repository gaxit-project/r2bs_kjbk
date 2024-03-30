using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Radio_Text : MonoBehaviour
{
    [SerializeField] string text;   //表示されるテキスト
    [SerializeField] TextMeshProUGUI TMP;   //表示されるtextMesh

    private bool ActiveText = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
