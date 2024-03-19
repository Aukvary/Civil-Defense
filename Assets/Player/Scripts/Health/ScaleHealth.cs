using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ScaleHealth : HealthController
{
    [Header("UI")]
    [SerializeField] private RectTransform _healthUI;
    [SerializeField] private TextMeshProUGUI _textValue;

    [Space]
    [SerializeField] private List<float> _healths;

    [Range(0, 15)] private int _level = 0;
    private float _currentHealth;

    private bool _isUnDead;

    private float maxHealth => _healths[_level];
    public override float health
    {
        get => _currentHealth;
        protected set
        {
            _currentHealth = value;

            _currentHealth = Mathf.Clamp(health, 0, maxHealth);
            if (health == 0)
            {
                Die();
            }
            DrawUI();
        }
    }

    public virtual int currentLevel
    {
        get => _level;

        set
        {
            if (_level < value)
            {
                _level = value;
                health = _healths[_level];
                DrawUI();
            }
        }
    }

    private void Start()
    {
        _currentHealth = _healths[_level];
        health = _currentHealth;
    }

    protected void DrawUI()
    {
        _healthUI.anchorMax = new Vector2(health / maxHealth, 1);

        _textValue.text = $"{(int)health} / {(int)maxHealth}";
    }

    public override void DealDamage(float damage)
    {
        if (_isUnDead)
            return;
        health -= damage;

        DrawUI();
    }

    public void GetHeal(int heal)
    {
        health += heal;
        DrawUI();
    }

    private void OnTriggerEnter(Collider col)
    {
        var missDome = col.GetComponent<MissDome>();

        if (missDome != null)
        {
            _isUnDead = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        var missDome = col.GetComponent<MissDome>();

        if (missDome != null)
        {
            _isUnDead = false;
        }
    }
}