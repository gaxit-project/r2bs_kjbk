using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollapseGauge2 : MonoBehaviour
{
    float CountTime = 0;            //���Ԍv��
    float Collapse = 100;            //�|��Q�[�W
    float Span = 1;                 //Span�b�Ɉ��|��Q�[�W��1%���炷
    public Radio Demoscript;        //Radio.cs����֐������ė�����
    public CollapseDesign2 Design;  //CollapseDesign2.cs����֐������ė�����
    //public SceneChange Over;        //SceneChange.cs����Q�[���I�[�o�[�������Ă���
    //public Sunaarashi_ON_OFF Suna;  //Sunaarashi���獻���������Ă���

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
            if (Collapse == 80)
            {
               // Suna.SunaONOFF();
                Demoscript.EightGauge();
                Design.EightHouse();
            }

            else if (Collapse == 60)
            {
                Demoscript.SixGauge();
                Design.SixHouse();
            }

            else if (Collapse == 40)
            {
                Demoscript.FourGauge();
                Design.FourHouse();
            }

            else if (Collapse == 20)
            {
                Demoscript.TwoGauge();
                Design.TwoHouse();
            }

            else if (Collapse == 10)
            {
                Demoscript.OneGauge();
                Design.OneHouse();
            }
            else if (Collapse <= 0)
            {
                //Over.GameOver();   
            }
        }


        // �|��Q�[�W�̕\��
        GetComponent<Text>().text = Collapse.ToString("0��");
    }

    /////////////////////////////////////////////////////

}