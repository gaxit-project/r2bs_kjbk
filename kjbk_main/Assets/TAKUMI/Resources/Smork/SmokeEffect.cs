using UnityEngine;

[ExecuteInEditMode]
public class SmokeEffect : MonoBehaviour
{
    // 煙のエフェクトを適用するマテリアル
    public Material smokeMaterial;

    // 煙の濃度を調整するための変数（0から1の範囲で調整可能）
    [Range(0, 1)]
    public float Alphe = 1f;

    // 煙の濃度を調整するための変数（0から1の範囲で調整可能）
    [Range(0, 1)]
    public float smoke = 1f;

    // スムーズな遷移のための速度係数
    public float transitionSpeed = 2f;

    float target;

    float sm;

    private void Start()
    {
        Alphe = 1;
        smoke = 1;
        target = 1;
    }

    void Update()
    {

        if(1 == PlayerPrefs.GetInt("InRoom"))
        {
            // target を PlayerPrefs から取得
            target = PlayerPrefs.GetFloat("SmokeConc");
        }
        else
        {
            target = 1;
        }
        Debug.Log(target + "target");
        if (target != smoke)
        {
            // Mathf.Lerp を使用して smoke を target にスムーズに遷移させる
            smoke = Mathf.Lerp(smoke, target, Time.deltaTime * transitionSpeed);
        }


        // 値が範囲外にならないようにクランプ
        smoke = Mathf.Clamp(smoke, 0f, 1f);
        Alphe = Mathf.Clamp(Alphe, 0f, 1f);

        // マテリアルが指定されている場合、エフェクトを適用
        if (smokeMaterial != null)
        {
            // マテリアルに煙の濃度を設定
            smokeMaterial.SetFloat("_Alphe", Alphe);

            // マテリアルに煙の色を設定
            smokeMaterial.SetFloat("_CenterRadius", smoke);
        }
    }
}
