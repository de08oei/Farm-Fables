using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
    public GameObject titlePage;
    public GameObject gamePage;
    public GameObject creditsPage;
    public GameObject minigameSelectorPage;
    public GameObject selectionButtonPage;
    public GameObject correctFeedbackPage;
    public GameObject introVideo;
    public AudioSource clickStartVO;
    public AudioSource helpTasiaVO;

    public GameManaging managerScript;

    IEnumerator HelpTasia()
    {
        helpTasiaVO.Play();
        yield return new WaitForSeconds(helpTasiaVO.clip.length);
        ToGame();
    }

    public void ToTasiaGame()
    {
        StartCoroutine(HelpTasia());
    }

    public void ToGame()
    {
        titlePage.SetActive(false);
        gamePage.SetActive(true);
        creditsPage.SetActive(false);
        correctFeedbackPage.SetActive(false);
        minigameSelectorPage.SetActive(false);
    }

    public void ToCredits()
    {
        titlePage.SetActive(false);
        gamePage.SetActive(false);
        creditsPage.SetActive(true);
        correctFeedbackPage.SetActive(false);
        minigameSelectorPage.SetActive(false);
    }

    public void ToTitle()
    {
        titlePage.SetActive(true);
        gamePage.SetActive(false);
        creditsPage.SetActive(false);
        correctFeedbackPage.SetActive(false);
        minigameSelectorPage.SetActive(false);
        clickStartVO.Play();
    }

    public void ToMinigameSelector()
    {
        gamePage.SetActive(false);
        creditsPage.SetActive(false);
        correctFeedbackPage.SetActive(false);
        minigameSelectorPage.SetActive(true);
        if (managerScript.GetPlayedIntroOnce())
        {
            introVideo.SetActive(false);
        }
        titlePage.SetActive(false);
    }

    public void BackToMinigameSelection()
    {
        titlePage.SetActive(false);
        gamePage.SetActive(false);
        creditsPage.SetActive(false);
        correctFeedbackPage.SetActive(false);
        minigameSelectorPage.SetActive(true);
        introVideo.SetActive(false);
        selectionButtonPage.SetActive(true);
    }

    private void Awake()
    {
        ToTitle();
        correctFeedbackPage.SetActive(false);
        managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManaging>();
    }
}
