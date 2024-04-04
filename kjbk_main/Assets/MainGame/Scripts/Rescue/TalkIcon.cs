using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkIcon : MonoBehaviour
{
    [SerializeField] GameObject PlayerIcon;
    [SerializeField] GameObject NPCIcon;
    public RescueNPC RescueNPC;

    void Update()
    {
        if (RescueNPC.IsItActiveIcon() && !RescueNPC.IsItLock())
        {
            RescueNPC.SetLock(true);
            ActivePlayerIcon();
            Invoke("ActiveNPCIcon", 1f);
            Invoke("FinishTalk", 2f);
        }
        if (RescueNPC.IsItActiveIcon() && RescueNPC.IsItSecondContact())
        {
            FinishTalk();
            RescueNPC.SetActiveIcon(false);
            PlayerIcon.SetActive(false);
            NPCIcon.SetActive(false);
        }


    }

    private void ActivePlayerIcon()
    {
        if (!RescueNPC.IsItSecondContact())
        {
            PlayerIcon.SetActive(true);
        }
    }

    private void ActiveNPCIcon()
    {
        if (!RescueNPC.IsItSecondContact())
        {
            PlayerIcon.SetActive(false);
            NPCIcon.SetActive(true);
        }
    }

    private void FinishTalk()
    {
        NPCIcon.SetActive(false);
        RescueNPC.SetActiveIcon(false);
        RescueNPC.SetLock(false);
    }
}
