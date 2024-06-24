using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaze_Maneger : MonoBehaviour
{
    public static Blaze_Maneger instance;
    [SerializeField]private bool Debug = false;
    [SerializeField]private float LvUpSecond;
    [SerializeField] private float LvUpProbability;
    [SerializeField] private float LvUpSize;
    [SerializeField] private float SpreadSecond;
    [SerializeField] private float SpreadProbability;
    [SerializeField] private int LvSpreadProbability;
    [SerializeField] private float SpreadRange;
    [SerializeField] private int BoostNum;
    [SerializeField] private string[] AntiBlazeTag;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public (float Second, float Probability, float Size) getLvData()
    {
        return (LvUpSecond, LvUpProbability, LvUpSize);
    }

    public (float Second, float Probability, int LvProbability, float Range, int Boost, string[] Tag) getSpreadData()
    {
        return (SpreadSecond, SpreadProbability, LvSpreadProbability, SpreadRange, BoostNum, AntiBlazeTag);
    }
}
