using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollDesign : MonoBehaviour
{
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �|��Q�[�W�̃f�U�C���̒�`
    /// 
    [SerializeField] GameObject TenDesign;
    [SerializeField] GameObject EightDesign;
    [SerializeField] GameObject SixDesign;
    [SerializeField] GameObject FourDesign;
    [SerializeField] GameObject TwoDesign;
    [SerializeField] GameObject OneDesign;

    // Start is called before the first frame update
    void Start()
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///�|��Q�[�W�̃f�U�C�����B��
    {
        //�ǉ��R�[�h
        TenDesign.SetActive(true);
        EightDesign.SetActive(false);
        SixDesign.SetActive(false);
        FourDesign.SetActive(false);
        TwoDesign.SetActive(false);
        OneDesign.SetActive(false);
    }
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �|��Q�[�W�̃f�U�C����\������
    public void EightHouse()
    {
        //�ǉ��R�[�h
        TenDesign.SetActive(false);        //80%�̎��̓|��f�U�C�����\��
        EightDesign.SetActive(true);          //80%�̎��|��f�U�C����\��
    }

    public void SixHouse()
    {
        EightDesign.SetActive(false);        //80%�̎��̓|��f�U�C�����\��
        SixDesign.SetActive(true);           //60%�̎��|��f�U�C����\��
    }

    public void FourHouse()
    {
        SixDesign.SetActive(false);          //60%�̎��̓|��f�U�C�����\��
        FourDesign.SetActive(true);          //40%�̎��|��f�U�C����\��
    }

    public void TwoHouse()
    {
        FourDesign.SetActive(false);         //40%�̎��̓|��f�U�C�����\��
        TwoDesign.SetActive(true);           //20%�̎��|��f�U�C����\��
    }

    public void OneHouse()
    {
        TwoDesign.SetActive(false);          //20%�̎��̓|��f�U�C�����\��
        OneDesign.SetActive(true);           //10%�̎��|��f�U�C����\��
    }
}