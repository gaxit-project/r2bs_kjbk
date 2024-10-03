using UnityEngine;

[ExecuteInEditMode]
public class SmokeEffect : MonoBehaviour
{
    // ���̃G�t�F�N�g��K�p����}�e���A��
    public Material smokeMaterial;

    // ���̔Z�x�𒲐����邽�߂̕ϐ��i0����1�͈̔͂Œ����\�j
    [Range(0, 1)]
    public float Alphe = 1f;

    // ���̔Z�x�𒲐����邽�߂̕ϐ��i0����1�͈̔͂Œ����\�j
    [Range(0, 1)]
    public float smoke = 1f;

    // �X���[�Y�ȑJ�ڂ̂��߂̑��x�W��
    public float transitionSpeed = 2f;

    float target;

    float sm;

    private void Start()
    {
        Alphe = 1;
        smoke = 1;
        target = 1;
    }

    void Update()
    {

        if(1 == PlayerPrefs.GetInt("InRoom"))
        {
            // target �� PlayerPrefs ����擾
            target = PlayerPrefs.GetFloat("SmokeConc");
        }
        else
        {
            target = 1;
        }
        Debug.Log(target + "target");
        if (target != smoke)
        {
            // Mathf.Lerp ���g�p���� smoke �� target �ɃX���[�Y�ɑJ�ڂ�����
            smoke = Mathf.Lerp(smoke, target, Time.deltaTime * transitionSpeed);
        }


        // �l���͈͊O�ɂȂ�Ȃ��悤�ɃN�����v
        smoke = Mathf.Clamp(smoke, 0f, 1f);
        Alphe = Mathf.Clamp(Alphe, 0f, 1f);

        // �}�e���A�����w�肳��Ă���ꍇ�A�G�t�F�N�g��K�p
        if (smokeMaterial != null)
        {
            // �}�e���A���ɉ��̔Z�x��ݒ�
            smokeMaterial.SetFloat("_Alphe", Alphe);

            // �}�e���A���ɉ��̐F��ݒ�
            smokeMaterial.SetFloat("_CenterRadius", smoke);
        }
    }
}
