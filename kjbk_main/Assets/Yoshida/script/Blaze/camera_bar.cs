using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camera_bar : MonoBehaviour
{
    [SerializeField] public GameObject Player;
    private Player_Ray ray;

    public GameObject UpPlane;
    public GameObject UnderPlane;
    public GameObject LeftPlane;
    public GameObject RightPlane;
    public GameObject UpRightPlane;
    public GameObject UpLeftPlane;
    public GameObject UnderRightPlane;
    public GameObject UnderLeftPlane;

    private MeshRenderer UpMesh;
    private MeshRenderer UnderMesh;
    private MeshRenderer LeftMesh;
    private MeshRenderer RightMesh;
    private MeshRenderer UpRightMesh;
    private MeshRenderer UpLeftMesh;
    private MeshRenderer UnderRightMesh;
    private MeshRenderer UnderLeftMesh;

    private double _time;

    // Start is called before the first frame update
    void Start()
    {
        ray = Player.GetComponent<Player_Ray>();

        UpMesh = UpPlane.GetComponent<MeshRenderer>();
        UnderMesh = UnderPlane.GetComponent<MeshRenderer>();
        LeftMesh = LeftPlane.GetComponent<MeshRenderer>();
        RightMesh = RightPlane.GetComponent<MeshRenderer>();
        UpRightMesh = UpRightPlane.GetComponent<MeshRenderer>();
        UpLeftMesh = UpLeftPlane.GetComponent<MeshRenderer>();
        UnderRightMesh = UnderRightPlane.GetComponent<MeshRenderer>();
        UnderLeftMesh = UnderLeftPlane.GetComponent<MeshRenderer>();

        UpMesh.material.color = new Color32(255, 0, 0, 255);
        UnderMesh.material.color = new Color32(255, 0, 0, 255);
        LeftMesh.material.color = new Color32(255, 0, 0, 255);
        RightMesh.material.color = new Color32(255, 0, 0, 255);
        UpRightMesh.material.color = new Color32(255, 0, 0, 255);
        UpLeftMesh.material.color = new Color32(255, 0, 0, 255);
        UnderRightMesh.material.color = new Color32(255, 0, 0, 255);
        UnderLeftMesh.material.color = new Color32(255, 0, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (ray.Up)
        {
            float Upalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / ray.ZpDistance)) * 0.5f + 0.5f;
            UpMesh.material.color = new Color32(255, 0, 0, (byte)Upalpha);
        }
        else
        {
            UpMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.Under)
        {
            float Underalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / ray.ZmDistance)) * 0.5f + 0.5f;
            UnderMesh.material.color = new Color32(255, 0, 0, (byte)Underalpha);
        }
        else
        {
            UnderMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.Right)
        {
            float Rightalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / ray.XpDistance)) * 0.5f + 0.5f;
            RightMesh.material.color = new Color32(255, 0, 0, (byte)Rightalpha);
        }
        else
        {
            RightMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.Left)
        {
            float Leftalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / Mathf.Round(ray.XmDistance))) * 0.5f + 0.5f;
            LeftMesh.material.color = new Color32(255, 0, 0, (byte)Leftalpha);
        }
        else
        {
            LeftMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.UpRight)
        {
            float UpRightalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / ray.XpZpDistance)) * 0.5f + 0.5f;
            UpRightMesh.material.color = new Color32(255, 0, 0, (byte)UpRightalpha);
        }
        else
        {
            UpRightMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.UnderRight)
        {
            float UnderRightalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / ray.XpZmDistance)) * 0.5f + 0.5f;
            UnderRightMesh.material.color = new Color32(255, 0, 0, (byte)UnderRightalpha);
        }
        else
        {
            UnderRightMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.UpLeft)
        {
            float UpLeftalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / ray.XmZpDistance)) * 0.5f + 0.5f;
            UpLeftMesh.material.color = new Color32(255, 0, 0, (byte)UpLeftalpha);
        }
        else
        {
            UpLeftMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.UnderLeft)
        {
            float UnderLeftalpha = 255 * Mathf.Cos((float)(2 * Mathf.PI * _time / ray.XmZmDistance)) * 0.5f + 0.5f;
            UnderLeftMesh.material.color = new Color32(255, 0, 0, (byte)UnderLeftalpha);
        }
        else
        {
            UnderLeftMesh.material.color = new Color32(255, 0, 0, 0);
        }
    }
}
