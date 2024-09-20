using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TalkAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool hasTalked = false;
    public static bool NPCDestroy = false;
    public static bool FFStop = false;  // プレイヤー停止フラグ
    public float fadeDuration = 2.0f;  // 透明化にかかる時間
    private SkinnedMeshRenderer npcRenderer;  // SkinnedMeshRendererを取得
    private Material npcMaterial;  // マテリアルの参照を保持
    private bool isFading = false;  // 透明化が開始されたかどうか
    private Collider[] npcColliders;  // NPCの全コライダー
    public RescueNPC rescueNPC;  // RescueNPCスクリプトへの参照
    public Canvas textCanvas;  // World SpaceモードのCanvas

    void Start()
    {
        NPCDestroy = false;
        hasTalked = false;
        FFStop = false;  // ゲーム開始時はプレイヤーが動けるように
        agent = GetComponent<NavMeshAgent>();

        // SkinnedMeshRendererを取得
        npcRenderer = GetComponentInChildren<SkinnedMeshRenderer>();  // 子要素から取得
        if (npcRenderer != null)
        {
            // マテリアルを取得
            npcMaterial = npcRenderer.material;
            if (npcMaterial != null)
            {
                Debug.Log("NPCのマテリアルが正常に取得されました。");
            }
            else
            {
                Debug.LogError("NPCにマテリアルが設定されていません。");
            }
        }
        else
        {
            Debug.LogError("SkinnedMeshRendererがNPCに設定されていません。");
        }

        // NPCの全てのコライダーを取得
        npcColliders = GetComponentsInChildren<Collider>();  // NPCおよび子オブジェクトに含まれる全てのコライダーを取得
    }

    void Update()
    {
        // NPCがゴールに向かって歩いている
        if (hasTalked && agent.remainingDistance <= agent.stoppingDistance && !isFading)
        {
            Debug.Log("NPCがゴールに到着しました。透明化を開始します。");
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    // NPCに話しかけた時に呼ばれる関数
    public void TalkToNPC()
    {
        Debug.Log("話しかける関数は呼び出されたよーーん");
        if (!hasTalked)
        {
            Debug.Log("話しかける関数は呼び出されたよーーんその後処理もするよーーん");
            // プレイヤーを停止させる
            RadioText.RescueFlag = true;

            HideTextMark();  // テキストマークを非表示にする
            Debug.Log("NPCのテキストマークを非表示にしました。");

            // NPCに話しかけた後、ナビメッシュで目的地へ移動開始
            hasTalked = true;
            agent.SetDestination(new Vector3(0, 0, 0)); // 座標(0,0,0)へ移動させる
            Debug.Log("NPCがゴールへ移動開始！");
            Debug.Log("透明化を開始します！");

            //オーディオの処理
            int number1 = PlayerPrefs.GetInt("R_number");
            if (number1 == 4)
            {
                Audio.GetInstance().PlaySound(5);  // SE_List[0]の効果音を再生
            }
            else if (number1 == 3 || number1 == 1)
            {
                Audio.GetInstance().PlaySound(4);  // SE_List[0]の効果音を再生
            }
            else if (number1 == 2)
            {
                Audio.GetInstance().PlaySound(3);  // SE_List[0]の効果音を再生
            }

            StartCoroutine(FadeOutAndDestroy());  // 透明化を開始
        }
    }

    // 透明化してオブジェクトを削除するコルーチン
    private IEnumerator FadeOutAndDestroy()
    {
        isFading = true;
        // 透明化が始まった瞬間にコライダーを無効化
        foreach (Collider col in npcColliders)
        {
            col.enabled = false;
        }
        Debug.Log("NPCの当たり判定が無効化されました。");

        // マテリアルを透明化モードに変更
        npcMaterial.SetFloat("_Mode", 2);  // Fadeモードに設定
        npcMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        npcMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        npcMaterial.SetInt("_ZWrite", 0);
        npcMaterial.DisableKeyword("_ALPHATEST_ON");
        npcMaterial.EnableKeyword("_ALPHABLEND_ON");
        npcMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        npcMaterial.renderQueue = 3000;  // 透明レンダリングを許可

        float fadeSpeed = 1.0f / fadeDuration;
        Color color = npcMaterial.color;

        // 透明化処理
        for (float t = 0; t < 1.0f; t += Time.deltaTime * fadeSpeed)
        {
            color.a = Mathf.Lerp(1, 0, t);  // Alpha値を1から0へ徐々に変化
            npcMaterial.color = color;  // マテリアルに反映
            yield return null;
            Debug.Log("透明化おん！！！");
        }

        // 完全に透明になったら削除
        color.a = 0;
        npcMaterial.color = color;


        CollGauge.TimeStop = false;
        // NPCが削除された後にプレイヤーを動けるようにする
        FFStop = false;  // プレイヤーキャラを再度動かす
        Debug.Log("NPCが削除されました。プレイヤーが再度動けます。");

        // 対話終了後、他のNPCとも話せるようにする
        RescueNPC.isTalkingToNPC = false;
        //RescueNPC.RescueStopButtom = true;
        Debug.Log("NPCに話しかけられるようになりました" + RescueNPC.isTalkingToNPC);
        NPCDestroy = true;
        hasTalked = false;

        Destroy(gameObject);
    }
    #region テキスト非表示
    public void HideTextMark()
    {
        // テキストマークを非表示にする
        textCanvas.gameObject.SetActive(false);
    }
    #endregion
}