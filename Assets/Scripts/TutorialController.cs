using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tutorials;

    private int _pageIndex = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        foreach (var tut in _tutorials)
        {
            tut.SetActive(false);
        }
        _tutorials[_pageIndex].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _tutorials[_pageIndex].SetActive(false);
            _pageIndex++;

            if (_pageIndex >= _tutorials.Count)
                SceneManager.LoadScene(1);
            else
            {
                print(_pageIndex);
                _tutorials[_pageIndex].SetActive(true);
            }
        }
    }
}
