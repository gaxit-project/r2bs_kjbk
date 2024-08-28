using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AratalBlaze : MonoBehaviour
{
    public GameObject[] Artal;
    int i;
    private int len;
    int rand;
    float ran;
    Transform myTransform;
    float AratalCap;

    public bool FireStatus = false; // 延焼ならfalse
    public bool P_O_Fire = false; //消化中判定
    public bool DesBlaze = false; //消化されたか


    void Start()
    {
        len = Artal.Length;
        for (i = 1; i < len; i++)
        {
            Artal[i].SetActive(false);
        }

        ran = UnityEngine.Random.Range(0f, 90f);
        myTransform = this.transform;

    }

    void Update()
    {
        rand = Random.Range(0, len);
        ran = UnityEngine.Random.Range(0f, 90f);
        //Debug.Log(rand);
        ArtalSet(rand);
        // ワールド座標基準で、現在の回転量へ加算する
        myTransform.Rotate(0f, ran, 0f, Space.World);
    }
    void ArtalSet(int num)
    {
        for (i = 0; i < len; i++)
        {
            if (i == num)
            {
                Artal[i].SetActive(true);
            }
            else
            {
                Artal[i].SetActive(false);
            }
        }
    }

    public void OnParticleCollision(GameObject obj)
    {
        //InfernoScriptが入る変数
        Inferno script = this.GetComponent<Inferno>();

        //当たっている火の子オブジェクトの取得
        GameObject BlazeR1 = this.transform.GetChild(0).gameObject;

        Debug.Log("消化中");
        script.P_O_Fire = true;
        AratalCap -= 4f * Time.deltaTime * 100;
        if (AratalCap <= 0f)// && BY1_MinMaxCurve.constant <= 0f
        {
            Debug.Log("消化されました");
            DesBlaze = true;
        }
    }

}
