using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHPBar : MonoBehaviour
{
    [SerializeField]
    private Image _progress = null;

    [SerializeField]
    private Text _healthPointText = null;
    [SerializeField]
    private Text _damageText = null;

    private NonPlayerActor _targetActor = null;
    private RectTransform _canvas = null;

    private bool _bInit = false;

	private void Start ()
    {
        if( Helper.isNull( _progress, _healthPointText, _damageText ) ) return;
	}   

    public void Init( NonPlayerActor target, Canvas canvas )
    {
        _targetActor = target;
        _canvas = canvas.GetComponent<RectTransform>();

        _bInit = true;
    }

    private void Update ()
    {
        if( _bInit )
        {
            if( null != _targetActor )
            {
                Vector3 screenPos = Camera.main.WorldToViewportPoint( _targetActor.transform.position );
                screenPos.x *= _canvas.rect.width;
                screenPos.y *= _canvas.rect.height;
                transform.position = screenPos;

                _progress.transform.localScale = new Vector3( _targetActor.CurrentHealthPoint / _targetActor.MaxHealthPoint, 1, 1 );

                _healthPointText.text = "H : "+_targetActor.CurrentHealthPoint +"/"+_targetActor.MaxHealthPoint;
                _damageText.text = "D : " + _targetActor.Damage;
            }
            else
            {
                _bInit = false;
                Destroy( gameObject );
            }
        }
    }
}
