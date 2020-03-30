using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

    public AudioClip[] bgmClips;
    private int currentMusicIndex = 0;
    private AudioSource audioPlayer;

    void Start() {
        DontDestroyOnLoad(this.gameObject);
        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = bgmClips[currentMusicIndex];
        audioPlayer.Play();
    }

    void Update() {
        if(!audioPlayer.isPlaying) {
            if(currentMusicIndex == bgmClips.Length - 1) {
                currentMusicIndex = 0;
            } else {
                currentMusicIndex++;
            }
            audioPlayer.clip = bgmClips[currentMusicIndex];
            audioPlayer.Play();
        }
    }
}
