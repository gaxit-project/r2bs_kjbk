using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk_Icon : MonoBehaviour
{
    [SerializeField] GameObject PlayerIcon;
    [SerializeField] GameObject NPCIcon;
    public Rescue_NPC NPCscript;

    private bool Lock = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NPCscript.IsItActiveIcon() && !IsItLock())
        {
            SetLock(true);
            ActivePlayerIcon();
            Invoke("ActiveNPCIcon", 1f);
        }
    }

    private void ActivePlayerIcon()
    {
        PlayerIcon.SetActive(true);
    }

    private void ActiveNPCIcon()
    {
        PlayerIcon.SetActive(false);
        NPCIcon.SetActive(true);

        Invoke("FinishTalk", 1f);
    }

    private void FinishTalk()
    {
        NPCIcon.SetActive(false);
        NPCscript.SetActiveIcon(false);
    }


    private bool IsItLock()
    {
        return Lock;
    }
    private void SetLock(bool b)
    {
        Lock = b;
    }
}
