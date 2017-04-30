using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAnalyst
{
    static private GameAnalyst _instance = null;

    static public GameAnalyst Instance
    {
        get
        {
            if(null == _instance)
            {
                _instance = new GameAnalyst();
                _instance.Init();
            }

            return _instance;
        }
    }

    public enum eType
    {
        Play_Time,
        Missile_Launch_Count,
        Damage_Upgrade_Count,

    }

    public int BestScore { get { return _bestScore; } }

    private int _bestScore = 0;
    
    private void Init()
    {
        _bestScore = PlayerPrefs.GetInt( PlayerPrefsKey._BEST_SCORE );
    }

    public void SaveScore( int value )
    {
        if( _bestScore < value )
        {
            _bestScore = value;
            PlayerPrefs.SetInt( PlayerPrefsKey._BEST_SCORE, _bestScore );
            PlayerPrefs.Save();
        }
    }

    public void AddCount()
    {

    }

}
