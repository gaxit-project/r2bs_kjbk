using UnityEngine;
using System.Collections;

public class Post : MonoBehaviour
{

    public Material monoTone;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, monoTone);
    }
}