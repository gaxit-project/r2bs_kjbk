using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    //���t�@�C��
    [SerializeField] AudioClip[] SE_List;
    [SerializeField] AudioClip[] BGM_List;
    [SerializeField] AudioClip[] Roop_SE_List;

    //���̖炵���w��
    [SerializeField] AudioSource audioSorceBGM;
    [SerializeField] AudioSource audioSorceSE;
    [SerializeField] AudioSource audioSorceRoopSE;

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

    public float DANCEVolume //RoopSE�{�����[��
    {
        get { return audioSorceRoopSE.volume; }
        set { audioSorceRoopSE.volume = value; }
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
    }

    public void PlayBGM(int index) //BGM�Đ�
    {
        audioSorceBGM.clip = BGM_List[index];
        audioSorceBGM.Play();
    }

    public void StopBGM() //BGM��~
    {
        audioSorceBGM.Stop();
        audioSorceRoopSE.Stop();
    }

    public void PlayRoopSE(int index) //DANCE_BGM�Đ�
    {
        audioSorceRoopSE.clip = Roop_SE_List[index];
        audioSorceRoopSE.Play();
    }

    public void StopRoopSE() //DANCE_BGM��~
    {
        audioSorceRoopSE.Stop();
    }
}
