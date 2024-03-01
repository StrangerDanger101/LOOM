using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour {

    public AudioSource[] songs;

    void Start() {
        StartCoroutine(PlayAllSongs());
    }

    IEnumerator PlayAllSongs() {
        //plays all songs in a loop in order one by one
        for(int i = 0; i < songs.Length; i++) {
            songs[i].Play();
            yield return new WaitForSeconds(songs[i].clip.length);
        }
    }
}
