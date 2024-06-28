using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;


public class LIFE : MonoBehaviour
{
    public static int HitPoint = 3;            //プレイヤーのHP

    //public Collider_On_Off FireColOff;  //Collider_On_OffからFireOffを持ってくる
    //public Collider_On_Off FireColOn;   //Collider_On_OffからFireOnを持ってくる

    public LIFE_Bar BarHP;

    private Animator Anim;


    //無敵時間関連の者たち
    public float invincibilityDuration = 5.0f; // 無敵時間（秒）
    private float invincibilityTimer = 0.0f;   // 経過時間を格納するタイマー変数(初期値0秒)
    private bool isInvincible = false;         // 無敵状態かどうかのフラグ


    //点滅関連の者たち
    bool flag = true;
    [SerializeField] private Renderer renderComponent1;
    [SerializeField] private Renderer renderComponent2;
    [SerializeField] private Renderer renderComponent3;


    void Start()
    {
        HitPoint = 3;
        Anim = GetComponent<Animator>();
    }

    public void GetStar()
    {
        //スターを取ったときに無敵状態フラグをTrueにする
        isInvincible = true;
    }

    // Update is called once per frame
    public void Update()
    {

        if (isInvincible)
        {
            //ここに無敵状態のときの処理を書く
            Debug.Log("無敵状態");


            //毎フレームタイマー変数にTime.deltaTimeを足す
            invincibilityTimer += Time.deltaTime;

            //タイマーが無敵時間(10秒)を超えたとき
            if (invincibilityTimer >= invincibilityDuration)
            {
                Debug.Log("無敵状態終わり");

                //無敵状態フラグをFalseにする
                isInvincible = false;
                //タイマーを0.0秒にリセットする
                invincibilityTimer = 0.0f;
            }
        }
    }

    //public void HPminus2()
    //{
    //    HitPoint--;                  //HPを減らす
    //}

    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="Hit"></param>
    //触れた瞬間HPを1減らす
    private void OnCollisionEnter(Collision Hit)
    {
        if (Hit.gameObject.tag == "Blaze")
        {
            if (isInvincible)
            {
                return;
            }
            else
            {
                HitPoint--;
                GetStar();
                //animator.SetBool("FallDown", true);
                Update();        //無敵付与(多分意味なし！！)
                StartCoroutine(Blink());     //点滅開始
                Anim.SetBool("Damage", true); //ダメージのアニメーションをONに

                BarHP.HPBar();
                //FireColOff.FireOff();        //炎のコライダーをオフに
                //FireColOn.FireOn();          //炎のコライダーをオンに
                Invoke(nameof(Animation_OFF), 2f); //ダメージのアニメーションをオフに

                Debug.Log("HP=" + HitPoint);

                StartCoroutine(Blink());     //点滅開始
                if (HitPoint <= 0)            //もしHPが尽きたら以下の処理を行う
                {
                    PlayerPrefs.SetString("Result", "GAMEOVER");
                    Scene.Instance.GameResult();
                }
            }

        }


    }


    void Animation_OFF()
    {
        Anim.SetBool("Damage", false);
    }


    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <returns></returns>
    //当たった時の点滅
    IEnumerator Blink()
    {
        if (flag)
        {
            for (int i = 0; i < 24; i++)
            {
                flag = false;
                renderComponent1.enabled = !renderComponent1.enabled;
                renderComponent2.enabled = !renderComponent2.enabled;
                renderComponent3.enabled = !renderComponent3.enabled;
                yield return new WaitForSeconds(0.1f);
                renderComponent1.enabled = !renderComponent1.enabled;
                renderComponent2.enabled = !renderComponent2.enabled;
                renderComponent3.enabled = !renderComponent3.enabled;
                yield return new WaitForSeconds(0.1f);
            }
            flag = true;
        }
    }

}
