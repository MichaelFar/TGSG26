using UnityEngine;

public class SubtitlePoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public SubtitleSequence subSequence;

    public bool triggerOnce = false;

    private SubtitleController subController;
    void Start()
    {
        subController = PlayerGlobal.Instance.subController;
    }

    // Update is called once per frame
    public void StartSubtitleSequence()
    {
        subController.ProcessSubtitleList(subSequence);
        subController.StartCoroutine(nameof(subController.ProcessSubtitleList), subSequence);
        if(triggerOnce)
        {
            enabled = false;
        }
    }
}
