using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePOP : MonoBehaviour
{
    // Start is called before the first frame update

    public CollRadio Radio;
    public RescuePOP Pop;

    [SerializeField] GameObject RBalcony;
    [SerializeField] GameObject RKitchen;
    [SerializeField] GameObject RBath;
    [SerializeField] GameObject RCloset;
    [SerializeField] GameObject RBedRoom;

    [HideInInspector] public int Rnd = 0;

    [HideInInspector] public int MCnt = -1;
    int a = 0;

    bool First = false;

    void Start()
    {
        RBalcony.SetActive(false);
        RKitchen.SetActive(false);
        RBath.SetActive(false);
        RCloset.SetActive(false);
        RBedRoom.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //�y�ǎ҂��~�������Ƃɂ���J���L�[
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MCnter();
            Radio.RHint();
        }
        //�d���҂��~�������Ƃɂ���J���L�[
        if (Input.GetKeyDown(KeyCode.C))
        {
            Rndom();
            Rpop();
            if (First)
            {
                Radio.RPeople = false;
            }
            First = true;

            if (!Radio.RPeople2)
            {
                Radio.RPeople = true;
            }
            Radio.RPeople2 = true;

            Debug.Log("�d���҂��~�����F" + Radio.RPeople);
        }
    }
    public int MCnter()
    {
        MCnt++;
        Debug.Log("�y�ǎ҂̋~���l��:" + MCnt);
        PlayerPrefs.SetInt("�y�ǎ҂̋~���l��", MCnt);
        return MCnt;
    }


    public int Rndom() //�����_���̐�������֐�
    {
        Rnd = Random.Range(1, 6);   //1�`5�܂ł̐��������_���ɓ����
        Debug.Log("�����_���ɓ��ꂽ��:" + Rnd);
        PlayerPrefs.SetInt("�d���Ҕԍ�", Rnd);
        return Rnd;
    }
    //�d���҂��~�����炱����N������
    public void Rpop()
    {
        int rndom = PlayerPrefs.GetInt("�d���Ҕԍ�");

        if (Rnd == 1)
        {
            RBalcony.SetActive(true);
        }
        else if (Rnd == 2)
        {
            RKitchen.SetActive(true);
        }
        else if (Rnd == 3)
        {
            RBath.SetActive(true);
        }
        else if (Rnd == 4)
        {
            RCloset.SetActive(true);
        }
        else if (Rnd == 5)
        {
            RBedRoom.SetActive(true);
        }
    }
}
