using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ActionSkill : Skill
{
    [SerializeField] private List<float> _coolDownTimes;

    [SerializeField] private Camera _camera;

    [Header("UI")]
    [SerializeField] private RectTransform _coolDownUI;
    [SerializeField] private TextMeshProUGUI _timer;

    public bool IsTaget;

    protected bool keyIsPressed => Input.GetKeyDown(actionKey) && !Input.GetKey(KeyCode.LeftControl);

    protected Ray viewDirection => _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

    protected float coolDownTime => _coolDownTimes[currentLevel];

    private float _currentCoolDownTime;

    protected void Start()
    {
        _currentCoolDownTime = coolDownTime;
        DrawUI();
    }

    protected override void Update()
    {
        if (_currentCoolDownTime < coolDownTime)
        {
            _currentCoolDownTime += Time.deltaTime;
            DrawUI();
            return;
        }
        _timer.text = "";
        if (!keyIsPressed || !IsTaget)
            return;
        UseSkill();
        _currentCoolDownTime = 0;
    }

    protected void DrawUI()
    {
        float value = 1 - (_currentCoolDownTime / coolDownTime);

        int time = (int)(coolDownTime - (coolDownTime * (_currentCoolDownTime / coolDownTime)));

        _timer.text = time.ToString();

        _coolDownUI.anchorMax = new Vector2(1, value);
    }
}
