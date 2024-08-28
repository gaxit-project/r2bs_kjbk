using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Danger_UI : MonoBehaviour
{
    #region 変数定義
    [SerializeField] public GameObject Player;  // プレイヤーオブジェクト
    private Danger_Ray ray;  // Danger_Rayコンポーネント

    public GameObject Up;    // 上方向のUI
    public GameObject Under; // 下方向のUI
    public GameObject Left;  // 左方向のUI
    public GameObject Right; // 右方向のUI

    private CanvasGroup UpImage;    // 上方向のCanvasGroup
    private CanvasGroup UnderImage; // 下方向のCanvasGroup
    private CanvasGroup LeftImage;  // 左方向のCanvasGroup
    private CanvasGroup RightImage; // 右方向のCanvasGroup

    private double _time;  // 経過時間
    #endregion

    #region 初期化
    // Start is called before the first frame update
    void Start()
    {
        ray = Player.GetComponent<Danger_Ray>();

        UpImage = Up.GetComponent<CanvasGroup>();
        UnderImage = Under.GetComponent<CanvasGroup>();
        LeftImage = Left.GetComponent<CanvasGroup>();
        RightImage = Right.GetComponent<CanvasGroup>();

        // 各UIの透明度を0に設定
        UpImage.alpha = 0;
        UnderImage.alpha = 0;
        LeftImage.alpha = 0;
        RightImage.alpha = 0;
    }
    #endregion

    #region 更新処理
    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime; // 経過時間を更新

        // 上方向の危険判定
        if (ray.Up)
        {
            var Upalpha = Mathf.Abs(Mathf.Clamp(ray.ZpDistance, 0, 1) - 1);
            UpImage.alpha = Upalpha;
        }
        else
        {
            UpImage.alpha = 0;
        }

        // 下方向の危険判定
        if (ray.Under)
        {
            float Underalpha = Mathf.Abs(Mathf.Clamp(ray.ZmDistance, 0, 1) - 1);
            UnderImage.alpha = Underalpha;
        }
        else
        {
            UnderImage.alpha = 0;
        }

        // 右方向の危険判定
        if (ray.Right)
        {
            float Rightalpha = Mathf.Abs(Mathf.Clamp(ray.XpDistance, 0, 1) - 1);
            RightImage.alpha = Rightalpha;
        }
        else
        {
            RightImage.alpha = 0;
        }

        // 左方向の危険判定
        if (ray.Left)
        {
            float Leftalpha = Mathf.Abs(Mathf.Clamp(ray.XmDistance, 0, 1) - 1);
            LeftImage.alpha = Leftalpha;
        }
        else
        {
            LeftImage.alpha = 0;
        }
    }
    #endregion
}
