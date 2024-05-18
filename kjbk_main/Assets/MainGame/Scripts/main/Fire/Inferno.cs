using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class Inferno : MonoBehaviour
{
    GameObject BlazeR;//赤色の火のパーティクルを格納
    //GameObject BlazeY;//黄色の火のパーティクルを格納

    ParticleSystem ParticleBlazeR;//赤色の火のパーティクルシステムを格納
    //ParticleSystem ParticleBlazeY;//黄色の火のパーティクルシステムを格納

    ParticleSystem.EmissionModule BR; //赤色の火のEmissionModule格納
    ///ParticleSystem.EmissionModule BY; //黄色の火のEmissionModule格納

    ParticleSystem.MinMaxCurve BR_MinMaxCurve;
    //ParticleSystem.MinMaxCurve BY_MinMaxCurve;

    float BlazeRDown = 4.0f;
    //float BlazeYDown = 2.0f;

    float BlazeRUp = 4.0f;
    //float BlazeYUp = 2.0f;

    public bool FireStatus = false; // 延焼ならfalse
    public bool P_O_Fire = false; //消化中判定
    public bool DesBlaze = false; //消化されたか

    FireLv FireLv;

    void Start()
    {
        GameObject BlazeR = GameObject.Find("Fire_Red");
        //GameObject BlazeY = GameObject.Find("Fire_Yellow");

        ParticleBlazeR = BlazeR.GetComponent<ParticleSystem>();
        //ParticleBlazeY = BlazeY.GetComponent<ParticleSystem>();

        BR = ParticleBlazeR.emission;
        //BY = ParticleBlazeY.emission;

        BR.rateOverTime = 0;
        //BY.rateOverTime = 0;
        BR_MinMaxCurve = BR.rateOverTime;
        //BY_MinMaxCurve = BY.rateOverTime;

        BR.rateOverTime = 100;
        //BY.rateOverTime = 100;
        BR_MinMaxCurve = BR.rateOverTime;
        //BY_MinMaxCurve = BY.rateOverTime;

        if (FireStatus)
        {
            BR.rateOverTime = 100;
            //BY.rateOverTime = 100;
            BR_MinMaxCurve = BR.rateOverTime;
            //BY_MinMaxCurve = BY.rateOverTime;
        }
        Audio.Instance.PlayRoopSE(0);
    }

    void Update()
    {
        if (!FireStatus && BR_MinMaxCurve.constant <= 100f  && !P_O_Fire)//&& BY_MinMaxCurve.constant <= 100f
        {
            BR_MinMaxCurve.constant += BlazeRUp * Time.deltaTime;
            //BY_MinMaxCurve.constant += BlazeRUp * Time.deltaTime;
            BR.rateOverTime = BR_MinMaxCurve;
            //BY.rateOverTime = BY_MinMaxCurve;
        }

    }

    public void OnParticleCollision(GameObject obj)
    {
        //InfernoScriptが入る変数
        Inferno script = this.GetComponent<Inferno>();

        //当たっている火の子オブジェクトの取得
        GameObject BlazeR1 = this.transform.GetChild(0).gameObject;
        //GameObject BlazeY1 = this.transform.GetChild(1).gameObject;

        //赤色の火のパーティクルシステムを格納
        ParticleSystem ParticleBlazeR1 = BlazeR1.GetComponent<ParticleSystem>();
        //黄色の火のパーティクルシステムを格納
        //ParticleSystem ParticleBlazeY1 = BlazeY1.GetComponent<ParticleSystem>();

        //赤色の火のEmissionModule格納
        ParticleSystem.EmissionModule BR1 = ParticleBlazeR1.emission;
        //黄色の火のEmissionModule格納
        //ParticleSystem.EmissionModule BY1 = ParticleBlazeY1.emission;

        ParticleSystem.MinMaxCurve BR1_MinMaxCurve = BR1.rateOverTime;
        //ParticleSystem.MinMaxCurve BY1_MinMaxCurve = BY1.rateOverTime;

        Debug.Log("消化中");
        script.P_O_Fire = true;
        BR1_MinMaxCurve.constant -= BlazeRDown * Time.deltaTime * 100;
        //BY1_MinMaxCurve.constant -= BlazeRDown * Time.deltaTime * 100;
        BR1.rateOverTime = BR1_MinMaxCurve;
        //BY1.rateOverTime = BY1_MinMaxCurve;
        if (BR1_MinMaxCurve.constant <= 0f)// && BY1_MinMaxCurve.constant <= 0f
        {
            Debug.Log("消化されました");
            DesBlaze = true;
        }
    }
}
