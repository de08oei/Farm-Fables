using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Video;

public class MinigameSelectScript : MonoBehaviour
{
    public GameObject introVideo;
    public GameObject selection;

    private void Awake()
    {
        introVideo.SetActive(false);
        selection.SetActive(false);
    }

    private void OnEnable()
    {
        introVideo.SetActive(true);
    }

}
