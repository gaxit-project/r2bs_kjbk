using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCput : MonoBehaviour
{

    float ran, rand;                       // ƒ‰ƒ“ƒ_ƒ€‚È‰ñ“]Šp“x


    void Start()
    {
        this.transform.Rotate(-90f, 0f, 0f, Space.World); // ƒ‰ƒ“ƒ_ƒ€‚ÈŠp“x‚Å‰ñ“]
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ran = UnityEngine.Random.Range(0f, 360f); // ƒ‰ƒ“ƒ_ƒ€‚È‰ñ“]Šp“x‚ğ¶¬
            this.transform.Rotate(0f, ran, 0f, Space.World); // ƒ‰ƒ“ƒ_ƒ€‚ÈŠp“x‚Å‰ñ“]
        }
    }
}
