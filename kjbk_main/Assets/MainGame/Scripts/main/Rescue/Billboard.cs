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
        // �e�L�X�g�̉�]��NPC�̉e�����󂯂Ȃ��悤�ɂ��A�J�����Ɍ�����
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 textPosition = transform.position;

        // �e�L�X�g���J�����̕����������悤�ɂ��邪�AY���̉�]������L���ɂ���
        Vector3 direction = (textPosition - cameraPosition).normalized; // �J��������e�L�X�g�܂ł̕����x�N�g��
        direction.y = 0;  // Y���̉�]�̂ݓK�p���A�㉺�̉�]��h��

        // ��]���v�Z���āA�e�L�X�g����ɃJ�����̕����������悤�ɂ���
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
