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
        // テキストの回転をNPCの影響を受けないようにし、カメラに向ける
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 textPosition = transform.position;

        // テキストがカメラの方向を向くようにするが、Y軸の回転だけを有効にする
        Vector3 direction = (textPosition - cameraPosition).normalized; // カメラからテキストまでの方向ベクトル
        direction.y = 0;  // Y軸の回転のみ適用し、上下の回転を防ぐ

        // 回転を計算して、テキストが常にカメラの方向を向くようにする
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
