using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollapseDesign2 : MonoBehaviour
{
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �|��Q�[�W�̃f�U�C���̒�`
    /// 
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
        Debug.Log("EightHouse");
        EightDesign.SetActive(true);          //80%�̎��|��f�U�C����\��
    }

    public void SixHouse()
    {
        Debug.Log("SixHouse");
        EightDesign.SetActive(false);        //80%�̎��̓|��f�U�C�����\��
        SixDesign.SetActive(true);           //60%�̎��|��f�U�C����\��
    }

    public void FourHouse()
    {
        Debug.Log("FourHouse");
        SixDesign.SetActive(false);          //60%�̎��̓|��f�U�C�����\��
        FourDesign.SetActive(true);          //40%�̎��|��f�U�C����\��
    }

    public void TwoHouse()
    {
        Debug.Log("TwoHouse");
        FourDesign.SetActive(false);         //40%�̎��̓|��f�U�C�����\��
        TwoDesign.SetActive(true);           //20%�̎��|��f�U�C����\��
    }

    public void OneHouse()
    {
        Debug.Log("OneHouse");
        TwoDesign.SetActive(false);          //20%�̎��̓|��f�U�C�����\��
        OneDesign.SetActive(true);           //10%�̎��|��f�U�C����\��
    }
}