using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapping : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
       /* void OnTriggerEnter(Collision collision)
	{
		// 衝突した相手にPlayerタグが付いているとき
		if (collision.gameObject.tag == "Player")
		{
			
			Destroy(gameObject);
		}
	}*/
    void OnTriggerEnter(Collider other)
    {
        // 衝突したオブジェクトがプレイヤーである場合
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    }
