using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;
using static UnityEngine.GraphicsBuffer;

public class DialogueOption : MonoBehaviour
{
    [SerializeField] AudioClip inputCorrect;
    [SerializeField] AudioClip inputIncorrect;
    [SerializeField] AudioClip inputSuccess;
    [SerializeField] AudioSource SFX;


    [SerializeField] TextMeshProUGUI option1;
    [SerializeField] TextMeshProUGUI input1_1;
    [SerializeField] TextMeshProUGUI input1_2;
    [SerializeField] TextMeshProUGUI input1_3;
    [SerializeField] TextMeshProUGUI input1_4;
    [SerializeField] TextMeshProUGUI input1_5;
    List<TextMeshProUGUI> inputs1 = new List<TextMeshProUGUI>();

    [SerializeField] TextMeshProUGUI option2;
    [SerializeField] TextMeshProUGUI input2_1;
    [SerializeField] TextMeshProUGUI input2_2;
    [SerializeField] TextMeshProUGUI input2_3;
    [SerializeField] TextMeshProUGUI input2_4;
    [SerializeField] TextMeshProUGUI input2_5;
    List<TextMeshProUGUI> inputs2 = new List<TextMeshProUGUI>();

    [SerializeField] TextMeshProUGUI option3;
    [SerializeField] TextMeshProUGUI input3_1;
    [SerializeField] TextMeshProUGUI input3_2;
    [SerializeField] TextMeshProUGUI input3_3;
    [SerializeField] TextMeshProUGUI input3_4;
    [SerializeField] TextMeshProUGUI input3_5;
    List<TextMeshProUGUI> inputs3 = new List<TextMeshProUGUI>();

    [SerializeField] SelectionScriptableObject dialogueOption;
    [SerializeField] GameObject optionPrefab;

    int countInput = -1;
    int playerInput;

    bool isFirstInput = true;
    bool canInput = false;
    int lineTypingOn = 0;

    void Start()
    {
        optionPrefab.SetActive(false);
        // put all options into corresponding lists
        inputs1.Add(input1_1);
        inputs1.Add(input1_2);
        inputs1.Add(input1_3);
        inputs1.Add(input1_4);
        inputs1.Add(input1_5);

        inputs2.Add(input2_1);
        inputs2.Add(input2_2);
        inputs2.Add(input2_3);
        inputs2.Add(input2_4);
        inputs2.Add(input2_5);

        inputs3.Add(input3_1);
        inputs3.Add(input3_2);
        inputs3.Add(input3_3);
        inputs3.Add(input3_4);
        inputs3.Add(input3_5);
        recieveNewOption();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canInput)
        {
            playerInput = 1;
            countInput++;
            compareInput(playerInput);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && canInput)
        {
            playerInput = 2;
            countInput++;
            compareInput(playerInput);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canInput)
        {
            playerInput = 3;
            countInput++;
            compareInput(playerInput);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canInput)
        {
            playerInput = 4;
            countInput++;
            compareInput(playerInput);
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowSelection();
        }

