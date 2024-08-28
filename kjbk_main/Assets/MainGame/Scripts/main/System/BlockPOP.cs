using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPOP : MonoBehaviour
{

    //��Q���̐錾
    [SerializeField] GameObject Corridor1;
    [SerializeField] GameObject Corridor2;
    [SerializeField] GameObject Corridor3;
    [SerializeField] GameObject Corridor4;
    [SerializeField] GameObject Wall1;
    [SerializeField] GameObject Wall2;

    //�|��Q�[�W�̃t���O
    [HideInInspector] public bool Generate40 = false;
    [HideInInspector] public bool Generate20 = false;
    [HideInInspector] public bool Generate10 = false;
    [HideInInspector] public bool Judge = true;


    // Start is called before the first frame update
    void Start()
    {
        Corridor1.SetActive(false);
        Corridor2.SetActive(false);
        Corridor3.SetActive(false);
        Corridor4.SetActive(false);
        Wall1.SetActive(true);
        Wall2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //��Q�������������ꏊ�ɒN�����Ȃ��Ƃ��ɏ�Q����ݒu
        if(Judge)
        {
            //�R���v�X�Q�[�W��40%�̂Ƃ��ɏ�Q���ݒu
            if (Generate40)
            {
                Corridor1.SetActive(true);
                Generate40 = false;
            }
            //�R���v�X�Q�[�W��20%�̂Ƃ��ɏ�Q���ݒu
            else if (Generate20)
            {
                Corridor2.SetActive(true);
                Corridor3.SetActive(true);
                Wall1.SetActive(false);
                Generate20 = false;
            }
            //�R���v�X�Q�[�W��10%�̂Ƃ��ɏ�Q���ݒu
            else if (Generate10)
            {
                Corridor4.SetActive(true);
                Wall2.SetActive(false);
                Generate10 = false;
            }
        } 
    }

    //��Q�����N���ꏊ�ɐl��������t���O���I�t�ɂ���
    public void OnCollisionEnter(Collision Hit2)
    {
        if(Hit2.gameObject.tag == "Player" || Hit2.gameObject.tag == "Minorlnjuries")
        {
            Judge = false;
            Debug.Log("��Q����POP�ł��Ȃ���" + Judge);
        }
    }


    //��Q�����N���ꏊ����l�����ꂽ��t���O���I���ɂ���
    public void OnCollisionExit(Collision Hit2)
    {
        if (Hit2.gameObject.tag == "Player" || Hit2.gameObject.tag == "Minorlnjuries")
        {
            Judge = true;
            Debug.Log("��Q����POP�ł����" + Judge);
        }
    }
}
