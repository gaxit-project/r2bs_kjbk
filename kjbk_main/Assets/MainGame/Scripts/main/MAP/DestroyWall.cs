using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    #region �ϐ��錾
    [SerializeField, Tooltip("Camera")]
    private Camera targetCamera;
    [SerializeField, Tooltip("Renderer")]
    private Renderer objectRenderer;

    private bool hasBeenOnScreen = false;
    #endregion

    void Start()
    {
        #region �I�u�W�F�N�g�̎擾
        objectRenderer = GetComponent<Renderer>();
        #endregion
    }

    void Update()
    {
        #region �J�����ƃ����_���[�̊m�F
        if (targetCamera != null && objectRenderer != null)
        {
            #region ��ʏ�̃I�u�W�F�N�g����
            Vector3 screenPoint = targetCamera.WorldToViewportPoint(transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            if (onScreen)
            {
                hasBeenOnScreen = true;
            }
            #endregion

            #region �I�u�W�F�N�g�̕\��/��\��
            if (hasBeenOnScreen)
            {
                objectRenderer.enabled = true;
            }
            else
            {
                objectRenderer.enabled = false;
            }
            #endregion
        }
        #endregion
    }
}
