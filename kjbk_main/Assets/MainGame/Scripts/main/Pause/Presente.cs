using UnityEngine;
using UnityEngine.UI;

public class Presente : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Text bgmValue;
    [SerializeField] Text seValue;

    public GameObject PauseUI;
    public GameObject SoundOptionUI;
    public GameObject TitleUI;

    public static Pause PauseScript;

    private void Start()
    {
        OnChangedBGMVolume();
        OnChangedSEVolume();
    }

    public void OnChangedBGMVolume()
    {
        Audio.GetInstance().BGMVolume = bgmSlider.value;
        bgmValue.text = string.Format("{0:0.00}", bgmSlider.value);

    }
    public void OnChangedSEVolume()
    {
        Audio.GetInstance().SEVolume = seSlider.value;
        Audio.GetInstance().RoopSEVolume = seSlider.value;
        Audio.GetInstance().WALKVolume = seSlider.value;
        seValue.text = string.Format("{0:0.00}", seSlider.value);

    }

    public void Quit()
    {
        Scene.GetInstance().EndGame();
    }

    public void Option()
    {
        SoundOptionUI.SetActive(true);
        PauseUI.SetActive(false);
    }

    public void PauseBack()
    {
        SoundOptionUI.SetActive(false);
        TitleUI.SetActive(false);
        PauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        //PauseScript.pause_status = false;
    }
    public void Title()
    {
        Scene.GetInstance().Title();
    }
    public void GoTitleUI()
    {
        TitleUI.SetActive(true);
        PauseUI.SetActive(false);
    }
    public void TitleBack()
    {
        TitleUI.SetActive(false);
        PauseUI.SetActive(true);
    }
    public void SoundBack()
    {
        SoundOptionUI.SetActive(false);
        PauseUI.SetActive(true);
    }
}
