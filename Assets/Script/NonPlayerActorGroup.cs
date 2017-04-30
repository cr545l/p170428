using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerActorGroup : MonoBehaviour
{
    private List<NonPlayerActor> _nonPlayerActorList = new List<NonPlayerActor>();
    private Transform[] children;

    public List<NonPlayerActor> NonPlayerActorList { get { return _nonPlayerActorList; } }

    void Start()
    {
        children = GetComponentsInChildren<Transform>();

        for( int i = 1; i < children.Length; i++ )
        {
            NonPlayerActor nonPlayerActor = null;
            
            nonPlayerActor = children[i].gameObject.AddComponent<ItemActor>();
            //nonPlayerActor = children[i].gameObject.AddCOomponent<EnemyActor>();

            nonPlayerActor.InitNonPlayer( GameManager.Instance.CurrentActorSpeed, GameManager.Instance.PlayerActor );

            _nonPlayerActorList.Add( nonPlayerActor );
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
