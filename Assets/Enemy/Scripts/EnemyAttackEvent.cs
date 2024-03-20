using System;
using UnityEngine;

public class EnemyAttackEvent : MonoBehaviour
{
    public event Action OnAttackEvent;

    public void AttackEvent()
    {
        print("che");
        OnAttackEvent?.Invoke();
    }
}