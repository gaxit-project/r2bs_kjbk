using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIFE : MonoBehaviour
{
    #region �錾
    // �v���C���[��HP
    public static int HitPoint = 3;

    // LIFE_Bar �̎Q��
    public LIFE_Bar BarHP;

    // �A�j���[�^�[�R���|�[�l���g
    private Animator Anim;

    // ���G���Ԋ֘A�̕ϐ�
    public float invincibilityDuration = 5.0f; // ���G���ԁi�b�j
    private float invincibilityTimer = 0.0f;   // �o�ߎ��Ԃ��i�[����^�C�}�[�ϐ�
    private bool isInvincible = false;         // ���G��ԃt���O

    // �_�Ŋ֘A�̕ϐ�
    bool flag = true;
    [SerializeField] private Renderer renderComponent1;
    [SerializeField] private Renderer renderComponent2;
    [SerializeField] private Renderer renderComponent3;
    bool Fadered = false;
    #endregion

    #region ������
    void Start()
    {
        // HP ��������
        HitPoint = 3;
        // �A�j���[�^�[�R���|�[�l���g���擾
        Anim = GetComponent<Animator>();
    }
    #endregion

    #region �X�V����
    void Update()
    {
        if (isInvincible)
        {
            // ���G��Ԃ̏���
            invincibilityTimer += Time.deltaTime;

            if (invincibilityTimer >= invincibilityDuration)
            {
                // ���G��Ԃ��I��
                isInvincible = false;
                invincibilityTimer = 0.0f;
            }
        }
    }
    #endregion

    #region �R���W��������
    private void OnCollisionEnter(Collision Hit)
    {
        if (Hit.gameObject.tag == "Blaze")
        {
            if (isInvincible)
            {
                return;
            }
            else
            {
                Audio.GetInstance().PlaySound(8);  // �_���[�W���󂯂������Đ�
                // HP �����炷
                HitPoint--;
                // �X�^�[��������Ƃ��̏���
                GetStar();
                // HP�o�[���X�V
                BarHP.HPBar();

                // �_�ŏ������J�n
                StartCoroutine(Blink());

                if (HitPoint <= 0)
                {
                    Fadered = true;
                    Audio.GetInstance().PlaySound(9);  // �_���[�W�Ŏ��񂾂Ƃ��̐����Đ�
                    // HP ���s�����ꍇ�̏���
                    Anim.SetBool("CarryWalk", false);
                    Anim.SetBool("Carry", false);
                    PlayerPrefs.SetString("Result", "GAMEOVER");
                    Scene.Instance.GameResult();
                }
            }
        }
    }
    #endregion

    #region ���G��ԏ���
    public void GetStar()
    {
        // �X�^�[��������Ƃ��ɖ��G��ԃt���O�� True �ɂ���
        isInvincible = true;
    }
    #endregion

    #region ���񂾂�t�F�[�h�A�E�g���
    public bool getred()
    {
        return Fadered;
    }
    #endregion

    #region �_�ŏ���
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
    #endregion
}
