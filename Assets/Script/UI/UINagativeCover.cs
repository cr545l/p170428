using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINagativeCover : MonoBehaviour
{
    [SerializeField]
    private Image _image = null;

    private bool _bEnable = false;
        
    void Start ()
    {
        if( Helper.isNull( _image ) ) return;
    }
    
    void Update ()
    {
		if( _bEnable )
        {
            Color color = _image.color;
            if( color.a < 1 )
            {
                color.a += GameConst._NAGATIVE_COVER_SPEED;
                _image.color = color;
                Debug.Log( _image.color );
            }
        }
        else
        {
            Color color = _image.color;
            if( 0 < color.a )
            {
                color.a -= GameConst._NAGATIVE_COVER_SPEED;
                _image.color = color;
                Debug.Log( _image.color );
            }
        }
    }

    public void InvokeAlpha( float time )
    {
        _bEnable = true;

        StartCoroutine( Helper.Wait( time, () =>
        {
            _bEnable = false;
        } ) );
    }
}
