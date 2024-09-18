using UnityEngine;
using UnityEngine.UI;

public class Presente : MonoBehaviour
{
    #region �錾: �ϐ�
    // �X���C�_�[�̎Q��
    [SerializeField] Slider bgmSlider; // BGM�{�����[�������X���C�_�[
    [SerializeField] Slider seSlider; // SE�{�����[�������X���C�_�[

    // �e�L�X�g�̎Q��
    [SerializeField] Text bgmValue; // BGM�{�����[���\���e�L�X�g
    [SerializeField] Text seValue; // SE�{�����[���\���e�L�X�g

    // UI�I�u�W�F�N�g�̎Q��
    public GameObject PauseUI; // �|�[�Y��ʂ�UI
    public GameObject SoundOptionUI; // �T�E���h�ݒ��UI
    public GameObject TitleUI; // �^�C�g����ʂ�UI
    public GameObject BackToTheTitle; // �^�C�g����ʂɖ߂�{�^��
    public GameObject SoundSetting; // �T�E���h�ݒ�{�^��

    // �{�^���̎Q��
    public Button a; // �T�E���h�ݒ�{�^��
    public Button TitleIcon; // �^�C�g���A�C�R���{�^��
    public Button TitlePIcon; // �^�C�g���߂�A�C�R���{�^��
    public Button SoundPIcon; // �T�E���h�ݒ�߂�A�C�R���{�^��

    // �X�e�[�^�X�t���O
    public bool ConfigSta; // �ݒ蒆�t���O
    public bool TitleSta; // �^�C�g�����t���O

    // �Q�[�����W�b�N�̎Q��
    public GoalJudgement Goal; // �S�[������X�N���v�g
    public Pause PauseScript; // �|�[�Y�X�N���v�g

    // �Q�[���i�s�J�E���^�[
    int Rcnt = 0; // �~���J�E���g
    #endregion

    #region ������: Start���\�b�h
    void Start()
    {
        // ���ʃX���C�_�[�̏����ݒ�
        OnChangedBGMVolume();
        OnChangedSEVolume();

        // �X���C�_�[�̒l��ۑ�����ǂݍ���
        bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        seSlider.value = PlayerPrefs.GetFloat("SE");

        // Audio�C���X�^���X�̉��ʐݒ�
        var audio = Audio.GetInstance();
        audio.BGMVolume = PlayerPrefs.GetFloat("BGM");
        audio.SEVolume = PlayerPrefs.GetFloat("SE");
        audio.RoopSEVolume = PlayerPrefs.GetFloat("SE");
        audio.WALKVolume = PlayerPrefs.GetFloat("SE");
        audio.FireVolume1 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume2 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume3 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume4 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume5 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume6 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume7 = PlayerPrefs.GetFloat("SE");

        // �X�e�[�^�X�t���O�̏�����
        ConfigSta = false;
        TitleSta = false;
    }
    #endregion

    #region ���ʕύX: �X���C�_�[�̒l�ύX
    public void OnChangedBGMVolume()
    {
        // BGM���ʂ̕ۑ��ƓK�p
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
        var audio = Audio.GetInstance();
        audio.BGMVolume = bgmSlider.value;
    }

    public void OnChangedSEVolume()
    {
        // SE���ʂ̕ۑ��ƓK�p
        PlayerPrefs.SetFloat("SE", seSlider.value);
        var audio = Audio.GetInstance();
        audio.SEVolume = seSlider.value;
        audio.RoopSEVolume = seSlider.value;
        audio.WALKVolume = seSlider.value;
        audio.FireVolume1 = seSlider.value;
        audio.FireVolume2 = seSlider.value;
        audio.FireVolume3 = seSlider.value;
        audio.FireVolume4 = seSlider.value;
        audio.FireVolume5 = seSlider.value;
        audio.FireVolume6 = seSlider.value;
        audio.FireVolume7 = seSlider.value;
    }
    #endregion

    #region �{�^������: UI���상�\�b�h
    public void Quit()
    {
        Audio.GetInstance().PlaySound(16);
        // �Q�[���I������
        Scene.GetInstance().EndGame();
    }

    public void Option()
    {
        Audio.GetInstance().PlaySound(16);
        // �T�E���h�ݒ�UI��\�����A�|�[�YUI���\���ɂ���
        SoundOptionUI.SetActive(true);
        PauseUI.SetActive(false);
        a.Select();
        ConfigSta = true;
    }

    public void PauseBack()
    {
        Audio.GetInstance().PlaySound(16);
        // �|�[�Y��ʂɖ߂鏈��
        SoundOptionUI.SetActive(false);
        TitleUI.SetActive(false);
        BackToTheTitle.SetActive(true);
        SoundSetting.SetActive(true);
        PauseScript.PauseCon();
    }

    public void Title()
    {
        Audio.GetInstance().PlaySound(16);
        // �^�C�g����ʂɑJ�ڂ��鏈��
        Scene.GetInstance().Title();
    }

    public void GoTitleUI()
    {
        Audio.GetInstance().PlaySound(16);
        // �^�C�g��UI��\�����A�|�[�YUI���\���ɂ���
        TitleUI.SetActive(true);
        PauseUI.SetActive(false);
        TitleIcon.Select();
        TitleSta = true;
    }

    public void TitleBack()
    {
        Audio.GetInstance().PlaySound(16);
        // �|�[�YUI�ɖ߂鏈��
        TitleUI.SetActive(false);
        PauseUI.SetActive(true);
        TitlePIcon.Select();
        TitleSta = false;
    }

    public void SoundBack()
    {
        Audio.GetInstance().PlaySound(16);
        // �T�E���h�ݒ�UI�ɖ߂鏈��
        SoundOptionUI.SetActive(false);
        PauseUI.SetActive(true);
        SoundPIcon.Select();
        ConfigSta = false;
    }

    public void Escape()
    {
        Audio.GetInstance().PlaySound(16);
        // �~���l���ɉ����ăQ�[�����ʂ����肷�鏈��
        Rcnt = PlayerPrefs.GetInt("RescueCount");
        Debug.Log("K");

        Time.timeScale = 1;
        if (Rcnt >= 5)
        {
            PlayerPrefs.SetString("Result", "CLEAR");
            Scene.Instance.GameResult();
        }
        else
        {
            PlayerPrefs.SetString("Result", "GAMEOVER");
            Scene.Instance.GameResult();
        }
    }
    #endregion
}
