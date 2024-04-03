using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    [SerializeField] private GameObject _descriptingUI;

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
            _descriptingUI.SetActive(true);
        else
            _descriptingUI.SetActive(false);
    }
}