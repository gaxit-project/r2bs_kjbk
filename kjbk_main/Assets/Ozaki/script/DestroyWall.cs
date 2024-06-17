using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    [SerializeField, Tooltip("Camera")]
    private Camera targetCamera;
    [SerializeField, Tooltip("Renderer")]
    private Renderer objectRenderer;

    private bool hasBeenOnScreen = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (targetCamera != null && objectRenderer != null)
        {
            Vector3 screenPoint = targetCamera.WorldToViewportPoint(transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            if (onScreen)
            {
                hasBeenOnScreen = true;
            }

            if (hasBeenOnScreen)
            {
                objectRenderer.enabled = true;
            }
            else
            {
                objectRenderer.enabled = false;
            }
        }
    }
}
