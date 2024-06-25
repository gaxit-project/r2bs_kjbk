using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpread : MonoBehaviour
{
    private float minSecond;
    private float maxSecond;

    private GameObject Blaze;
    private Blaze_Maneger m_Blaze;

    private bool Action = true;

    private void Start()
    {
        Blaze = GameObject.Find("BlazeManeger");
        m_Blaze = Blaze.GetComponent<Blaze_Maneger>();
        var Data = m_Blaze.getReData();
        minSecond = Data.min;
        maxSecond = Data.max;
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
        Vector3 plane = new Vector3 (this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z);
        m_Blaze.CreateBlaze(this.transform.position);
        m_Blaze.CreateSpreadPlane(plane);
        Destroy(this.gameObject);
    }
}
