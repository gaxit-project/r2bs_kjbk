using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterHose : MonoBehaviour
{
    public GameObject Child; //水のパーティクルを格納
    
    public static bool WaterStatus = false; //放水状態
    public static bool Hold = false; //長押し判定

    float capacity;

    public GameObject FF;
    public GameObject Hand;
    public GameObject Waist;

    Transform HandPos;
    Transform WaistPos;

    private Animator animator;

    bool isOnes = false;
    private AudioSource audiosource;

    bool HoldLock = false;

    private void OnEnable()
    {
        Debug.Log("aaaa");
        WaterStatus = false;
        Hold = false;
        audiosource = GetComponent<AudioSource>();
        WaterCannon(WaterStatus);
        HandPos = Hand.transform;
        WaistPos = Waist.transform;

        this.transform.position = new Vector3(WaistPos.position.x, WaistPos.position.y, WaistPos.position.z);

    }

    public void OnFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                // ボタンが押された時の処理
                Hold = true;
                break;

            case InputActionPhase.Canceled:
                // ボタンが離された時の処理
                Hold = false;
                break;
        }
    }
    void Start(){
        Child = gameObject.transform.GetChild(0).gameObject;
        //アニメーション読み込み
        animator = FF.GetComponent<Animator>();
    }

    void Update()
    {
        HandPos = Hand.transform;
        WaistPos = Waist.transform;
        capacity = PlayerPrefs.GetFloat("capacity");
        if(Input.GetMouseButtonDown(1))
        {
            Hold = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            
            Hold = false;
            HoldLock = true;
        }

        /*
        if (Hold)
        {
            Debug.Log("Hold");
        }
        */

        if(HoldLock)
        {
            Hold = false;
        }

        if (PlayerRayCast.HosuStatus == true)
        {
            Debug.Log("ホースは持っている");
            if (Hold)
            {
                if (!isOnes)
                {
                    isOnes = true;
                    animator.SetBool("Gun", isOnes);
                    Invoke(nameof(HandPocket), 0.4f);

                }
                Invoke(nameof(WaterChange), 0.3f);

            }
            else
            {
                WaterStatus = false;
                if (isOnes)
                {
                    isOnes = false;
                    animator.SetBool("Gun", isOnes);
                }
                Invoke(nameof(HoldLockOFF), 1f);
                this.transform.position = new Vector3(WaistPos.position.x, WaistPos.position.y, WaistPos.position.z);
            }
        }
        else
        {
            WaterStatus = false;
        }
        WaterCannon(WaterStatus);

    }
    void WaterCannon(bool WaterStatus)
    {
        capacity = PlayerPrefs.GetFloat("capacity");
        if (WaterStatus && 0f <= capacity)
        {
            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }
            Child.SetActive(true);
            capacity -= 10 * Time.deltaTime;
            //Debug.Log("capacity = " + capacity);
        }
        else
        {
            Child.SetActive(false);
        }
        PlayerPrefs.SetFloat("capacity",capacity);
    }
    void WaterChange()
    {
        WaterStatus = true;
    }
    void HoldLockOFF()
    {
        HoldLock = false;
    }

    void HandPocket()
    {
        this.transform.position = new Vector3(HandPos.position.x, HandPos.position.y, HandPos.position.z);
    }
}
