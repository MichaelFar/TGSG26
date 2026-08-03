using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PlayerGlobal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static PlayerGlobal _instance;

    
    public static PlayerGlobal Instance { get { return _instance; } }
    private void Awake()
    {
        
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        playerRootObject = gameObject;
        

    }
    [HideInInspector]
    public GameObject playerRootObject;

    public ScaredSense scareStingController;
}
