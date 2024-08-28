using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollDesign : MonoBehaviour
{
    #region �t�B�[���h�̒�`
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �|��Q�[�W�̃f�U�C���̒�`
    ///
    [SerializeField] GameObject TenDesign;
    [SerializeField] GameObject EightDesign;
    [SerializeField] GameObject SixDesign;
    [SerializeField] GameObject FourDesign;
    [SerializeField] GameObject TwoDesign;
    [SerializeField] GameObject OneDesign;
    #endregion

    #region ���������\�b�h
    // Start is called before the first frame update
    void Start()
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �|��Q�[�W�̃f�U�C�����B��
    {
        TenDesign.SetActive(true);    // 100%�̓|��f�U�C����\��
        EightDesign.SetActive(false); // 80%�̓|��f�U�C�����\��
        SixDesign.SetActive(false);   // 60%�̓|��f�U�C�����\��
        FourDesign.SetActive(false);  // 40%�̓|��f�U�C�����\��
        TwoDesign.SetActive(false);   // 20%�̓|��f�U�C�����\��
        OneDesign.SetActive(false);   // 10%�̓|��f�U�C�����\��
    }
    #endregion

    #region �|��Q�[�W�̃f�U�C����\�����郁�\�b�h
    /// <summary>
    /// 80%�̓|��f�U�C����\������
    /// </summary>
    public void EightHouse()
    {
        TenDesign.SetActive(false);    // 100%�̓|��f�U�C�����\��
        EightDesign.SetActive(true);   // 80%�̓|��f�U�C����\��
    }

    /// <summary>
    /// 60%�̓|��f�U�C����\������
    /// </summary>
    public void SixHouse()
    {
        EightDesign.SetActive(false);  // 80%�̓|��f�U�C�����\��
        SixDesign.SetActive(true);     // 60%�̓|��f�U�C����\��
    }

    /// <summary>
    /// 40%�̓|��f�U�C����\������
    /// </summary>
    public void FourHouse()
    {
        SixDesign.SetActive(false);    // 60%�̓|��f�U�C�����\��
        FourDesign.SetActive(true);    // 40%�̓|��f�U�C����\��
    }

    /// <summary>
    /// 20%�̓|��f�U�C����\������
    /// </summary>
    public void TwoHouse()
    {
        FourDesign.SetActive(false);   // 40%�̓|��f�U�C�����\��
        TwoDesign.SetActive(true);     // 20%�̓|��f�U�C����\��
    }

    /// <summary>
    /// 10%�̓|��f�U�C����\������
    /// </summary>
    public void OneHouse()
    {
        TwoDesign.SetActive(false);    // 20%�̓|��f�U�C�����\��
        OneDesign.SetActive(true);     // 10%�̓|��f�U�C����\��
    }
    #endregion
}
