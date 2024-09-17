using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera mainCamera; // ���C���J�������Q��

    void Start()
    {
        // �J�������w�肳��Ă��Ȃ���΃��C���J�����������Őݒ�
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        // �J�����̕����Ɋ�Â��ăL�����o�X�̉�]��ݒ肷��i�ʒu�͕ύX���Ȃ��j
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;

        // Y���̉�]�̂ݓK�p���āA�e�L�X�g���㉺�ɉ�]���Ȃ��悤�ɂ���
        direction.y = 0;

        // �L�����o�X�̉�]���J�����̕����Ɍ�����
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
