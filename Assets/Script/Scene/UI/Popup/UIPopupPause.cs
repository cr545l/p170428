using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupPause : MonoBehaviour
{    
    public void ContinueButtonClick()
    {
        GameManager.Instance.InvokePause( false );
    }

    public void ReplayButtonClick()
    {
        GameManager.Instance.InvokeStart();
    }

    public void ExitButtonClick()
    {
        GameManager.Instance.InvokeTitle();
    }
}
