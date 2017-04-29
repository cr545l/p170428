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

    public void Show( ResultData resultData )
    {
        gameObject.SetActive( true );
        _messageText.text = GameConst._RANDOM_MESSAGES[UnityEngine.Random.Range( 0, GameConst._RANDOM_MESSAGES.Length )];
        _scoreText.text = resultData._score.ToString();
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
