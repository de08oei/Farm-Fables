using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class Video_Controller : MonoBehaviour
{
    public VideoPlayer myVideoPlayer;
    public GameObject selectionPage;
    public GameObject selfPage;

    void Start()
    {
        StartCoroutine(PlayIntro());
    }

    void GoToSelection()
    {
        selectionPage.SetActive(true);
        selfPage.SetActive(false);
    }

    IEnumerator PlayIntro()
    {
        myVideoPlayer.Play();
        yield return new WaitForSeconds(34f);
        GoToSelection();
    }

    public void SkipIntro()
    {
        myVideoPlayer.Pause();
        GoToSelection();
    }
}