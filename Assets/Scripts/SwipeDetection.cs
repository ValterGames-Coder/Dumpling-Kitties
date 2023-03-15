using System;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public static event Action<Vector2> SwipeEvent;

    private Vector2 _tapPosition;
    private Vector2 _swipeDelta;

    private float _deadZone = 80;

    private bool _isSwiping;
    private bool _isMobile;

    private void Start()
    {
        _isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if (_isMobile == false)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SwipeEvent?.Invoke(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                SwipeEvent?.Invoke(Vector2.right);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                SwipeEvent?.Invoke(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                SwipeEvent?.Invoke(Vector2.down);
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _isSwiping = true;
                    _tapPosition = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                    ResetSwipe();
            }
        }

        CheckSwipe();
    }

    private void ResetSwipe()
    {
        _isSwiping = false;
        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }

    private void CheckSwipe()
    {
        _swipeDelta = Vector2.zero;

        if (_isSwiping)
        {
            if (_isMobile && Input.touchCount > 0)
                _swipeDelta = Input.GetTouch(0).position - _tapPosition;
        }

        if (_swipeDelta.magnitude > _deadZone)
        {
            if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                SwipeEvent?.Invoke(_swipeDelta.x > 0 ? Vector2.right : Vector2.left);
            else 
                SwipeEvent?.Invoke(_swipeDelta.y > 0 ? Vector2.up : Vector2.down);
        }
    }
}