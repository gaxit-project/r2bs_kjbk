using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControll : MonoBehaviour
{
	private GameObject firePoint;

	[SerializeField]
	[Tooltip("炎")]
	private GameObject fire;
   void Start()
   {
        SpreadFire();
   }

   void SpreadFire()
   {
        firePoint = GameObject.Find("FirePoint");
        // 炎を発射する場所を取得
		Vector3 firePosition = firePoint.transform.position;
        Destroy(firePoint);
	    // 上で取得した場所に、"fire"のPrefabを出現させる。
	    GameObject newfire = Instantiate(fire, firePosition, this.gameObject.transform.rotation);
        // 出現させた炎の名前を"fire"に変更
	    newfire.name = fire.name;
        //Debug.Log("炎");
        StartCoroutine(CoolTime());
   }
   private IEnumerator CoolTime()
   {
        yield return new WaitForSeconds(5);
        SpreadFire();
   }
}
