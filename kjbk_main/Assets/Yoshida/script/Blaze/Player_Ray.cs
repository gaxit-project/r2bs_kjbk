using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ray : MonoBehaviour
{
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

    public bool Up = false;
    public bool Under = false;
    public bool Left = false;
    public bool Right = false;

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

        rayXp = new Ray(this.transform.position, Xp);
        rayZp = new Ray(this.transform.position, Zp);
        rayXm = new Ray(this.transform.position, Xm);
        rayZm = new Ray(this.transform.position, Zm);

        Debug.DrawRay(rayXp.origin, rayXp.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayZp.origin, rayZp.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayXm.origin, rayXm.direction * 100, Color.red, 1, false);
        Debug.DrawRay(rayZm.origin, rayZm.direction * 100, Color.red, 1, false);

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
    }
}
