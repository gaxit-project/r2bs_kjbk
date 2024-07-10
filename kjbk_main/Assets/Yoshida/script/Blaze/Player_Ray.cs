using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ray : MonoBehaviour
{
    private Vector3 Xp;
    private Vector3 Zp;
    private Vector3 Xm;
    private Vector3 Zm;
    private Vector3 XpZp;
    private Vector3 XpZm;
    private Vector3 XmZp;
    private Vector3 XmZm;

    private Ray rayXp;
    private Ray rayZp;
    private Ray rayXm;
    private Ray rayZm;
    private Ray rayXpZp;
    private Ray rayXpZm;
    private Ray rayXmZp;
    private Ray rayXmZm;

    private RaycastHit XpHit;
    private RaycastHit ZpHit;
    private RaycastHit XmHit;
    private RaycastHit ZmHit;
    private RaycastHit XpZpHit;
    private RaycastHit XpZmHit;
    private RaycastHit XmZpHit;
    private RaycastHit XmZmHit;

    public bool Up = false;
    public bool Under = false;
    public bool Left = false;
    public bool Right = false;
    public bool UpRight = false;
    public bool UpLeft = false;
    public bool UnderRight = false;
    public bool UnderLeft = false;

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
        XpZp = new Vector3(1, 0, 1);
        XpZm = new Vector3(1, 0, -1);
        XmZp = new Vector3(-1, 0, 1);
        XmZm = new Vector3(-1, 0, -1);

        rayXp = new Ray(this.transform.position, Xp);
        rayZp = new Ray(this.transform.position, Zp);
        rayXm = new Ray(this.transform.position, Xm);
        rayZm = new Ray(this.transform.position, Zm);
        rayXpZp = new Ray(this.transform.position, XpZp);
        rayXpZm = new Ray(this.transform.position, XpZm);
        rayXmZp = new Ray(this.transform.position, XmZp);
        rayXmZm = new Ray(this.transform.position, XmZm);

        /*
        Debug.DrawRay(rayXp.origin, rayXp.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayZp.origin, rayZp.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayXm.origin, rayXm.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayZm.origin, rayZm.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayXpZp.origin, rayXpZp.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayXpZm.origin, rayXpZm.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayXmZp.origin, rayXmZp.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayXmZm.origin, rayXmZm.direction * 100, Color.red, 1, false);
        */

        if (Physics.Raycast(rayXp, out XpHit, 100))
        {
            if(XpHit.collider.CompareTag("Blaze")) 
            {
                Right = true;
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
                Up = true;
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
                Left = true;
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
                Under = true;
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

        if (Physics.Raycast(rayXpZp, out XpZpHit, 100))
        {
            if (XpZpHit.collider.CompareTag("Blaze"))
            {
                UpRight = true;
            }
            else
            {
                UpRight= false;
            }
        }
        else
        {
            UpRight = false;
        }

        if (Physics.Raycast(rayXpZm, out XpZmHit, 100))
        {
            if (XpZmHit.collider.CompareTag("Blaze"))
            {
                UnderRight = true;
            }
            else
            {
                UnderRight= false;
            }
        }
        else
        {
            UnderRight = false;
        }

        if (Physics.Raycast(rayXmZp, out XmZpHit, 100))
        {
            if (XmZpHit.collider.CompareTag("Blaze"))
            {
                UpLeft = true;
            }
            else
            {
                UpLeft= false;
            }
        }
        else
        {
            UpLeft = false;
        }

        if (Physics.Raycast(rayXmZm, out XmZmHit, 100))
        {
            if (XmZmHit.collider.CompareTag("Blaze"))
            {
                UnderLeft = true;
            }
            else
            {
                UnderLeft= false;
            }
        }
        else
        {
            UnderLeft = false;
        }
    }
}
