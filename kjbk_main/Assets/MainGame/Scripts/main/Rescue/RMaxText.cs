using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RMaxText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RMax;

    

    void Start()
    {
        //�~���\�ő�l���̕\��
        RMax.SetText("<sprite=3><sprite=0>");
    }
}
