using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffect : MonoBehaviour
{
    ParticleSystem m_ParticleSystem;
    private ParticleSystem.Particle[] m_Particles;
    float m_TotalTime = 0;
    public float m_Speed = 1;
    Vector3 tmp;

    void Start()
    {
        //コンポーネントを取得
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // 必要な場合のみ配列を作成しなおす
        int maxParticles = m_ParticleSystem.main.maxParticles;
        if (m_Particles == null || m_Particles.Length < maxParticles)
        {
            m_Particles = new ParticleSystem.Particle[maxParticles];
        }

        // 現在のパーティクルを取得する
        int particleNum = m_ParticleSystem.GetParticles(m_Particles);

        //パーティクルひとつひとつの処理
        for (int i = 0; i < particleNum; i++)
        {
            tmp = m_Particles[i].position;
            // ひとつひとつのパーティクルをゆらす
            tmp.x += Mathf.Cos(m_TotalTime * i) * m_Speed;
            m_Particles[i].position = tmp;
        }

        // 変更を適用する
        m_ParticleSystem.SetParticles(m_Particles, particleNum);

        m_TotalTime += Time.deltaTime;
    }
}
