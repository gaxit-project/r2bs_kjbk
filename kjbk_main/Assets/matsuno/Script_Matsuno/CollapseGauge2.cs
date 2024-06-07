using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollapseGauge2 : MonoBehaviour
{
    [HideInInspector] public float CountTime = 0;            //���Ԍv��
    float Collapse = 100;            //�|��Q�[�W
    float Span = 3.5f;                 //Span�b�Ɉ��|��Q�[�W��1%���炷
    public Radio Demoscript;        //Radio.cs����֐������ė�����
    public CollapseDesign2 Design;  //CollapseDesign2.cs����֐������ė�����
    public Sunaarashi_ON_OFF Suna;  //�����������Ă���
    private bool STOP = false;      //�����̃t���O
    int a = 5;                      //�����̎�ޕ���
    //public SceneChange Over;        //SceneChange.cs����Q�[���I�[�o�[�������Ă���
    //public Sunaarashi_ON_OFF Suna;  //Sunaarashi���獻���������Ă���


    public bool Collapse80 = false;
    public bool Collapse60 = false;
    public bool Collapse40 = false;
    public bool Collapse20 = false;
    public bool Collapse10 = false;

    // Use this for initialization
    void Start()
    {
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

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //�|��Q�[�W�̖����ʒm�{�|��Q�[�W�̃f�U�C���\��
            if(Collapse == 80)
            {
                Design.EightHouse();             //�Ƃ̃f�U�C�����o��
                Suna.SunaONOFF();                //������\��
                Invoke(nameof(STOPFlagON), 2f);  //�t���O���������ON�ɂ���
            }

            else if (Collapse == 60)
            {
                Design.SixHouse();
                Suna.SunaONOFF();
                Invoke(nameof(STOPFlagON), 2f);
            }

            else if (Collapse == 40)
            {
                Design.FourHouse();
                Suna.SunaONOFF();
                Invoke(nameof(STOPFlagON), 2f);
            }

            else if (Collapse == 20)
            {
                Design.TwoHouse();
                Suna.SunaONOFF();
                Invoke(nameof(STOPFlagON), 2f);
            }

            else if (Collapse == 10)
            {
                Design.OneHouse();
                Suna.SunaONOFF();
                Invoke(nameof(STOPFlagON), 2f);
            }
            else if (Collapse <= 0)
            {
                Scene.Instance.GameOver(); 
            }
            
            //����̃Q�[�W���ɖ������o���悤�ɂ���
            if (STOP)
            {
                //�t���O���͂�����ȉ��̒ʂ�ɖ��������s
                if(a == 5)
                {
                    Collapse80 = true;
                }
                else if(a == 4)
                {
                    Collapse60 = true;
                }
                else if (a == 3)
                {
                    Collapse40 = true;
                }
                else if (a == 2)
                {
                    Collapse20 = true;
                }
                else if (a == 1)
                {
                    Collapse10 = true;
                }
                STOP = false;  //�t���O��OFF��
                a--;           //���̖����ɕύX
            }
        }


        // �|��Q�[�W�̕\��
        GetComponent<Text>().text = Collapse.ToString("0��");
    }


    //�����̃t���O
    void STOPFlagON()
    {
        STOP = true;
    }
    /////////////////////////////////////////////////////

}