using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_daichi : MonoBehaviour
{
    //���t�@�C��
    [SerializeField] AudioClip[] SE_List;
    [SerializeField] AudioClip[] BGM_List;
    [SerializeField] AudioClip[] Roop_SE_List;

    //���̖炵���w��
    [SerializeField] AudioSource audioSorceBGM;
    [SerializeField] AudioSource audioSorceSE;
    [SerializeField] AudioSource audioSorceRoopSE;
    [SerializeField] AudioSource audioSourceWALK;
    [SerializeField] AudioSource audioSourceWater;
    [SerializeField] AudioSource audioSourceFire1;
    [SerializeField] AudioSource audioSourceFire2;
    [SerializeField] AudioSource audioSourceFire3;
    [SerializeField] AudioSource audioSourceFire4;
    [SerializeField] AudioSource audioSourceFire5;
    [SerializeField] AudioSource audioSourceFire6;
    [SerializeField] AudioSource audioSourceFire7;

    public float BGMVolume //BGM�{�����[��
    {
        get { return audioSorceBGM.volume; }
        set { audioSorceBGM.volume = value; }
    }

    public float SEVolume //SE�{�����[��
    {
        get { return audioSorceSE.volume; }
        set { audioSorceSE.volume = value; }
    }

    public float RoopSEVolume //RoopSE�{�����[��
    {
        get { return audioSorceRoopSE.volume; }
        set { audioSorceRoopSE.volume = value; }
    }

    public float WALKVolume //WALKSE�{�����[��
    {
        get { return audioSourceWALK.volume; }
        set { audioSourceWALK.volume = value; }
    }

    public float WaterVolume //WALKSE�{�����[��
    {
        get { return audioSourceWater.volume; }
        set { audioSourceWater.volume = value; }
    }

    public float FireVolume1 //FireSE�{�����[��
    {
        get { return audioSourceFire1.volume; }
        set { audioSourceFire1.volume = value; }

    }
    public float FireVolume2 //FireSE�{�����[��
    {
        get { return audioSourceFire2.volume; }
        set { audioSourceFire2.volume = value; }

    }

    public float FireVolume3 //FireSE�{�����[��
    {
        get { return audioSourceFire3.volume; }
        set { audioSourceFire3.volume = value; }

    }

    public float FireVolume4 //FireSE�{�����[��
    {
        get { return audioSourceFire4.volume; }
        set { audioSourceFire4.volume = value; }

    }

    public float FireVolume5 //FireSE�{�����[��
    {
        get { return audioSourceFire5.volume; }
        set { audioSourceFire5.volume = value; }

    }

    public float FireVolume6 //FireSE�{�����[��
    {
        get { return audioSourceFire6.volume; }
        set { audioSourceFire6.volume = value; }

    }

    public float FireVolume7 //FireSE�{�����[��
    {
        get { return audioSourceFire7.volume; }
        set { audioSourceFire7.volume = value; }

    }

    public static Audio Instance = null;

    public static Audio GetInstance()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<Audio>();
        }
        return Instance;
    }

    private void Awake() //�V���O���g��
    {
        if (this != GetInstance())
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(int index) //SE�Đ�
    {
        audioSorceSE.PlayOneShot(SE_List[index]);
        Debug.Log("�Ƃ��Ă܂����H�H�H�H�H�H�H�H�H�H�H�H�H�H�H�H�H�H�H�H�H");
    }

    public void PlayBGM(int index) //BGM�Đ�
    {
        audioSorceBGM.clip = BGM_List[index];
        audioSorceBGM.Play();
    }

    public void StopBGM() //BGM��~
    {
        audioSorceBGM.Stop();
    }

    public void PlayRoopSE(int index) //DANCE_BGM�Đ�
    {
        audioSorceRoopSE.clip = Roop_SE_List[index];
        audioSorceRoopSE.Play();
    }

    public void StopRoopSE() //Roop_SE��~
    {
        audioSorceRoopSE.Stop();
    }
}
