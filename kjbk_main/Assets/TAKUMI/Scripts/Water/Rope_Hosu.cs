using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_Hosu : MonoBehaviour
{
    public GameObject[] vertices = new GameObject[20];
    LineRenderer line;
    [SerializeField] public PlayerRayCast PlayerRayCast;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Unlit/Color"));
        line.positionCount = vertices.Length;

        foreach (GameObject v in vertices)
        {
            v.GetComponent<MeshRenderer>().enabled = false;
        }
        //this.gameObject.SetActive(false);
    }

    void Update()
    {
        int idx = 0;
        foreach (GameObject v in vertices)
        {
            line.SetPosition(idx, v.transform.position);
            idx++;
        }
        /*if (PlayerRayCast.HosuStatus)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }*/
    }
}
