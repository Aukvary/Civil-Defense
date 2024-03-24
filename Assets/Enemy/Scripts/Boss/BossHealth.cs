using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : EnemyHealth
{ 
    public override void Die()
    {
        Invoke("ChangeScene", 4);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(2);
    }

}
