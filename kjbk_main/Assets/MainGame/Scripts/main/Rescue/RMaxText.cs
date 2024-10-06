using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RMaxText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RMax;
    private Animator RMaxAnimator; // Animator を追加

    int Cnt;
    bool hasPlayedAnimation = false; // アニメーションが再生されたかのフラグ

    private void Awake()
    {
        Cnt = PlayerPrefs.GetInt("RescueCount");
        RMaxAnimator = GetComponent<Animator>(); // Animator を取得
    }

    void Start()
    {
        // 救助可能最大人数の表示
        RMax.SetText("<sprite=1><sprite=0>");
    }

    void Update()
    {
        Cnt = PlayerPrefs.GetInt("RescueCount");

        if (Cnt >= 10 && !hasPlayedAnimation)
        {
            RMax.SetText("<sprite=3><sprite=0>");
            RMaxAnimator.SetTrigger("ScaleUpTrigger"); // アニメーションのトリガーをセット
            hasPlayedAnimation = true; // アニメーションが再生されたことを記録
        }
        else if (Cnt < 10 && hasPlayedAnimation)
        {
            // Cnt が 10 未満になったときにフラグをリセット
            hasPlayedAnimation = false;
            RMax.SetText("<sprite=1><sprite=0>");
        }
    }
}
