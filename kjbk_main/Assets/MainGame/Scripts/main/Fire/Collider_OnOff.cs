using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_OnOff : MonoBehaviour
{

    BoxCollider FireCol;

    // Start is called before the first frame update
    void Start()
    {
        FireCol = this.GetComponent<BoxCollider>();
    }

    // Update is called once per frame

    /// 
    /// ���̃R���C�_�[���I�t�ɂ���
    /// 
    public void FireOff()
    {
        FireCol.enabled = false;
    }


    /// 
    /// ���̃R���C�_�[�I���ɂ��邽�߂�n�b�ԑ҂�
    /// 
    public void FireOn()
    {
        Invoke(nameof(FireOn2), 5f);
    }


    /// 
    /// ���̃R���C�_�[���I���ɂ���
    /// 

    public void FireOn2()
    {
        FireCol.enabled = true;
    }
}
