using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inferno : MonoBehaviour
{
    #region フィールドの定義
    /// <summary>
    /// 火災シミュレーションの変数定義
    /// </summary>
    public GameObject[] Artal;       // 火オブジェクトの配列
    int i;                           // ループカウンタ
    private int len;                 // 配列の長さ
    int rand;                        // ランダムなインデックス
    float ran;                       // ランダムな回転角度
    Transform myTransform;           // 自身のTransform
    float AratalCap;                 // 火のキャパシティ

    public bool FireStatus = false;  // 延焼状態を判定するフラグ（延焼していればfalse）
    public bool P_O_Fire = false;    // 消火中かどうかの判定フラグ
    public bool DesBlaze = false;    // 消火完了の判定フラグ

    private AudioSource audiosource; // オーディオソースの参照
    #endregion

    #region 初期化メソッド
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        len = Artal.Length; // 配列の長さを取得
        for (i = 1; i < len; i++)
        {
            Artal[i].SetActive(false); // 初期状態で全ての配列を非アクティブに設定
        }

        ran = UnityEngine.Random.Range(0f, 90f); // ランダムな角度を生成
        myTransform = this.transform;            // 自身のTransformを取得
    }
    #endregion

    #region 更新メソッド
    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    void Update()
    {
        rand = Random.Range(0, len); // ランダムなインデックスを生成
        ran = UnityEngine.Random.Range(0f, 90f); // ランダムな回転角度を生成
        ArtalSet(rand); // アルタイルのアクティブ状態を更新
        myTransform.Rotate(0f, ran, 0f, Space.World); // ランダムな角度で回転
    }
    #endregion

    #region 配列設定メソッド
    /// <summary>
    /// アルタイルのアクティブ状態を設定
    /// </summary>
    void ArtalSet(int num)
    {
        for (i = 0; i < len; i++)
        {
            if (i == num)
            {
                Artal[i].SetActive(true); // 指定されたインデックスの配列をアクティブにする
            }
            else
            {
                Artal[i].SetActive(false); // それ以外は非アクティブにする
            }
        }
    }
    #endregion

    #region パーティクル衝突時の処理
    /// <summary>
    /// パーティクルがオブジェクトに衝突した時の処理
    /// </summary>
    public void OnParticleCollision(GameObject obj)
    {
        // Infernoスクリプトの参照を取得
        Inferno script = this.GetComponent<Inferno>();
        audiosource = this.GetComponent<AudioSource>();

        // 火の子オブジェクトを取得
        GameObject BlazeR1 = this.transform.GetChild(0).gameObject;

        // 消火中の処理
        script.P_O_Fire = true;
        AratalCap -= 4f * Time.deltaTime * 100; // 配列キャパシティを減少
        if (AratalCap <= 0f)
        {
            // 消火が完了した場合の処理
            Audio.GetInstance().PlaySound(17);
            audiosource.Play(); // 消火完了音を再生
            DesBlaze = true;    // 消火完了フラグを立てる
        }
    }
    #endregion
}
