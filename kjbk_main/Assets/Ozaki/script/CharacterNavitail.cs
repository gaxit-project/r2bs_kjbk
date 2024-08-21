using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavitail : MonoBehaviour
{
    private Renderer objRenderer;
    private float timer = 0f;
    private SwitchCamera switchCamera;
    private float interval = 4.5f; // 5秒間隔
    // Start is called before the first frame update
    void Start()
    {
        // Rendererコンポーネントを取得
        objRenderer = GetComponent<Renderer>();
        switchCamera = FindObjectOfType<SwitchCamera>();

        // オブジェクトを非表示にする
        objRenderer.enabled = false;
        SetChildrenRenderersEnabled(false);
    }

    void Update()
    {
        // タイマーを更新
        timer += Time.deltaTime;

        // 5秒ごとにレンダラーをオフにする
        /*if (timer >= interval)
        {*/
            if(!switchCamera.map_status){
            objRenderer.enabled = false;
            SetChildrenRenderersEnabled(false);
            timer = 0f; // タイマーをリセット
            }
        //}
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        // �Փ˂����I�u�W�F�N�g���v���C���[�ł���ꍇ
        if (other.CompareTag("NaviSystem"))
        {
            
            SetChildrenRenderersEnabled(true);
            objRenderer.enabled = false;
            timer = 0f; // タイマーをリセットしてすぐにオフにならないようにする
        }
        if (other.CompareTag("Player"))
        {
            objRenderer.enabled = false;
            SetChildrenRenderersEnabled(false);
        }
    }

    private void SetChildrenRenderersEnabled(bool isEnabled)
    {
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = isEnabled;
        }
    }
}
