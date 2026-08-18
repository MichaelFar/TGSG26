using UnityEngine;

public class ChickenCounter : MonoBehaviour
{
    public static ChickenCounter Instance;
    private int ChickensCollected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddChicken()
    {
        ChickensCollected++;
        print("Added a chicken current chicken count: " + ChickensCollected);
    }

    public int GetChickensCollected()
    {
        return ChickensCollected;
    }
}
