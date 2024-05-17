using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireLv : MonoBehaviour
{
    [SerializeField] private float LvUpSecond;   //���x���㏸�Ԋu(�b)
    [SerializeField] private float LvUpProbability;   //���x���㏸�̊m��
    [SerializeField] private float LvUpSize;   //���x���㏸���̃G�t�F�N�g�T�C�Y�̑��吔�l
    [SerializeField] private float Size = 1;   //�G�t�F�N�g�̃T�C�Y

    public int FireLvel;   //���x��


    void Start()
    {
        FireLvel = 1;
        this.transform.localScale = new Vector3(Size, Size, Size);
        StartCoroutine("LvUp");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LvUp()
    {
        float PreSize = Size;
        while (true)
        {
            if (FireLvel == 3) break;
            yield return new WaitForSeconds(LvUpSecond);

            int probability = Random.Range(0, 100);
            if (probability < LvUpProbability)
            {
                PreSize += LvUpSize;
                this.transform.localScale = new Vector3(PreSize, PreSize, PreSize);
                Debug.Log("SizeUp");
                FireLvel++;
            }
        }
    }
}
