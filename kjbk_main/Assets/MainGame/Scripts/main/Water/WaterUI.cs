using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterUI : MonoBehaviour
{
    #region �錾
    public GameObject WaterBar;
    private Image shokaImg;
    private float PassedTime;
    #endregion

    #region ������
    void Start()
    {
        shokaImg = WaterBar.GetComponent<Image>();
    }
    #endregion

    #region ���Ί�Q�[�W�̕\��

    void Update()
    {
        float amount = PlayerPrefs.GetFloat("capacity") / 100;

        //�h��Ԃ��ʂ�������
        shokaImg.fillAmount = amount;
    }
    #endregion
}