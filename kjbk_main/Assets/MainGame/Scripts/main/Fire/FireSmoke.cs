using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireSmoke : MonoBehaviour
{
    //部屋の煙のレベル　f:低い　t:高い
    public bool roomLevel = false;
    int count = 0;
    float SmokeConc;
    int InRoom;

    public BoxCollider boxCollider;
    void Start()
    {
        count = 0;
        SmokeConc = 0;
        PlayerPrefs.SetFloat("Smoke", 1);
        InRoom = 0;
        PlayerPrefs.SetInt("InRoom", InRoom);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("SmokeConc * " + SmokeConc);
        PlayerPrefs.SetFloat("SmokeConc", SmokeConc);
    }

    void OnTriggerStay(Collider obj)
    {
        int count = 0; // countを毎フレームリセット

        if (obj.CompareTag("Player"))
        {
            InRoom = 1;
            PlayerPrefs.SetInt("InRoom", InRoom);
            // プレイヤーの BoxCollider を取得
            if (boxCollider != null)
            {
                // BoxCollider の中心をワールド座標に変換
                Vector3 boxCenter = boxCollider.transform.TransformPoint(boxCollider.center);

                // BoxCollider のサイズを取得し、オブジェクトのスケールを考慮
                Vector3 boxSize = Vector3.Scale(boxCollider.size, obj.transform.localScale) * 0.5f; // OverlapBoxのサイズは半分にする必要がある

                // OverlapBox を使用して範囲内の Collider を取得
                Collider[] StayObj = Physics.OverlapBox(boxCenter, boxSize, boxCollider.transform.rotation); // 回転も考慮

                foreach (Collider blaze in StayObj)
                {
                    if (blaze.CompareTag("Blaze"))
                    {
                        count++;
                    }
                }
            }

            if (roomLevel)

            {
                if (count >= 13)
                {   //90
                    SmokeConc = 0f;
                }
                else if (12 >= count && count >= 10)
                {   //50
                    SmokeConc = 0.2f;
                }
                else if (9 >= count && count >= 7)
                {   //30
                    SmokeConc = 0.4f;
                }
                else
                {
                    SmokeConc = 1f;
                }
            }
            else
            {
                if (count == 7)
                {
                    SmokeConc = 0f;
                }
                else if (6 >= count && count >= 5)
                {
                    SmokeConc = 0.2f;
                }
                else if (4 >= count && count >= 3)
                {
                    SmokeConc = 0.4f;
                }
                else
                {
                    SmokeConc = 1;
                }
            }


        }

        Debug.Log("Blaze count: " + count);
        Debug.Log(roomLevel + ":::::room");

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InRoom = 0;
            PlayerPrefs.SetInt("InRoom", InRoom);
        }
    }

}
