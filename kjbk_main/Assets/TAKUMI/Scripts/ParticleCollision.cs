using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
	private void OnParticleCollision(GameObject other)
	{
		//Debug.Log("消化中");
		// 当たった相手の色をランダムに変える
		//other.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
	}
}
