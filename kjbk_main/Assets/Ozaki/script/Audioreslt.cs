using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioreslt : MonoBehaviour
{
    //public AudioClip sound1;
    private AudioSource audioSource;
    private float playInterval = 1f; // オーディオを再生する間隔（秒）
    private int i=0;
    void Start()
    {
        // AudioSource コンポーネントを取得
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource がアタッチされていない場合、自動的に追加
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        
    }

    public void PlaySE(AudioClip clip)
    {
        //audioSource.clip = sound1;
        audioSource.clip = clip;
        audioSource.Play();
    }

    IEnumerator PlayAudioPeriodically()
    {
        while (true)
        {
          if(i==8){
            break;
          }
          if(i!=0){
            audioSource.Play(); // オーディオを再生
          }
          if(i==5){
            playInterval=1.5f;
          }
          if(i==6){
            playInterval=1.2f;
          }
            yield return new WaitForSeconds(playInterval); // 5秒待つ
            i++;
        }
    }
}
