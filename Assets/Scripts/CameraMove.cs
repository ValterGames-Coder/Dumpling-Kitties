using Cinemachine;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private PlayerMove _target;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private CinemachineBrain _brain;
    private Coroutine _cameraMoveCoroutine;
    [SerializeField] private float _cameraSize, _moveSize, _speed;

    private void Start()
    {
        _target.StartMoveCamera += Move;
    }

    private void Update()
    {
        if(_camera.m_Lens.OrthographicSize - _cameraSize > 0.1f)
            _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _cameraSize, _speed * Time.deltaTime);
    }

    private void Move()
    {
        _brain.enabled = true; 
        _camera.Follow = _target.transform;
        _cameraSize = _moveSize;
    }
}
