using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RMaxText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RMax;

    

    // Start is called before the first frame update
    void Start()
    {
        //RMax.SetText("/ 13");
        RMax.SetText("<sprite=3><sprite=0>");
    }
}
