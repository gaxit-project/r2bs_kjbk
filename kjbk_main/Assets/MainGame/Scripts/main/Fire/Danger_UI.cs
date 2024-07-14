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

    private CanvasGroup UpImage;
    private CanvasGroup UnderImage;
    private CanvasGroup LeftImage;
    private CanvasGroup RightImage;

    private double _time;

    // Start is called before the first frame update
    void Start()
    {
        ray = Player.GetComponent<Danger_Ray>();

        UpImage = Up.GetComponent<CanvasGroup>();
        UnderImage = Under.GetComponent<CanvasGroup>();
        LeftImage = Left.GetComponent<CanvasGroup>();
        RightImage = Right.GetComponent<CanvasGroup>();

        UpImage.alpha = 0;
        UnderImage.alpha = 0;
        LeftImage.alpha = 0;
        RightImage.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (ray.Up)
        {
            var Upalpha = Mathf.Abs(Mathf.Clamp(ray.ZpDistance, 0, 1) - 1);
            UpImage.alpha = Upalpha;
        }
        else
        {
            UpImage.alpha = 0;
        }

        if (ray.Under)
        {
            float Underalpha = Mathf.Abs(Mathf.Clamp(ray.ZmDistance, 0, 1) - 1);
            UnderImage.alpha = Underalpha;
        }
        else
        {
            UnderImage.alpha = 0;
        }

        if (ray.Right)
        {
            float Rightalpha = Mathf.Abs(Mathf.Clamp(ray.XpDistance, 0, 1) - 1);
            RightImage.alpha = Rightalpha;
        }
        else
        {
            RightImage.alpha = 0;
        }

        if (ray.Left)
        {
            float Leftalpha = Mathf.Abs(Mathf.Clamp(ray.XmDistance, 0, 1) - 1);
            LeftImage.alpha = Leftalpha;
        }
        else
        {
            LeftImage.alpha = 0;
        }
    }
}
