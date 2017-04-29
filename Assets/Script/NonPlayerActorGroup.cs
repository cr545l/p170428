using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerActorGroup : MonoBehaviour
{
    private Transform[] children;

    void Start()
    {
        children = GetComponentsInChildren<Transform>();

        for( int i = 1; i < children.Length; i++ )
        {
            NonPlayerActor nonPlayer = children[i].gameObject.AddComponent<EnemyActor>();
            nonPlayer.InitNonPlayer( 0.5f, GameManager.Instance.PlayerActor );
        }
    }

    void Update()
    {
        bool hasChildren = false;
        for( int i = 1; i < children.Length; i++ )
        {
            if( children[i] != null )
            {
                hasChildren = true;
                break;
            }
        }

        if( !hasChildren ) Destroy( gameObject );
    }
}
