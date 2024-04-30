using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] seList;
    [SerializeField] AudioClip[] bgmList;

    [SerializeField] AudioSource audioSourceBGM;
    [SerializeField] AudioSource audioSourceSE;

    public float BGMVolume
    {
        get { return audioSourceBGM.volume; }
        set { audioSourceBGM.volume = value; }
    }
    public float SEVolume
    {
        get { return audioSourceSE.volume; }
        set { audioSourceSE.volume = value; }
    }

    static AudioManager Instance = null;

    public static AudioManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<AudioManager>();
        }
        return Instance;
    }
    private void Awake()
    {
        if (this != GetInstance())
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(int index)
    {
        audioSourceSE.PlayOneShot(seList[index]);
    }
    public void PlayBGM(int index)
    {
        audioSourceBGM.clip = bgmList[index];
        audioSourceBGM.Play();
    }
}
