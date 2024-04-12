using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlaze : MonoBehaviour
{
    Inferno Inferno;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnParticleCollision(GameObject obj)
    {
        Inferno = obj.GetComponent<Inferno>();
        if (Inferno.DesBlaze)
        {
            Debug.Log("è¡âªÇ≥ÇÍÇ‹ÇµÇΩ");
            Destroy(obj);
        }
    }
}
