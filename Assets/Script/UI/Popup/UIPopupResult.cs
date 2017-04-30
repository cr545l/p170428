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
    [SerializeField]
    private GameObject[] _ranks = null;

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
        VisibleRank( resultData._score );
    }

    private void VisibleRank( int score )
    {
        for( int i = 0; i < _ranks.Length; ++i )
        {
            _ranks[i].gameObject.SetActive( false );
        }

        if( 25000 < score )
        {
            _ranks[6].gameObject.SetActive( true );
        }
        else if( 10000 < score )
        {
            _ranks[5].gameObject.SetActive( true );
        }
        else if( 5000 < score )
        {
            _ranks[4].gameObject.SetActive( true );
        }
        else if( 1500 < score )
        {
            _ranks[3].gameObject.SetActive( true );
        }
        else if( 500 < score )
        {
            _ranks[2].gameObject.SetActive( true );
        }
        else if( 0 < score )
        {
            _ranks[1].gameObject.SetActive( true );
        }
        else
        {
            _ranks[0].gameObject.SetActive( true );
        }
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
