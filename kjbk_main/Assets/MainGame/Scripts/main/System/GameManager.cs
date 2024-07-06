using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayController PlayCon;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Audio.Instance.PlayBGM(0);
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator Inoperable(float i) // �����s�\�ɂ���i�����̕b���ԁj
    {
        GameObject inputObj = GameObject.Find("InputManager");
        PlayCon = Player.GetComponent<PlayController>();
        PlayCon.enabled = false; // �X�N���v�g�𖳌���
        yield return new WaitForSeconds(i); // �����̕b�������҂�
        PlayCon.enabled = true; // �X�N���v�g��L����
        yield break;
    }

    public void CallInoperable(float i)
    {
        StartCoroutine("Inoperable", i); // ���̃X�N���v�g����Ăяo���p
    }
}
