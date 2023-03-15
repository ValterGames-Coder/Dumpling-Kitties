using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed, _rotateSpeed, _pushForce;
    private float _defaultSpeed;
    [SerializeField] private DrawLine _line;
    [SerializeField] private Timer _timer;
    [SerializeField] private GameObject _losePanel, _winPanel, _winParticle;
    [SerializeField] private Rigidbody2D _rigidbody;
    private Coroutine _moveToCoroutine;
    private List<Vector3> _path;
    private Vector3 currentPoint;
    private bool _canMove = true;
    public event Action StartMoveCamera, AddMoney;
    public event Action<bool> Finish;
    private void Start()
    {
        Time.timeScale = 1;
        _line.SetPath += GetPath;
        _timer.TimerFinish += StopMove;
        Finish += StopMove;
        _defaultSpeed = _moveSpeed;
    }

    private void GetPath(List<Vector3> path)
    {
        _path = path;
        _moveToCoroutine = StartCoroutine(MoveTo());
        StartMoveCamera?.Invoke();
    }

    private IEnumerator MoveTo()
    {
        for (int i = 0; i < _path.Count; i++)
        {
            currentPoint = _path[i];
            Quaternion pathRotate = Quaternion.LookRotation(transform.forward,_path[i] - transform.position);
            while (Vector2.Distance(transform.position, _path[i]) > 0.01f)
            {
                if (Input.GetMouseButton(0))
                    _moveSpeed = 0;
                else if (Input.GetMouseButtonUp(0) && _canMove)
                    _moveSpeed = _defaultSpeed;
                transform.position = Vector2.MoveTowards(transform.position,
                    _path[i], _moveSpeed * Time.deltaTime);
                
                transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                    pathRotate, _rotateSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        Finish?.Invoke(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector3 dir = (transform.position - currentPoint);
        _rigidbody.AddForce(dir.normalized * _pushForce , ForceMode2D.Impulse);
        Finish?.Invoke(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CityMoney"))
        {
            other.gameObject.SetActive(false);
            AddMoney?.Invoke();
        }
        if(other.GetComponent<Entrance>())
            Finish?.Invoke(true);
    }

    public void StopMove(bool finish)
    {
        StartCoroutine(StopMoveIE(finish));
    }

    private IEnumerator StopMoveIE(bool finish)
    {
        if(_moveToCoroutine != null)
            StopCoroutine(_moveToCoroutine);
        yield return new WaitForSeconds(1);
        _losePanel.SetActive(!finish);
        _winPanel.SetActive(finish);
        _canMove = false;
        AIMove[] cars = FindObjectsOfType<AIMove>();
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].MoveSpeed = 0;
        }
    }
}
