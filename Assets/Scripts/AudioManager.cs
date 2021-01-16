using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource = null;
    // Start is called before the first frame update
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("MusicVol", 0.5f);
        audioSource.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
