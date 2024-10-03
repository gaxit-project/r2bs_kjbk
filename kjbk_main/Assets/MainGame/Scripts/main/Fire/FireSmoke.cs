using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireSmoke : MonoBehaviour
{
    //�����̉��̃��x���@f:�Ⴂ�@t:����
    public bool roomLevel = false;
    int count = 0;
    float SmokeConc;
    int InRoom;

    public BoxCollider boxCollider;
    void Start()
    {
        count = 0;
        SmokeConc = 0;
        PlayerPrefs.SetFloat("Smoke", 1);
        InRoom = 0;
        PlayerPrefs.SetInt("InRoom", InRoom);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("SmokeConc * " + SmokeConc);
        PlayerPrefs.SetFloat("SmokeConc", SmokeConc);
    }

    void OnTriggerStay(Collider obj)
    {
        int count = 0; // count�𖈃t���[�����Z�b�g

        if (obj.CompareTag("Player"))
        {
            InRoom = 1;
            PlayerPrefs.SetInt("InRoom", InRoom);
            // �v���C���[�� BoxCollider ���擾
            if (boxCollider != null)
            {
                // BoxCollider �̒��S�����[���h���W�ɕϊ�
                Vector3 boxCenter = boxCollider.transform.TransformPoint(boxCollider.center);

                // BoxCollider �̃T�C�Y���擾���A�I�u�W�F�N�g�̃X�P�[�����l��
                Vector3 boxSize = Vector3.Scale(boxCollider.size, obj.transform.localScale) * 0.5f; // OverlapBox�̃T�C�Y�͔����ɂ���K�v������

                // OverlapBox ���g�p���Ĕ͈͓��� Collider ���擾
                Collider[] StayObj = Physics.OverlapBox(boxCenter, boxSize, boxCollider.transform.rotation); // ��]���l��

                foreach (Collider blaze in StayObj)
                {
                    if (blaze.CompareTag("Blaze"))
                    {
                        count++;
                    }
                }
            }

            if (roomLevel)

            {
                if (count >= 13)
                {   //90
                    SmokeConc = 0f;
                }
                else if (12 >= count && count >= 10)
                {   //50
                    SmokeConc = 0.2f;
                }
                else if (9 >= count && count >= 7)
                {   //30
                    SmokeConc = 0.4f;
                }
                else
                {
                    SmokeConc = 1f;
                }
            }
            else
            {
                if (count == 7)
                {
                    SmokeConc = 0f;
                }
                else if (6 >= count && count >= 5)
                {
                    SmokeConc = 0.2f;
                }
                else if (4 >= count && count >= 3)
                {
                    SmokeConc = 0.4f;
                }
                else
                {
                    SmokeConc = 1;
                }
            }


        }

        Debug.Log("Blaze count: " + count);
        Debug.Log(roomLevel + ":::::room");

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InRoom = 0;
            PlayerPrefs.SetInt("InRoom", InRoom);
        }
    }

}
