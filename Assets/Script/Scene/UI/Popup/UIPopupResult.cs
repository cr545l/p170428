using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupResult : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText = null;
    [SerializeField]
    private Text _messageText = null;

    private void Start ()
    {
        if( Helper.isNull( _scoreText, _messageText ) ) return;

    }

    private void Update ()
    {
		
	}

    public void ReplayButtonClick()
    {

    }

    public void ExitButtonClick()
    {

    }
}
