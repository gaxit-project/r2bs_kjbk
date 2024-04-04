using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RescueNPC : MonoBehaviour
{
    //����
    [SerializeField] public bool Severe;
    [SerializeField] int NpcUp;   //NPC���^������Ƃ��Ƀv���C���[�̓���̂ǂꂾ����ɒǏ]���邩�̐��l
    [SerializeField] public string text;   //NPC�ɋ߂Â����Ƃ��ɕ\�������text

    //�A�^�b�`
    public GameObject Player;   //Player��GameObject
    public GameObject Zone;   //�~�o�����GameObject
    [SerializeField] TextMeshPro TMP;   //NPC�ɋ߂Â����Ƃ��ɕ\�������textMesh
    [SerializeField] public NPCAI NPCAI;   //NPC��AI�X�N���v�g
    [SerializeField] public RadioText RadioText;   //��������

    MeshRenderer mesh;   //MeshRendere

    bool Follow = false;   //NPC�̒Ǐ] true = �Ǐ] : false = �ҋ@
    bool InGoal = false;   //�~�o�n�_�ɐڐG true =�@�ڐG : false = ��ڐG
    bool InZone = false;   //�~�o�͈͂ɐڐG true = �ڐG : false = ��ڐG
    bool NPCrun = false;   //NPC�̎������� true = �������� : false = NPC_AI�ɂ�鑀��
    bool Rescued = false;   //�L�[�{�[�h����񂾂����͂��邽�߂̃t���O
    bool ActiveIcon = false;   //��b�A�C�R���̐���
    bool FirstContact = false;   //��b�񐔂̔���
    bool SecondContact = false;   //��b�񐔂̔���
    bool Lock = false;   //Player�̓����̌Œ�

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Transform target = Player.transform;   //Player��Transform
        Vector3 TargetPosition = target.position;
        if (IsItInZone())
        {
            if (Input.GetKeyDown(KeyCode.E) && Severe == true && !IsItInGoal())   //�d���҂ɋ߂Â����Ƃ�
            {
                if (!IsItFollow())   //��Ǐ]��
                {
                    StopNPC();
                    SetFollow(true);
                }
                else   //�Ǐ]��
                {
                    SetFollow(false);
                    PutVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);
                }
            }
            if (Input.GetKeyDown(KeyCode.E) && Severe == false)   //�y�ǎ҂ɋ߂Â����Ƃ�
            {
                if (!IsItFirstContact())
                {
                    Debug.Log(SecondContact);
                    SetFirstContact(true);
                    SetActiveIcon(true);
                    StopNPC();
                    SetNPCrun(true);   //NPC���~�o�n�_�܂ŗU������
                    WaitChange(3.5f);
                    RadioText.SetActiveText(true);
                }
                else if (IsItFirstContact())
                {
                    SetSecondContact(true);
                    Debug.Log("Second");
                    StopNPC();
                    SetNPCrun(true);   //NPC���~�o�n�_�܂ŗU������
                    WaitChange(3.5f);
                    RadioText.SetActiveText(true);
                }
            }


            if (IsItFollow() && !IsItInGoal())   //�Ǐ]��
            {
                FollowVectorNPC(TargetPosition.x, TargetPosition.y + NpcUp, TargetPosition.z);   //NPC���^�����鎞��Vector
                SetText("[E]Put");
            }

            if (IsItInGoal() && !IsItRescued() && Severe == true)   //�~�o�n�_�ɐڐG�����~�o���d����
            {
                SetText("");
                SetFollow(false);
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPC���~�o�����Ƃ���Vector
                SetRescued(true);
                CountDestroy();   //��莞�Ԍ�ɃI�u�W�F�N�g�폜
            }
        }
    }

    //�֐�
    public void CountDestroy()//�I�u�W�F�N�g�̔j��
    {
        Invoke("Destroy", 3f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    void FollowVectorNPC(float x, float y, float z)//NPC�̒Ǐ]
    {
        transform.position = new Vector3(x, y, z);
        transform.forward = PlayerController.CurrentForward;
    }

    void RescuedVectorNPC(float x, float y, float z)//NPC�~�o���̓���
    {
        Vector3 NowPosition = new Vector3(x, y, z);
        Vector3 HelpPosition = new Vector3(x, y, z - 30);
        transform.position = Vector3.MoveTowards(NowPosition, HelpPosition, 12f);
    }

    void PutVectorNPC(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }

    public void StopNPC()   //NPC_AI�̒�~
    {
        NPCAI.MoveNPC();
        SetText("");
    }

    public void SetText(string text)   //text�̐ݒ�
    {
        TMP.text = text;
    }

    public void WaitChange(float f)
    {
        Invoke("ChangeAlpha", f);
    }

    private void ChangeAlpha()
    {
        StartCoroutine("TransParent");
    }

    IEnumerator TransParent()
    {
        for (int i = 0; i < 51; i++)
        {
            mesh.material.color = mesh.material.color - new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(0.07f);
        }
        Destroy();
    }

    //bool����
    public bool IsItFollow()
    {
        return Follow;
    }
    public bool IsItInGoal()
    {
        return InGoal;
    }
    public bool IsItNPCrun()
    {
        return NPCrun;
    }
    public bool IsItRescued()
    {
        return Rescued;
    }
    public bool IsItActiveIcon()
    {
        return ActiveIcon;
    }
    public bool IsItInZone()
    {
        return InZone;
    }
    public bool IsItFirstContact()
    {
        return FirstContact;
    }
    public bool IsItSecondContact()
    {
        return SecondContact;
    }
    public bool IsItLock()
    {
        return Lock;
    }

    //boolSet
    public void SetFollow(bool b)
    {
        Follow = b;
    }
    public void SetInGoal(bool b)
    {
        InGoal = b;
    }
    public void SetNPCrun(bool b)
    {
        NPCrun = b;
    }
    public void SetRescued(bool b)
    {
        Rescued = b;
    }
    public void SetActiveIcon(bool b)
    {
        ActiveIcon = b;
    }
    public void SetInZone(bool b)
    {
        InZone = b;
    }
    public void SetFirstContact(bool b)
    {
        FirstContact = b;
    }
    public void SetSecondContact(bool b)
    {
        SecondContact = b;
    }
    public void SetLock(bool b)
    {
        Lock = b;
    }
}
