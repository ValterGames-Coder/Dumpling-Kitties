using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public float TimeToFinish;
    [SerializeField] private List<GameObject> _moneySets = new List<GameObject>();

    private void Start()
    {
        _moneySets[Random.Range(0, _moneySets.Count)].SetActive(true);
    }
}
