using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    //音ファイル
    [SerializeField] AudioClip[] SE_List;
    [SerializeField] AudioClip[] BGM_List;
    [SerializeField] AudioClip[] Roop_SE_List;

    //音の鳴らし方指定
    [SerializeField] AudioSource audioSorceBGM;
    [SerializeField] AudioSource audioSorceSE;
    [SerializeField] AudioSource audioSorceRoopSE;

    public float BGMVolume //BGMボリューム
    {
        get { return audioSorceBGM.volume; }
        set { audioSorceBGM.volume = value; }
    }

    public float SEVolume //SEボリューム
    {
        get { return audioSorceSE.volume; }
        set { audioSorceSE.volume = value; }
    }

    public float DANCEVolume //RoopSEボリューム
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

    private void Awake() //シングルトン
    {
        if (this != GetInstance())
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(int index) //SE再生
    {
        audioSorceSE.PlayOneShot(SE_List[index]);
    }

    public void PlayBGM(int index) //BGM再生
    {
        audioSorceBGM.clip = BGM_List[index];
        audioSorceBGM.Play();
    }

    public void StopBGM() //BGM停止
    {
        audioSorceBGM.Stop();
        audioSorceRoopSE.Stop();
    }

    public void PlayRoopSE(int index) //DANCE_BGM再生
    {
        audioSorceRoopSE.clip = Roop_SE_List[index];
        audioSorceRoopSE.Play();
    }

    public void StopRoopSE() //DANCE_BGM停止
    {
        audioSorceRoopSE.Stop();
    }
}
