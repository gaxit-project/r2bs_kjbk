using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class FadeSceneLoader : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1.0f;
    private EventSystem eventSystem;
    private string Scene;
    private LIFE LIFE;
    private Presente presente;
    private void Start()
    {
        // イベントシステムの取得
        eventSystem = EventSystem.current;
        LIFE= FindObjectOfType<LIFE>();
        presente= FindObjectOfType<Presente>();
    }
    public void CallCoroutine(string scenename)
    {
        StartCoroutine(FadeOutAndLoadScene(scenename));
    }

    public IEnumerator FadeOutAndLoadScene(string scenename)
    {
        Scene=scenename;
        fadePanel.enabled = true;                 // パネルを有効化
        float elapsedTime = 0.0f;                 // 経過時間を初期化
        Color startColor = fadePanel.color;       // フェードパネルの開始色を取得
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f); // フェードパネルの最終色を設定
        if (LIFE != null && LIFE.getred()==true)
        {
            endColor= Color.red;
        }else if (presente != null && presente.Getwhite() == false)
        {
            endColor = Color.white;
        }
 

        if (eventSystem != null)
        {
            eventSystem.enabled = false;
        }
        // フェードアウトアニメーションを実行
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // 経過時間を増やす
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);  // フェードの進行度を計算
            fadePanel.color = Color.Lerp(startColor, endColor, t); // パネルの色を変更してフェードアウト
            yield return null;                                     // 1フレーム待機
        }
        SceneManager.LoadScene(scenename);
        if (eventSystem != null)
        {
            eventSystem.enabled = true;
        }
    }

    public string getScene(){
        return Scene;
    }
    
}