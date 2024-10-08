using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpSound : MonoBehaviour
{
    public AudioSource audioSource;

    private bool InPlayer = false; // �v���C���[���͈͓��ɂ��邩�ǂ�����ǐ�
    private bool soundPlayed = false; // �����Đ����ꂽ���ǂ�����ǐ�


    private void OnTriggerEnter(Collider obj)
    {
        // �v���C���[���g���K�[�ɓ��������A�܂������Đ�����Ă��Ȃ��ꍇ�̂ݏ���
        if (obj.CompareTag("Player") && !soundPlayed)
        {
            int rnd = Random.Range(0, 2);
            InPlayer = true;
            if (InPlayer)
            {
                soundPlayed = true; // �����Đ������̂ŁA�t���O�𗧂Ăďd����h��
                //audioSource.Play(); // �����Đ�
                Audio.GetInstance().PlaySound(19 + rnd); // �ʂ̃T�E���h�Đ�����������΂����ɋL�q
            }
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        // �v���C���[���g���K�[�͈͂���o����A�t���O�����Z�b�g
        if (obj.CompareTag("Player"))
        {
            InPlayer = false;
            soundPlayed = false; // �ēx�g���K�[�ɓ��������ɉ����Đ��ł���悤�Ƀ��Z�b�g
        }
    }
}
