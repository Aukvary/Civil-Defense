using UnityEngine;

public class HealAura : MonoBehaviour
{
    [SerializeField] private float _healCount;

    public float healCount => _healCount;
}