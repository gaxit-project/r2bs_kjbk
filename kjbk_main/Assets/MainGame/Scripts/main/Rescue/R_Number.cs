using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Number : MonoBehaviour
{
    #region éŒ¾
    // ŒyÇÒ¯•Ê‚Ì‚½‚ß‚Ì•Ï”
    public int Number;
    #endregion

    #region ‰Šú‰»
    void Start()
    {
        PlayerPrefs.SetInt("R_number", 0);
    }
    #endregion


    #region ŠÖ”
    // ŒyÇÒ‚ğ¯•Ê‚·‚é‚½‚ß‚ÌŠÖ”
    public void RNumber()
    {
        PlayerPrefs.SetInt("R_number", Number);
    }
    #endregion
}
