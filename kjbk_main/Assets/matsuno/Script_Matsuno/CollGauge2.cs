using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CollGauge2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CGauge;
    [HideInInspector] public float Counter = 0;            //Τvͺ
    int Collapse = 100;            //|σQ[W
    float Span = 3.5f;                 //SpanbΙκρ|σQ[Wπ1%Έη·

    int number100 = 1;
    int number10 = 0;
    int number1 = 0;
    int persent = 11;

    // Start is called before the first frame update
    void Start()
    {
        CGauge.SetText("<sprite="+ number100 +">"+"<sprite=" + number10 + ">" + "<sprite=" + number1 + ">"+"<sprite="+ persent +">");
    }

    // Update is called once per frame
    void Update()
    {
        // |σQ[WJEg
        Counter += Time.deltaTime;   //bJEg
        if (Counter >= Span)          //|σQ[Wͺ1%ΈΜb
        {
            Collapse--;                //|σQ[W-1%
            Counter = 0;             //bJEgZbg
            number10 = Collapse / 10 % 10;
            number1 = Collapse % 10;
        }

        if(Collapse == 100)
        {
            
        }
        else
        {
            // |σQ[WΜ\¦
            CGauge.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        }

       
    }
}
