using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    [SerializeField] private float SpreadSecond;   //���ĊԊu(�b)
    [SerializeField] private float SpreadProbability;   //���Ċm��(%)
    [SerializeField] private int LvSpreadProbability;   //�����x���ɂ��m���̏㏸(�m���ɐ��l*(Lv-1)�v���X)
    [SerializeField] private float SpreadRange;   //���Ď��̈ړ�����

    [SerializeField] private string[] UntiBlazeTag;

    public static bool FirstAction = true;

    //�����͂S�}�X�̉�����
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
            Debug.Log("Blaze�͈ȉ��̃^�O�̃I�u�W�F�N�g�𖳎����܂��B");
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

        if (SpreadRange < 5) SpreadRange = 5;   //SpreadRange5�ȉ��̎��d���΍��5�ɂ���
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
        //Ray�ɐڐG�����I�u�W�F�N�g�̔���(�z��ɂ��Ă�����Tag)�ɂ͉��Ă��Ȃ�)
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
