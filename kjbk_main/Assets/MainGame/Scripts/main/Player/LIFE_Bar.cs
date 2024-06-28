using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class LIFE_Bar : MonoBehaviour
{
    [SerializeField] GameObject HP1Red;
    [SerializeField] GameObject HP2;
    [SerializeField] GameObject HP2Red;
    [SerializeField] GameObject HP3;
    [SerializeField] GameObject HP3Red;

    bool flag = true;
    [SerializeField] private Renderer renderComponent3;
    [SerializeField] private Renderer renderComponent2;
    [SerializeField] private Renderer renderComponent1;

    [SerializeField] private Renderer renderComponent4;
    [SerializeField] private Renderer renderComponent5;
    [SerializeField] private Renderer renderComponent6;


    int i = 3;

    void Start()
    {
        HP1Red.SetActive(false);
        HP2.SetActive(false);
        HP2Red.SetActive(false);
        HP3.SetActive(false);
        HP3Red.SetActive(false);
    }


    public void HPBar()
    {
        if (i == 3)
        {
            HP1Red.SetActive(true);
            HP2Red.SetActive(false);
            HP3Red.SetActive(true);
            HP3.SetActive(true);
            HP2.SetActive(true);
            StartCoroutine(HPBar2());
            Invoke(nameof(HP1RedKesu), 5f);
            Invoke(nameof(HP2RedKesu), 5f);
            Invoke(nameof(HP3RedKesu), 5f);
            Invoke(nameof(HP3Kesu), 5f);
            Invoke(nameof(HP2Kesu), 5f);
        }
        else if (i == 2)
        {
            HP2.SetActive(false);
            HP1Red.SetActive(true);
            HP2Red.SetActive(true);
            HP3.SetActive(true);
            StartCoroutine(HPBar1());
            Invoke(nameof(HP1RedKesu), 5f);
            Invoke(nameof(HP2RedKesu), 5f);
            Invoke(nameof(HP2Kesu), 5f);
            Invoke(nameof(HP3Kesu), 5f);
        }
        i--;
    }

    IEnumerator HPBar2()
    {
        {
            for (int i = 0; i < 10; i++)
            {
                HP3Red.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                HP3Red.SetActive(true);
                yield return new WaitForSeconds(0.2f);
            }
        }
        HP3Red.SetActive(false);
    }

    IEnumerator HPBar1()
    {
        {
            for (int i = 0; i < 10; i++)
            {
                HP2Red.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                HP2Red.SetActive(true);
                yield return new WaitForSeconds(0.2f);
            }
        }
        HP2Red.SetActive(false);
    }





    public void HP3RedKesu()
    {
        HP3Red.SetActive(false);
    }
    public void HP2RedKesu()
    {
        HP2Red.SetActive(false);
    }
    public void HP1RedKesu()
    {
        HP1Red.SetActive(false);
    }

    public void HP3Kesu()
    {
        HP3.SetActive(false);
    }
    public void HP2Kesu()
    {
        HP2.SetActive(false);
    }
}
