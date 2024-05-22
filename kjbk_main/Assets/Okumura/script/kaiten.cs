using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaiten : MonoBehaviour
{
    Animator anima;
    bool animeHold = false;
    public GameObject ky;

    BoxCollider kyCol;

    void Start()
    {
        anima = this.GetComponent<Animator>();
        kyCol = this.GetComponent<BoxCollider>();
    }



    void Update()


    {
        //Transform plTransform = this.transform;
        //Transform nplTransform = this.transform;
        if (Input.GetKeyDown("m"))
        {
            animeHold = true;
        }
        else if (Input.GetKeyUp("m"))
        {
            //animeHold = false;
        }
        if (animeHold)
        {
            anima.SetBool("FFcarry", true);
            /*
            Vector3 pl = plTransform.transform.position;
            Vector3 npl = nplTransform.transform.position;
            pl.y += 100.00f;
            plTransform.position = pl;
            nplTransform.position = npl;
            */
            // forward-正面　back-後ろ　left-左　right up-上　down-下　
            if (transform.position.y <= 5)
            {

                transform.Translate(Vector3.forward * Time.deltaTime * 3);
                transform.Translate(Vector3.up * Time.deltaTime * 3);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                kyCol.enabled = false;
            }
            else
            {
                anima.SetBool("FFcarry", false);
            }



        }

        else
        {
            anima.SetBool("FFcarry", false);

        }
    }
}

