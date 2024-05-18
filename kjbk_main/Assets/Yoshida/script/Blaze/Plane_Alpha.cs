using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Alpha : MonoBehaviour
{

    MeshRenderer mesh;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        StartCoroutine("Blink");
        Invoke("Des", 1f);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            for (int i = 0; i < 100; i++)
            {
                mesh.material.color = mesh.material.color - new Color32(0, 0, 0, 1);
            }

            yield return new WaitForSeconds(0.2f);

            for (int j = 0; j < 100; j++)
            {
                mesh.material.color = mesh.material.color + new Color32(0, 0, 0, 1);
            }

            yield return new WaitForSeconds(0.2f);

        }
    }

    //ÚG”»’è(ÚG‚µ‚½uŠÔ)
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.name == "Blaze")
        {
            Des();
        }
    }

    void Des()
    {
        Destroy(gameObject, 0.1f);
    }
}
