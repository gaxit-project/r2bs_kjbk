using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inferno : MonoBehaviour
{
    //赤色の火のパーティクルを格納
    public GameObject BlazeR;
    //黄色の火のパーティクルを格納
    public GameObject BlazeY;

    //赤色の火のパーティクルシステムを格納
    public ParticleSystem ParticleBlazeR;
    //黄色の火のパーティクルシステムを格納
    public ParticleSystem ParticleBlazeY;

    //消火時間
    public float time = 0.0f;
    public float span = 3.0f;

    void Start()
    {
        BlazeR = transform.Find("Fire_Red").gameObject;
        BlazeY = transform.Find("Fire_Yellow").gameObject;

        ParticleBlazeR = BlazeR.GetComponent<ParticleSystem>();
        ParticleBlazeY = BlazeY.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        
    }

    public void OnParticleCollision(GameObject obj)
    {
        Debug.Log("消化中");
        time += Time.deltaTime;
        if (time >= 2.0f){
            ParticleBlazeR.Stop();
            ParticleBlazeY.Stop();
            time = 0.0f;
        }
    }
}
