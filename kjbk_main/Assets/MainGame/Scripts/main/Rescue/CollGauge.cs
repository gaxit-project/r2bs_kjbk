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
    float Span = 4f;                 //Span�b�Ɉ��|��Q�[�W��1%���炷
    public CollRadio Demoscript;        //Radio.cs����֐������ė�����
    public CollDesign Design;  //CollapseDesign2.cs����֐������ė�����
    public Sunaarashi_ON_OFF Suna;  //�����������Ă���
    private bool STOP = false;      //�����̃t���O
    int a = 5;                      //�����̎�ޕ���

    int number100 = 1;
    int number10 = 0;
    int number1 = 0;
    int persent = 10;


    public BlockPOP POP;
    //public SceneChange Over;        //SceneChange.cs����Q�[���I�[�o�[�������Ă���
    //public Sunaarashi_ON_OFF Suna;  //Sunaarashi���獻���������Ă���

    public Radio_ver3 Radio3;

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
                Suna.SunaONOFF();                //������\��
                Invoke(nameof(STOPFlagON), 2f);  //�t���O���������ON�ɂ���
                R80();
                Debug.Log("radioFlag="+Radio3.Radio80);
            }

            else if (Collapse == 60)
            {
                Design.SixHouse();
                Suna.SunaONOFF();
                Invoke(nameof(STOPFlagON), 2f);
                R60();
            }

            else if (Collapse == 40)
            {
                Design.FourHouse();
                Suna.SunaONOFF();
                POP.Generate40 = true;
                Invoke(nameof(STOPFlagON), 2f);
                R40();
            }

            else if (Collapse == 20)
            {
                Design.TwoHouse();
                Suna.SunaONOFF();
                POP.Generate20 = true;
                Invoke(nameof(STOPFlagON), 2f);
                R20();
            }

            else if (Collapse == 10)
            {
                Design.OneHouse();
                Suna.SunaONOFF();
                POP.Generate10 = true;
                Invoke(nameof(STOPFlagON), 2f);
                R10();
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
                Radio3.CollapseFlag = true;
                Radio3.CollapsePanel();
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
    public void R80()
    {
        Radio3.Radio80 = true;
    }

    public void R60()
    {
        Radio3.Radio60 = true;
    }
    public void R40()
    {
        Radio3.Radio40 = true;
    }
    public void R20()
    {
        Radio3.Radio20 = true;
    }
    public void R10()
    {
        Radio3.Radio10 = true;
    }
    /////////////////////////////////////////////////////

}