using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    private int score = 0;
    private int streak = 0;
    private int maxStreak = 0;
    private int correctAnswers = 0;
    private int wrongAnswers = 0;
    private string objectiveCountry = "NA";

    [SerializeField] private float textTimeOnScreen = 3f;
    private float timeToDissapear = 0f;

    public TMP_Text scoreTxt;
    public TMP_Text streakTxt;
    public TMP_Text objectiveTxt;
    public TMP_Text correctTxt;
    public TMP_Text wrongTxt;

    // Start is called before the first frame update
    void Start()
    {
        objectiveTxt.text = "Objective: " + objectiveCountry;
        scoreTxt.text = "Score: " + score;
        streakTxt.text = "Streak: " + streak;

        //disable correctTxt and wrongTxt
        correctTxt.enabled = false;
        wrongTxt.enabled = false;
    }

    private void Update()
    {
        if ((correctTxt.enabled || wrongTxt.enabled) && Time.time > timeToDissapear)
        {
            correctTxt.enabled = false;
            wrongTxt.enabled = false;
        }
    }


    public void CorrectCountry()
    {
        score += 100 + 50*streak;

        scoreTxt.text = "Score: " + score;

        timeToDissapear = Time.time + textTimeOnScreen;
        correctTxt.enabled = true;
        correctTxt.text = "Correct! + " + (100 + 50 * streak) + " points";


        streak++;
        correctAnswers++;

        if (streak > maxStreak)
            maxStreak = streak;

        streakTxt.text = "Streak: " + streak;
    }

    public void WrongCountry()
    {
        streak = 0;
        score -= 20;
        wrongAnswers++;

        if (score < 0)
            score = 0;

        scoreTxt.text = "Score: " + score;

        timeToDissapear = Time.time + textTimeOnScreen;
        wrongTxt.enabled = true;
        wrongTxt.text = "Wrong! - 20 points";
        streakTxt.text = "Streak: " + streak;
    }

    public void SkipCountry()
    {
        streak = 0;
        score -= 50;
        wrongAnswers++;

        if (score < 0)
            score = 0;

        scoreTxt.text = "Score: " + score;

        timeToDissapear = Time.time + textTimeOnScreen;
        wrongTxt.enabled = true;
        wrongTxt.text = "Skipped! - 50 points";
        streakTxt.text = "Streak: " + streak;
    }

    public void NewObjective(string country)
    {
        objectiveCountry = country;
        objectiveTxt.text = "Objective: " + objectiveCountry;
    }
}
