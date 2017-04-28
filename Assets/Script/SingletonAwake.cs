using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAwake<T> : MonoBehaviour where T : SingletonAwake<T>
{
    static private T _instance = null;
    public static T Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this.GetComponent<T>();
    }
}
