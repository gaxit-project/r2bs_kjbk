using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CollGauge : MonoBehaviour
{
    #region �t�B�[���h�̒�`
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �|��Q�[�W�̐ݒ�Ƒ��̃X�N���v�g�̎Q��
    ///
    [SerializeField] TextMeshProUGUI CGauge;

    float CountTime = 0;            // ���Ԍv���p�̕ϐ�
    public static int Collapse = 100; // �|��Q�[�W�̏����l
    float Span = 4.5f;               // Span�b���Ƃɓ|��Q�[�W��1%����������
    public CollDesign Design;        // CollDesign�X�N���v�g�̎Q��
    private bool STOP = false;       // �����̃t���O
    int a = 5;                       // �����̎�ޕ����p�ϐ�

    int number100 = 1;
    int number10 = 0;
    int number1 = 0;
    int persent = 10;

    public BlockPOP POP;             // ��Q����ݒu����R�[�h����ϐ��������Ă���

    public Radio_ver4 Radio4;        // �����X�N���v�g����ϐ��������Ă���

    public static bool TimeStop = false;
    #endregion

    #region ���������\�b�h
    // Use this for initialization
    void Start()
    {
        // �|��Q�[�W�̏����\����ݒ�
        CGauge.SetText("<sprite=" + number100 + ">" + "<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        Collapse = 100; // �|��Q�[�W��100�Ƀ��Z�b�g
    }
    #endregion

    #region �X�V���\�b�h
    // Update is called once per frame
    void Update()
    {
        if(!TimeStop)
        {
            // ���Ԃ̃J�E���g
            CountTime += Time.deltaTime;
        }
        

        // �|��Q�[�W�̍X�V
        if (CountTime >= Span)
        {
            Collapse--;            // �|��Q�[�W��1%����
            CountTime = 0;          // �b���J�E���g�����Z�b�g
            number10 = Collapse / 10 % 10;
            number1 = Collapse % 10;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // �|��Q�[�W������̒l�ɒB�����ۂ̏���
            if (Collapse == 80)
            {
                Design.EightHouse();             // 80%���̉Ƃ̃f�U�C����\��
                CollapseRadioON();               // �������I���ɂ���
            }
            else if (Collapse == 60)
            {
                Design.SixHouse();               // 60%���̉Ƃ̃f�U�C����\��
                CollapseRadioON();               // �������I���ɂ���
            }
            else if (Collapse == 40)
            {
                Design.FourHouse();              // 40%���̉Ƃ̃f�U�C����\��
                POP.Generate40 = true;           // ��Q�������t���O���I��
                CollapseRadioON();               // �������I���ɂ���
            }
            else if (Collapse == 20)
            {
                Design.TwoHouse();               // 20%���̉Ƃ̃f�U�C����\��
                POP.Generate20 = true;           // ��Q�������t���O���I��
                CollapseRadioON();               // �������I���ɂ���
            }
            else if (Collapse == 10)
            {
                Design.OneHouse();               // 10%���̉Ƃ̃f�U�C����\��
                POP.Generate10 = true;           // ��Q�������t���O���I��
                CollapseRadioON();               // �������I���ɂ���
            }
            else if (Collapse <= 0)
            {
                PlayerPrefs.SetString("Result", "GAMEOVER"); // �Q�[���I�[�o�[���̏���
                Scene.Instance.GameResult();                 // �Q�[�����ʉ�ʂ֑J��
            }

            // �����t���O�������Ă���ꍇ�̏���
            if (STOP)
            {
                STOP = false;  // �t���O�����Z�b�g
            }
        }

        // �|��Q�[�W��100�����̎��̕\���X�V
        if (Collapse < 100)
        {
            CGauge.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        }
    }
    #endregion

    #region �����֘A�̃��\�b�h
    // �����̃t���O���I���ɂ���
    void STOPFlagON()
    {
        STOP = true;
    }

    // �|�󖳐����I���ɂ���
    void CollapseRadioON()
    {
        Radio4.CollapseDialogue();
    }
    #endregion
}
