using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anime_Esc1 : MonoBehaviour
{
    Animator anima;
    bool animeHold = false;
    public GameObject player1;
    public GameObject ky;

    float n;

    void Start()
    {
        anima = this.GetComponent<Animator>();
    }

    void Update()        
    {

        transform.Rotate(0f, -1f * Time.deltaTime, 0f);
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
            // forward-正面　back-後ろ　left-左　right up-飢え　down-下
            //

            if (Input.GetKeyUp("m"))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 0);
                transform.Rotate(0f, -90f * Time.deltaTime, 0f);
            }
            else
            {
                anima.SetBool("FFcarry", false);
            }
            ky.transform.position = new Vector3(transform.position.x, ky.transform.position.y, transform.position.z);
        }
        else
        {
            anima.SetBool("FFcarry", false);

        }
    }
}
