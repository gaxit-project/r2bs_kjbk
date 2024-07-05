using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingButton : MonoBehaviour
{
    // public
    public float speed = 1.0f;

    // private
    private RescueCount CounterScript;
    private Text text;
    private Image image;
    private float time;
    private SwitchCamera blink;
    private bool blinking = true;
    private bool blinking2 = false;

    private enum ObjType {
        TEXT,
        IMAGE
    };
    private ObjType thisObjType = ObjType.TEXT;

    void Start() {
        blink = FindObjectOfType<SwitchCamera>();
        CounterScript = FindObjectOfType<RescueCount>();
        // アタッチしてるオブジェクトを判別
        if (this.gameObject.GetComponent<Image>()) {
            thisObjType = ObjType.IMAGE;
            image = this.gameObject.GetComponent<Image>();
        } else if (this.gameObject.GetComponent<Text>()) {
            thisObjType = ObjType.TEXT;
            text = this.gameObject.GetComponent<Text>();
        }
    }

    void Update() {
            if (thisObjType == ObjType.IMAGE && image != null) {
                image.color = GetAlphaColor(image.color);
            } else if (thisObjType == ObjType.TEXT && text != null) {
                text.color = GetAlphaColor(text.color);
            }
    }

    void OnDisable() {
        // ゲームオブジェクトが無効化されたときにtimeをリセット
        if (thisObjType == ObjType.IMAGE && image != null) {
            if(blinking2){
                blinking=false;
            }
            Color color = image.color;
            color.a = 1.0f; // アルファ値を最大に設定
            image.color = color;
        } else if (thisObjType == ObjType.TEXT && text != null) {
            Color color = text.color;
            color.a = 1.0f; // アルファ値を最大に設定
            text.color = color;
        }

    }

    // Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color) {
        
        if (CounterScript.getNum() == 1 && !blink.initialMapStatusActivated) {
            if(blink.Ui_status){
            if(blinking){
            blinking2=true;
            time += Time.deltaTime * 5.0f * speed;
            color.a = Mathf.Sin(time) * 0.5f + 0.5f;
        }
            }
        }
        return color;
    }
}