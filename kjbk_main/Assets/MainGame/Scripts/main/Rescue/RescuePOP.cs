using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePOP : MonoBehaviour
{
    #region �ϐ��̐錾

    // RescuePOP�I�u�W�F�N�g�̎Q��
    public RescuePOP Pop;

    // �d���җp�̃Q�[���I�u�W�F�N�g
    [SerializeField] GameObject RBalcony;
    [SerializeField] GameObject RKitchen;
    [SerializeField] GameObject RBath;
    [SerializeField] GameObject RCloset;
    [SerializeField] GameObject RBedRoom;

    // �y�ǎҗp�̃Q�[���I�u�W�F�N�g
    [SerializeField] GameObject hito1st;
    [SerializeField] GameObject JK1st;
    [SerializeField] GameObject kurohuku1st;
    [SerializeField] GameObject ILOVENY1st;
    [SerializeField] GameObject hito1_st;
    [SerializeField] GameObject hito2nd;
    [SerializeField] GameObject JK2nd;
    [SerializeField] GameObject kurohuku2nd;
    [SerializeField] GameObject hito3rd;
    [SerializeField] GameObject JK3rd;
    [SerializeField] GameObject hito3_2rd;
    [SerializeField] GameObject kurohuku4th;
    [SerializeField] GameObject ILOVENY4th;
    [SerializeField] GameObject hito4th;
    [SerializeField] GameObject JK5th;
    [SerializeField] GameObject kurohuku5th;
    [SerializeField] GameObject ILOVENY5th;

    // �|�b�v�A�b�v�p�̃Q�[���I�u�W�F�N�g
    [SerializeField] GameObject FirstPop;
    [SerializeField] GameObject SecondPop;
    [SerializeField] GameObject ThirdPop;
    [SerializeField] GameObject ForthPop;
    [SerializeField] GameObject FifthPop;

    // �����_���Ȓl��y�ǎ҂̐l�����i�[����ϐ�
    [HideInInspector] public int Rnd = 0;
    [HideInInspector] public int MCnt = -1;
    int a = 0;

    // ��ԊǗ��p�̃t���O
    bool First = false;
    bool RndomONOFF = true;

    bool R1 = true;
    bool R2 = true;
    bool R3 = true;
    bool R4 = true;
    bool R5 = true;

    int cnt = 1;

    [HideInInspector] public bool ArrowONFlag = false;

    public int rndom;

    // �y�ǎҐ�
    public int AllRCnt = 3;

    // Radio_ver4�̎Q��
    public Radio_ver4 Radio4;

    #endregion

    #region �y�ǎ҂��~�������̏���
    // �y�ǎ҂��~�����Ƃ��ɌĂяo���֐�
    public void LightR()
    {
        MCnter();  // �~�����y�ǎ҂��J�E���g����֐�
    }
    #endregion

    #region �d���҂��~�������̏���
    // �d���҂��~�����Ƃ��ɌĂяo���֐�
    public void HeavyR()
    {
        Radio4.BringDialogue(); // �_�C�A���O��\��
        RndomONOFF = true;      // �����_��������L����
        Rndom();                // �����_���ɐ��l�𐶐�
        Rpop();                 // �V�����d���҂��|�b�v�A�b�v
        ArrowONFlag = false;
    }
    #endregion

    #region �y�ǎҐ��̃J�E���g
    // �~�����y�ǎ҂̐l�����J�E���g
    public int MCnter()
    {
        MCnt++;
        return MCnt;
    }
    #endregion

    #region �����_������
    // �����_���̐�������֐�
    public int Rndom()
    {
        while (RndomONOFF)
        {
            if (!R1 && !R2 && !R3 && !R4 && !R5)  // �S���~�����ꂽ�ꍇ
            {
                break;
            }
            else
            {
                Rnd = Random.Range(1, 6);  // 1�`5�̒l�̒��Ń����_����1����

                // �����_���l�̏d���`�F�b�N
                if ((Rnd == 1 && R1) || (Rnd == 2 && R2) || (Rnd == 3 && R3) || (Rnd == 4 && R4) || (Rnd == 5 && R5))
                {
                    switch (Rnd)
                    {
                        case 1: R1 = false; break;
                        case 2: R2 = false; break;
                        case 3: R3 = false; break;
                        case 4: R4 = false; break;
                        case 5: R5 = false; break;
                    }
                    RndomONOFF = false;
                    break;
                }
            }
        }
        return Rnd;
    }
    #endregion

    #region �V���ȏd���҂̐ݒu
    // �V���ȏd���҂̐ݒu�{�d���҃q���g���X�^�b�N�փv�b�V��
    public void Rpop()
    {
        // �����_���̐��l���󂯎��
        rndom = Pop.Rnd;

        switch (rndom)
        {
            case 1:
                RBalcony.SetActive(true);
                Radio4.Push();
                break;
            case 2:
                RKitchen.SetActive(true);
                Radio4.Push();
                break;
            case 3:
                RBath.SetActive(true);
                Radio4.Push();
                break;
            case 4:
                RCloset.SetActive(true);
                Radio4.Push();
                break;
            case 5:
                RBedRoom.SetActive(true);
                Radio4.Push();
                break;
        }
    }
    #endregion

    #region �V���Ȍy�ǎ҂̐ݒu
    // �V���Ȍy�ǎ҂̐ݒu
    public void PopR()
    {
        switch (cnt)
        {
            case 1:
                FirstPop.SetActive(true);
                AllRCnt += 5;
                break;
            case 2:
                SecondPop.SetActive(true);
                AllRCnt += 4;
                break;
            case 3:
                ThirdPop.SetActive(true);
                AllRCnt += 5;
                break;
            case 4:
                ForthPop.SetActive(true);
                AllRCnt += 4;
                break;
            case 5:
                FifthPop.SetActive(true);
                AllRCnt += 3;
                break;
        }
        cnt++;
    }
    #endregion
}
