using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire_Lv : MonoBehaviour
{
    [SerializeField] private float LvUpSecond;   //レベル上昇間隔(秒)
    [SerializeField] private float LvUpProbability;   //レベル上昇の確率
    [SerializeField] private float LvUpSize;   //レベル上昇時のエフェクトサイズの増大数値
    [SerializeField] private float Size = 1;   //エフェクトのサイズ

    public int FireLv;   //レベル

    // Start is called before the first frame update
    void Start()
    {
        FireLv = 1;
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
            if (FireLv == 3) break;
            yield return new WaitForSeconds(LvUpSecond);

            int probability = Random.Range(0, 100);
            if (probability < LvUpProbability)
            {
                PreSize += LvUpSize; 
                this.transform.localScale = new Vector3(PreSize, PreSize, PreSize);
                Debug.Log("SizeUp");
                FireLv++;
            }
        }
    }
}
