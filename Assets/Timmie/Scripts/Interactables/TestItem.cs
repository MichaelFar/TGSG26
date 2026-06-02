using UnityEngine;
using UnityEngine.Events;

/*
Contributor(s): Timmie Xiong
Brief Description: Test item for interaction
Date: 6/1/26
*/
public class TestItem : MonoBehaviour, IInteractable
{
    public UnityEvent itemPickUp;

    public void OnInteract(GameObject interacting_object)
    {
        print("Picked up item");
        itemPickUp.Invoke();
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
