using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger_Ray : MonoBehaviour
{
    #region �ϐ���`
    [SerializeField] private float reacDistance = 0.45f;  // ���A�N�V�����̔�������

    private Vector3 Xp;  // �E�����̃x�N�g��
    private Vector3 Zp;  // �O�����̃x�N�g��
    private Vector3 Xm;  // �������̃x�N�g��
    private Vector3 Zm;  // ������̃x�N�g��

    private Ray rayXp;  // �E������Ray
    private Ray rayZp;  // �O������Ray
    private Ray rayXm;  // ��������Ray
    private Ray rayZm;  // �������Ray

    private RaycastHit XpHit;  // �E�����̃q�b�g���
    private RaycastHit ZpHit;  // �O�����̃q�b�g���
    private RaycastHit XmHit;  // �������̃q�b�g���
    private RaycastHit ZmHit;  // ������̃q�b�g���

    [System.NonSerialized] public bool Up = false;    // �O�����̊댯���
    [System.NonSerialized] public bool Under = false; // ������̊댯���
    [System.NonSerialized] public bool Left = false;  // �������̊댯���
    [System.NonSerialized] public bool Right = false; // �E�����̊댯���

    [System.NonSerialized] public float XpDistance = 100;  // �E�����̋���
    [System.NonSerialized] public float ZpDistance = 100;  // �O�����̋���
    [System.NonSerialized] public float XmDistance = 100;  // �������̋���
    [System.NonSerialized] public float ZmDistance = 100;  // ������̋���
    #endregion

    #region ����������
    // Start is called before the first frame update
    void Start()
    {
        // �����������͂���܂��񂪁A�K�v�ɉ����Ă����ɒǉ��ł��܂�
    }
    #endregion

    #region Ray�̐ݒ�
    // �e������Ray�̐ݒ���s��
    private void SetRayDirections()
    {
        Xp = Vector3.right;
        Zp = Vector3.forward;
        Xm = Vector3.left;
        Zm = Vector3.back;

        Vector3 t = new Vector3(this.transform.position.x, 5f, this.transform.position.z);

        rayXp = new Ray(t, Xp);
        rayZp = new Ray(t, Zp);
        rayXm = new Ray(t, Xm);
        rayZm = new Ray(t, Zm);
    }
    #endregion

    #region Ray���菈��
    // �e������Ray�ɂ�锻����s��
    private void CheckRaycast()
    {
        CheckRayDirection(rayXp, ref XpHit, ref XpDistance, ref Right);
        CheckRayDirection(rayZp, ref ZpHit, ref ZpDistance, ref Up);
        CheckRayDirection(rayXm, ref XmHit, ref XmDistance, ref Left);
        CheckRayDirection(rayZm, ref ZmHit, ref ZmDistance, ref Under);
    }

    // �P��̕����ɑ΂���Ray���菈��
    private void CheckRayDirection(Ray ray, ref RaycastHit hit, ref float distance, ref bool directionFlag)
    {
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.CompareTag("Blaze"))
            {
                float dist = Vector3.Distance(hit.transform.position, transform.position);
                distance = dist / 100;
                directionFlag = distance <= reacDistance;
            }
            else
            {
                directionFlag = false;
            }
        }
        else
        {
            directionFlag = false;
        }
    }
    #endregion

    #region �X�V����
    // Update is called once per frame
    void Update()
    {
        SetRayDirections();  // Ray�̐ݒ�
        CheckRaycast();      // Ray���菈��
    }
    #endregion
}
