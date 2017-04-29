using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NonPlayer : GameActor
{
    private AudioSource _audioSource = null;

    private Animator animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private Animator _deathAnimator = null;

    protected override void InitActor()
    {
        /*Init( 0.1f );*/
    }

    public void Init( float speed )
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = GameManager.Instance.NonPlayerDeathAudioClip;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if( null != animator )
        {
            animator.speed = speed;
        }

        InitSelectResource( UnityEngine.Random.Range( 0, 3 ) );
    }

    private void InitSelectResource( int index )
    {
        _spriteRenderer.sprite = GameManager.Instance.NonPlayerSprites[index];
        _spriteRenderer.enabled = true;

        _deathAnimator = Instantiate( GameManager.Instance.NonPlayerDeathAnimators[index] );
        _deathAnimator.transform.parent = transform;
        _deathAnimator.transform.localPosition = Vector3.zero;
        _deathAnimator.gameObject.SetActive( false );
    }

    protected override void UpdateActor()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        InvokeMessage(new GameActorMessage {
            _targetType = eMessageType.NonPlayerAttack_FromUser,
            _targetActor = null});
    }

    public void SetPlaySpeed(float speed)
    {
        animator.speed = speed;
    }

    public void SetAnimatorController(RuntimeAnimatorController controller)
    {
        animator.runtimeAnimatorController = controller;
    }

    protected override void CallbackMessage(GameMessage message)
    {
        
    }

    protected override void CallbackDeath(GameActor target)
    {
        GameManager.Instance.InvokeShakeCamera();

        animator.speed = 0.0f;

        _spriteRenderer.enabled = false;

        _deathAnimator.gameObject.SetActive( true );
        _deathAnimator.Play( 0 );

        _audioSource.Play();

        StartCoroutine( Helper.Wait( _deathAnimator.GetClip().length, () =>
        {
            Destroy( gameObject );
        } ) );        
    }
}
