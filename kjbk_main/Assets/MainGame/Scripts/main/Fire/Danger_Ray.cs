using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger_Ray : MonoBehaviour
{
    [SerializeField] float reacDistance = 0.45f;

    private Vector3 Xp;
    private Vector3 Zp;
    private Vector3 Xm;
    private Vector3 Zm;

    private Ray rayXp;
    private Ray rayZp;
    private Ray rayXm;
    private Ray rayZm;

    private RaycastHit XpHit;
    private RaycastHit ZpHit;
    private RaycastHit XmHit;
    private RaycastHit ZmHit;

    [System.NonSerialized] public bool Up = false;
    [System.NonSerialized] public bool Under = false;
    [System.NonSerialized] public bool Left = false;
    [System.NonSerialized] public bool Right = false;

    [System.NonSerialized] public float XpDistance = 100;
    [System.NonSerialized] public float ZpDistance = 100;
    [System.NonSerialized] public float XmDistance = 100;
    [System.NonSerialized] public float ZmDistance = 100;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Xp = Vector3.right;
        Zp = Vector3.forward;
        Xm = Vector3.left;
        Zm = Vector3.back;

        Vector3 t = new Vector3(this.transform.position.x, 5f, this.transform.position.z);

        Ray rayXp = new Ray(t, Xp);
        Ray rayZp = new Ray(t, Zp);
        Ray rayXm = new Ray(t, Xm);
        Ray rayZm = new Ray(t, Zm);

        /*
        Debug.DrawRay(rayXp.origin, rayXp.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayZp.origin, rayZp.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayXm.origin, rayXm.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayZm.origin, rayZm.direction * 100, Color.red, 1, false);
        */


        if (Physics.Raycast(rayXp, out XpHit, 100))
        {
            if (XpHit.collider.CompareTag("Blaze"))
            {
                float Xpdistance = Vector3.Distance(XpHit.transform.position, transform.position);
                XpDistance = Xpdistance / 100;
                if(XpDistance > reacDistance)
                {
                    Right = false;
                }
                else
                {
                    Right = true;
                }
            }
            else
            {
                Right = false;
            }
        }
        else
        {
            Right = false;
        }

        if (Physics.Raycast(rayZp, out ZpHit, 100))
        {
            if (ZpHit.collider.CompareTag("Blaze"))
            {
                float Zpdistance = Vector3.Distance(ZpHit.transform.position, transform.position);
                ZpDistance = Zpdistance / 100;
                if (ZpDistance > reacDistance)
                {
                    Up = false;
                }
                else
                {
                    Up = true;
                }
            }
            else
            {
                Up = false;
            }
        }
        else
        {
            Up = false;
        }

        if (Physics.Raycast(rayXm, out XmHit, 100))
        {
            if (XmHit.collider.CompareTag("Blaze"))
            {
                float Xmdistance = Vector3.Distance(XmHit.transform.position, transform.position);
                XmDistance = Xmdistance / 100;
                if (XmDistance > reacDistance)
                {
                    Left = false;
                }
                else
                {
                    Left = true;
                }
            }
            else
            {
                Left = false;
            }
        }
        else
        {
            Left = false;
        }

        if (Physics.Raycast(rayZm, out ZmHit, 100))
        {
            if (ZmHit.collider.CompareTag("Blaze"))
            {
                float Zmdistance = Vector3.Distance(ZmHit.transform.position, transform.position);
                ZmDistance = Zmdistance / 100;
                if (ZmDistance > reacDistance)
                {
                    Under = false;
                }
                else
                {
                    Under = true;
                }
            }
            else
            {
                Under = false;
            }
        }
        else
        {
            Under = false;
        }
    }
}