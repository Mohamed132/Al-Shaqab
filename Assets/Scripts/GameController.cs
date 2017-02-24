using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    [SerializeField]
    private Text RankText;
    [SerializeField]
    private Text TimerText;
    [SerializeField]
    private GameObject CongratesObject;
    [SerializeField]
    private HorseMovementController PlayerHorseController;
    [SerializeField]
    private HorseMovementController[] AIHorses;
    [SerializeField]
    float CountDownTime = 120;

    
    public static GameController singleton;
    private int Rank;
    // Use this for initialization
    [HideInInspector]
    public float GameTimer;
    private void Awake()
    {
        GameTimer = .01f;
        if(!singleton)
        {
            singleton = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTimer();
        UpdatePlayerRanking();
	}

    void UpdatePlayerRanking()
    {
        Rank = 1;

        foreach (var Ai in AIHorses)
        {
            if (Ai.transform.position.x > PlayerHorseController.transform.position.x)
            {
                Rank++;
            }
        }
        RankText.text = Rank + "/4";
    }

    void UpdateTimer()
    {
        GameTimer += Time.deltaTime;
        int NoOfSec = (int)(CountDownTime - GameTimer);
        if(NoOfSec < 0)
        {
            NoOfSec = 0;
        }
        TimerText.text = NoOfSec / 60 + ":" + NoOfSec % 60;
    }

    public void SetSolvingRate(float RateOfSolving)
    {
        PlayerHorseController.SetSpeed(RateOfSolving);
    }

    public void OnFinishPuzzle()
    {
        CongratesObject.SetActive(true);
    }
}
