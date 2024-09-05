using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class OutlinePostEffect : MonoBehaviour
{
    public Shader outlineShader;
    private Material outlineMaterial;

    void OnEnable()
    {
        if (outlineShader == null)
        {
            Debug.LogError("Outline Shader is missing!");
            return;
        }

        if (outlineMaterial == null)
        {
            outlineMaterial = new Material(outlineShader);
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (outlineShader == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        // Render the outline pass
        var tempRT = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);
        Graphics.Blit(src, tempRT, outlineMaterial, 0);

        // Apply stencil test to highlight only tagged objects
        outlineMaterial.SetTexture("_MainTex", tempRT);
        Graphics.Blit(src, dest, outlineMaterial, 1);

        RenderTexture.ReleaseTemporary(tempRT);
    }
}
