using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorrectFeedback : MonoBehaviour
{
    public GameObject[] tileSlideshow;
    int currentSlide = 0;
    public AudioSource PosFeedback;
    public AudioSource correctSound;
    public AudioSource winMusic;
    public Animator standAnimator;
    bool VOPlaying = false;
    public GameObject goodJob;

    public Button playPauseButton;
    public Sprite playSprite;
    public Sprite pauseSprite;

    public Button nextButton;
    public GameObject rightButton;
    public GameObject leftButton;

    public GameManaging managerScript;

    bool playedThroughOnce;

    bool[] tilesPlayed;

    private void Awake()
    {
        managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManaging>();
        playedThroughOnce = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < tileSlideshow.Length; i++)
        {
            tileSlideshow[i].SetActive(false);
        }
        currentSlide = 0;
    }

    void HideCurrentSlide()
    {
        tileSlideshow[currentSlide].SetActive(false);
    }

    void ShowCurrentSlide()
    {
        tileSlideshow[currentSlide].SetActive(true);
    }

    public void ShowNextSlide()
    {
        leftButton.SetActive(true);
        if (currentSlide == 2)
        {
            rightButton.SetActive(false);
        }
        HideCurrentSlide();
        currentSlide++;
        ShowCurrentSlide();
        playPauseButton.Select();
    }

    public void ShowPrevSlide()
    {
        rightButton.SetActive(true);
        if(currentSlide == 1)
        {
            leftButton.SetActive(false);
        }
        HideCurrentSlide();
        currentSlide--;
        ShowCurrentSlide();
    }

    public void HideFeedback()
    {
        goodJob.SetActive(false);
    }

    public void ChangeVOStatus()
    {
        AudioSource currentAudio = tileSlideshow[currentSlide].GetComponent<AudioSource>();
        if (VOPlaying)
        {
            currentAudio.Pause();
            playPauseButton.GetComponent<Image>().sprite = playSprite;
        } else
        {
            currentAudio.Play();
            playPauseButton.GetComponent<Image>().sprite = pauseSprite;
            tilesPlayed[currentSlide] = true;
            if (currentSlide == 3)
            {
                nextButton.interactable = true;
                managerScript.SetPlayedThroughOnce(true);
            }
        }
        VOPlaying = !VOPlaying;
    }

    private void OnEnable()
    {
        playedThroughOnce = managerScript.GetPlayedThroughOnce();
        if (!playedThroughOnce)
        {
            StartAtBeginning();
        } else
        {
            Start();
            ShowCurrentSlide();
            leftButton.SetActive(false);
            rightButton.SetActive(true);
            rightButton.GetComponent<Button>().interactable = true;
            tilesPlayed[3] = true;
        }
        playPauseButton.Select();
    }

    private void StartAtBeginning()
    {
        StartCoroutine(PlayPosFeedback());
        correctSound.Play();
        winMusic.Play();
        nextButton.interactable = false;
        leftButton.SetActive(false);
        playPauseButton.interactable = false;
        rightButton.GetComponent<Button>().interactable = false;
        tilesPlayed = new bool[] { false, false, false, false };
    }

    IEnumerator PlayPosFeedback()
    {
        PosFeedback.Play();
        yield return new WaitForSeconds(PosFeedback.clip.length);
        playPauseButton.interactable = true;
    }

    private void Update()
    {
        if (tileSlideshow[currentSlide].GetComponent<AudioSource>().isPlaying || PosFeedback.isPlaying)
        {
            standAnimator.SetBool("isTalking", true);
        } else
        {
            standAnimator.SetBool("isTalking", false);
            playPauseButton.GetComponent<Image>().sprite = playSprite;
        }

        rightButton.GetComponent<Button>().interactable = tilesPlayed[currentSlide];
        playPauseButton.Select();
    }
}
