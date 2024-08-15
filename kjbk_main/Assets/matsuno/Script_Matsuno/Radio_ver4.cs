using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class Radio_ver4 : MonoBehaviour
{
    [SerializeField] GameObject ChatPanel;
    [SerializeField] GameObject ChatPanel1;
    [SerializeField] GameObject ChatPanel2;
    [SerializeField] GameObject ChatPanel3;
    [SerializeField] GameObject ChatPanel4;
    [SerializeField] GameObject ChatR;


    [SerializeField] public TMP_Text RadioText;
    [SerializeField] private TMP_Text RadioText2;

    public RescuePOP RPOP;
    [HideInInspector] public bool Radio80;
    [HideInInspector] public bool Radio60;
    [HideInInspector] public bool Radio40;
    [HideInInspector] public bool Radio20;
    [HideInInspector] public bool Radio10;


    //�Z���t������X�^�b�N
    public Stack<string> stackObj = new Stack<string>();
    public Stack<string> stackRadio = new Stack<string>();
    public Stack<string> stackBring = new Stack<string>();

    //�Z���t���|�b�v�����Ƃ��ɓ����ϐ�
    string NewText;

    //�y�ǎ҂̕ϐ�������
    public int number1 = 1;
    //��ԍŏ��̃e�L�X�g���ǂ����̃t���O
    public bool FirstFlag = true;
    //�e�L�X�g���\������Ă��邩�̃t���O
    bool TextONFlag = false;
    //�������҂���Ԃ��ǂ����̃t���O
    bool CollapseDialogueFlag = false;
    //�����̃A�C�R����\�����邩�̃t���O
    bool CollapseIconFlag = false;
    //��ԍŏ��̃e�L�X�g�̃t���O
    bool FirstTextFlag = false;

    //�R���[�`���̊m�F
    private Coroutine activeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        stackObj.Clear();
        stackRadio.Clear();
        stackBring.Clear();
        ChatPanel.SetActive(false);
        ChatPanel1.SetActive(false);
        ChatPanel2.SetActive(false);
        ChatPanel3.SetActive(false);
        ChatR.SetActive(false);
        //�X�^�b�N�̒��g������
        stackObj.Push("���A�Ԏ�肪�킩��Ȃ�?\r\n�}�b�v�������邩��m�F���Ă݂�");
        stackObj.Push("����������I<sprite=1>��\r\n���̕��Ől���|��Ă���!");
        stackRadio.Push("�|�󐡑O����\r\n����������");
        stackRadio.Push("�V�䂪����n�߂Ă邼\r\n�}���ł���");
        stackRadio.Push("�h�΃V���b�^�[�����낵�ĉ��̉��Ă�h���ł�����");
        stackRadio.Push("�q�r���g�債�Ă�����������������邼");
        stackRadio.Push("���������Ƀq�r�������Ă��Ȃ����H");
        stackRadio.Push("����͊w�������I�s���s���҂̓�10�l���~���̂��N�̔C����");
        stackBring.Push("�d���҂͂������Ȃ��������I");
        stackBring.Push("�܂��d���҂�����悤���I�������������𗊂ށI");
        stackBring.Push("�܂��d���҂�����悤���I�������������𗊂ށI");
        stackBring.Push("�܂��d���҂�����悤���I�������������𗊂ށI");
        stackBring.Push("�܂��d���҂�����悤���I�������������𗊂ށI");
        stackBring.Push("���̌����̍\������肵��\n\r���Њ��p���Ă݂Ă���");
        FirstFlag = true;
        //���[�������̃��W�I���o��
        CollapseDialogue();
    }

    // �y�ǎ҂̃q���g���v�b�V������
    public void Push()
    {
        //�X�^�b�N�̒��g�����Z�b�g
        stackObj.Clear();
        int RandomText = RPOP.rndom;
        if(RandomText == 1)
        {
            stackObj.Push("<sprite=6>�Ől�������Ȃ����Ă�����ł���");
            stackObj.Push("������ʂɐl�e����������������Ȃ��ꉞ�������Ă���Ȃ���");
            stackObj.Push("���̕��ɐl�������Ă�������");
        }
        else if(RandomText == 2)
        {
            stackObj.Push("<sprite=5>�Ől���|��Ă�����");
            stackObj.Push("����������<sprite=5>���ʂŋ��ѐ������������");
            stackObj.Push("�k���ɐl�������Ă�������");
        }
        else if(RandomText == 3)
        {
            stackObj.Push("������<sprite=2>�ɓ������l���łĂ��Ȃ���...");
            stackObj.Push("���Ŋ����~�܂�Ȃ���...�����C�ɓ��肽��...");
            stackObj.Push("�������k���ɐl���������Ă�������");
        }
        else if(RandomText == 4)
        {
            stackObj.Push("<sprite=3>�ŕ����|��ē����Ȃ��l������́I");
            stackObj.Push("�������܂݂�`�����������ւ������I");
            stackObj.Push("�쑤�ɐl���������Ă�������");
        }
        else if(RandomText == 5)
        {
            stackObj.Push("<sprite=4>�Ő����ς��������Q�ĂċN���Ȃ��񂾁I�����Ă���Ă���");
            stackObj.Push("�܂�������ȏ󋵂ŐQ�Ă��͂��Ȃ����...");
            stackObj.Push("�����������ɑ����ē�����l��������");
        }
    }

    // �q���g���o�Ȃ��Ƃ��͂������烉���_���Ńe�L�X�g���o�͂���
    void RandomDialugue()
    {
        int RndDialugue = Random.Range(1, 4);
        if(RndDialugue == 1)
        {
            RadioText.SetText("�ق�Ƃ��ɏ���������I�N�͖��̉��l���I");
        }
        else if(RndDialugue == 2)
        {
            RadioText.SetText("���肪�Ƃ�...�����ċA���...");
        }
        else if(RndDialugue == 3)
        {
            RadioText.SetText("�Ȃ�Ă��΂炵���g�̂��Ȃ��Ȃ񂾁I���肪�Ƃ��I");
        }
    }

    //�Z���t��\������
    public void Dialogue()
    {
        //�e�L�X�g���Z�b�g
        TextPanelOFF();
        RPOP.AllRCnt--;

        // ���݂̃R���[�`�������s���Ȃ��~����
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }

        int RndHalf = Random.Range(1, 3);
        //�X�^�b�N�̒��g���J���������烉���_���Ƀe�L�X�g������
        if(stackObj.Count == 0)
        {
            RandomDialugue();
        }
        //�X�^�b�N����|�b�v�����Ă��̃e�L�X�g������
        else if (RndHalf == 1 || FirstFlag)
        {
            NewText = stackObj.Pop();
            RadioText.SetText(NewText);

            if(FirstFlag)
            {
                FirstTextFlag = true;
            }
            FirstFlag = false;
        }
        //�y�ǎҐ��ƃX�^�b�N�̒��g�������������͈ȉ��̏ꍇ�|�b�v���ăe�L�X�g�ɓ����
        else if(stackObj.Count >= RPOP.AllRCnt)
        {
            NewText = stackObj.Pop();
            RadioText.SetText(NewText);
        }
        //����ȊO�̓����_���Ńe�L�X�g�ɓ����
        else
        {
            RandomDialugue();
        }
        // �V�����R���[�`�����J�n���A���̎Q�Ƃ�ۑ�����
        activeCoroutine = StartCoroutine(Simple1());
        TextPanelON();
    }


    //�R���v�X�Q�[�W��\������
    public void CollapseDialogue()
    {
        CollapseDialogueFlag = true;
        //�����e�L�X�g���o�Ă��Ȃ����
        if(!TextONFlag)
        {
            CollapseIconFlag = true;
            NewText = stackRadio.Pop();
            RadioText.SetText(NewText);
            TextPanelON();
            CollapseDialogueFlag = false;
            // �V�����R���[�`�����J�n���A���̎Q�Ƃ�ۑ�����
            activeCoroutine = StartCoroutine(Simple1());
        }
    }

    public void BringDialogue()
    {
        CollapseIconFlag = true;
        // ���݂̃R���[�`�������s���Ȃ��~����
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }
        NewText = stackBring.Pop();
        RadioText.SetText(NewText);
        TextPanelON();
        // �V�����R���[�`�����J�n���A���̎Q�Ƃ�ۑ�����
        activeCoroutine = StartCoroutine(Simple1());
    }






