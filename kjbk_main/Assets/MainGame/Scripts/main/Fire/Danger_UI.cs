using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Danger_UI : MonoBehaviour
{
    #region �ϐ���`
    [SerializeField] public GameObject Player;  // �v���C���[�I�u�W�F�N�g
    private Danger_Ray ray;  // Danger_Ray�R���|�[�l���g

    public GameObject Up;    // �������UI
    public GameObject Under; // ��������UI
    public GameObject Left;  // ��������UI
    public GameObject Right; // �E������UI

    private CanvasGroup UpImage;    // �������CanvasGroup
    private CanvasGroup UnderImage; // ��������CanvasGroup
    private CanvasGroup LeftImage;  // ��������CanvasGroup
    private CanvasGroup RightImage; // �E������CanvasGroup

    private double _time;  // �o�ߎ���
    #endregion

    #region ������
    // Start is called before the first frame update
    void Start()
    {
        ray = Player.GetComponent<Danger_Ray>();

        UpImage = Up.GetComponent<CanvasGroup>();
        UnderImage = Under.GetComponent<CanvasGroup>();
        LeftImage = Left.GetComponent<CanvasGroup>();
        RightImage = Right.GetComponent<CanvasGroup>();

        // �eUI�̓����x��0�ɐݒ�
        UpImage.alpha = 0;
        UnderImage.alpha = 0;
        LeftImage.alpha = 0;
        RightImage.alpha = 0;
    }
    #endregion

    #region �X�V����
    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime; // �o�ߎ��Ԃ��X�V

        // ������̊댯����
        if (ray.Up)
        {
            var Upalpha = Mathf.Abs(Mathf.Clamp(ray.ZpDistance, 0, 1) - 1);
            UpImage.alpha = Upalpha;
        }
        else
        {
            UpImage.alpha = 0;
        }

        // �������̊댯����
        if (ray.Under)
        {
            float Underalpha = Mathf.Abs(Mathf.Clamp(ray.ZmDistance, 0, 1) - 1);
            UnderImage.alpha = Underalpha;
        }
        else
        {
            UnderImage.alpha = 0;
        }

        // �E�����̊댯����
        if (ray.Right)
        {
            float Rightalpha = Mathf.Abs(Mathf.Clamp(ray.XpDistance, 0, 1) - 1);
            RightImage.alpha = Rightalpha;
        }
        else
        {
            RightImage.alpha = 0;
        }

        // �������̊댯����
        if (ray.Left)
        {
            float Leftalpha = Mathf.Abs(Mathf.Clamp(ray.XmDistance, 0, 1) - 1);
            LeftImage.alpha = Leftalpha;
        }
        else
        {
            LeftImage.alpha = 0;
        }
    }
    #endregion
}
