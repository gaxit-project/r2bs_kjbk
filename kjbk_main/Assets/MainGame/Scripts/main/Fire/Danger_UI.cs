using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;


public class Danger_UI : MonoBehaviour
{
    [SerializeField] public GameObject Player;
    private Danger_Ray ray;

    public GameObject Up;
    public GameObject Under;
    public GameObject Left;
    public GameObject Right;

    private Image UpImage;
    private Image UnderImage;
    private Image LeftImage;
    private Image RightImage;

    private double _time;

    // Start is called before the first frame update
    void Start()
    {
        ray = Player.GetComponent<Danger_Ray>();

        UpImage = Up.GetComponent<Image>();
        UnderImage = Under.GetComponent<Image>();
        LeftImage = Left.GetComponent<Image>();
        RightImage = Right.GetComponent<Image>();

        var c = UpImage.color;
        c.a = 0;
        UpImage.color = c;

        c = UnderImage.color;
        c.a = 0;
        UnderImage.color = c;

        c = LeftImage.color;
        c.a = 0;
        LeftImage.color = c;

        c = RightImage.color;
        c.a = 0;
        RightImage.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (ray.Up)
        {
            float Upalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / ray.ZpDistance)) * 0.5f + 0.5f;
            var c = UpImage.color;
            c.a = Upalpha;
            UpImage.color = c;
        }
        else
        {
            var c = UpImage.color;
            c.a = 0;
            UpImage.color = c;
        }

        if (ray.Under)
        {
            float Underalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / ray.ZmDistance)) * 0.5f + 0.5f;
            var c = UnderImage.color;
            c.a = Underalpha;
            UnderImage.color = c;
        }
        else
        {
            var c = UnderImage.color;
            c.a = 0;
            UnderImage.color = c;
        }

        if (ray.Right)
        {
            float Rightalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / ray.XpDistance)) * 0.5f + 0.5f;
            var c = RightImage.color;
            c.a = Rightalpha;
            RightImage.color = c;
        }
        else
        {
            var c = RightImage.color;
            c.a = 0;
            RightImage.color = c;
        }

        if (ray.Left)
        {
            float Leftalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / Mathf.Round(ray.XmDistance))) * 0.5f + 0.5f;
            var c = LeftImage.color;
            c.a = Leftalpha;
            LeftImage.color = c;
        }
        else
        {
            var c = LeftImage.color;
            c.a = 0;
            LeftImage.color = c;
        }
    }
}
