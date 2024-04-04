using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inferno : MonoBehaviour
{
    //public GameObject BlazeR;//赤色の火のパーティクルを格納
    //public GameObject BlazeY;//黄色の火のパーティクルを格納

    public ParticleSystem ParticleBlazeR;//赤色の火のパーティクルシステムを格納
    public ParticleSystem ParticleBlazeY;//黄色の火のパーティクルシステムを格納

    float BlazeRMAX = 4.0f;
    float BlazeYMAX = 2.0f;

    float BlazeRDown = 4.0f;
    float BlazeYDown = 2.0f;

    float BlazeRUp = 4.0f;
    float BlazeYUp = 2.0f;

    ParticleSystem.MainModule BR;
    ParticleSystem.MainModule BY;

    public bool FireStatus = false;


    //消火時間
    public float time = 2.0f;

    void Start()
    {
        GameObject BlazeR = GameObject.Find("Fire_Red");
        GameObject BlazeY = GameObject.Find("Fire_Yellow");

        ParticleBlazeR = BlazeR.GetComponent<ParticleSystem>();
        ParticleBlazeY = BlazeY.GetComponent<ParticleSystem>();

        BR = ParticleBlazeR.main;
        BY = ParticleBlazeY.main;

        BR.startLifetime = BlazeRMAX;
        BY.startLifetime = BlazeYMAX;


        /*if (FireStatus)
        {
            BR.startLifetime = BlazeRMAX;
            BY.startLifetime = BlazeYMAX;
        }
        else
        {
            BR.startLifetime = 0.0f;
            BY.startLifetime = 0.0f;
        }*/
    }

    void Update()
    {
        /*if (!FireStatus)
        {
            BlazeRUp += Time.deltaTime / 10f;
            BlazeYUp += Time.deltaTime / 10f;
            BR.startLifetime = BlazeRUp;
            BY.startLifetime = BlazeYUp;
        }*/
    }

    public void OnParticleCollision()
    {
        Debug.Log("消化中");
        BlazeRDown -= Time.deltaTime * time * BlazeRMAX;
        BlazeYDown -= Time.deltaTime * time * BlazeYMAX;
        BR.startLifetime = BlazeRDown;
        BY.startLifetime = BlazeYDown;
        if(BlazeRDown <= 0f &&  BlazeYDown <= 0f)
        {
            Destroy(this);
        }
    }
    
}