//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //�e�L�X�g����������v���O����

    //�e�L�X�g���ꕶ�����\������R�[�h
    private IEnumerator Simple1()
    {
        TextONFlag = true;
        RadioText2.maxVisibleCharacters = 0;

        for (var i = 0; i < RadioText2.text.Length; i++)
        {
            //�����̒l�ύX����ƕb���ύX�\
            yield return new WaitForSeconds(0.06f);
            RadioText2.maxVisibleCharacters = i + 1;
        }

        //1�l�ڂ̋~���̎��݂̂̓���
        if(FirstTextFlag)
        {
            yield return new WaitForSeconds(0.5f);
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
            }
            //��ԍŏ��ɏd���҂��~�����Ƃ��̃Z���t���X�^�b�N����|�b�v����
            stackBring.Pop();
            stackBring.Push("�܂��d���҂�����悤���I�������������𗊂ށI");
            NewText = stackObj.Pop();
            RadioText.SetText(NewText);
            activeCoroutine = StartCoroutine(Simple1());
            FirstTextFlag = false;
        }

        //�e�L�X�g�𐔕b��ɃI�t�ɂ���
        yield return new WaitForSeconds(1.5f);
        TextPanelOFF();
        TextONFlag = false;

        //�����R���v�X�Q�[�W�̖����̏��ԑ҂�������������s����
        if(CollapseDialogueFlag)
        {
            yield return new WaitForSeconds(2.0f);
            CollapseDialogue();
        }
    }


    //�e�L�X�g���\���ɂ���
    private void TextPanelOFF()
    {
        ChatPanel.SetActive(false);
        ChatPanel1.SetActive(false);
        ChatPanel2.SetActive(false);
        ChatPanel3.SetActive(false);
        ChatPanel4.SetActive(false);
        ChatR.SetActive(false);
    }


    //�e�L�X�g��\������
    public void TextPanelON()
    {
        number1 = PlayerPrefs.GetInt("R_number");
        if(CollapseIconFlag)
        {
            ChatPanel.SetActive(true);
            CollapseIconFlag = false;
        }
        else if(number1 == 1)
        {
            ChatPanel1.SetActive(true);
        }
        else if(number1 == 2)
        {
            ChatPanel2.SetActive(true);
        }
        else if(number1 == 3)
        {
            ChatPanel3.SetActive(true);
        }
        else if(number1 == 4)
        {
            ChatPanel4.SetActive(true);
        }
        ChatR.SetActive(true);
    }
}
