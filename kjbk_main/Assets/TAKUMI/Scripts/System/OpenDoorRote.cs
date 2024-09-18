using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorRote : MonoBehaviour
{
    #region �ϐ��錾

    // �h�A�̃R���C�_�[
    public BoxCollider door1;
    public BoxCollider door2;

    // �h�A�̉�]���x
    float DoorSpeed = 10f;

    public Transform doorTransform; // �h�A��Transform
    private Quaternion initialRotation; // �h�A�̏�����]
    private Quaternion targetRotation; // �h�A�̖ڕW��]

    public bool isOpen = false;
    #endregion

    #region Start���\�b�h
    void Start()
    {
        #region �ϐ��̏�����
        initialRotation = doorTransform.rotation; // �h�A�̏�����]���L�^
        targetRotation = initialRotation; // �����̖ڕW��]�̓h�A�����Ă�����
        #endregion
    }
    #endregion

    #region Update���\�b�h
    void Update()
    {


        // �h�A�̉�]���X�V
        doorTransform.rotation = Quaternion.Slerp(doorTransform.rotation, targetRotation, Time.deltaTime * DoorSpeed);

        if (Close())
        {
            isOpen = false;
        }
    }
    #endregion



    #region �h�A�̓���
    // �L�����N�^�[�̈ʒu�Ɋ�Â��ăh�A���J��
    public void OpenDoorTowards(bool Front, bool Back, bool conect)
    {
        if (!isOpen)
        {
            DoorColOnOff(false);
            isOpen = true;

            float con = conect ? -1 : 1;

            if (Front)
            {
                // �v���C���[���h�A�̉E���ɂ���ꍇ
                targetRotation = doorTransform.rotation * Quaternion.Euler(0, -90f * con, 0);
            }
            else if (Back)
            {
                // �v���C���[���h�A�̍����ɂ���ꍇ
                targetRotation = doorTransform.rotation * Quaternion.Euler(0, 90f * con, 0);
            }
        }
    }

    // �h�A�����
    public void CloseDoor()
    {
        DoorColOnOff(true);
        targetRotation = initialRotation; // �h�A�����̉�]�ʒu�ɖ߂�
    }
    #endregion

    void DoorColOnOff(bool isDoor)
    {
        door1.enabled = isDoor;
        door2.enabled = isDoor;
    }

    public bool Close()
    {
        if (doorTransform.rotation == initialRotation)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
