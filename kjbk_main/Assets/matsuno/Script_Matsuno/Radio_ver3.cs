using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Radio_ver3 : MonoBehaviour
{
    [SerializeField] GameObject ChatPanel;
    [SerializeField] GameObject ChatPanel1;
    [SerializeField] GameObject ChatPanel2;
    [SerializeField] GameObject ChatPanel3;

    [HideInInspector] public bool JorE = true;
    [HideInInspector] public bool SwitchONOFF = true;

    //�����̃t���O
    [HideInInspector] public bool RPeople = true;
    [HideInInspector] public bool RPeople2 = true;

    [HideInInspector] public bool CollapseRadio = false;
    [HideInInspector] public bool RHintFlag = false;
    [HideInInspector] public bool RPopFlag = false;
    [HideInInspector] public bool FirstFlag = true;


    public CollGauge CG2;

    [SerializeField] private TMP_Text RadioText;

    public RescuePOP RPOP;


    //�������o���Ƃ��Ƃ��܂��Ƃ��̎���
    float StartTimer = 15f;   //�����t����Ƃ��̃^�C�}�[
    float EndTimer = 10f;     //����������Ƃ��̃^�C�}�[


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayCoroutine());
        ChatPanel.SetActive(false);
        ChatPanel1.SetActive(false);
        ChatPanel2.SetActive(false);
        ChatPanel3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(FirstFlag)
        {
            ChatPanel.SetActive(true);
            RadioText.SetText("����̌���͊w�������I�s���s���҂̔������~���̂��N�̔C����");
            StartCoroutine(Simple());

            Invoke(nameof(FirstRadio1),7.5f);
            Invoke(nameof(FirstRadio2), 11f);
            Invoke(nameof(RadioOFF), StartTimer);
            FirstFlag = false;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (SwitchONOFF)
            {
                Debug.Log("�p�ꉻ");
                JorE = false;
                SwitchONOFF = false;
            }
            else
            {
                Debug.Log("���{�ꉻ");
                JorE = true;
                SwitchONOFF = true;
            }

        }
    }

    public void FirstRadio1()
    {
        RadioText.SetText("�d���҂�����������Ƃ̏��");
        StartCoroutine(Simple());
    }
    public void FirstRadio2()
    {
        RadioText.SetText("�y�ǎ҂��~���Ȃ�������W�߂Ă���");
        StartCoroutine(Simple());
    }
    public void RadioStoper()
    {
        Debug.Log("Radio");
        if (CollapseRadio)
        {
            CollapsePanel();
            StartCoroutine(Simple());
            RadioON();
            Invoke(nameof(RadioOFF), EndTimer);
            CollapseRadio = false;
        }
        else if (RHintFlag)
        {
            RHintStop();
            RHintFlag = false;
        }
        else if (RPopFlag)
        {
            SymbolStop();
            RPopFlag = false;
        }
    }

    public void RadioON()
    {
        ChatPanel.SetActive(true);          //�����̃f�U�C����\��
    }
    public void RadioOFF()
    {
        ChatPanel.SetActive(false);          //�����̃f�U�C����\��
    }
    public void Radio1OFF()
    {
        ChatPanel1.SetActive(false);          //�����̃f�U�C����\��
    }
    public void Radio2OFF()
    {
        ChatPanel2.SetActive(false);          //�����̃f�U�C����\��
    }
    public void Radio3OFF()
    {
        ChatPanel3.SetActive(false);          //�����̃f�U�C����\��
    }

    public int RCnt(int mcnt)
    {
        return mcnt;
    }

    //�d���҂̖������Ǘ�
    public void SymbolStop()
    {
        SymbolR();
        StartCoroutine(Simple());
        RadioON();
        Invoke(nameof(RadioOFF), EndTimer);
    }

    //�y�ǎ҂̖������Ǘ�
    public void RHintStop()
    {
        RHint();
        StartCoroutine(Simple());
        RadioON();
        Invoke(nameof(RadioOFF), EndTimer);
    }

    //�|��Q�[�W�Ɋւ��閳�����Ǘ��������
    public void CollapsePanel()
    {
        Debug.Log("��������");
        if (JorE)
        {
            Debug.Log("��������2");
            if (CG2.Radio80)
            {
                RadioText.SetText("���������Ƀq�r�������Ă��Ȃ����H");
                CG2.Radio80 = false;
            }
            else if (CG2.Radio60)
            {
                RadioText.SetText("�q�r���g�債�Ă�����������������邼");
                CG2.Radio60 = false;
            }
            else if (CG2.Radio40)
            {
                RadioText.SetText("�ǂ�����n�߂Ă���\r\n�撣���Ă���");
                CG2.Radio40 = false;
            }
            else if (CG2.Radio20)
            {
                RadioText.SetText("�V�䂪����n�߂Ă邼\r\n�}���ł���");
                CG2.Radio20 = false;
            }
            else if (CG2.Radio10)
            {
                RadioText.SetText("�|�󐡑O����\r\n����������");
                CG2.Radio10 = false;
            }
        }
        else
        {
            if (CG2.Radio80)
            {
                RadioText.SetText("Hello80");
                CG2.Radio80 = false;
            }
            else if (CG2.Radio60)
            {
                RadioText.SetText("Hello60");
                CG2.Radio60 = false;
            }
            else if (CG2.Radio40)
            {
                RadioText.SetText("Hello40");
                CG2.Radio40 = false;
            }
            else if (CG2.Radio20)
            {
                RadioText.SetText("Hello20");
                CG2.Radio20 = false;
            }
            else if (CG2.Radio10)
            {
                RadioText.SetText("Hello10");
                CG2.Radio10 = false;
            }
        }
    }

    

    //�d���҂̖���
    public void SymbolR()
    {
        int rnd = RPOP.Rnd;
        if (JorE)
        {
            if (rnd == 0)
            {
                RadioText.SetText("���ɂ��d���҂�����Ƃ̏�񂾁I���}�T���Ă���I");
            }
            else if (rnd == 1)
            {
                RadioText.SetText("���ɂ��d���҂�����Ƃ̏�񂾁I���}�T���Ă���I");
            }
            else if (rnd == 2)
            {
                RadioText.SetText("���ɂ��d���҂�����Ƃ̏�񂾁I���}�T���Ă���I");
            }
            else if (rnd == 3)
            {
                RadioText.SetText("���ɂ��d���҂�����Ƃ̏�񂾁I���}�T���Ă���I");
            }
            else if (rnd == 4)
            {
                RadioText.SetText("���ɂ��d���҂�����Ƃ̏�񂾁I���}�T���Ă���I");
            }
            else if (rnd == 5)
            {
                RadioText.SetText("���ɂ��d���҂�����Ƃ̏�񂾁I���}�T���Ă���I");
            }
        }
        else if(!JorE)
        {
            if (rnd == 0)
            {
                RadioText.SetText("�|�󐡑O����\r\n����������");
            }
            else if (rnd == 1)
            {
                RadioText.SetText("�|�󐡑O����\r\n����������");
            }
            else if (rnd == 2)
            {
                RadioText.SetText("�|�󐡑O����\r\n����������");
            }
            else if (rnd == 3)
            {
                RadioText.SetText("�|�󐡑O����\r\n����������");
            }
            else if (rnd == 4)
            {
                RadioText.SetText("�|�󐡑O����\r\n����������");
            }
            else if (rnd == 5)
            {
                RadioText.SetText("�|�󐡑O����\r\n����������");
            }
        }
        

    }

    //�y�ǎ҂̖����֘A
    public void RHint()
    {
        int Cnt = 0;
        int RCnt = RPOP.MCnt;
        int rnd = RPOP.Rnd;
        Debug.Log("�󂯎�����d���Ҕԍ�:" + rnd);
        Debug.Log("�󂯎�����y�ǎ�:" + RCnt);


        if (RPeople2)
        {
            if (RPeople)
            {
                if (RCnt == 0)
                {
                    if (rnd == 0)
                    {
                        RadioText.SetText("�L�b�`�����琺�����������Ƃ̏�񂾁I���}�������Ă���I");
                        Debug.Log("��l�ڂ̈ʒu�m��");
                        //�����\��
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (rnd == 1)
                    {
                        if (RCnt % 3 == 1)
                        {
                            RadioText.SetText("���̕��ɐl�������Ă������Ƃ̏��");
                            Debug.Log("1-1");
                        }
                        else if (RCnt % 3 == 2)
                        {
                            RadioText.SetText("������ʂɐl�e����������������Ȃ��ꉞ�������Ă���Ȃ���");
                            Debug.Log("1-2");
                        }
                        else if (RCnt % 3 == 0)
                        {
                            RadioText.SetText("�o���R�j�[�Ől���|��Ă���Ƃ̏��");
                            Debug.Log("1-3");
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 2)
                    {
                        if (RCnt % 3 == 1)
                        {
                            RadioText.SetText("�k���ɐl���������Ă������Ƃ̏�񂪓�����");
                            Debug.Log("2-1");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            RadioText.SetText("�k�������狩�ѐ���������������������Ă���");
                            Debug.Log("2-2");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            RadioText.SetText("���r���O�Ől���|��Ă���Ƃ̏��");
                            Debug.Log("2-3");
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 3)
                    {
                        if (RCnt % 3 == 1)
                        {
                            RadioText.SetText("�k���ɐl���������Ă������Ƃ̏�񂪓�����");
                            Debug.Log("3-1");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            RadioText.SetText("���͂��̎��Ԃ悭�����C�ɓ�����...���A������t�����܂܂�����...");
                            Debug.Log("3-2");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            RadioText.SetText("�����C��ɂĊ����ē����Ȃ��l������Ƃ̏��");
                            Debug.Log("3-3");
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 4)
                    {
                        if (RCnt % 3 == 1)
                        {
                            RadioText.SetText("�쑤�ɐl���������Ă������Ƃ̏�񂪓�����");
                            Debug.Log("4-1");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            RadioText.SetText("���̕������܂݂�ł������낻�뒅�ւ��������I");
                            Debug.Log("4-2");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            RadioText.SetText("�N���[�[�b�g�ɂĕ����|��ē����Ȃ��l������Ƃ̏��");
                            Debug.Log("4-3");
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 5)
                    {
                        if (RCnt % 3 == 1)
                        {
                            RadioText.SetText("�����ɑ����ē�����l�������Ƃ̏��");
                            Debug.Log("5-1");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            RadioText.SetText("�܂�������ȏ󋵂ŐQ�Ă��͂��Ȃ����...");
                            Debug.Log("5-2");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            RadioText.SetText("�Q���őS�R�N���Ȃ��l������݂������I�}���ŋN�����ɍs���Ă���");
                            Debug.Log("5-3");
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                }
            }
        }

        if (RCnt % 3 == 0)
        {
            RPeople = true;
            Debug.Log("�~���҃t���O�F" + RPeople);
        }
    }

    public void RMessage()
    {
        RadioText.SetText("���Ȃ��͖��̉��l��I���肪�Ƃ��I");
        StartCoroutine(Simple());
        ChatPanel1.SetActive(true);
        Invoke(nameof(Radio1OFF), EndTimer);
    }
    public void RMessage1()
    {
        RadioText.SetText("�O�̋�C���߂��I�I");
        StartCoroutine(Simple());
        ChatPanel2.SetActive(true);
        Invoke(nameof(Radio2OFF), EndTimer);
    }
    public void RMessage2()
    {
        RadioText.SetText("����������I���肪�Ƃ��I");
        StartCoroutine(Simple());
        ChatPanel3.SetActive(true);
        Invoke(nameof(Radio3OFF), EndTimer);
    }



    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �����������Ƃ��Ɏg���v���O����(���Ƃ���z��ɕς���)
    /// 
    private IEnumerator Simple()
    {
        RadioText.maxVisibleCharacters = 0;

        for(var i = 0; i < RadioText.text.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            RadioText.maxVisibleCharacters = i + 1;
        }
    }
    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(7.5f);
    }
}




    