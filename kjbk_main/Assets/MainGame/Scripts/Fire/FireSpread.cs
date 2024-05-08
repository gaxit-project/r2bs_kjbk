using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    [SerializeField] private float SpreadSecond;   //延焼間隔(秒)
    [SerializeField] private float SpreadProbability;   //延焼確立(%)
    [SerializeField] private int LvSpreadProbability;   //炎レベルによる確率の上昇(確率に数値*(Lv-1)プラス)
    [SerializeField] private float SpreadRange;   //延焼時の移動距離

    [SerializeField] private string[] UntiBlazeTag;

    public static bool FirstAction = true;

    //炎周囲４マスの炎判定
    private bool FireXp = false;
    private bool FireZp = false;
    private bool FireXm = false;
    private bool FireZm = false;
    private int FireNum = 0;


    Inferno Inferno;
    FireLv FireLv;

    public GameObject PrefabBlaze;

    void Awake()
    {
        if (FirstAction)
        {
            Debug.Log("=============================================");
            Debug.Log("Blazeは以下のタグのオブジェクトを無視します。");
            for (int i = 0; i < UntiBlazeTag.Length; i++)
            {
                Debug.Log("Tag: " + UntiBlazeTag[i]);
            }
            Debug.Log("=============================================");
            FirstAction = false;
        }
    }

    void Start()
    {
        Inferno = this.GetComponent<Inferno>();
        FireLv = this.GetComponent<FireLv>();

        if (SpreadRange < 5) SpreadRange = 5;   //SpreadRange5以下の時重さ対策で5にする
        StartCoroutine("SpreadFire");
    }

    void Update()
    {
        /*if (Inferno.FireStatus)
        {
            StopCoroutine("SpreadFire");
        }*/
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

        //Debug.DrawRay(rayXp.origin, rayXp.direction * 10, Color.red, 10, false);
        //Debug.DrawRay(rayZp.origin, rayZp.direction * 10, Color.red, 10, false);
        //Debug.DrawRay(rayXm.origin, rayXm.direction * 10, Color.red, 10, false);
        //Debug.DrawRay(rayZm.origin, rayZm.direction * 10, Color.red, 10, false);

        Vector3 prefabXp = new Vector3(this.transform.position.x + SpreadRange, this.transform.position.y, this.transform.position.z);
        Vector3 prefabZp = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + SpreadRange);
        Vector3 prefabXm = new Vector3(this.transform.position.x - SpreadRange, this.transform.position.y, this.transform.position.z);
        Vector3 prefabZm = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - SpreadRange);

        while (true)
        {
            yield return new WaitForSeconds(SpreadSecond);
            decision(rayXp, rayZp, rayXm, rayZm);
            if (!FireEmpty()) break; //!Inferno.FireStatus && 
            if (FireLv.FireLvel == 1) continue;
            int spreadprobability = Random.Range(1, 100) + LvSpreadProbability * (FireLv.FireLvel - 1);
            if (spreadprobability < SpreadProbability)
            {
                int Probability = Random.Range(1, 100);
                int preProbability = 0;
                int probability = 100 / (4 - FireNum);
                if (!FireXp)
                {
                    if (probability < Probability)
                    {
                        GameObject newObject = Instantiate(PrefabBlaze, prefabXp, Quaternion.identity);
                        newObject.name = "Blaze";
                        preProbability = probability;
                        probability += probability;
                    }
                }
                if (!FireZp)
                {
                    if (preProbability <= Probability && probability > Probability)
                    {
                        GameObject newObject = Instantiate(PrefabBlaze, prefabZp, Quaternion.identity);
                        newObject.name = "Blaze";
                        preProbability = probability;
                        probability += probability;
                    }
                }
                if (!FireXm)
                {
                    if (preProbability <= Probability && probability > Probability)
                    {
                        GameObject newObject = Instantiate(PrefabBlaze, prefabXm, Quaternion.identity);
                        newObject.name = "Blaze";
                        preProbability = probability;
                        probability += probability;
                    }
                }
                if (!FireZm)
                {
                    if (preProbability <= Probability && probability > Probability)
                    {
                        GameObject newObject = Instantiate(PrefabBlaze, prefabZm, Quaternion.identity);
                        newObject.name = "Blaze";
                    }
                }

            }

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
        else
        {
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
        else
        {
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
        else
        {
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
        else
        {
            FireZm = false;
        }
    }

    private bool Raydecision(RaycastHit hit)
    {
        //Rayに接触したオブジェクトの判別(配列にしていしたTag)には延焼しない)
        for (int i = 0; i < UntiBlazeTag.Length; i++)
        {
            if (hit.collider.CompareTag(UntiBlazeTag[i]))
            {
                return true;
            }
        }
        return false;
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
