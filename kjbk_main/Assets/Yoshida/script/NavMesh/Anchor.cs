using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)   //炎に触れた際アンカー削除
    {
        if (collision.gameObject.name == "Blaze")
        {
            Destroy(this.gameObject);
        }
    }
}
