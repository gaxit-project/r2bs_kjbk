using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameFlag : MonoBehaviour
{
    [SerializeField] private InputActionReference _hold;
    [SerializeField] private Image _gaugeImage;

    private InputAction _holdAction;

    private bool ExitStatus = false;

    private int Rcnt = 0;

    public int RescueBorder = 10;


    private void Awake()
    {
        if (_hold == null) return;

        _holdAction = _hold.action;
        _holdAction.Enable();
    }

    void Start()
    {
        Rcnt = 0;
    }

    void Update()
    {
        if (_holdAction == null) return;

        // 長押しの進捗を取得
        var progress = _holdAction.GetTimeoutCompletionPercentage();

        // 進捗をゲージに反映
        _gaugeImage.fillAmount = progress;

        Rcnt = PlayerPrefs.GetInt("RescueCount");

        if (progress >= 1)
        {
            ExitStatus = true; // ExitStatusをtrueにする
            _gaugeImage.fillAmount = 0;
            _holdAction.Disable();  // Actionを一旦無効化
            _holdAction.Enable();   // すぐに有効化して次の入力に備える

            //救助した人数がRescueBorder人以上ならクリアへ移行
            if (Rcnt >= RescueBorder)
            {
                PlayerPrefs.SetString("Result", "CLEAR");
                Scene.Instance.GameResult();
            }

            //違うならゲームオーバーに移行
            else
            {
                PlayerPrefs.SetString("Result", "GAMEOVER");
                Scene.Instance.GameResult();
            }
        }
        else
        {
            // ExitStatusをfalseにリセット
            ExitStatus = false;
        }
    }
}
