using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Timer : MonoBehaviour
{
    [SerializeField] private List<Entrance> _entrances;
    private Entrance _currentEntrance;
    [SerializeField] private TMP_Text _timerText;
    private float time;
    public event Action<bool> TimerFinish;
    public bool TimeStop;

    void Start()
    {
        _currentEntrance = _entrances[Random.Range(0, _entrances.Count)];
        _currentEntrance.gameObject.SetActive(true);
        SetTimeEntrance();
    }

    public void SetTimeEntrance()
    {
        time = _currentEntrance.TimeToFinish;
    }

    private void Update()
    {
        if (time >= 0)
        {
            if (TimeStop == false)
            {
                time -= Time.deltaTime;
                _timerText.text = time.ToString("F2");
            }
        }
        else
        {
            TimerFinish?.Invoke(false);
        }
    }
}
