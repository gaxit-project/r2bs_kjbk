using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    #region �ϐ��錾
    [SerializeField] private GameObject StaminaGauge;

    // �Q�[�W��\������Image�R���|�[�l���g
    [SerializeField] private Image _gaugeImage;

    float Stamina;

    float time;

    public Color color_1, color_2, color_3, color_4;

    #endregion

    private void Start()
    {
        _gaugeImage.fillAmount = PlayerPrefs.GetFloat("Stamina");
    }

    #region �X�V����
    void Update()
    {
        #region �X�^�~�iUI����
        Stamina = PlayerPrefs.GetFloat("Stamina");

        #region �X�^�~�i�����^������1�b�o�߂Ŕ�\��
        //�X�^�~�i�����^������1�b�o�߂Ŕ�\��
        if (Stamina >= 1f)
        {
            time += Time.deltaTime;
            if(time >= 1f)
            {
                StaminaGauge.SetActive(false);
            }
            
        }
        else
        {
            time = 0f;
            StaminaGauge.SetActive(true);
        }
        #endregion

        #region MAP�g�p���ɔ�\��
        //MAP�g�p���ɔ�\��
        if (1 == PlayerPrefs.GetInt("Map"))
        {
            StaminaGauge.SetActive(false);
        }
        #endregion


        if (0 == PlayerPrefs.GetInt("RunOut"))
        {
            //���؂���N�����Ă��Ȃ��Ƃ��͗΂����
            if (Stamina > 0.75f)
            {
                _gaugeImage.color = Color.Lerp(color_2, color_1, (Stamina - 0.75f) * 4f);
            }
            else if (Stamina > 0.25f)
            {
                _gaugeImage.color = Color.Lerp(color_3, color_2, (Stamina - 0.25f) * 4f);
            }
            else
            {
                _gaugeImage.color = Color.Lerp(color_4, color_3, Stamina * 4f);
            }
        }
        else
        {
            //���؂ꎞ�͐ԂŖ��^���܂ŉ�
            _gaugeImage.color = new Color(255, 0, 0);
        }


        // �i�����Q�[�W�ɔ��f
        _gaugeImage.fillAmount = PlayerPrefs.GetFloat("Stamina");
        #endregion

    }
    #endregion

}
