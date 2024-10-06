using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIFE : MonoBehaviour
{
    #region 宣言
    // プレイヤーのHP
    public static int HitPoint = 3;

    // LIFE_Bar の参照
    public LIFE_Bar BarHP;

    // アニメーターコンポーネント
    private Animator Anim;

    // 無敵時間関連の変数
    public float invincibilityDuration = 5.0f; // 無敵時間（秒）
    private float invincibilityTimer = 0.0f;   // 経過時間を格納するタイマー変数
    private bool isInvincible = false;         // 無敵状態フラグ

    // 点滅関連の変数
    bool flag = true;
    [SerializeField] private Renderer renderComponent1;
    [SerializeField] private Renderer renderComponent2;
    [SerializeField] private Renderer renderComponent3;
    bool Fadered = false;
    #endregion

    #region 初期化
    void Start()
    {
        // HP を初期化
        HitPoint = 3;
        // アニメーターコンポーネントを取得
        Anim = GetComponent<Animator>();
    }
    #endregion

    #region 更新処理
    void Update()
    {
        if (isInvincible)
        {
            // 無敵状態の処理
            invincibilityTimer += Time.deltaTime;

            if (invincibilityTimer >= invincibilityDuration)
            {
                // 無敵状態を終了
                isInvincible = false;
                invincibilityTimer = 0.0f;
            }
        }
    }
    #endregion

    #region コリジョン処理
    private void OnCollisionEnter(Collision Hit)
    {
        if (Hit.gameObject.tag == "Blaze")
        {
            if (isInvincible)
            {
                return;
            }
            else
            {
                Audio.GetInstance().PlaySound(8);  // ダメージを受けた声を再生
                // HP を減らす
                HitPoint--;
                // スターを取ったときの処理
                GetStar();
                // HPバーを更新
                BarHP.HPBar();

                // 点滅処理を開始
                StartCoroutine(Blink());

                if (HitPoint <= 0)
                {
                    Fadered = true;
                    Audio.GetInstance().PlaySound(9);  // ダメージで死んだときの声を再生
                    // HP が尽きた場合の処理
                    Anim.SetBool("CarryWalk", false);
                    Anim.SetBool("Carry", false);
                    PlayerPrefs.SetString("Result", "GAMEOVER");
                    Scene.Instance.GameResult();
                }
            }
        }
    }
    #endregion

    #region 無敵状態処理
    public void GetStar()
    {
        // スターを取ったときに無敵状態フラグを True にする
        isInvincible = true;
    }
    #endregion

    #region 死んだらフェードアウトを赤
    public bool getred()
    {
        return Fadered;
    }
    #endregion

    #region 点滅処理
    IEnumerator Blink()
    {
        if (flag)
        {
            for (int i = 0; i < 24; i++)
            {
                flag = false;
                renderComponent1.enabled = !renderComponent1.enabled;
                renderComponent2.enabled = !renderComponent2.enabled;
                renderComponent3.enabled = !renderComponent3.enabled;
                yield return new WaitForSeconds(0.1f);
                renderComponent1.enabled = !renderComponent1.enabled;
                renderComponent2.enabled = !renderComponent2.enabled;
                renderComponent3.enabled = !renderComponent3.enabled;
                yield return new WaitForSeconds(0.1f);
            }
            flag = true;
        }
    }
    #endregion
}
