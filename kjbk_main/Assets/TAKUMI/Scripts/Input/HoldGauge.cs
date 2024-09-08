using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

internal class HoldGauge : MonoBehaviour
{
    [SerializeField] private InputActionReference _hold;
    [SerializeField] private Image _gaugeImage;

    private InputAction _holdAction;
    public static bool gaugeStatus = false;

    float progress;
    bool gaugeActivated = false;

    private void Awake()
    {
        if (_hold == null) return;

        _holdAction = _hold.action;
        _holdAction.Enable();
    }

    private void OnEnable()
    {
        progress = 0.0f;
        gaugeStatus = false;
        gaugeActivated = false;
    }

    private void Update()
    {
        if (_holdAction == null) return;

        // 長押しの進捗を取得
        progress = _holdAction.GetTimeoutCompletionPercentage();

        // 進捗をゲージに反映
        _gaugeImage.fillAmount = progress;

        // 進捗が1以上になったときの処理
        if (progress >= 1 && !gaugeActivated)
        {
            // gaugeStatusをtrueにする
            gaugeStatus = true;
            gaugeActivated = true;

            // ゲージをリセット
            _gaugeImage.fillAmount = 0;
            _holdAction.Disable();  // Actionを一旦無効化
            _holdAction.Enable();   // すぐに有効化して次の入力に備える
            progress = 0.0f;
        }
        else if (gaugeActivated)
        {
            // 1フレーム後にgaugeStatusをfalseに戻す
            gaugeStatus = false;
            gaugeActivated = false;
        }
    }
}
