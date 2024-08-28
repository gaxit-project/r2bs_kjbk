using System.Collections;
using UnityEngine;

public class FireLv : MonoBehaviour
{
    #region 変数定義
    private float LvUpSecond;   // レベル上昇間隔(秒)
    private float LvUpProbability;   // レベル上昇の確率
    private float LvUpSize;   // レベル上昇時のエフェクトサイズの増大数値
    private float Size = 1;   // エフェクトのサイズ

    public int FireLvel;   // レベル
    #endregion

    #region 参照オブジェクト
    private GameObject Blaze;   // Blazeオブジェクト
    private Blaze_Maneger m_Blaze;   // Blaze_Manegerの参照
    Transform BlazePos;   // Blazeの位置
    Vector3 pos;   // Blazeの位置ベクトル
    #endregion

    #region 初期化
    private void Awake()
    {
        Blaze = GameObject.Find("BlazeManeger");
        m_Blaze = Blaze.GetComponent<Blaze_Maneger>();
        var Data = m_Blaze.getLvData();
        LvUpSecond = Data.Second;
        LvUpProbability = Data.Probability;
        LvUpSize = Data.Size;
    }

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
    #endregion

    #region コルーチン
    IEnumerator LvUp()
    {
        float PreSize = Size;
        while (true)
        {
            if (FireLvel == 3) break;   // レベルが3で停止
            yield return new WaitForSeconds(LvUpSecond);

            int probability = Random.Range(0, 100);
            if (probability < LvUpProbability)
            {
                PreSize += LvUpSize;
                this.transform.localScale = new Vector3(PreSize, PreSize, PreSize);
                FireLvel++;
                pos.y = 4.2f;
                BlazePos.position = pos;
            }
        }
    }
    #endregion
}
