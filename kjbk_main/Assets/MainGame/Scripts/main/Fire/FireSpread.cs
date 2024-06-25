using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    private float SpreadSecond;   //延焼間隔(秒)
    private float SpreadProbability;   //延焼確立(%)
    private int LvSpreadProbability;   //炎レベルによる確率の上昇(確率に数値*(Lv-1)プラス)
    private float SpreadRange;   //延焼時の移動距離

    private string[] AntiBlazeTag;

    private GameObject Rescue;
    RescueCount Counter;

    public static bool FirstAction = true;
    private bool Action = true;

    private int boostNum;
    private bool boost = false;

    //炎周囲４マスの炎判定
    private bool FireXp = false;
    private bool FireZp = false;
    private bool FireXm = false;
    private bool FireZm = false;
    private int FireNum = 0;

    private int d = 0;

    public Inferno inferno;
    public FireLv Fire_Lv1;
    private GameObject Blaze;
    private Blaze_Maneger m_Blaze;

    // Start is called before the first frame update
    void Start()
    {
        Rescue = GameObject.Find("Rcounter");
        Counter = Rescue.GetComponent<RescueCount>();

        if (SpreadRange < 5) SpreadRange = 5;   //SpreadRange5以下の時重さ対策で5にする

        StartCoroutine("SpreadFire");

        Blaze = GameObject.Find("BlazeManeger");
        m_Blaze = Blaze.GetComponent<Blaze_Maneger>();
        var Data = m_Blaze.getSpreadData();
        SpreadSecond = Data.Second;
        SpreadProbability = Data.Probability;
        LvSpreadProbability = Data.LvProbability;
        SpreadRange = Data.Range;
        boostNum = Data.Boost;
        AntiBlazeTag = Data.Tag;

        if (FirstAction)
        {
            Debug.Log("=============================================");
            Debug.Log("Blazeは以下のタグのオブジェクトを無視します。");
            for (int i = 0; i < AntiBlazeTag.Length; i++)
            {
                Debug.Log("Tag: " + AntiBlazeTag[i]);
            }
            Debug.Log("=============================================");
            FirstAction = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inferno.DesBlaze)
        {
            StopCoroutine("SpreadFire");
            if (Action)
            {
                m_Blaze.CreateExtPlane(this.transform.position);
                Action = false;
            }
        }
        if (Counter.getNum() >= boostNum && !boost)   //倒壊ゲージは参照してないため追加する場合は条件増やしてください
        {
            SpreadSecond = SpreadSecond * 0.5f;
            boost = true;
        }
    }

    IEnumerator SpreadFire()
    {
        Vector3 Xp = Vector3.right;
        Vector3 Zp = Vector3.forward;
        Vector3 Xm = Vector3.left;
        Vector3 Zm = Vector3.back;

        Ray rayXp = new Ray(this.transform.position, Xp);
        Ray rayZp = new Ray(this.transform.position, Zp);
        Ray rayXm = new Ray(this.transform.position, Xm);
        Ray rayZm = new Ray(this.transform.position, Zm);

        //Debug.DrawRay(rayXp.origin, rayXp.direction * 10, Color.red, 100000, false);
        //Debug.DrawRay(rayZp.origin, rayZp.direction * 10, Color.red, 100000, false);
        //Debug.DrawRay(rayXm.origin, rayXm.direction * 10, Color.red, 100000, false);
        //Debug.DrawRay(rayZm.origin, rayZm.direction * 10, Color.red, 100000, false);

        while (true)
        {
            yield return new WaitForSeconds(SpreadSecond);
            decision(rayXp, rayZp, rayXm, rayZm);
            if (!inferno.FireStatus && !FireEmpty()) break;
            if (Fire_Lv1.FireLvel == 1) continue;
            d = dice();
            Plane();
            Spread();
        }
    }

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
        //Rayに接触したオブジェクトの判別(配列にしていしたTag)には延焼しない)
        for (int i = 0; i < AntiBlazeTag.Length; i++)
        {
            if (hit.collider.CompareTag(AntiBlazeTag[i]))
            {
                return true;
            }
        }
        return false;
    }

    private int dice()
    {
        int d = 0;

        int spreadprobability = Random.Range(1, 100) + LvSpreadProbability * (Fire_Lv1.FireLvel - 1);
        if (spreadprobability < SpreadProbability)
        {
            int Probability = Random.Range(1, 100);
            int preProbability = 0;
            int probability = 100 / (4 - FireNum);
            if (!FireXp)
            {
                if (probability > Probability)
                {
                    return 1;

                }
                preProbability = probability;
                probability += probability;
            }
            if (!FireZp)
            {
                if (preProbability <= Probability && probability > Probability)
                {
                    return 2;
                }
                preProbability = probability;
                probability += probability;
            }
            if (!FireXm)
            {
                if (preProbability <= Probability && probability > Probability)
                {
                    return 3;
                }
                preProbability = probability;
                probability += probability;
            }
            if (!FireZm)
            {
                if (preProbability <= Probability && probability > Probability)
                {
                    return 4;
                }
            }
        }
        return 0;
    }

    private void Plane()
    {
        Vector3 prefabXp = new Vector3(this.transform.position.x + SpreadRange, this.transform.position.y - 0.5f, this.transform.position.z);
        Vector3 prefabZp = new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z + SpreadRange);
        Vector3 prefabXm = new Vector3(this.transform.position.x - SpreadRange, this.transform.position.y - 0.5f, this.transform.position.z);
        Vector3 prefabZm = new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z - SpreadRange);

        if (d == 0) return;
        if (d == 1)
        {
            m_Blaze.CreateSpreadPlane(prefabXp);

        }
        if (d == 2)
        {
            m_Blaze.CreateSpreadPlane(prefabZp);
        }
        if (d == 3)
        {
            m_Blaze.CreateSpreadPlane(prefabXm);
        }
        if (d == 4)
        {
            m_Blaze.CreateSpreadPlane(prefabZm);
        }
    }

    private void Spread()
    {
        Vector3 prefabXp = new Vector3(this.transform.position.x + SpreadRange, this.transform.position.y, this.transform.position.z);
        Vector3 prefabZp = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + SpreadRange);
        Vector3 prefabXm = new Vector3(this.transform.position.x - SpreadRange, this.transform.position.y, this.transform.position.z);
        Vector3 prefabZm = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - SpreadRange);

        if (d == 0) return;
        if (d == 1)
        {
            m_Blaze.CreateBlaze(prefabXp);
        }
        if (d == 2)
        {
            m_Blaze.CreateBlaze(prefabZp);
        }
        if (d == 3)
        {
            m_Blaze.CreateBlaze(prefabXm);
        }
        if (d == 4)
        {
            m_Blaze.CreateBlaze(prefabZm);
        }
    }

    private bool FireEmpty()
    {
        if (FireXp && FireZp && FireXm && FireZm)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
