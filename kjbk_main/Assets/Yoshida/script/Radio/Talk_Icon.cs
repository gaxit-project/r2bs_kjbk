using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk_Icon : MonoBehaviour
{
    [SerializeField] GameObject PlayerIcon;
    [SerializeField] GameObject NPCIcon;
    public Rescue_NPC Rescue_NPC;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Rescue_NPC.IsItActiveIcon() && !Rescue_NPC.IsItLock())
        {
            Rescue_NPC.SetLock(true);
            ActivePlayerIcon();
            Invoke("ActiveNPCIcon", 1f);
            Invoke("FinishTalk", 2f);
        }
        if (Rescue_NPC.IsItActiveIcon() && Rescue_NPC.IsItSecondContact())
        {
            FinishTalk();
            Rescue_NPC.SetActiveIcon(false);
            PlayerIcon.SetActive(false);
            NPCIcon.SetActive(false);
        }


    }

    private void ActivePlayerIcon()
    {
        if (!Rescue_NPC.IsItSecondContact())
        {
            PlayerIcon.SetActive(true);
        }
    }

    private void ActiveNPCIcon()
    {
        if (!Rescue_NPC.IsItSecondContact())
        {
            PlayerIcon.SetActive(false);
            NPCIcon.SetActive(true);
        }
    }

    private void FinishTalk()
    {
        NPCIcon.SetActive(false);
        Rescue_NPC.SetActiveIcon(false);
        Rescue_NPC.SetLock(false);
    }
}
