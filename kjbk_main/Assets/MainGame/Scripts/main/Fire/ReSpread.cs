using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpread : MonoBehaviour
{
    #region フィールド宣言
    private float minSecond;       // 最小再発時間
    private float maxSecond;       // 最大再発時間

    private GameObject Blaze;      // Blaze_Maneger オブジェクト
    private Blaze_Maneger m_Blaze; // Blaze_Maneger コンポーネント

    private bool Action = true;    // アクションフラグ
    #endregion

    #region 初期化
    private void Start()
    {
        // Blaze_Maneger オブジェクトの取得
        Blaze = GameObject.Find("BlazeManeger");
        m_Blaze = Blaze.GetComponent<Blaze_Maneger>();

        // Blaze_Maneger からデータの取得
        var Data = m_Blaze.getReData();
        minSecond = Data.min;
        maxSecond = Data.max;

        // ランダムな再発時間を設定
        RandomReSpread();

        // maxSecond + 1秒後に Des メソッドを呼び出す
        Invoke("Des", maxSecond + 1f);
    }
    #endregion

    #region 再発処理
    private void RandomReSpread()
    {
        float Second = Random.Range(minSecond, maxSecond);

        if (Action)
        {
            // ランダムな時間後に Spread メソッドを呼び出す
            Invoke("Spread", Second);
            Action = false;
        }
    }

    private void Spread()
    {
        // Blaze を生成する位置の設定
        Vector3 blaze = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);

        // Blaze_Maneger で Blaze と SpreadPlane を生成
        m_Blaze.CreateBlaze(blaze);
        m_Blaze.CreateSpreadPlane(this.transform.position);

        // 自身のゲームオブジェクトを破壊
        Destroy(this.gameObject);
    }
    #endregion

    #region コリジョン処理
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "SpreadPlane")
        {
            Des();
        }
    }
    #endregion

    #region 自分を破壊
    private void Des()
    {
        // 自身のゲームオブジェクトを破壊
        Destroy(this.gameObject);
    }
    #endregion
}
