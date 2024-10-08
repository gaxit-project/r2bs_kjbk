using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpSound : MonoBehaviour
{
    public AudioSource audioSource;

    private bool InPlayer = false; // プレイヤーが範囲内にいるかどうかを追跡
    private bool soundPlayed = false; // 音が再生されたかどうかを追跡


    private void OnTriggerEnter(Collider obj)
    {
        // プレイヤーがトリガーに入った時、まだ音が再生されていない場合のみ処理
        if (obj.CompareTag("Player") && !soundPlayed)
        {
            int rnd = Random.Range(0, 2);
            InPlayer = true;
            if (InPlayer)
            {
                soundPlayed = true; // 音を再生したので、フラグを立てて重複を防ぐ
                //audioSource.Play(); // 音を再生
                Audio.GetInstance().PlaySound(19 + rnd); // 別のサウンド再生処理があればここに記述
            }
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        // プレイヤーがトリガー範囲から出たら、フラグをリセット
        if (obj.CompareTag("Player"))
        {
            InPlayer = false;
            soundPlayed = false; // 再度トリガーに入った時に音を再生できるようにリセット
        }
    }
}
