using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Muteki : MonoBehaviour
{

    [SerializeField] public GameObject FF;
    double _time;
    float _cycle = 1;
    SpriteRenderer Sr;
    float Transparency = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Tenmetsu()
    {
        

        for(int i=0;i<10;i++)
        {
            Sr.color = new Color(Sr.color.r, Sr.color.g, Sr.color.b, Transparency); 
        }
        
        
    }


}