        if (!optionPrefab.activeSelf)
        {
            canInput = false;
        }
        else
        {
            canInput = true;
        }
    }

    // update new option
    public void recieveNewOption()
    {
        // put option text into the text area
        option1.text = dialogueOption.GetOption1();
        option2.text = dialogueOption.GetOption2();
        option3.text = dialogueOption.GetOption3();

        

        // put player input and convert it into arrows
        for (int i = 0; i < 5; i++)
        {
            inputs1[i].text = convertToText(dialogueOption.GetInput1(i));
            inputs2[i].text = convertToText(dialogueOption.GetInput2(i));
            inputs3[i].text = convertToText(dialogueOption.GetInput3(i));
        }
    }

    string convertToText(int input)
    {
        if (input == 1)
        {
            return "up";
        }
        else if (input == 2)
        {
            return "down";
        }
        else if (input == 3)
        {
            return "left";
        }
        else if (input == 4)
        {
            return "right";
        }
        else
        {
            return "";
        }
    }


    // compare player input with arrow directions
    void compareInput(int input)
    {
        if (input == dialogueOption.GetInput1(countInput) && (isFirstInput || lineTypingOn == 1))
        {
            SFX.PlayOneShot(inputCorrect, 0.5f);
            isFirstInput = false;
            lineTypingOn = 1;
            inputs1[countInput].text = "<b><u>" + convertToText(dialogueOption.GetInput1(countInput)) + "</u></b>";
            inputs1[countInput].color = Color.yellow;
        }
        else if (input == dialogueOption.GetInput2(countInput) && (isFirstInput || lineTypingOn == 2))
        {
            SFX.PlayOneShot(inputCorrect, 0.5f);
            isFirstInput = false;
            lineTypingOn = 2;
            inputs2[countInput].text = "<b><u>" + convertToText(dialogueOption.GetInput2(countInput)) + "</u></b>";
            inputs2[countInput].color = Color.yellow;
        }
        else if (input == dialogueOption.GetInput3(countInput) && (isFirstInput || lineTypingOn == 3))
        {
            SFX.PlayOneShot(inputCorrect, 0.5f);
            isFirstInput = false;
            lineTypingOn = 3;
            inputs3[countInput].text = "<b><u>" + convertToText(dialogueOption.GetInput3(countInput)) + "</u></b>";
            inputs3[countInput].color = Color.yellow;
        }
        else
        {
            SFX.PlayOneShot(inputIncorrect, 0.5f);
            resetField();
        }




        if (lineTypingOn == 1 && countInput == dialogueOption.numOfInput1 - 1)
        {
            SFX.PlayOneShot(inputSuccess);
            option1.text = "<b><u>" + dialogueOption.GetOption1() + "</u></b>";
            option1.color = Color.yellow;
            dialogueOption = dialogueOption.GetSelection(0);
            recieveNewOption();
            resetField();
            optionPrefab.SetActive(false);
        }
        else  if (lineTypingOn == 2 && countInput == dialogueOption.numOfInput2 - 1)
        {
            SFX.PlayOneShot(inputSuccess);
            option2.text = "<b><u>" + dialogueOption.GetOption2() + "</u></b>";
            option2.color = Color.yellow;
            dialogueOption = dialogueOption.GetSelection(1);
            recieveNewOption();
            resetField();
            optionPrefab.SetActive(false);
        }
        else if (lineTypingOn == 3 && countInput == dialogueOption.numOfInput3 - 1)
        {
            SFX.PlayOneShot(inputSuccess);
            option3.text = "<b><u>" + dialogueOption.GetOption3() + "</u></b>";
            option3.color = Color.yellow;
            dialogueOption = dialogueOption.GetSelection(2);
            recieveNewOption();
            resetField();
            optionPrefab.SetActive(false);
        }

        
    }

    void resetField()
    {
        isFirstInput = true;
        lineTypingOn = 0;
        countInput = -1;
        for (int i = 0; i < 5; i++)
        {
            inputs1[i].color = Color.white;
            inputs2[i].color = Color.white;
            inputs3[i].color = Color.white;
            inputs1[i].text = convertToText(dialogueOption.GetInput1(i));
            inputs2[i].text = convertToText(dialogueOption.GetInput2(i));
            inputs3[i].text = convertToText(dialogueOption.GetInput3(i));

            option1.text = dialogueOption.GetOption1();
            option2.text = dialogueOption.GetOption2();
            option3.text = dialogueOption.GetOption3();
            option1.color = Color.white;
            option2.color = Color.white;
            option3.color = Color.white;
        }
    }


    [YarnCommand("setOptionActive")]
    public static void ShowSelection()
    {
        Debug.Log("Yarn ������ debuglog �ɹ���");
        var dialogueOption = GameObject.Find("/Dialogue/Canvas/Dialogue Option");
        dialogueOption.SetActive(true);
    }
}
