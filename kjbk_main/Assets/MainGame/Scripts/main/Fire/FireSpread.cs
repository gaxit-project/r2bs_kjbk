using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    // 延焼に関する変数
    #region 延焼関連の変数
    private float SpreadSecond;   // 延焼間隔(秒)
    private float SpreadProbability;   // 延焼確立(%)
    private int LvSpreadProbability;   // 炎レベルによる確率の上昇(確率に数値*(Lv-1)プラス)
    private float SpreadRange;   // 延焼時の移動距離
    private float PosY;
    private string[] AntiBlazeTag;
    #endregion

    // 救助や炎関連の変数
    #region 救助と炎関連の変数
    private GameObject Rescue;
    RescueCount Counter;
    public static bool FirstAction = true;
    private int boostNum;
    private bool boost = false;
    private bool FireXp = false;   // 炎周囲4マスの炎判定
    private bool FireZp = false;
    private bool FireXm = false;
    private bool FireZm = false;
    private int FireNum = 0;
    private int d = 0;
    public Inferno inferno;
    public FireLv Fire_Lv1;
    private GameObject Blaze;
    private Blaze_Maneger m_Blaze;
    #endregion

    // スタート時の処理
    #region スタート処理
    void Start()
    {
        // 救助オブジェクトの取得
        Rescue = GameObject.Find("Rcounter");
        Counter = Rescue.GetComponent<RescueCount>();

        // SpreadRangeが5以下の場合は5にする
        if (SpreadRange < 5) SpreadRange = 5;

        StartCoroutine("SpreadFire");

        // BlazeManagerのデータ取得
        Blaze = GameObject.Find("BlazeManeger");
        m_Blaze = Blaze.GetComponent<Blaze_Maneger>();
        var Data = m_Blaze.getSpreadData();
        SpreadSecond = Data.Second;
        SpreadProbability = Data.Probability;
        LvSpreadProbability = Data.LvProbability;
        SpreadRange = Data.Range;
        PosY = Data.Pos;
        boostNum = Data.Boost;
        AntiBlazeTag = Data.Tag;
    }
    #endregion

    // 毎フレームの処理
    #region 毎フレームの処理
    void Update()
    {
        #region 消化処理
        // 消火された場合の処理
        if (inferno.DesBlaze)
        {
            StopCoroutine("SpreadFire");
            m_Blaze.CreateExtPlane(new Vector3(this.transform.position.x, PosY, this.transform.position.z));
            Destroy(this.gameObject);
        }
        #endregion

        #region 延焼速度のブースト
        // 延焼速度のブースト
        if (Counter.getNum() >= boostNum && !boost)
        {
            SpreadSecond = SpreadSecond * 0.5f;
            boost = true;
        }
        #endregion
    }
    #endregion

    // 延焼処理のコルーチン
    #region 延焼処理
    IEnumerator SpreadFire()
    {
        // 延焼方向のベクトル設定
        Vector3 Xp = Vector3.right;
        Vector3 Zp = Vector3.forward;
        Vector3 Xm = Vector3.left;
        Vector3 Zm = Vector3.back;

        // 初期位置の設定
        Vector3 t = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);

        // Rayの設定
        Ray rayXp = new Ray(t, Xp);
        Ray rayZp = new Ray(t, Zp);
        Ray rayXm = new Ray(t, Xm);
        Ray rayZm = new Ray(t, Zm);

        while (true)
        {
            yield return new WaitForSeconds(SpreadSecond);
            decision(rayXp, rayZp, rayXm, rayZm);
            if (!inferno.FireStatus && !FireEmpty()) break;
            if (Fire_Lv1.FireLvel == 1) continue;
            d = dice();
            Plane();
            Invoke(nameof(Spread), 1f);
        }
    }
    #endregion

    // Rayの当たり判定処理
    #region Ray判定
    private void decision(Ray rayXp, Ray rayZp, Ray rayXm, Ray rayZm)
    {
        FireNum = 0;
        RaycastHit hit;
        if (Physics.Raycast(rayXp, out hit, SpreadRange))
        {
            if (Raydecision(hit) && !FireXp)
            {
                FireXp = true;
                FireNum++;
            }
        }
        else if (FireXp)
        {
            FireNum--;
            FireXp = false;
        }

        if (Physics.Raycast(rayZp, out hit, SpreadRange))
        {
            if (Raydecision(hit) && !FireZp)
            {
                FireZp = true;
                FireNum++;
            }
        }
        else if (FireZp)
        {
            FireNum--;
            FireZp = false;
        }

        if (Physics.Raycast(rayXm, out hit, SpreadRange))
        {
            if (Raydecision(hit) && !FireXm)
            {
                FireXm = true;
                FireNum++;
            }
        }
        else if (FireXm)
        {
            FireNum--;
            FireXm = false;
        }

        if (Physics.Raycast(rayZm, out hit, SpreadRange))
        {
            if (Raydecision(hit) && !FireZm)
            {
                FireZm = true;
                FireNum++;
            }
        }
        else if (FireZm)
        {
            FireNum--;
            FireZm = false;
        }
    }

    private bool Raydecision(RaycastHit hit)
    {
        for (int i = 0; i < AntiBlazeTag.Length; i++)
        {
            if (hit.collider.CompareTag(AntiBlazeTag[i]))
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    // ダイスロール処理
    #region ダイス処理
    private int dice()
    {
        int d = 0;
        int spreadprobability = Random.Range(1, 100) + LvSpreadProbability * (Fire_Lv1.FireLvel - 1);
        if (spreadprobability < SpreadProbability)
        {
            int Probability = Random.Range(1, 100);
            int preProbability = 0;
            int probability = 100 / (4 - FireNum);

            if (!FireXp && probability > Probability)
            {
                return 1;
            }
            preProbability = probability;
            probability += probability;

            if (!FireZp && preProbability <= Probability && probability > Probability)
            {
                return 2;
            }
            preProbability = probability;
            probability += probability;

            if (!FireXm && preProbability <= Probability && probability > Probability)
            {
                return 3;
            }
            preProbability = probability;
            probability += probability;

            if (!FireZm && preProbability <= Probability && probability > Probability)
            {
                return 4;
            }
        }
        return 0;
    }
    #endregion

    // 炎の生成処理
    #region 炎の生成処理
    private void Plane()
    {
        Vector3 prefabXp = new Vector3(this.transform.position.x + SpreadRange, PosY, this.transform.position.z);
        Vector3 prefabZp = new Vector3(this.transform.position.x, PosY, this.transform.position.z + SpreadRange);
        Vector3 prefabXm = new Vector3(this.transform.position.x - SpreadRange, PosY, this.transform.position.z);
        Vector3 prefabZm = new Vector3(this.transform.position.x, PosY, this.transform.position.z - SpreadRange);

        if (d == 0) return;

        if (d == 1) m_Blaze.CreateSpreadPlane(prefabXp);
        if (d == 2) m_Blaze.CreateSpreadPlane(prefabZp);
        if (d == 3) m_Blaze.CreateSpreadPlane(prefabXm);
        if (d == 4) m_Blaze.CreateSpreadPlane(prefabZm);
    }

    private void Spread()
    {
        Vector3 prefabXp = new Vector3(this.transform.position.x + SpreadRange, this.transform.position.y, this.transform.position.z);
        Vector3 prefabZp = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + SpreadRange);
        Vector3 prefabXm = new Vector3(this.transform.position.x - SpreadRange, this.transform.position.y, this.transform.position.z);
        Vector3 prefabZm = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - SpreadRange);

        if (d == 0) return;

        if (d == 1) m_Blaze.CreateBlaze(prefabXp);
        if (d == 2) m_Blaze.CreateBlaze(prefabZp);
        if (d == 3) m_Blaze.CreateBlaze(prefabXm);
        if (d == 4) m_Blaze.CreateBlaze(prefabZm);
    }
    #endregion

    // 炎がすべて埋まっているか確認する処理
    #region 炎の確認処理
    private bool FireEmpty()
    {
        return !(FireXp && FireZp && FireXm && FireZm);
    }
    #endregion

    // 衝突
    #region 炎の壁衝突
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Wall")
        {
            StopCoroutine("SpreadFire");
            FireXp = true;
            FireZp = true;
            FireXm = true;
            FireZm = true;
            FireNum = 4;
        }
    }
    #endregion
}
