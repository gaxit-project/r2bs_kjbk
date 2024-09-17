using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera mainCamera; // メインカメラを参照

    void Start()
    {
        // カメラが指定されていなければメインカメラを自動で設定
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        // カメラの方向に基づいてキャンバスの回転を設定する（位置は変更しない）
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;

        // Y軸の回転のみ適用して、テキストが上下に回転しないようにする
        direction.y = 0;

        // キャンバスの回転をカメラの方向に向ける
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
