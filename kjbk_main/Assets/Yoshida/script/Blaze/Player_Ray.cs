using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ray : MonoBehaviour
{
    [SerializeField] float clampMax = 2;
    [SerializeField] float clampMin = 1;

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

    public float XpDistance = 100;
    public float ZpDistance = 100;
    public float XmDistance = 100;
    public float ZmDistance = 100;
    public float XpZpDistance = 100;
    public float XpZmDistance = 100;
    public float XmZpDistance = 100;
    public float XmZmDistance = 100;


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
            if (XpHit.collider.CompareTag("Blaze"))
            {
                float Xpdistance = Vector3.Distance(XpHit.transform.position, transform.position);
                XpDistance = Xpdistance / 10;
                if (XpDistance > 5)
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
                ZpDistance = Zpdistance / 10;
                if (ZpDistance > 5)
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
                XmDistance = Xmdistance / 10;
                if (XmDistance > 5)
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
                ZmDistance = Zmdistance / 10;
                if (ZmDistance > 5)
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

        if (Physics.Raycast(rayXpZp, out XpZpHit, 100))
        {
            if (XpZpHit.collider.CompareTag("Blaze"))
            {
                float XpZpdistance = Vector3.Distance(XpZpHit.transform.position, transform.position);
                XpZpDistance = XpZpdistance / 10;
                if (XpZpDistance > 5)
                {
                    UpRight = false;
                }
                else
                {
                    UpRight = true;
                }
            }
            else
            {
                UpRight = false;
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
                float XpZmdistance = Vector3.Distance(XpZmHit.transform.position, transform.position);
                XpZmDistance = XpZmdistance / 10;
                if (XpZmDistance > 5)
                {
                    UnderRight = false;
                }
                else
                {
                    UnderRight = true;
                }
            }
            else
            {
                UnderRight = false;
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
                float XmZpdistance = Vector3.Distance(XmZpHit.transform.position, transform.position);
                XmZpDistance = XmZpdistance / 10;
                if (XmZpdistance > 5)
                {
                    UpLeft = false;
                }
                else
                {
                    UpLeft = true;
                }
            }
            else
            {
                UpLeft = false;
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
                float XmZmdistance = Vector3.Distance(XmZmHit.transform.position, transform.position);
                XmZmDistance = XmZmdistance / 10;
                if (XmZmDistance > 5)
                {
                    UnderLeft = false;
                }
                else
                {
                    UnderLeft = true;
                }
            }
            else
            {
                UnderLeft = false;
            }
        }
        else
        {
            UnderLeft = false;
        }
    }
}
