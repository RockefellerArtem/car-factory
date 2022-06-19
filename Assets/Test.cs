using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private int _speed = 150;

    public int Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
            if (_speed >= 300)
            {
                _speed = 10000;
            }
        }
    }

    public int SpeedAuto { get; set; }
}
