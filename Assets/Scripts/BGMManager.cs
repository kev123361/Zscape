using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{

    public AudioClip[] bgm;
    private BoardManager board;
    private AudioSource audioPlayer;


    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        board = GameObject.Find("Board").GetComponent<BoardManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playTrack(int i)
    {
        audioPlayer.clip = bgm[i];
        audioPlayer.Play();
    }

    public void checkLevel()
    {
        if (board.level == 4)
        {
            playTrack(2);
        }
    }
}
