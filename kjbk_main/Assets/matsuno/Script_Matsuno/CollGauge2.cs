using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CollGauge2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CGauge;
    [HideInInspector] public float Counter = 0;            //���Ԍv��
    int Collapse = 100;            //�|��Q�[�W
    float Span = 3.5f;                 //Span�b�Ɉ��|��Q�[�W��1%���炷

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
        // �|��Q�[�W�J�E���g
        Counter += Time.deltaTime;   //�b���J�E���g
        if (Counter >= Span)          //�|��Q�[�W��1%���̕b��
        {
            Collapse--;                //�|��Q�[�W-1%
            Counter = 0;             //�b���J�E���g���Z�b�g
            number10 = Collapse / 10 % 10;
            number1 = Collapse % 10;
        }

        if(Collapse == 100)
        {
            
        }
        else
        {
            // �|��Q�[�W�̕\��
            CGauge.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        }

       
    }
}
