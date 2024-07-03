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
        Invoke("Stop", 1.5f);
        
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

    private void Stop()
    {
        StopCoroutine("Blink");
        Destroy(this.gameObject);
    }
}
