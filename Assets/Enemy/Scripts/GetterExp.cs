using UnityEngine;

class GetterExp : MonoBehaviour
{
	[SerializeField] private int _containsExp;
	[SerializeField] private PlayerTrigger _playerTrigger;

    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();

        _enemyHealth.OnHitEvent += GiveExp;
    }

    private void GiveExp()
    {
		if (_playerTrigger.count <= 0)
			return;

        foreach(var item in _playerTrigger.objects)
        {
            var clone = item.GetComponent<CloneExpSystem>();
            if(clone != null)
            {
                clone.AddExp(_containsExp);
                return;
            }

            var player = item.GetComponent<LevelController>();
            if(player != null)
            {
                player.AddExp(_containsExp);
                return;
            }
        }

    }
}