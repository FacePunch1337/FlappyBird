using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float difficulty;
    [SerializeField] private BirdController birdController;
    [SerializeField] private SpawnPipes spawnPipes;
    [SerializeField] private Parallax parallax;
    [SerializeField] private TMP_Text scoreCounter;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private Image hungryImage;
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject MenuUI;


    private int bestScore;
    private float hunger = 0.0003f;
    private float speed = 3f;
    private float currentTime;
    private bool startGame;
    private bool stopTimer;
    private bool isMenu;

    public Image HungryImage { get => hungryImage; set => hungryImage = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool StartGame { get => startGame; set => startGame = value; }
    public bool IsMenu { get => isMenu; set => isMenu = value; }

    void Start()
    {

        startGame = false;
        isMenu = true;
        birdController.GetComponent<Rigidbody2D>().isKinematic = true;
        GameUI.SetActive(false);
        MenuUI.SetActive(true);
        currentTime = 0f;

       
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    
        DisplayBestScore();
        SetDifficultyLevel("normal");
        
    }

    
    void Update()
    {
        
        if (startGame)
        {
            isMenu = false;
            Timer();
            birdController.GetComponent<Rigidbody2D>().isKinematic = false;
            scoreCounter.text = birdController.Score.ToString();
            if (HungryImage.fillAmount == 0)
            {
                birdController.IsDead = true;
            }
            if (birdController.IsDead)
            {
                stopTimer = true;
                Speed = 0;
                Invoke("DefeatScreen", 1f);

            }
            if (birdController.IsEat)
            {
                hungryImage.fillAmount += 1f;
                birdController.IsEat = false;
            }


            HungryImage.fillAmount -= hunger;
            HungryImage.color = new Color(1 - HungryImage.fillAmount, HungryImage.fillAmount, hungryImage.color.b);

            if (birdController.Score > bestScore)
            {
               
                bestScore = birdController.Score;
                PlayerPrefs.SetInt("BestScore", bestScore);
               
            }

        }



    }

    public void StartGameMethod() 
    {
        startGame = true;
        MenuUI.SetActive(false);
        GameUI.SetActive(true);
    }

    
    private void DefeatScreen()
    {
        defeatScreen.SetActive(true);
        Invoke("ReloadScene", 1f);

    }

    private void ReloadScene()
    {

        SceneManager.LoadScene(0);
    }

    private void Timer()
    {
        if (!stopTimer)
        {
            currentTime += Time.deltaTime;
            string formattedTime = FormatTime(currentTime);
            timer.text = formattedTime;
        }
       

        
    }

    private void DisplayBestScore()
    {
        
         bestScoreText.text = bestScore.ToString();
    }

    private void SetDifficultyLevel(string level)
    {
        switch (level)
        {
            case "easy":
                hunger = 0.0002f;
                speed = 5f;
                spawnPipes.SpawnInterval = 2.5f;
                break;
            case "normal":
                hunger = 0.0003f;
                speed = 5f;
                spawnPipes.SpawnInterval = 2.5f;
                break;
            case "hard":
                hunger = 0.0005f;
                speed = 10f;
                spawnPipes.SpawnInterval = 1.5f;
                break;
            default:
                break;
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
