using UnityEngine;
using UnityEngine.UI;

public class DynamicTextBox : MonoBehaviour
{
    [SerializeField] private Text uiText; // テキストコンポーネントの参照

    // テキストを設定し、テキストボックスのサイズを自動で調整
    public void UpdateText(string newText)
    {
        uiText.text = newText;
        // 強制的にレイアウトを更新してテキストボックスのサイズを調整
        LayoutRebuilder.ForceRebuildLayoutImmediate(uiText.rectTransform);
    }
}