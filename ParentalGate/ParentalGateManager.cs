using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ParentalGateManager : MonoBehaviour
{
    public Text[] randomText;
    public Text[] displayText;
    public Button closeButton;
    
    [SerializeField]
    private string previousScene, sceneToLoad;
    private string randomNumbers1, randomNumbers2, randomNumbers3;
    private string codeSequence, displaySequence;
    private int touchButtonCounter;

    void Start()
    {
        closeButton.onClick.AddListener(() => LoadPreviousScene());
        
        PickRandomFromList();

        codeSequence = "";
        touchButtonCounter = 3;                

        PushButtonGate.ButtonPressed += AddDigitToCodeSequence;
        PushButtonGate.ButtonPressed += AddDigitToDisplayText;
    }

    private void Update()
    {
        CheckTouchButtonCounter();
    }

    private void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousScene);
    }

    private void PickRandomFromList()
    {
        string[] numbers = new string[] {"Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan", "Nol"};
        randomNumbers1 = numbers[Random.Range(0, numbers.Length)];
        randomNumbers2 = numbers[Random.Range(0, numbers.Length)];
        randomNumbers3 = numbers[Random.Range(0, numbers.Length)];
        
        randomText[0].text = randomNumbers1;
        randomText[1].text = randomNumbers2;
        randomText[2].text = randomNumbers3;
    }

    private void AddDigitToCodeSequence(string digitEntered)
    {
        if(touchButtonCounter > 0)
        {
            switch(digitEntered)
            {
                case "Satu":
                    codeSequence += "Satu";
                    touchButtonCounter -= 1;
                    displaySequence = "1";
                    break;
                case "Dua":
                    codeSequence += "Dua";
                    touchButtonCounter -= 1;
                    displaySequence = "2";
                    break;
                case "Tiga":
                    codeSequence += "Tiga";
                    touchButtonCounter -= 1;
                    displaySequence = "3";
                    break;
                case "Empat":
                    codeSequence += "Empat";
                    touchButtonCounter -= 1;
                    displaySequence = "4";
                    break;
                case "Lima":
                    codeSequence += "Lima";
                    touchButtonCounter -= 1;
                    displaySequence = "5";
                    break;
                case "Enam":
                    codeSequence += "Enam";
                    touchButtonCounter -= 1;
                    displaySequence = "6";
                    break;
                case "Tujuh":
                    codeSequence += "Tujuh";
                    touchButtonCounter -= 1;
                    displaySequence = "7";
                    break;
                case "Delapan":
                    codeSequence += "Delapan";
                    touchButtonCounter -= 1;
                    displaySequence = "8";
                    break;
                case "Sembilan":
                    codeSequence += "Sembilan";
                    touchButtonCounter -= 1;
                    displaySequence = "9";
                    break;
                case "Nol":
                    codeSequence += "Nol";
                    touchButtonCounter -= 1;
                    displaySequence = "0";
                    break;
            }            
        }
    }

    private void AddDigitToDisplayText(string numberEntered)
    {
        switch(touchButtonCounter)
        {
            case 2:
                displayText[0].text = displaySequence;
                break;
            case 1:                
                displayText[1].text = displaySequence;
                break;
            case 0:                
                displayText[2].text = displaySequence;
                break;
        }
    }

    private void CheckTouchButtonCounter()
    {
        if(touchButtonCounter == 0)
        {
            CheckResults();
        }
    }

    private void CheckResults()
    {
        if(codeSequence == randomNumbers1 + randomNumbers2 + randomNumbers3)
        {
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("Anda Benar!");
        }
        else
        {
            Invoke("ResetDisplay", 0.25f);
            Debug.LogWarning("Anda Salah!");
        }
    }

    private void ResetDisplay()
    {
        codeSequence = "";
        touchButtonCounter = 3;

        displayText[0].text = "";
        displayText[1].text = "";
        displayText[2].text = "";
    }

    private void OnDestroy()
    {
        PushButtonGate.ButtonPressed -= AddDigitToCodeSequence;
        PushButtonGate.ButtonPressed -= AddDigitToDisplayText;
    }
}
