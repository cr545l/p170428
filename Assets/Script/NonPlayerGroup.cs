using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerGroup : MonoBehaviour
{

    private Transform[] children;

	void Start ()
    {
        children = GetComponentsInChildren<Transform>();

        for(int i=1; i<children.Length; i++)
        {
            children[i].gameObject.AddComponent<CircleCollider2D>();
            children[i].gameObject.AddComponent<NonPlayer>();
        }
    }
	
	void Update ()
    {
        bool hasChildren = false;
        for(int i=1; i<children.Length; i++)
        {
            if(children[i] != null)
            {
                hasChildren = true;
                break;
            }
        }

        if (!hasChildren) Destroy(gameObject);
	}
}
