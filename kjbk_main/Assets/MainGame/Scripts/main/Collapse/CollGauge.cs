using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CollGauge : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CGauge;

    float CountTime = 0;            //���Ԍv��
    public static int Collapse = 100;            //�|��Q�[�W
    float Span = 4.5f;                 //Span�b�Ɉ��|��Q�[�W��1%���炷
    public CollDesign Design;  //CollapseDesign2.cs����֐������ė�����
    private bool STOP = false;      //�����̃t���O
    int a = 5;                      //�����̎�ޕ���

    int number100 = 1;
    int number10 = 0;
    int number1 = 0;
    int persent = 10;

    
    public BlockPOP POP;  //��Q����ݒu����R�[�h����ϐ��������Ă���

    public Radio_ver4 Radio4;  //��������ϐ��������Ă���

    // Use this for initialization
    void Start()
    {
        CGauge.SetText("<sprite=" + number100 + ">" + "<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        Collapse = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // �|��Q�[�W�J�E���g
        CountTime += Time.deltaTime;   //�b���J�E���g
        if (CountTime >= Span)          //�|��Q�[�W��1%���̕b��
        {
            Collapse--;                //�|��Q�[�W-1%
            CountTime = 0;             //�b���J�E���g���Z�b�g
            number10 = Collapse / 10 % 10;
            number1 = Collapse % 10;


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //�|��Q�[�W�̖����ʒm�{�|��Q�[�W�̃f�U�C���\��
            if (Collapse == 80)
            {
                Design.EightHouse();             //�Ƃ̃f�U�C�����o��
                CollapseRadioON();
            }

            else if (Collapse == 60)
            {
                Design.SixHouse();
                CollapseRadioON();
            }

            else if (Collapse == 40)
            {
                Design.FourHouse();
                POP.Generate40 = true;
                CollapseRadioON();
            }

            else if (Collapse == 20)
            {
                Design.TwoHouse();
                POP.Generate20 = true;
                CollapseRadioON();
            }

            else if (Collapse == 10)
            {
                Design.OneHouse();
                POP.Generate10 = true;
                CollapseRadioON();
            }
            else if (Collapse <= 0)
            {
                PlayerPrefs.SetString("Result", "GAMEOVER");
                Scene.Instance.GameResult();
            }

            //����̃Q�[�W���ɖ������o���悤�ɂ���
            if (STOP)
            {
                //�t���O���͂�����ȉ��̒ʂ�ɖ��������s
                STOP = false;  //�t���O��OFF��
            }
        }

        if(Collapse < 100)
        {
            // �|��Q�[�W�̕\��
            CGauge.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        }

    }


    //�����̃t���O
    void STOPFlagON()
    {
        STOP = true;
    }
    void CollapseRadioON()
    {
        Radio4.CollapseDialogue();
    }
    /////////////////////////////////////////////////////

}