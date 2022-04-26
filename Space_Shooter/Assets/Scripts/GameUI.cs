using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    public TMP_Text _score;
    public TMP_Text _lives;
    public Button _playButton;

    //Game Over panel

    public GameObject _gameOverpanel;
    public TMP_Text _finalScore;



    public static event Base _Start;
    public void OnEnable()
    {
        GameManager._ScoreUpdate += UpdateScore;
        GameManager._LiveUpdate += UpdateLives;
        GameManager._FinalScoreUpdate += EndGame;



    }
    public void OnDisable()
    {

        GameManager._ScoreUpdate -= UpdateScore;
        GameManager._LiveUpdate -= UpdateLives;
        GameManager._FinalScoreUpdate -= EndGame;

    }
    private void UpdateScore(int score)
    {
        _score.text = score.ToString();
    }
    private void UpdateLives(int lives)
    {
        Debug.Log("Update Lives");
        _lives.text = lives.ToString();

    }

    public void StartGame()
    {
        _Start();
        _playButton.gameObject.SetActive(false);
    }

    public void EndGame(int score)
    {
        _gameOverpanel.SetActive(true);
        _finalScore.text = score.ToString();
    }

}
