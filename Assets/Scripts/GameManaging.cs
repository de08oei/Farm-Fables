using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManaging : MonoBehaviour
{
    bool soundOn = true;
    bool playedThroughOnce;
    bool playedIntroOnce;

    public Texture2D pointerCursor;
    public Texture2D grabCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Button titleSB;
    public Button minigameSelectSB;
    public Button gameSB;
    public Button correctFeedbackSB;
    public Button creditsSB;
    public Button videoSB;
    public GameObject videoCanvas;
    public VideoPlayer introVidPlayer;

    private void Start()
    {
        playedThroughOnce = false;
        playedIntroOnce = false;
    }

    public bool GetPlayedThroughOnce()
    {
        return playedThroughOnce;
    }

    public void SetPlayedThroughOnce(bool toSet)
    {
        playedThroughOnce = toSet;
    }

    public bool GetPlayedIntroOnce()
    {
        return playedIntroOnce;
    }

    public void SetPlayedIntroOnce(bool toSet)
    {
        playedIntroOnce = toSet;
    }

    public void SwitchSound()
    {
        if (soundOn)
        {
            AudioListener.volume = 0;
            titleSB.GetComponent<Image>().sprite = soundOffSprite;
            minigameSelectSB.GetComponent<Image>().sprite = soundOffSprite;
            gameSB.GetComponent<Image>().sprite = soundOffSprite;
            correctFeedbackSB.GetComponent<Image>().sprite = soundOffSprite;
            creditsSB.GetComponent<Image>().sprite = soundOffSprite;
            videoSB.GetComponent<Image>().sprite = soundOffSprite;
            if (videoCanvas.activeSelf)
            {
                introVidPlayer.SetDirectAudioMute(0, true);
            }
        } else
        {
            AudioListener.volume = 1;
            titleSB.GetComponent<Image>().sprite = soundOnSprite;
            minigameSelectSB.GetComponent<Image>().sprite = soundOnSprite;
            gameSB.GetComponent<Image>().sprite = soundOnSprite;
            correctFeedbackSB.GetComponent<Image>().sprite = soundOnSprite;
            creditsSB.GetComponent<Image>().sprite = soundOnSprite;
            videoSB.GetComponent<Image>().sprite = soundOnSprite;
            if (videoCanvas.activeSelf)
            {
                introVidPlayer.SetDirectAudioMute(0, false);
            }
        }
        soundOn = !soundOn;
    }
}
