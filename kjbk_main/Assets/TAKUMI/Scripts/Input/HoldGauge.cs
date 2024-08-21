using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

internal class HoldGauge : MonoBehaviour
{
    // 入力を受け取る対象のAction
    [SerializeField] private InputActionReference _hold;

    // ゲージのUI
    [SerializeField] private Image _gaugeImage;

    //[SerializeField] private GameObject cube;

    private InputAction _holdAction;

    private void Awake()
    {
        if (_hold == null) return;

        _holdAction = _hold.action;

        // 入力を受け取るためには必ず有効化する必要がある
        _holdAction.Enable();
    }

    private void Update()
    {
        if (_holdAction == null) return;

        // 長押しの進捗を取得
        var progress = _holdAction.GetTimeoutCompletionPercentage();

        // 進捗をログ出力
        //Debug.Log($"Progress : {progress * 100}%");
        if (progress >= 1)
        {
            Debug.Log("長押し完了");
        }

        // 進捗をゲージに反映
        _gaugeImage.fillAmount = progress;

        /*if(progress >= 1)
        {
            cube.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 1);
        }
        else
        {
            cube.GetComponent<Renderer>().material.color = new Color32(248, 168, 133, 1);
        }*/
    }

    // 長押しされたときに呼ばれるメソッド
    private void OnHold(InputAction.CallbackContext context)
    {
        Debug.Log("長押しされた！");
    }
}