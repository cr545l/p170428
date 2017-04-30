using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupUpgrade : MonoBehaviour
{
    public void InvincibilityButtonClick()
    {
        print("invincibility");
        //GameManager.Instance.PlayerActor.InvokePowerModeDurationUpdate();

        GameManager.Instance.CurrentActorSpeed -= GameConst._DEFAULT_ACTOR_SPEED_SLOW_UPGRADE;
        GameManager.Instance.InvokePause(false);
        gameObject.SetActive(false);
    }

    public void HpButtonClick()
    {
        print("hp");
        GameManager.Instance.PlayerActor.MaxHealthPoint++;
        GameManager.Instance.PlayerActor.InvokeHealthRecovery();
        GameManager.Instance.InvokePause(false);
        gameObject.SetActive(false);
    }

    public void AttackButton()
    {
        print("attack");
        GameManager.Instance.PlayerActor.InvokeDamageUpdate();
        GameManager.Instance.InvokePause(false);
        gameObject.SetActive(false);
    }
}
