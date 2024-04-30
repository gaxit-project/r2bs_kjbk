using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    [SerializeField] public GameObject FF;
    double _time;
    float _cycle = 1;
    SpriteRenderer Sr;
    float Transparency = 0.0f;

    void Start()
    {
        Sr = GetComponent<SpriteRenderer>();
    }

    public void Tenmetsu()
    {


        for (int i = 0; i < 10; i++)
        {
            Sr.color = new Color(Sr.color.r, Sr.color.g, Sr.color.b, Transparency);
        }


    }
}
