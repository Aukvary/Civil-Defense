using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Trigger<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public event Action OnAddEvent;

    public event Action OnRemoveEvent;

    private List<T> _objects = new List<T>();

    public List<T> objects => _objects;

    public int count => _objects.Count;

    public T GetRandom()
    {
        if (_objects.Count == 0) return null;

        return _objects[Random.Range(0, _objects.Count - 1)];
    }

    public void Clear() =>
        _objects.Clear();

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<T>();

        if (obj == null || other.isTrigger) return;

        _objects.Add(obj);
        OnAddEvent?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        var obj = other.GetComponent<T>();

        if (obj == null || other.isTrigger) return;
        _objects.Remove(obj);
        OnRemoveEvent?.Invoke();
    }
}