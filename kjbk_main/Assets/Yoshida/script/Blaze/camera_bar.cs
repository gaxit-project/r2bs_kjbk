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
        if (ray.Up)
        {
            UpMesh.material.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            UpMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.Under)
        {
            UnderMesh.material.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            UnderMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.Right)
        {
            RightMesh.material.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            RightMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.Left)
        {
            LeftMesh.material.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            LeftMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.UpRight)
        {
            UpRightMesh.material.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            UpRightMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.UnderRight)
        {
            UnderRightMesh.material.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            UnderRightMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.UpLeft)
        {
            UpLeftMesh.material.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            UpLeftMesh.material.color = new Color32(255, 0, 0, 0);
        }

        if (ray.UnderLeft)
        {
            UnderLeftMesh.material.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            UnderLeftMesh.material.color = new Color32(255, 0, 0, 0);
        }
    }
}
