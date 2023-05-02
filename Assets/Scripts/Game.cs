using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Game : MonoBehaviour
{
    public GameObject storyPanel;
    GameObject[] storyTiles;

    public GameObject instructions;
    public GameObject instructions2;
    public GameObject instructions3;
    public GameObject instructions4;
    public GameObject rightFeedback;
    public GameObject wrongFeedback;
    public GameObject hint;
    public Button doneButton;

    public GameObject gameSelf;

    public GameObject tasiaStand;
    public GameObject tasiaThink;

    public AudioSource instructionsVO1;
    public AudioSource instructionsVO2;
    public AudioSource instructionsVO3;
    public AudioSource negFeedback;
    public AudioSource farmAmbience;
    public AudioSource incorrectSound;
    AudioSource currentAudio;
    
    public Animator standAnimator;
    public Animator thinkAnimator;

    private int validateCount = 0;

    bool[] tileCorrectness;

    public void ValidatePanel()
    {
        StopCoroutine(nameof(PlayInstructions));
        currentAudio.Pause();
        validateCount++;
        RemoveDialogue();
        for (int i = 0; i < 4; i++)
        {
            storyTiles[i] = storyPanel.transform.GetChild(i).gameObject;
        }

        storyTiles = storyTiles.OrderBy(tile => tile.transform.position.x).ToArray<GameObject>();
        
        ResetResults();

        bool val1 = storyTiles[0].CompareTag("First") || storyTiles[0].CompareTag("Locked");
        bool val2 = storyTiles[1].CompareTag("Second") || storyTiles[1].CompareTag("Locked");
        bool val3 = storyTiles[2].CompareTag("Third") || storyTiles[2].CompareTag("Locked");
        bool val4 = storyTiles[3].CompareTag("Fourth") || storyTiles[3].CompareTag("Locked");

        tileCorrectness[0] = val1;
        tileCorrectness[1] = val2;
        tileCorrectness[2] = val3;
        tileCorrectness[3] = val4;

        if (val1 && val2 && val3 && val4)
        {
            ShowRFeedback();
            return;
        } else
        {
            incorrectSound.Play();
            GameObject rvc1 = ShowResult(storyTiles[0], val1);
            GameObject rvc2 = ShowResult(storyTiles[1], val2);
            GameObject rvc3 = ShowResult(storyTiles[2], val3);
            GameObject rvc4 = ShowResult(storyTiles[3], val4);

            StartCoroutine(RemoveRedVC(rvc1, rvc2, rvc3, rvc4));

            if (validateCount >= 2)
            {
                wrongFeedback.SetActive(false);
                hint.SetActive(true);
            }
            else
            {
                ShowWFeedback();
            }
            return;
        }
    }

    // Shows if the tile is in the correct spot or not. Returns the game object
    // of the red visual cue for the given tile.
    GameObject ShowResult(GameObject tile, bool result)
    {
        GameObject correctSym = tile.transform.GetChild(1).gameObject;
        GameObject correctOutline = tile.transform.GetChild(0).gameObject;
        GameObject incorrectSym = tile.transform.GetChild(3).gameObject;
        GameObject incorrectOutline = tile.transform.GetChild(2).gameObject;
        if (result)
        {
            correctSym.SetActive(true);
            correctOutline.SetActive(true);
            tile.tag = "Locked";
        } else
        {      
            incorrectSym.SetActive(true);
            incorrectOutline.SetActive(true);
        }

        return incorrectSym;
    }

    IEnumerator RemoveRedVC(GameObject sym1, GameObject sym2, GameObject sym3, GameObject sym4)
    {
        yield return new WaitForSeconds(6.5f);
        sym1.SetActive(false);
        sym2.SetActive(false);
        sym3.SetActive(false);
        sym4.SetActive(false);
    }

    void ResetResults()
    {
        foreach (GameObject tile in storyTiles)
        {
            tile.transform.GetChild(0).gameObject.SetActive(false);
            tile.transform.GetChild(1).gameObject.SetActive(false);
            tile.transform.GetChild(2).gameObject.SetActive(false);
            tile.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        ShowInstructions();
        EnableTasiaStand();
        hint.SetActive(false);
        tileCorrectness = new bool[] { false, false, false, false};
        storyTiles = new GameObject[4];
    }

    void ShowInstructions()
    {
        instructions.SetActive(true);
        instructions2.SetActive(false);
        instructions3.SetActive(false);
        instructions4.SetActive(false);
        rightFeedback.SetActive(false);
        wrongFeedback.SetActive(false);
    }

    void RemoveDialogue()
    {
        instructions.SetActive(false);
        instructions2.SetActive(false);
        instructions3.SetActive(false);
        instructions4.SetActive(false);
        rightFeedback.SetActive(false);
        wrongFeedback.SetActive(false);
    }

    void ShowRFeedback()
    {
        rightFeedback.SetActive(true);
        gameSelf.SetActive(false);
    }

    void ShowWFeedback()
    {
        RemoveDialogue();
        rightFeedback.SetActive(false);
        wrongFeedback.SetActive(true);
        EnableTasiaThink();
    }

    void EnableTasiaStand()
    {
        tasiaStand.SetActive(true);
        tasiaThink.SetActive(false);
    }

    void EnableTasiaThink()
    {
        tasiaStand.SetActive(false);
        tasiaThink.SetActive(true);
        StartCoroutine(nameof(PlayNegFeedback));
    }

    private void OnEnable()
    {
        EnableTasiaStand();
        StartCoroutine(nameof(PlayInstructions));
        farmAmbience.Play();
    }

    IEnumerator PlayInstructions()
    {
        RemoveDialogue();
        doneButton.interactable = false;
        instructions.SetActive(true);
        standAnimator.SetBool("isTalking", true);
        instructionsVO1.Play();
        currentAudio = instructionsVO1;
        yield return new WaitForSeconds(instructionsVO1.clip.length);
        instructions.SetActive(false);
        instructions2.SetActive(true);
        instructionsVO2.Play();
        currentAudio = instructionsVO2;
        yield return new WaitForSeconds(3);
        instructions2.SetActive(false);
        instructions3.SetActive(true);
        yield return new WaitForSeconds(instructionsVO2.clip.length - 2.5f);
        instructions3.SetActive(false);
        instructions4.SetActive(true);
        instructionsVO3.Play();
        currentAudio = instructionsVO3;
        yield return new WaitForSeconds(instructionsVO3.clip.length - 0.25f);
        standAnimator.SetBool("isTalking", false);
        doneButton.interactable = true;
    }

    IEnumerator PlayNegFeedback()
    {
        thinkAnimator.SetBool("isThinkTalk", true);
        negFeedback.Play();
        yield return new WaitForSeconds(negFeedback.clip.length - 1);
        thinkAnimator.SetBool("isThinkTalk", false);
    }
}
