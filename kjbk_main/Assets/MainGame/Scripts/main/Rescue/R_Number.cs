using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Number : MonoBehaviour
{
    #region �錾
    // �y�ǎҎ��ʂ̂��߂̕ϐ�
    public int Number;
    #endregion

    #region ������
    void Start()
    {
        PlayerPrefs.SetInt("R_number", 0);
    }
    #endregion


    #region �֐�
    // �y�ǎ҂����ʂ��邽�߂̊֐�
    public void RNumber()
    {
        PlayerPrefs.SetInt("R_number", Number);
    }
    #endregion
}
