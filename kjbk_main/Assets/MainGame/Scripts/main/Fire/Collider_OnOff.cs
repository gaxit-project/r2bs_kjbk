using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_OnOff : MonoBehaviour
{

    BoxCollider FireCol;

    public void Start()
    {
        FireCol = this.GetComponent<BoxCollider>();
    }


    // ���̃R���C�_�[���I���ɂ���
    #region ���̃R���C�_�[�I��


    public void OnCollisionEnter(Collision Hit)
    {
        if (Hit.gameObject.tag == "Player")
        {
            FireCol.enabled = false;
            Invoke(nameof(FireOn), 5f);
        }
    }
    #endregion


    // ���̃R���C�_�[���I�t�ɂ���
    #region ���̃R���C�_�[�I�t

    public void FireOn()
    {
        FireCol.enabled = true;
    }
    #endregion
}
