using UnityEngine;
using UnityEngine.UI;

public class Presente : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Text bgmValue;
    [SerializeField] Text seValue;

    private void Start()
    {
        Audio.GetInstance().PlayBGM(0); // ƒeƒXƒg‚Æ‚µ‚ÄBGM‚ð‚È‚ç‚·
        OnChangedBGMVolume();
        OnChangedSEVolume();
    }

    public void OnChangedBGMVolume()
    {
        Audio.GetInstance().BGMVolume = bgmSlider.value;
        //bgmValue.text = string.Format("{0:0.00}", bgmSlider.value);

    }
    public void OnChangedSEVolume()
    {
        Audio.GetInstance().SEVolume = seSlider.value;
        //seValue.text = string.Format("{0:0.00}", seSlider.value);
    }
}
