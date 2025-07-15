using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action Dead;
    public event Action Changed;

    public int Current
    {
        get { return _current; }
        private set
        {
            _current = value;

            if (_current > Max)
            {
                _current = Max;
            }
            else if (_current <= 0)
            {
                _current = 0;
                Dead?.Invoke();
            }
        }
    }

    public int Max { get; private set; }

    private int _current;

    public void Set(int start = 1, int max = 1)
    {
        _current = start;
        Max = max;
    }

    public void Decrease(int amount)
    {
        Current -= amount;
        Changed?.Invoke();
    }
        

    public bool CanIncrease(int amount)
    {
        if (Current < Max)
        {
            Current += amount;
            Changed?.Invoke();

            return true;
        }

        return false;
    }
}
