using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleUI : MonoBehaviour
{
    public float LimitTime1 = 5.0f; //�^�C�}�[�̐ݒ莞��1
    public float LimitTime2 = 8.0f; //�^�C�}�[�̐ݒ莞��2
    public GameObject CircleProgress; //�~�^�C�v�̃v���O���X�o�[
    public string ScoreFlag = "Best"; // �X�R�A�p�t���O
    private int ColorFlag; //�F�ύX�p�t���O
    private Image ImgCircle; //CircleProgress��Image�擾�p
    private float PassedTime; //�o�ߎ���
    private GameObject Rescue;
    public RescueNPC resNPC;

    public static int ResNumBest = 0;   //Best�~���Ґ�
    public static int ResNumNormal = 0;   //Normal�~���Ґ�
    public static int ResNumBad = 0;   //Bad�~���Ґ�

    // Start is called before the first frame update
    void Start()
    {
        //CircleProgress��Image�R���|�[�l���g�擾
        ImgCircle = CircleProgress.GetComponent<Image>();

        //Rescue = GameObject.Find("Rescue");
        //resNPC = Rescue.GetComponent<RescueNPC>();

        PlayerPrefs.SetInt("ResCntBest", 0);
        PlayerPrefs.SetInt("ResCntNormal", 0);
        PlayerPrefs.SetInt("ResCntBad", 0);

        //�^�C�}�[�X�^�[�g
        ColorFlag = 1;
    }

    //�h��Ԃ�
    private void Paint(float LimitTime)
    {
        //�o�ߎ��Ԃ���h��Ԃ��ʂ��v�Z
        PassedTime += Time.deltaTime;
        float amount = PassedTime / LimitTime;

        //�h��Ԃ��ʂ�������
        ImgCircle.fillAmount = 1 - amount;
    }

    //�d���҃J�E���g�p
    public void SevereCount()
    {
        if (ScoreFlag == "Best")
        {
            ResNumBest++;
            PlayerPrefs.SetInt("ResCntBest", ResNumBest);
            Debug.Log("Best++");
        }
        else if (ScoreFlag == "Normal")
        {
            ResNumNormal++;
            PlayerPrefs.SetInt("ResCntNormal", ResNumNormal);
            Debug.Log("Normal++");
        }
        else
        {
            ResNumBad++;
            PlayerPrefs.SetInt("ResCntBad", ResNumBad);
            Debug.Log("Bad++");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("�X�R�A��" + ScoreFlag);

        if (ColorFlag != 0)
        {
            if (ColorFlag == 1)
            {
                Paint(LimitTime1);
                if (LimitTime1 < PassedTime)
                {
                    ColorFlag = 2;
                    ImgCircle.color = new Color32(233, 6, 4, 255);
                    PassedTime = 0f;
                    ScoreFlag = "Normal";
                }
            }
            else if (ColorFlag == 2)
            {
                Paint(LimitTime2);
                if (LimitTime2 < PassedTime)
                {
                    ColorFlag = 0;
                    ScoreFlag = "Bad";
                }
            }
        }

        if (resNPC.IsItInGoal() && !resNPC.IsItRescued() && resNPC.Severe == true)
        {
            SevereCount();
        }
    }
}
