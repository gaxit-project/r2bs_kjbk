using UnityEngine;
using UnityEngine.AI;

public class TalkAI : MonoBehaviour
{
    public static NavMeshAgent agent;
    private Renderer npcRenderer;
    public static bool hasTalked = false;
    public static bool NPCDestroy = false;

    void Start()
    {
        NPCDestroy = false;
        hasTalked = false;
        agent = GetComponent<NavMeshAgent>();
        npcRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // NPC���ړ����ăJ�����O�ɏo���ꍇ�ɍ폜����
        if (hasTalked && !npcRenderer.isVisible)
        {
            // NPC���J�����O�ɏo���̂ŏ���
            Destroy(gameObject);
            Debug.Log("NPC��j��I");
            NPCDestroy = true;
            hasTalked = false;
        }
    }

    // NPC�ɘb�����������ɌĂ΂��֐�
    public static void TalkToNPC()
    {
        if (!hasTalked)
        {
            // NPC�ɘb����������A�i�r���b�V���ŖړI�n�ֈړ��J�n
            hasTalked = true;
            agent.SetDestination(new Vector3(0, 0, 0)); // ���W(0,0,0)�ֈړ�������
            Debug.Log("NPC���S�[���ֈړ��J�n�I");
        }
    }
}

