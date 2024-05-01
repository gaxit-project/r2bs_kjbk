using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingDoor : MonoBehaviour
{
    //mappingdoorにつける
    public GameObject door;
   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
	{
		// 衝突した相手にPlayerタグが付いているとき
		if (collision.gameObject.tag == "Player")
		{
			door.SetActive(false);
		}
	}

    void OnCollisionExit(Collision collision)
    {
        // 衝突から離れた相手がPlayerタグが付いているとき
        if (collision.gameObject.tag == "Player")
        {
            // 復活させる
            door.SetActive(true);
        }
    }
}
