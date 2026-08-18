using System;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField] private GameObject ChickenPrefab;
    [SerializeField] private Transform ChickenSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeleteChicken()
    {
        Instantiate(ChickenPrefab, ChickenSpawn.position, Quaternion.identity);
    }
}
