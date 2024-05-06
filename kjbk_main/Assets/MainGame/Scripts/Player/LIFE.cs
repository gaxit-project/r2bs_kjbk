using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;


public class LIFE : MonoBehaviour
{
    int HitPoint = 3;            //�v���C���[��HP

    public Collider_OnOff FireColOff;  //Collider_On_Off����FireOff�������Ă���
    public Collider_OnOff FireColOn;   //Collider_On_Off����FireOn�������Ă���

    public LIFE_Bar BarHP;

    private Animator animator;




    //���G���Ԋ֘A�̎҂���
    public float invincibilityDuration = 10.0f; // ���G���ԁi�b�j
    private float invincibilityTimer = 0.0f;   // �o�ߎ��Ԃ��i�[����^�C�}�[�ϐ�(�����l0�b)
    private bool isInvincible = false;         // ���G��Ԃ��ǂ����̃t���O


    //�_�Ŋ֘A�̎҂���
    bool flag = true;
    [SerializeField] private Renderer renderComponent1;
    [SerializeField] private Renderer renderComponent2;
    [SerializeField] private Renderer renderComponent3;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (HitPoint <= 0)            //����HP���s������ȉ��̏������s��
        {
            Debug.Log("�Q�[���I�[�o�[");
            //Scene.instance.GameOver();         //�Q�[���I�[�o�[�ɔ�΂�
        }
    }

    public void GetStar()
    {
        //�X�^�[��������Ƃ��ɖ��G��ԃt���O��True�ɂ���
        isInvincible = true;
    }

    // Update is called once per frame
    public void Muteki2()
    {
        if (isInvincible)
        {
            //�����ɖ��G��Ԃ̂Ƃ��̏���������
            Debug.Log("���G���");

            //���t���[���^�C�}�[�ϐ���Time.deltaTime�𑫂�
            invincibilityTimer += Time.deltaTime;

            //�^�C�}�[�����G����(10�b)�𒴂����Ƃ�
            if (invincibilityTimer >= invincibilityDuration)
            {
                Debug.Log("���G��ԏI���");

                //���G��ԃt���O��False�ɂ���
                flag = false;
                //�^�C�}�[��0.0�b�Ƀ��Z�b�g����
                invincibilityTimer = 0.0f;
            }
        }
    }

    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="Hit"></param>
    //�G�ꂽ�u��HP��1���炷
    private void OnCollisionEnter(Collision Hit)
    {
        if (Hit.gameObject.tag == "Blaze")
        {
            StartCoroutine(Blink());     //�_�ŊJ�n
            //animator.SetBool("FallDown", true);
            Muteki2();            //���G�t�^(�����Ӗ��Ȃ��I�I)
            HitPoint--;                  //HP�����炷
            BarHP.HPBar();
            FireColOff.FireOff();        //���̃R���C�_�[���I�t��
            FireColOn.FireOn();          //���̃R���C�_�[���I����

            Debug.Log("HP=" + HitPoint);
        }
    }


    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <returns></returns>
    //�����������̓_��
    IEnumerator Blink()
    {
        if (flag)
        {
            for (int i = 0; i < 24; i++)
            {
                flag = false;
                renderComponent1.enabled = !renderComponent1.enabled;
                renderComponent2.enabled = !renderComponent2.enabled;
                renderComponent3.enabled = !renderComponent3.enabled;
                yield return new WaitForSeconds(0.1f);
                renderComponent1.enabled = !renderComponent1.enabled;
                renderComponent2.enabled = !renderComponent2.enabled;
                renderComponent3.enabled = !renderComponent3.enabled;
                yield return new WaitForSeconds(0.1f);
            }
            flag = true;

        }
    }
}
