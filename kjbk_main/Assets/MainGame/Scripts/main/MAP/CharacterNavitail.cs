using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavitail : MonoBehaviour
{
    public Renderer objRenderer;
    private SwitchCamera switchCamera;
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
        if(!switchCamera.map_status)
        {
            objRenderer.enabled = false;
            SetChildrenRenderersEnabled(false);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NaviSystem"))
        {
            SetChildrenRenderersEnabled(true);
            objRenderer.enabled = false;
        }
        if (other.CompareTag("Player"))
        {
            objRenderer.enabled = false;
            SetChildrenRenderersEnabled(false);
        }
    }

    public void DisableRenderers()
    {
    objRenderer.enabled = false;
    SetChildrenRenderersEnabled(false);
    }

    private void SetChildrenRenderersEnabled(bool isEnabled)
    {
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = isEnabled;
        }
    }
}
