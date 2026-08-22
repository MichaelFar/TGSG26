using UnityEngine;

public class ChickenCounter : MonoBehaviour
{
    public static ChickenCounter Instance;
    private int ChickensCollected;
    [SerializeField] GameObject CaveWall;
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
        if (ChickensCollected == 4)
        {
            CaveWall.SetActive(false);
        }
    }

    public int GetChickensCollected()
    {
        return ChickensCollected;
    }
}
