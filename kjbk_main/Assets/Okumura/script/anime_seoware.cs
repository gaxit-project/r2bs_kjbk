
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anime_seoware : MonoBehaviour
{
    Animator anima;
    bool animeHold = false;
    public GameObject ky;//背負われてる方
    public GameObject player1;//背負ってる方

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
            transform.rotation = Quaternion.Euler(0, player1.transform.localEulerAngles.y - 90, 0);
            /*
            Vector3 pl = plTransform.transform.position;
            Vector3 npl = nplTransform.transform.position;
            pl.y += 100.00f;
            plTransform.position = pl;
            nplTransform.position = npl;
            */
            // forward-正面　back-後ろ　left-左　right up-上　down-下　
            if (transform.position.y <= 3)
            {
                
                transform.Translate(Vector3.forward * Time.deltaTime * 10);
                transform.Translate(Vector3.up * Time.deltaTime * 4);


                //transform.rotation = Quaternion.Euler(0,  player1.transform.rotation.y - 90 , 0);
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
