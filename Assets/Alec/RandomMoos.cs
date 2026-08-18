using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomMoos : MonoBehaviour
{
    public Collider Area;
    public GameObject Player;
    public List<AudioClip> audioClips;
    public AudioClip currentClip;
    public AudioSource source;
    public float minWaitBetweenPlays = 1f;
    public float maxWaitBetweenPlays = 5f;
    public float waitTimeCountdown = -1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);
        transform.position = closestPoint;
        
        if (!source.isPlaying)
        {
            if (waitTimeCountdown <0f)
            {
                currentClip = audioClips[Random.Range(0, audioClips.Count)];
                source.clip = currentClip;
                source.Play();
                waitTimeCountdown = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);

            }
            else
            {
                waitTimeCountdown -= Time.deltaTime;
            }
        }
    }
}
