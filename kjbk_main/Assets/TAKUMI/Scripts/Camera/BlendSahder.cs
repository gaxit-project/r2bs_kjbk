using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendSahder : MonoBehaviour
{
    [SerializeField]
    private Material _material;

    private MeshFilter _meshFilter;

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }

    // OnRenderObjectで描画
    private void OnRenderObject()
    {
        if (_material == null)
        {
            return;
        }

        // セットパスしてからDrawMeshNowすればこのタイミングで描画できる
        _material.SetPass(0);
        Graphics.DrawMeshNow(_meshFilter.sharedMesh, transform.position, transform.rotation);
    }
}
