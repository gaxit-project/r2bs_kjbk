using UnityEngine;
using UnityEngine.UI;

public class DynamicTextBox : MonoBehaviour
{
    [SerializeField] private Text uiText; // �e�L�X�g�R���|�[�l���g�̎Q��

    // �e�L�X�g��ݒ肵�A�e�L�X�g�{�b�N�X�̃T�C�Y�������Œ���
    public void UpdateText(string newText)
    {
        uiText.text = newText;
        // �����I�Ƀ��C�A�E�g���X�V���ăe�L�X�g�{�b�N�X�̃T�C�Y�𒲐�
        LayoutRebuilder.ForceRebuildLayoutImmediate(uiText.rectTransform);
    }
}