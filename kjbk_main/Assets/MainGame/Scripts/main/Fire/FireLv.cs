using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class FireLv : MonoBehaviour
{
    private float LvUpSecond;   //レベル上昇間隔(秒)
    private float LvUpProbability;   //レベル上昇の確率
    private float LvUpSize;   //レベル上昇時のエフェクトサイズの増大数値
    private float Size = 1;   //エフェクトのサイズ

    public int FireLvel;   //レベル

    private GameObject Blaze;
    private Blaze_Maneger m_Blaze;
    Transform BlazePos;
    Vector3 pos;

    private void Awake()
    {
        Blaze = GameObject.Find("BlazeManeger");
        m_Blaze = Blaze.GetComponent<Blaze_Maneger>();
        var Data = m_Blaze.getLvData();
        LvUpSecond = Data.Second;
        LvUpProbability = Data.Probability;
        LvUpSize = Data.Size;
    }

    // Start is called before the first frame update
    void Start()
    {
        FireLvel = 1;
        this.transform.localScale = new Vector3(Size, Size, Size);
        BlazePos = this.transform;
        pos = BlazePos.position;
        pos.y = 2.2f;
        BlazePos.position = pos;
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
                FireLvel++;
                pos.y =4.2f;
                BlazePos.position = pos;
            }
        }
    }
}
