using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeManager : HalfSingleMono<TimeManager>
{
    //[SerializeField] private Light2D lit;
    //[SerializeField] private Gradient gradient;
    private float _globalTime;
    private int _day;
    /*public float GlobalTime
    {
        get => _globalTime;
        set
        {
            if (_globalTime >= 1)
            {
                value = 0;
            }
            if (_globalTime != value)
            {
                _globalTime = value;
                lit.color = gradient.Evaluate(_globalTime);
            }
        }
    }*/
    public int Day
    {
        get => _day;
        set
        {

        }
    }

   /* private void Update()
    {
        GlobalTime += Time.deltaTime / 200f;
    }*/

}
