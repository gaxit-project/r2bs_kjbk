using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Rescue_NPC : MonoBehaviour
{
    //入力
    [SerializeField] public bool Severe;
    [SerializeField] int NpcUp;   //NPCを運搬するときにプレイヤーの頭上のどれだけ上に追従するかの数値
    [SerializeField] public string text;   //NPCに近づいたときに表示されるtext

    //アタッチ
    public GameObject Player;   //PlayerのGameObject
    public GameObject Zone;   //救出判定のGameObject
    [SerializeField] TextMeshPro TMP;   //NPCに近づいたときに表示されるtextMesh
    //[SerializeField] public Player_Script Player_Script;   //Player操作スクリプト
    //[SerializeField] public NPC_AI NPC_AI;   //NPCのAIスクリプト
    [SerializeField] public Radio_Text Radio_Text;   //無線制御

    MeshRenderer mesh;   //MeshRendere
    private GameObject counter;
    Rescue_Counter counterScript;
    private GameObject RescueDiplication;
    Rescue_Diplication DiplicationScript;

    bool Follow = false;   //NPCの追従 true = 追従 : false = 待機
    bool InGoal = false;   //救出地点に接触 true =　接触 : false = 非接触
    bool InZone = false;   //救出範囲に接触 true = 接触 : false = 非接触
    bool NPCrun = false;   //NPCの自動操作 true = 自動操作 : false = NPC_AIによる操作
    bool Rescued = false;   //キーボードを一回だけ入力するためのフラグ
    bool ActiveIcon = false;   //会話アイコンの制御
    bool FirstContact = false;   //会話回数の判定
    bool SecondContact = false;   //会話回数の判定
    bool Lock = false;   //Playerの動きの固定

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        counter = GameObject.Find("RescueCounter");
        counterScript = counter.GetComponent<Rescue_Counter>();
        RescueDiplication = GameObject.Find("RescueDiplication");
        DiplicationScript = RescueDiplication.GetComponent<Rescue_Diplication>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = Player.transform;   //PlayerのTransform
        Vector3 TargetPosition = target.position;
        if (IsItInZone())
        {
            if (Input.GetKeyDown(KeyCode.E) && Severe == true && !IsItInGoal())   //重傷者に近づいたとき
            {
                if (!IsItFollow() && !DiplicationScript.getFlag())   //非追従時
                {
                    DiplicationScript.OnFlag();
                    StopNPC();
                    SetFollow(true);
                }
                else   //追従時
                {
                    DiplicationScript.OffFlag();
                    SetFollow(false);
                    PutVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);
                }
            }
            if (Input.GetKeyDown(KeyCode.E) && Severe == false)   //軽症者に近づいたとき
            {
                if (!IsItFirstContact())
                {
                    Debug.Log(SecondContact);
                    SetFirstContact(true);
                    SetActiveIcon(true);
                    StopNPC();
                    SetNPCrun(true);   //NPCを救出地点まで誘導する
                    WaitChange(3.5f);
                    //Radio_Text.SetMildText();
                    //Radio_Text.SetActiveText(true);   重傷者の無線表示をするためと、軽症者の自動脱出行動のため取り敢えずコメント化
                }
                else if (IsItFirstContact())
                {
                    SetSecondContact(true);
                    StopNPC();
                    SetNPCrun(true);   //NPCを救出地点まで誘導する
                    WaitChange(3.5f);
                    //Radio_Text.SetMildText();
                    //Radio_Text.SetActiveText(true);   重傷者の無線表示をするためと、軽症者の自動脱出行動のため取り敢えずコメント化
                }
            }

            if (Severe == false)   //Playerの操作の固定
            {
                if (IsItLock())
                {
                    //Player_Script.PlayerStop();
                }
                else if (!IsItLock())
                {
                    //Player_Script.PlayerMove();
                }
            }

            if (IsItFollow() && !IsItInGoal())   //追従時
            {
                FollowVectorNPC(TargetPosition.x, TargetPosition.y + NpcUp, TargetPosition.z);   //NPCを運搬する時のVector
                SetText("[E]Put");
            }

            if (IsItInGoal() && !IsItRescued() && Severe == true)   //救出地点に接触かつ未救出かつ重傷者
            {
                Radio_Text.SetSevereText();
                Radio_Text.SetActiveText(true);
                DiplicationScript.OffFlag();
                SetText("");
                SetFollow(false);
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPCを救出したときのVector
                SetRescued(true);
                CountDestroy();   //一定時間後にオブジェクト削除
                counterScript.Count();
                 
            }
        }
    }

    //関数
    public void CountDestroy()//オブジェクトの破壊
    {
        Invoke("Destroy", 3f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    void FollowVectorNPC(float x, float y, float z)//NPCの追従
    {
        transform.position = new Vector3(x, y, z);
    }

    void RescuedVectorNPC(float x, float y, float z)//NPC救出時の動作
    {
        Vector3 NowPosition = new Vector3(x, y, z);
        Vector3 HelpPosition = new Vector3(x, y, z - 30);
        transform.position = Vector3.MoveTowards(NowPosition, HelpPosition, 12f);
    }

    void PutVectorNPC(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }

    public void StopNPC()   //NPC_AIの停止
    {
        //NPC_AI.MoveNPC();
        SetText("");
    }

    public void SetText(string text)   //textの設定
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
        counterScript.Count();
    }

    //bool判定
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
