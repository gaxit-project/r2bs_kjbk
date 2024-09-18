using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioreslt : MonoBehaviour
{
    private AudioSource audioSourceSE; // 効果音（SE）用のAudioSource
    private AudioSource audioSourceBGM; // BGM用のAudioSource
    private float playInterval = 1f; // オーディオを再生する間隔（秒）
    private int i = 0;

    void Start()
    {
        // 効果音（SE）用のAudioSourceを取得
        audioSourceSE = GetComponent<AudioSource>();
        if (audioSourceSE == null)
        {
            // AudioSourceがアタッチされていない場合、自動的に追加
            audioSourceSE = gameObject.AddComponent<AudioSource>();
        }

        // BGM用のAudioSourceを追加（新しく作成）
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
        audioSourceBGM.loop = true; // BGMは通常ループ再生するので、ループを有効にする
    }

    void Update()
    {
        // 特に何も処理していないが、将来的に利用する場合
    }

    // 効果音（SE）を再生するメソッド
    public void PlaySE(AudioClip clip)
    {
        audioSourceSE.clip = clip;
        audioSourceSE.Play();
    }

    // BGMを再生するメソッド
    public void PlayBGM(AudioClip bgmClip)
    {
        audioSourceBGM.clip = bgmClip;
        audioSourceBGM.Play();
    }

    // BGMを停止するメソッド
    public void StopBGM()
    {
        if (audioSourceBGM.isPlaying)
        {
            audioSourceBGM.Stop();
        }
    }

    IEnumerator PlayAudioPeriodically()
    {
        while (true)
        {
            if (i == 8)
            {
                break;
            }
            if (i != 0)
            {
                audioSourceSE.Play(); // 効果音（SE）を再生
            }
            if (i == 5)
            {
                playInterval = 1.5f;
            }
            if (i == 6)
            {
                playInterval = 1.2f;
            }
            yield return new WaitForSeconds(playInterval); // 指定した秒数待機
            i++;
        }
    }
}
