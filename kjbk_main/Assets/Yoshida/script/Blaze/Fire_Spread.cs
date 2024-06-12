using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fire_Spread : MonoBehaviour
{
    [SerializeField] private float SpreadSecond;   //延焼間隔(秒)
    [SerializeField] private float SpreadProbability;   //延焼確立(%)
    [SerializeField] private int LvSpreadProbability;   //炎レベルによる確率の上昇(確率に数値*(Lv-1)プラス)
    [SerializeField] private float SpreadRange;   //延焼時の移動距離

    [SerializeField] private string[] AntiBlazeTag;

    private GameObject Rescue;
    RescueCount_verMatsuno Counter;

    public static bool FirstAction = true;

    [SerializeField] private int boostNum;
    private bool boost = false;

    //炎周囲４マスの炎判定
    [SerializeField] private bool FireXp = false;
    [SerializeField] private bool FireZp = false;
    [SerializeField] private bool FireXm = false;
    [SerializeField] private bool FireZm = false;
    private int FireNum = 0;

    private int d = 0;

    public inferno inferno;
    public Fire_Lv Fire_Lv;

    public GameObject PrefabBlaze;
    public GameObject PrefabPlane;

    void Awake()
    {
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

    // Start is called before the first frame update
    void Start()
    {
        Rescue = GameObject.Find("Rescue");
        Counter = Rescue.GetComponent<RescueCount_verMatsuno>();

        if (SpreadRange < 5) SpreadRange = 5;   //SpreadRange5以下の時重さ対策で5にする
        StartCoroutine("SpreadFire");
    }

    // Update is called once per frame
    void Update()
    {
        if(inferno.FireStatus)
        {
            StopCoroutine("SpreadFire");
        }     
        if (Counter.getNum() >= boostNum && !boost)
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
            if (Fire_Lv.FireLv == 1) continue;
            d = dice();
            CreatePlane();
            decision(rayXp, rayZp, rayXm, rayZm);
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
        else
        {
            if (FireXp) FireNum--;
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
            if (FireZp) FireNum--;
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
            if (FireXm) FireNum--;
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
            if (FireZm) FireNum--;
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

        int spreadprobability = Random.Range(1, 100) + LvSpreadProbability * (Fire_Lv.FireLv - 1);
        if (spreadprobability < SpreadProbability)
        {
            int Probability = Random.Range(1, 100);
            int preProbability = 0;
            int probability = 100 / (4 - FireNum);
            if (!FireXp)
            {
                if (probability < Probability)
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

    private void CreatePlane()
    {
        Vector3 prefabXp = new Vector3(this.transform.position.x + SpreadRange, this.transform.position.y, this.transform.position.z);
        Vector3 prefabZp = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + SpreadRange);
        Vector3 prefabXm = new Vector3(this.transform.position.x - SpreadRange, this.transform.position.y, this.transform.position.z);
        Vector3 prefabZm = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - SpreadRange);

        if (d == 0) return;
        if (d == 1)
        {
            GameObject newObject = Instantiate(PrefabPlane, prefabXp, Quaternion.identity);
            newObject.name = "Plane";
        }
        if (d == 2)
        {
            GameObject newObject = Instantiate(PrefabPlane, prefabZp, Quaternion.identity);
            newObject.name = "Plane";
        }
        if (d == 3)
        {
            GameObject newObject = Instantiate(PrefabPlane, prefabXm, Quaternion.identity);
            newObject.name = "Plane";
        }
        if (d == 4)
        {
            GameObject newObject = Instantiate(PrefabPlane, prefabZm, Quaternion.identity);
            newObject.name = "Plane";
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
            GameObject newObject = Instantiate(PrefabBlaze, prefabXp, Quaternion.identity);
            newObject.name = "Blaze";
        }
        if (d == 2)
        {
            GameObject newObject = Instantiate(PrefabBlaze, prefabZp, Quaternion.identity);
            newObject.name = "Blaze";
        }
        if (d == 3)
        {
            GameObject newObject = Instantiate(PrefabBlaze, prefabXm, Quaternion.identity);
            newObject.name = "Blaze";
        }
        if (d == 4)
        {
            GameObject newObject = Instantiate(PrefabBlaze, prefabZm, Quaternion.identity);
            newObject.name = "Blaze";
        }
    }

    private bool FireEmpty()
    {
        if(FireXp && FireZp && FireXm && FireZm)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
