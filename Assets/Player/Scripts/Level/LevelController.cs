using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private RectTransform _valueUI;
    [SerializeField] private TextMeshProUGUI _currentLevelUI;
    [SerializeField] private Color _boarderColor;

    [Space]

    [SerializeField] private List<Image> _boards;
    [SerializeField] private List<RectTransform> _skillsProgress;
    [SerializeField] private List<GameObject> _skillSblock;

    [Space]

    [SerializeField] private Image _ultiSkillBoarder;
    [SerializeField] private RectTransform _ultiProgress;
    [SerializeField] private GameObject _ultiBlock;

    [Space]

    [SerializeField] private List<int> _needExp = new List<int>();

    public List<bool> _enabled = new List<bool>(new bool[]{false, false, false, false});

    [Range(0, 15)] private int _level = 0;

    private float _currentExp;

    private int _skillPoints = 1;

    private int _ultiPoints;

    private ExplosionDomeSkill _explosionDomeSkill;
    private MissDomeSkill _missDomeSkill;
    private ExtraAttackSkill _extraAttackSkill;
    private DarkMirrorSkill _darkMirrorSkill;

    private List<Skill> _skills = new List<Skill>();

    private PlayerHealth _playerHealth;
    private AttackController _attackController;

    public int level
    {
        get => _level;

        set
        {
            if (value > 16)
                return;
            _level = value;
            DrawUI();
        }
    }

    private int needExp => _needExp[level];

    private bool canUpgradeSkill => _skillPoints > 0;

    private bool canUpgradeUlti => _ultiPoints > 0;

    private void Awake()
    {
        _explosionDomeSkill = GetComponent<ExplosionDomeSkill>();
        _missDomeSkill = GetComponent<MissDomeSkill>();
        _extraAttackSkill = GetComponent<ExtraAttackSkill>();
        _darkMirrorSkill = GetComponent<DarkMirrorSkill>();

        _skills.Add(_explosionDomeSkill);
        _skills.Add(_missDomeSkill);
        _skills.Add(_extraAttackSkill);

        _playerHealth = GetComponentInParent<PlayerHealth>();
        _attackController = GetComponent<AttackController>();
    }

    private void Start()
    {
        _level = 0;
        _explosionDomeSkill.enabled = false;
        _missDomeSkill.enabled = false;
        _extraAttackSkill.enabled = false;
        _darkMirrorSkill.enabled = false;
        DrawUI();
    }

    private void DrawUI()
    {
        _currentLevelUI.text = (level + 1).ToString();

        _valueUI.anchorMax = new Vector2(1, _currentExp / needExp);

        DrawSkills();
        DrawUlti();
    }

    private void DrawSkills()
    {
        for (int i = 0; i < _skills.Count; i++)
        {
            if (!_skills[i].enabled)
            {
                _skillSblock[i].SetActive(true);
                _skillsProgress[i].anchorMax = new Vector2(1.3f, 0);
            }
            else
            {
                _skillSblock[i].SetActive(false);
                float cur = _skills[i].currentLevel;
                _skillsProgress[i].anchorMax = new Vector2(1.3f, (cur + 1) / 4);
            }

            if (_skills[i].currentLevel == 3)
            {
                _boards[i].color = Color.white;
                continue;
            }
            if (_skillPoints == 0)
            {
                _boards[i].color = Color.white;
            }
            else
            {
                _boards[i].color = _boarderColor;
            }
        }
    }

    private void DrawUlti()
    {
        if (!_darkMirrorSkill.enabled)
        {
            _ultiBlock.SetActive(true);
            _ultiProgress.anchorMax = new Vector2(1.3f, 0);
        }
        else
        {
            _ultiBlock.SetActive(false);
            float cur = _darkMirrorSkill.currentLevel;
            _ultiProgress.anchorMax = new Vector2(1.3f, (cur + 1) / 4);
        }

        if (_darkMirrorSkill.currentLevel == 3)
        {
            _ultiSkillBoarder.color = Color.white;
            return;
        }
        if (_ultiPoints == 0)
        {
            _ultiSkillBoarder.color = Color.white;
        }
        else
        {
            _ultiSkillBoarder.color = _boarderColor;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            AddExp(needExp);
        DrawSkills();
        UpgradeSkill();
        UpgradeUlti();
    }

    private void UpgradeSkill()
    {
        if (!canUpgradeSkill)
            return;
        foreach(Skill skill in _skills)
        {
            if (!Input.GetKeyDown(skill.actionKey) || !Input.GetKey(KeyCode.LeftAlt))
                continue;
            if (skill.currentLevel == 3)
                continue;
            if (skill.enabled)
            {
                skill.currentLevel++;
            }
            else
            {
                skill.currentLevel = 0;
                skill.enabled = true;
                _enabled[_skills.IndexOf(skill)] = true;
            }
            _skillPoints--;
        }
        DrawUI();
    }

    private void UpgradeUlti()
    {
        if (!canUpgradeUlti)
            return;
        if (!Input.GetKeyDown(_darkMirrorSkill.actionKey) || !Input.GetKey(KeyCode.LeftAlt))
            return;
        if (_darkMirrorSkill.currentLevel == 3)
            return;
        if (_darkMirrorSkill.enabled)
        {
            _darkMirrorSkill.currentLevel++;
        }
        else
        {
            _darkMirrorSkill.currentLevel = 0;
            _darkMirrorSkill.enabled = true;
            _enabled[3] = true;
        }
        _ultiPoints--;
        DrawUI();
    }

    public void AddExp(int exp)
    {
        if (level == 15)
            return;
        _currentExp += exp;
        DrawUI();

        if (_currentExp < needExp)
            return;
        _currentExp -= _needExp[level];
        level += 1;
        if ((level + 1) % 4 == 0)
            _ultiPoints++;
        else
            _skillPoints++;
        _playerHealth.currentLevel = level;
        _attackController.currentLevel = level;
        DrawUI();
    }
}
