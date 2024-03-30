using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShoot : MonoBehaviour
{
	[SerializeField]
	[Tooltip("弾の発射場所")]
	private GameObject firingPoint;

	[SerializeField]
	[Tooltip("弾")]
	private GameObject water;

	[SerializeField]
	[Tooltip("弾の速さ")]
	public float speed = 30f;

	// Update is called once per frame
	void Update()
	{
		// スペースキーが押されたかを判定
		if(Input.GetMouseButton(0))
		{
			// 弾を発射する場所を取得
			Vector3 waterPosition = firingPoint.transform.position;
			// 上で取得した場所に、"bullet"のPrefabを出現させる。Bulletの向きはMuzzleのローカル値と同じにする（3つ目の引数）
			GameObject newWater = Instantiate(water, waterPosition, this.gameObject.transform.rotation);
			// 出現させた弾の方向を取得（MuzzleのローカルY軸方向のこと）
			Vector3 direction = newWater.transform.forward;
			// 弾の発射方向にnewWaterの方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
			newWater.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
			// 出現させた弾の名前を"bullet"に変更
			newWater.name = water.name;
			// 出現させた弾を0.8秒後に消す
			Destroy(newWater, 0.8f);
		}
	}
}
