using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire_Lv : MonoBehaviour
{
    [SerializeField] private float LvUpSecond;   //���x���㏸�Ԋu(�b)
    [SerializeField] private float LvUpProbability;   //���x���㏸�̊m��
    [SerializeField] private float LvUpSize;   //���x���㏸���̃G�t�F�N�g�T�C�Y�̑��吔�l
    [SerializeField] private float Size = 1;   //�G�t�F�N�g�̃T�C�Y

    [System.NonSerialized] public int FireLv = 1;   //���x��

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LvUp");
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    IEnumerator LvUp()
    {
        while (FireLv < 3)
        {
            yield return new WaitForSeconds(LvUpSecond);

            int probability = Random.Range(0, 100);
            if (probability < LvUpProbability)
            {
                Size += LvUpSize; 
                this.transform.localScale = new Vector3(Size, Size, Size);
                FireLv++;
            }
        }
    }
}
