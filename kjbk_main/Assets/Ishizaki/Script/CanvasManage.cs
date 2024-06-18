using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManage : MonoBehaviour
{
    private RectTransform MyRectTfm;

    // Start is called before the first frame update
    void Start()
    {
        MyRectTfm = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // ©g‚ÌŒü‚«‚ğƒJƒƒ‰‚ÉŒü‚¯‚é
        MyRectTfm.LookAt(Camera.main.transform);
    }
}

