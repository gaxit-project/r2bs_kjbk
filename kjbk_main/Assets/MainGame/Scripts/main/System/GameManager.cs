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


    private IEnumerator Inoperable(float i) // 操作を不能にする（引数の秒数間）
    {
        GameObject inputObj = GameObject.Find("InputManager");
        PlayCon = Player.GetComponent<PlayController>();
        PlayCon.enabled = false; // スクリプトを無効化
        yield return new WaitForSeconds(i); // 引数の秒数だけ待つ
        PlayCon.enabled = true; // スクリプトを有効化
        yield break;
    }

    public void CallInoperable(float i)
    {
        StartCoroutine("Inoperable", i); // 他のスクリプトから呼び出す用
    }
}
