using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupPause : MonoBehaviour
{    
    public void ContinueButtonClick()
    {
        Debug.Log( "ContinueButtonClick" );
        GameManager.Instance.InvokePause( false );
        gameObject.SetActive( false );
    }

    public void ReplayButtonClick()
    {
        Debug.Log( "ReplayButtonClick" );
        GameManager.Instance.InvokeStart();
        gameObject.SetActive( false );
    }

    public void ExitButtonClick()
    {
        Debug.Log( "ExitButtonClick" );
        GameManager.Instance.InvokeTitle();
    }
}
