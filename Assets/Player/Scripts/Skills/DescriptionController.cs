using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    [SerializeField] private GameObject _descriptingUI;

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl))
            _descriptingUI.SetActive(true);
        else
            _descriptingUI.SetActive(false);
    }
}