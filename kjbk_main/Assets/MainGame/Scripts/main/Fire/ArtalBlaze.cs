using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AratalBlaze : MonoBehaviour
{
    public GameObject[] Artal;
    int i;
    private int len;
    int rand;
    float ran;
    Transform myTransform;
    float AratalCap;

    public bool FireStatus = false; // ���ĂȂ�false
    public bool P_O_Fire = false; //����������
    public bool DesBlaze = false; //�������ꂽ��


    void Start()
    {
        len = Artal.Length;
        for (i = 1; i < len; i++)
        {
            Artal[i].SetActive(false);
        }

        ran = UnityEngine.Random.Range(0f, 90f);
        myTransform = this.transform;

    }

    void Update()
    {
        rand = Random.Range(0, len);
        ran = UnityEngine.Random.Range(0f, 90f);
        //Debug.Log(rand);
        ArtalSet(rand);
        // ���[���h���W��ŁA���݂̉�]�ʂ։��Z����
        myTransform.Rotate(0f, ran, 0f, Space.World);
    }
    void ArtalSet(int num)
    {
        for (i = 0; i < len; i++)
        {
            if (i == num)
            {
                Artal[i].SetActive(true);
            }
            else
            {
                Artal[i].SetActive(false);
            }
        }
    }

    public void OnParticleCollision(GameObject obj)
    {
        //InfernoScript������ϐ�
        Inferno script = this.GetComponent<Inferno>();

        //�������Ă���΂̎q�I�u�W�F�N�g�̎擾
        GameObject BlazeR1 = this.transform.GetChild(0).gameObject;

        Debug.Log("������");
        script.P_O_Fire = true;
        AratalCap -= 4f * Time.deltaTime * 100;
        if (AratalCap <= 0f)// && BY1_MinMaxCurve.constant <= 0f
        {
            Debug.Log("��������܂���");
            DesBlaze = true;
        }
    }

}
