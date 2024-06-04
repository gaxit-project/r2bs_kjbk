using UnityEngine;
using UnityEngine.UI;

public class Presenter : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Text bgmValue;
    [SerializeField] Text seValue;
    [SerializeField] Text walkValue;

    private void Start()
    {
        AudioManager.GetInstance().PlayBGM(0); // ƒeƒXƒg‚Æ‚µ‚ÄBGM‚ð‚È‚ç‚·
        OnChangedBGMVolume();
        OnChangedSEVolume();
    }

    public void OnChangedBGMVolume()
    {
        AudioManager.GetInstance().BGMVolume = bgmSlider.value;
        bgmValue.text = string.Format("{0:0.00}", bgmSlider.value);

    }
    public void OnChangedSEVolume()
    {
        AudioManager.GetInstance().SEVolume = seSlider.value;
        AudioManager.GetInstance().WALKVolume = seSlider.value;
        seValue.text = string.Format("{0:0.00}", seSlider.value);
        walkValue.text = string.Format("{0:0.00}", seSlider.value);
    }
}
