using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIPath : MonoBehaviour
{
    public List<Transform> Points = new List<Transform>();
    [SerializeField] private AIMove _prefab;
    [SerializeField] private int _count;
    [SerializeField] private float _minWait, _maxWait;
    private Coroutine spawnCoroutine;

    private void Start()
    {
        spawnCoroutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _count; i++)
        {
            AIMove ai = Instantiate(_prefab, Points[0].position, Quaternion.identity);
            ai.path = Points;
            yield return new WaitForSeconds(Random.Range(_minWait, _maxWait));
        }
        StopCoroutine(spawnCoroutine);
    }

    private void OnDrawGizmos()
    {
        if(Points == null || Points.Count < 2)
            return;
        
        for (int i = 1; i < Points.Count; i++)
        {
            Gizmos.DrawLine(Points[i - 1].position, Points[i].position);
        }
    }
}
