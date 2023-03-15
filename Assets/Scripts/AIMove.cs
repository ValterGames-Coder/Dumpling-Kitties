using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    public List<Transform> path;
    public float MoveSpeed, _rotateSpeed;
    [SerializeField] private float _waitTime;
    private float _simpleSpeed;
    private Coroutine moveCoroutine;
    private void Start()
    {
        moveCoroutine = StartCoroutine(Move());
        _simpleSpeed = MoveSpeed;
    }

    private IEnumerator Move()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        for (int i = 0; i < path.Count; i++)
        {
            Quaternion pathRotate = Quaternion.LookRotation(transform.forward,
                path[i].position - transform.position);
            while (Vector2.Distance(transform.position, path[i].position) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    path[i].position, MoveSpeed * Time.deltaTime);
                
                transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                    pathRotate, _rotateSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        GetComponent<SpriteRenderer>().enabled = false;
        transform.position = path[0].position;
        yield return new WaitForSeconds(_waitTime);
        StartCoroutine(Move());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        MoveSpeed = 0;
    }
}
