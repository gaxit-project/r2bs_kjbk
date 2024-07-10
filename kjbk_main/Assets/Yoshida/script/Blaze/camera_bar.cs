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

    private MeshRenderer UpMesh;
    private MeshRenderer UnderMesh;
    private MeshRenderer LeftMesh;
    private MeshRenderer RightMesh;

    // Start is called before the first frame update
    void Start()
    {
        ray = Player.GetComponent<Player_Ray>();

        UpMesh = UpPlane.GetComponent<MeshRenderer>();
        UnderMesh = UnderPlane.GetComponent<MeshRenderer>();
        LeftMesh = LeftPlane.GetComponent<MeshRenderer>();
        RightMesh = RightPlane.GetComponent<MeshRenderer>();

        UpMesh.material.color = UpMesh.material.color - new Color32(255, 0, 0, 255);
        UnderMesh.material.color = new Color32(255, 0, 0, 255);
        LeftMesh.material.color = new Color32(255, 0, 0, 255);
        RightMesh.material.color = new Color32(255, 0, 0, 255);
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
    }
}
