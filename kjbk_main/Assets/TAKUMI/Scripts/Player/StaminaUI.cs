using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    #region 変数宣言
    [SerializeField] private GameObject StaminaGauge;

    // ゲージを表示するImageコンポーネント
    [SerializeField] private Image _gaugeImage;

    float Stamina;

    float time;

    public Color color_1, color_2, color_3, color_4;

    #endregion

    private void Start()
    {
        _gaugeImage.fillAmount = PlayerPrefs.GetFloat("Stamina");
    }

    #region 更新処理
    void Update()
    {
        #region スタミナUI処理
        Stamina = PlayerPrefs.GetFloat("Stamina");

        #region スタミナが満タン時に1秒経過で非表示
        //スタミナが満タン時に1秒経過で非表示
        if (Stamina >= 1f)
        {
            time += Time.deltaTime;
            if(time >= 1f)
            {
                StaminaGauge.SetActive(false);
            }
            
        }
        else
        {
            time = 0f;
            StaminaGauge.SetActive(true);
        }
        #endregion

        #region MAP使用時に非表示
        //MAP使用時に非表示
        if (1 == PlayerPrefs.GetInt("Map"))
        {
            StaminaGauge.SetActive(false);
        }
        #endregion


        if (0 == PlayerPrefs.GetInt("RunOut"))
        {
            //息切れを起こしていないときは緑から赤
            if (Stamina > 0.75f)
            {
                _gaugeImage.color = Color.Lerp(color_2, color_1, (Stamina - 0.75f) * 4f);
            }
            else if (Stamina > 0.25f)
            {
                _gaugeImage.color = Color.Lerp(color_3, color_2, (Stamina - 0.25f) * 4f);
            }
            else
            {
                _gaugeImage.color = Color.Lerp(color_4, color_3, Stamina * 4f);
            }
        }
        else
        {
            //息切れ時は赤で満タンまで回復
            _gaugeImage.color = new Color(255, 0, 0);
        }


        // 進捗をゲージに反映
        _gaugeImage.fillAmount = PlayerPrefs.GetFloat("Stamina");
        #endregion

    }
    #endregion

}
