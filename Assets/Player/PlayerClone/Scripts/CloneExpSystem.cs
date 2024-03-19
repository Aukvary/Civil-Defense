using UnityEngine; 

class CloneExpSystem : MonoBehaviour
{
    private LevelController _levelController;

    public LevelController levelController
    {
        get => _levelController;

        set
        {
            if(value != null) 
            { 
                _levelController = value;
            }
        }
    }

    public void AddExp(int exp) =>
        _levelController.AddExp(exp);
}