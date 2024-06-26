using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaze_Maneger : MonoBehaviour
{
    public static Blaze_Maneger instance;

    [Header("Lv")]
    [SerializeField] private float LvUpSecond;
    [SerializeField] private float LvUpProbability;
    [SerializeField] private float LvUpSize;

    [Header("âÑèƒ")]
    [SerializeField] private float SpreadSecond;
    [SerializeField] private float SpreadProbability;
    [SerializeField] private int LvSpreadProbability;
    [SerializeField] private float SpreadRange;
    [SerializeField] private int BoostNum;
    [SerializeField] private string[] AntiBlazeTag;

    [Header("çƒâÑèƒ")]
    [SerializeField] private float minSecond;
    [SerializeField] private float maxSecond;

    [Header("prefab")]
    [SerializeField] GameObject PrefabBlaze;
    [SerializeField] GameObject PrefabSpreadPlane;
    [SerializeField] GameObject PrefabExtPlane;

    private void Awake()
    {
        instance = this;
    }

    public void CreateBlaze(Vector3 position)
    {
        GameObject newObject = Instantiate(PrefabBlaze, position, Quaternion.identity);
        newObject.name = "Blaze";
        newObject.transform.parent = this.transform;
    }

    public void CreateSpreadPlane(Vector3 position)
    {
        GameObject newObject = Instantiate(PrefabSpreadPlane, position, Quaternion.identity);
        newObject.name = "SpreadPlane";
        newObject.transform.parent = this.transform;
    }

    public void CreateExtPlane(Vector3 position)
    {
        GameObject newObject = Instantiate(PrefabExtPlane, position, Quaternion.identity);
        newObject.name = "ExtPlane";
        newObject.transform.parent = this.transform;
    }

    public (float Second, float Probability, float Size) getLvData()
    {
        return (LvUpSecond, LvUpProbability, LvUpSize);
    }

    public (float Second, float Probability, int LvProbability, float Range, int Boost, string[] Tag) getSpreadData()
    {
        return (SpreadSecond, SpreadProbability, LvSpreadProbability, SpreadRange, BoostNum, AntiBlazeTag);
    }

    public (float min, float max) getReData()
    {
        return (minSecond, maxSecond);
    }
}
