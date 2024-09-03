using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FireSpread : MonoBehaviour
{
    // ���ĂɊւ���ϐ�
    #region ���Ċ֘A�̕ϐ�
    private float SpreadSecond;   // ���ĊԊu(�b)
    private float SpreadProbability;   // ���Ċm��(%)
    private int LvSpreadProbability;   // �����x���ɂ��m���̏㏸(�m���ɐ��l*(Lv-1)�v���X)
    private float SpreadRange;   // ���Ď��̈ړ�����
    private float PosY;
    private string[] AntiBlazeTag;
    #endregion

    // �~���≊�֘A�̕ϐ�
    #region �~���Ɖ��֘A�̕ϐ�
    private GameObject Rescue;
    RescueCount Counter;
    public static bool FirstAction = true;
    private int boostNum;
    private bool boost = false;
    private bool FireXp = false;   // ������4�}�X�̉�����
    private bool FireZp = false;
    private bool FireXm = false;
    private bool FireZm = false;
    private int FireNum = 0;
    private int d = 0;
    public Inferno inferno;
    public FireLv Fire_Lv1;
    private GameObject Blaze;
    private Blaze_Maneger m_Blaze;
    private int blazeThreshold = 20;  // 閾値
    private bool isWeakened = false; // 火の広がりが弱くなっているかどうかを判定するフラグ
    #endregion

    // �X�^�[�g���̏���
    #region �X�^�[�g����
    void Start()
    {
        // �~���I�u�W�F�N�g�̎擾
        Rescue = GameObject.Find("Rcounter");
        Counter = Rescue.GetComponent<RescueCount>();

        // SpreadRange��5�ȉ��̏ꍇ��5�ɂ���
        if (SpreadRange < 5) SpreadRange = 5;

        StartCoroutine("SpreadFire");

        // BlazeManager�̃f�[�^�擾
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

        Debug.Log("元の広がりが: " + SpreadProbability + "%");
    }
    #endregion

    // ���t���[���̏���
    #region ���t���[���̏���
    void Update()
    {
         // Blazeオブジェクトの数を取得
        int activeBlazeCount = GetBlazeChildCount();
        Debug.Log($"BlazeCount: {activeBlazeCount}. ");
        // Blazeの数に応じて火の広がりの確率を調整
        if (activeBlazeCount >= blazeThreshold && !isWeakened)
        {
            SpreadProbability *= 0.5f; // 火の広がりを弱める
            isWeakened = true;
            Debug.Log("火の広がりが弱くなりました: " + SpreadProbability + "%");
        }
        else if (activeBlazeCount < blazeThreshold && isWeakened)
        {
            SpreadProbability *= 2.0f; // 火の広がりを元に戻す
            isWeakened = false;
            Debug.Log("火の広がりが元に戻りました: " + SpreadProbability + "%");
        }
        #region ��������
        // ���΂��ꂽ�ꍇ�̏���
        if (inferno.DesBlaze)
        {
            StopCoroutine("SpreadFire");
            m_Blaze.CreateExtPlane(new Vector3(this.transform.position.x, PosY, this.transform.position.z));
            Destroy(this.gameObject);
        }
        #endregion

        #region ���đ��x�̃u�[�X�g
        // ���đ��x�̃u�[�X�g
        if (Counter.getNum() >= boostNum && !boost)
        {
            SpreadSecond = SpreadSecond * 0.5f;
            boost = true;
        }
        #endregion
    }
    #endregion
    #region Blazeの数をカウントするメソッド
    private int GetBlazeChildCount()
    {
        int count = 0;
        GameObject[] allBlazes = GameObject.FindGameObjectsWithTag("Blaze");

        foreach (GameObject blaze in allBlazes)
        {
            if (blaze.activeInHierarchy)
            {
                count++;
            }
        }

        return count;
    }
    #endregion

    // ���ď����̃R���[�`��
    #region ���ď���
    IEnumerator SpreadFire()
    {
        // ���ĕ����̃x�N�g���ݒ�
        Vector3 Xp = Vector3.right;
        Vector3 Zp = Vector3.forward;
        Vector3 Xm = Vector3.left;
        Vector3 Zm = Vector3.back;

        // �����ʒu�̐ݒ�
        Vector3 t = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);

        // Ray�̐ݒ�
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

    // Ray�̓����蔻�菈��
    #region Ray����
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

    // �_�C�X���[������
    #region �_�C�X����
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

    // ���̐�������
    #region ���̐�������
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

    // �������ׂĖ��܂��Ă��邩�m�F���鏈��
    #region ���̊m�F����
    private bool FireEmpty()
    {
        return !(FireXp && FireZp && FireXm && FireZm);
    }
    #endregion

    // �Փ�
    #region ���̕ǏՓ�
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
