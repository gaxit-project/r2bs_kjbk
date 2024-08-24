using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

internal class HoldGauge : MonoBehaviour
{
    [SerializeField] private InputActionReference _hold;
    [SerializeField] private Image _gaugeImage;
    [SerializeField] private GameObject cube;

    private InputAction _holdAction;
    public static bool gaugeStatus = false;

    private void Awake()
    {
        if (_hold == null) return;

        _holdAction = _hold.action;
        _holdAction.Enable();
    }

    private void Update()
    {
        if (_holdAction == null) return;

        gaugeStatus = false;

        // 長押しの進捗を取得
        var progress = _holdAction.GetTimeoutCompletionPercentage();

        // 進捗をゲージに反映
        _gaugeImage.fillAmount = progress;

        // 進捗が1以上になったときの処理
        if (progress >= 0.97)
        {
            gaugeStatus = true; // aaをtrueにする

            

            // キューブの色を変更
            //cube.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 255);

            // ゲージをリセット
            _gaugeImage.fillAmount = 0;
            _holdAction.Disable();  // Actionを一旦無効化
            _holdAction.Enable();   // すぐに有効化して次の入力に備える
        }
        else
        {
            // aaをfalseにリセット
            gaugeStatus = false;
            //cube.GetComponent<Renderer>().material.color = new Color32(248, 168, 133, 255);
        }
    }
}
