using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawLine : MonoBehaviour
{
    private Vector3 mousePos;
    [SerializeField] private Line _linePrefab;
    [SerializeField] private Timer _timer;
    [SerializeField] private GameObject _cross;
    [SerializeField] private GameObject _acceptPanel;
    private GameObject _currentCross;
    private Line _currentLine;
    private Camera _camera;
    private bool _canDraw = true;
    private bool _acceptWindowActive;
    public const float RESOLUTION = 0.1f;
    public event Action<List<Vector3>> SetPath;

    void Awake ()
    {
        _camera = Camera.main;
        _timer.TimerFinish += finish => _canDraw = finish;
    }
    void Update ()
    {
        if (_canDraw && EventSystem.current.IsPointerOverGameObject() == false && _acceptPanel.activeInHierarchy == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _currentLine = Instantiate(_linePrefab, _camera.ScreenToWorldPoint(Input.mousePosition),
                    Quaternion.identity);
            }
            else if (Input.GetMouseButton(0) )
            {
                mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                _currentLine.SetPosition(mousePos);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _timer.TimeStop = true;
                _canDraw = false;
                _currentCross = Instantiate(_cross, (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition),
                    Quaternion.identity);
                if (_acceptWindowActive == false)
                {
                    _acceptPanel.SetActive(true);
                    _acceptWindowActive = true;
                }
                else
                    SetPath?.Invoke(_currentLine.points);
                
            }
        }
    }

    public void Accept()
    {
        SetPath?.Invoke(_currentLine.points);
        _acceptPanel.SetActive(false);
    }
    
    public void NotAccept()
    {
        _acceptPanel.SetActive(false);
        Destroy(_currentCross);
        Destroy(_currentLine.gameObject);
        _canDraw = true;
        _timer.TimeStop = false;
        _timer.SetTimeEntrance();
    }
}
