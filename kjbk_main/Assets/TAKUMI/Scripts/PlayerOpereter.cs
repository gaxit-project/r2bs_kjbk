using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOpereter : MonoBehaviour
{
    public GameObject mainCamera;      //メインカメラ格納用
    public GameObject subCamera;       //サブカメラ格納用 

    private bool CameraStatus = false; //カメラの状態

    public float speed = 1f; //移動速度

    public float sensi;  //感度

    private Animator _animator;

    static readonly int WalkStateHash = Animator.StringToHash("walking");
    void Start () {
        //メインカメラとサブカメラをそれぞれ取得
        mainCamera = GameObject.Find("Main Camera");
        subCamera = GameObject.Find("FPCamera");

        //サブカメラを非アクティブにする
        mainCamera.SetActive(true); 
        subCamera.SetActive(false);

        //Animatorの情報を取得
        _animator = GetComponent<Animator>();

	}
		void Update () {

        EDir d = KeyToDir();
        KeyToDirMOVE(d, CameraStatus, speed, _animator);
        if(Input.GetMouseButton(0)){
            cameracon();  
        }

		//スペースキーが押されている間、サブカメラをアクティブにする
        if(Input.GetKeyDown("p")){
            if(CameraStatus == false)
            {
                Debug.Log("Sub");
                //サブカメラをアクティブに設定
                mainCamera.SetActive(false);
                subCamera.SetActive(true);
                CameraStatus = true;
            }else{
                Debug.Log("Main");
                //メインカメラをアクティブに設定
                subCamera.SetActive(false);
                mainCamera.SetActive(true);
                CameraStatus = false;
            }
        }
	}

    private EDir KeyToDir()
    {
        if (!Input.anyKey)
        {
            return EDir.Pause;
        }
        if (Input.GetKey("a"))
        {
            return EDir.Left;
        }
        if (Input.GetKey("w"))
        {
            return EDir.Up;
        }
        if (Input.GetKey("d"))
        {
            return EDir.Right;
        }
        if (Input.GetKey("s"))
        {
            return EDir.Down;
        }
        return EDir.Pause;
    }

    public void KeyToDirMOVE(EDir d, bool CameraStatus, float speed, Animator _animator)
    {
        if(CameraStatus == false) //三人称カメラON
        {
            if(d == EDir.Up)
            {
                _animator.SetBool(WalkStateHash, true);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            if(d == EDir.Down){
                _animator.SetBool(WalkStateHash, true);
                transform.position -= transform.forward * speed * Time.deltaTime;
            }
            if(d == EDir.Right){
                _animator.SetBool(WalkStateHash, true);
                transform.position += transform.right * speed * Time.deltaTime;
            }
            if(d == EDir.Left){
                _animator.SetBool(WalkStateHash, true);
                transform.position -= transform.right * speed * Time.deltaTime;
            }
            if(d == EDir.Pause){
                _animator.SetBool(WalkStateHash, false);
            }
        }else{ //一人称カメラON
            if(d == EDir.Up)
            {
                _animator.SetBool(WalkStateHash, true);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            if(d == EDir.Down){
                _animator.SetBool(WalkStateHash, true);
                transform.position -= transform.forward * speed * Time.deltaTime;
            }
            if(d == EDir.Right){
                _animator.SetBool(WalkStateHash, true);
                transform.position += transform.right * speed * Time.deltaTime;
            }
            if(d == EDir.Left){
                _animator.SetBool(WalkStateHash, true);
                transform.position -= transform.right * speed * Time.deltaTime;
            }
            if(d == EDir.Pause){
                _animator.SetBool(WalkStateHash, false);
            }
        }
    }

    void cameracon()
    {
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");
        x_Rotation = x_Rotation * sensi;
        y_Rotation = y_Rotation * sensi;
        this.transform.Rotate(0, x_Rotation, 0);
        //camera.transform.Rotate(-y_Rotation, 0, 0);
    }

    /**
    * 引数で与えられた向きに対応する回転のベクトルを返す
    */
    private Quaternion DirToRotation(EDir d)
    {
        Quaternion r = Quaternion.Euler(0, 0, 0);
        switch (d)
        {
            case EDir.Left:
                r = Quaternion.Euler(0, 270, 0); break;
            case EDir.Up:
                r = Quaternion.Euler(0, 0, 0); break;
            case EDir.Right:
                r = Quaternion.Euler(0, 90, 0); break;
            case EDir.Down:
                r = Quaternion.Euler(0, 180, 0); break;
        }
        return r;
    }
}
