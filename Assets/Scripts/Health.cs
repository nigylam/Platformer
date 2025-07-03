using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action Dead;

    public int Current
    {
        get { return _current; }
        private set
        {
            _current = value;

            if (_current > _max)
            {
                _current = _max;
            }
            else if (_current <= 0)
            {
                Dead.Invoke();
            }
        }
    }

    private int _max;
    private int _current;

    public void Set(int start = 1, int max = 1)
    {
        _current = start;
        _max = max;
    }

    public void Decrease(int amount) => Current -= amount;
    public bool CanIncrease(int amount)
    {
        if (Current < _max)
        {
            Current += amount;
            
            return true;
        }

        return false;
    }
}
