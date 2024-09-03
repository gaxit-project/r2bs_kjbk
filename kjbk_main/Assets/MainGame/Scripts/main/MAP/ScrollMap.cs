using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScrollViewController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public InputAction ScrollMissionMap;

    private void OnEnable()
    {
        ScrollMissionMap.Enable();
    }

    private void OnDisable()
    {
        ScrollMissionMap.Disable();
    }

    void Update()
    {
        Vector2 navigationInput = ScrollMissionMap.ReadValue<Vector2>();

        if (navigationInput.y != 0)
        {
            // �㉺�̓��͂Ɋ�Â���ScrollView���X�N���[��
            scrollRect.verticalNormalizedPosition += navigationInput.y * Time.deltaTime;
        }
    }
}
