using System.Collections;
using UnityEngine;

public class FireLv : MonoBehaviour
{
    #region �ϐ���`
    private float LvUpSecond;   // ���x���㏸�Ԋu(�b)
    private float LvUpProbability;   // ���x���㏸�̊m��
    private float LvUpSize;   // ���x���㏸���̃G�t�F�N�g�T�C�Y�̑��吔�l
    private float Size = 1;   // �G�t�F�N�g�̃T�C�Y

    public int FireLvel;   // ���x��
    #endregion

    #region �Q�ƃI�u�W�F�N�g
    private GameObject Blaze;   // Blaze�I�u�W�F�N�g
    private Blaze_Maneger m_Blaze;   // Blaze_Maneger�̎Q��
    Transform BlazePos;   // Blaze�̈ʒu
    Vector3 pos;   // Blaze�̈ʒu�x�N�g��
    #endregion

    #region ������
    private void Awake()
    {
        Blaze = GameObject.Find("BlazeManeger");
        m_Blaze = Blaze.GetComponent<Blaze_Maneger>();
        var Data = m_Blaze.getLvData();
        LvUpSecond = Data.Second;
        LvUpProbability = Data.Probability;
        LvUpSize = Data.Size;
    }

    void Start()
    {
        FireLvel = 1;
        this.transform.localScale = new Vector3(Size, Size, Size);
        BlazePos = this.transform;
        pos = BlazePos.position;
        pos.y = 2.2f;
        BlazePos.position = pos;
        StartCoroutine("LvUp");
    }
    #endregion

    #region �R���[�`��
    IEnumerator LvUp()
    {
        float PreSize = Size;
        while (true)
        {
            if (FireLvel == 3) break;   // ���x����3�Œ�~
            yield return new WaitForSeconds(LvUpSecond);

            int probability = Random.Range(0, 100);
            if (probability < LvUpProbability)
            {
                PreSize += LvUpSize;
                this.transform.localScale = new Vector3(PreSize, PreSize, PreSize);
                FireLvel++;
                pos.y = 4.2f;
                BlazePos.position = pos;
            }
        }
    }
    #endregion
}
