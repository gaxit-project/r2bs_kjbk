using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpread : MonoBehaviour
{
    [SerializeField] private float minSecond;
    [SerializeField] private float maxSecond;

    public GameObject PrefabBlaze;

    private bool Action = true;

    private void Start()
    {
        RandomReSpread();
    }

    private void RandomReSpread()
    {
        float Second = Random.Range(minSecond, maxSecond);
        if (Action)
        {
            Invoke("Spread", Second);
            Action = false;
        }
    }

    private void Spread()
    {
        GameObject newObject = (GameObject)Instantiate(PrefabBlaze, this.transform.position, Quaternion.identity);
        newObject.name = "SpreadPlane";
        Destroy(this.gameObject);
    }

}
