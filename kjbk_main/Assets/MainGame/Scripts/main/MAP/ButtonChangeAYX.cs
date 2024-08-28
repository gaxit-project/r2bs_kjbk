using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonChangeAYX : MonoBehaviour
{
    [SerializeField] private InputActionReference _holdLong;

    private InputAction _holdLongAction;

    public GameObject YDbutton;
    public GameObject YSbutton;
    public GameObject Abutton;
    public GameObject Xbutton;
    private RescueNPC rescueNPC;
    private RescueDiplication DiplicationScript;
    private GameObject Rescue;
    private bool Xb = false;
    private bool x=false;
    public int cnt = 0;

    private void Awake()
    {
        // 長押しアクションが設定されている場合、アクションを有効化
        if (_holdLong == null) return;

        _holdLongAction = _holdLong.action;
        _holdLongAction.Enable();
    }

    void Start()
    {
        Rescue = GameObject.Find("Rescue");
        rescueNPC = FindObjectOfType<RescueNPC>();
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
        // ObjectMonitorのイベントを登録
        ObjectMonitor.OnObjectDestroyed += HandleObjectDestroyed;

        DeactivateAllButtons();
    }

    void Update()
    {

        if (_holdLongAction == null) return;

        if (rescueNPC.IsItFollow())
        {
            var progress = _holdLongAction.GetTimeoutCompletionPercentage();

            if(progress > 0)
            {
                if (Xbutton != null) Xbutton.SetActive(true);
            }
            else
            {
                if (Xbutton != null) Xbutton.SetActive(false);
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ButtonAYX"))
        {
            cnt++;
            // 衝突したオブジェクトの名前に基づいて適切なボタンをアクティブ化
            switch (other.gameObject.name)
            {
                case "SyoukaButton":
                    if(!x){
                    DeactivateAllButtons();
                    if(YSbutton != null)YSbutton.SetActive(true);
                    x=true;
                    }
                    break;
                case "AButton":
                    if(!x){
                    DeactivateAllButtons();
                    if (Abutton != null) Abutton.SetActive(true);
                    x=true;
                    }
                    break;
                case "XButton":
                    if (!Xb && Xbutton != null) Xbutton.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ButtonAYX"))
        {
            cnt = cnt-1;
            DeactivateButton(other.gameObject.name);
        }
    }

    void OnDestroy()
    {
        ObjectMonitor.OnObjectDestroyed -= HandleObjectDestroyed;
        DeactivateAllButtons();
    }

    void HandleObjectDestroyed(string buttonName)
    {
        cnt=0;
        DeactivateButton(buttonName);
    }
    // 全てのボタンを非アクティブにする
    private void DeactivateAllButtons()
    {
        if(YDbutton != null){
        YDbutton.SetActive(false);
        }
        if(YSbutton != null){
        YSbutton.SetActive(false);
        }
        if(Abutton != null){
        Abutton.SetActive(false);
        }
        if (!Xb){ 
        if(Xbutton != null){
        Xbutton.SetActive(false);
        }
        }
        x = false;
    }
    // 離れたオブジェクトに対応するボタンを非アクティブ
    private void DeactivateButton(string buttonName)
    {
        switch (buttonName)
        {
            case "DoorButton":
                if (YDbutton != null) YDbutton.SetActive(false);
                x=false;
                break;
            case "SyoukaButton":
                if (YSbutton != null) YSbutton.SetActive(false);
                x=false;
                break;
            case "AButton":
                if (Abutton != null) Abutton.SetActive(false);
                x=false;
                break;
            case "XButton":
                if (Xbutton != null && !Xb) Xbutton.SetActive(false);
                break;
            default:
                break;
        }
    }
}
