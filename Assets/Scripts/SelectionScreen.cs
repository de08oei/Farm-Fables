using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScreen : MonoBehaviour
{
    public AudioSource busInt;
    public GameManaging managerScript;

    private void Awake()
    {
        managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManaging>();
    }

    private void OnEnable()
    {
        busInt.Play();
        managerScript.SetPlayedIntroOnce(true);
    }
}
