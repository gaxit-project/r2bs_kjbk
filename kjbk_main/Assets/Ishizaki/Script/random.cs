using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random : MonoBehaviour
{
    public float MoveRange; // �ŏ��̈ʒu���甼�aMoveRange�ȓ��͈̔͂������_���Ɉړ�
    private float ChargeTime = 2.0f;
    private float TimeCount;
    private Vector3 Position;

    // Start is called before the first frame update
    void Start()
    {
        // �ŏ��̈ʒu���擾
        Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCount += Time.deltaTime;

        // �����O�i
        transform.position += transform.forward * 20.0f * Time.deltaTime;

        // �͈͓����ǂ������肵�C�͈͊O�ł���΍ŏ��̈ʒu�ɖ߂�
        Vector3 MovePosition = transform.position - Position;
        if (MovePosition.magnitude > MoveRange)
        {
            transform.position = Position;
        }

        // ��莞�Ԍo�߂������ǂ���
        if (TimeCount > ChargeTime)
        {
            // �i�H�������_���ɕύX����
            Vector3 course = new Vector3(0, Random.Range(0, 180), 0);
            transform.localRotation = Quaternion.Euler(course);

            // �^�C���J�E���g��0�ɖ߂�
            TimeCount = 0;
        }
    }
}
