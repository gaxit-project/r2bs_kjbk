using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterHose : MonoBehaviour
{
    #region フィールド宣言
    [SerializeField] private GameObject Child; // 水のパーティクルを格納
    [SerializeField] private GameObject FF; // Fire Fighterオブジェクト
    [SerializeField] private GameObject Hand; // 手の位置オブジェクト
    [SerializeField] private GameObject Waist; // 腰の位置オブジェクト

    private Transform HandPos; // 手の位置を取得するためのTransform
    private Transform WaistPos; // 腰の位置を取得するためのTransform

    private bool HandHold = false; // 手に持っている状態かどうか
    private bool WaistHold = false; // 腰に持っている状態かどうか

    private bool isOnes = false; // 初回の放水状態を確認するフラグ
    private bool HoldLock = false; // 長押しロックのフラグ

    private float capacity; // 水の容量

    private Animator animator; // アニメーターコンポーネント
    private AudioSource audiosource; // オーディオソースコンポーネント

    public static bool WaterStatus = false; // 放水状態
    public static bool Hold = false; // 長押し判定
    #endregion

    #region イベント処理
    private void OnEnable()
    {
        // オブジェクトが有効になったときの処理
        WaterStatus = false;
        Hold = false;
        audiosource = GetComponent<AudioSource>(); // AudioSourceコンポーネントの取得
        WaterCannon(WaterStatus); // 放水処理の初期化
        WaistHold = true;
        HandPos = Hand.transform; // 手の位置Transformの取得
        WaistPos = Waist.transform; // 腰の位置Transformの取得

        // 初期位置の設定
        this.transform.position = new Vector3(WaistPos.position.x, WaistPos.position.y, WaistPos.position.z);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        // 放水ボタンが押されたときの処理
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                FF.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Hold = true;
                break;

            case InputActionPhase.Canceled:
                Hold = false;
                HoldLock = true;
                break;
        }
    }
    #endregion

    #region 初期化
    void Start()
    {
        // 初期化処理
        Child = gameObject.transform.GetChild(0).gameObject; // 水のパーティクルの取得
        animator = FF.GetComponent<Animator>(); // アニメーターコンポーネントの取得
    }
    #endregion

    #region 更新処理
    void Update()
    {
        #region 手と腰の位置更新
        HandPos = Hand.transform; // 手の位置を更新
        WaistPos = Waist.transform; // 腰の位置を更新
        #endregion

        #region 水の容量取得
        capacity = PlayerPrefs.GetFloat("capacity"); // 水の容量を取得
        #endregion

        #region 入力処理
        // マウス右ボタンが押されたとき
        if (Input.GetMouseButtonDown(1))
        {
            Hold = true;
        }

        // マウス右ボタンが離されたとき
        if (Input.GetMouseButtonUp(1))
        {
            Hold = false;
            HoldLock = true;
        }

        // 長押しロックが有効なとき
        if (HoldLock)
        {
            Hold = false;
        }
        #endregion

        #region 持っている状態の更新
        // 手に持っている状態
        if (HandHold)
        {
            this.transform.position = new Vector3(HandPos.position.x, HandPos.position.y, HandPos.position.z);
        }
        // 腰に持っている状態
        if (WaistHold)
        {
            this.transform.position = new Vector3(WaistPos.position.x, WaistPos.position.y, WaistPos.position.z);
        }
        #endregion

        #region 放水処理
        if (PlayerRayCast.HosuStatus == true)
        {
            if (Hold)
            {
                if (!isOnes)
                {
                    isOnes = true;
                    animator.SetBool("Gun", isOnes); // アニメーションの状態を変更
                    Invoke(nameof(HandPocket), 0.4f); // 手に持つ処理を遅延実行
                }
                Invoke(nameof(WaterChange), 0.5f); // 水の状態を変更する処理を遅延実行
            }
            else
            {
                WaterStatus = false;
                if (isOnes)
                {
                    isOnes = false;
                    animator.SetBool("Gun", isOnes); // アニメーションの状態を変更
                }
                Invoke(nameof(HoldLockOFF), 1f); // 長押しロックを解除する処理を遅延実行
                WaistPocket(); // 腰に持つ処理を実行
            }
        }
        else
        {
            WaterStatus = false;
        }
        WaterCannon(WaterStatus); // 放水処理の実行
        #endregion
    }
    #endregion

    #region メソッド
    void WaterCannon(bool WaterStatus)
    {
        // 放水処理
        capacity = PlayerPrefs.GetFloat("capacity"); // 水の容量を取得
        if (WaterStatus && 0f <= capacity)
        {
            if (!audiosource.isPlaying)
            {
                audiosource.Play(); // オーディオを再生
            }
            Child.SetActive(true); // 水のパーティクルを表示
            capacity -= 10 * Time.deltaTime; // 水の容量を減少
        }
        else
        {
            Child.SetActive(false); // 水のパーティクルを非表示
        }
        PlayerPrefs.SetFloat("capacity", capacity); // 水の容量を保存
    }

    void WaterChange()
    {
        WaterStatus = true; // 放水状態を設定
    }

    void HoldLockOFF()
    {
        HoldLock = false; // 長押しロックを解除
    }

    void HandPocket()
    {
        WaistHold = false; // 腰に持つ状態を解除
        HandHold = true; // 手に持つ状態を設定
    }

    void WaistPocket()
    {
        HandHold = false; // 手に持つ状態を解除
        WaistHold = true; // 腰に持つ状態を設定
    }
    #endregion
}
