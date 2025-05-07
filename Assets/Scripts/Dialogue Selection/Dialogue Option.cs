using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueOption : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public RectTransform messageContainerRect;
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

    [SerializeField] TextMeshProUGUI option4;
    [SerializeField] TextMeshProUGUI input4_1;
    [SerializeField] TextMeshProUGUI input4_2;
    [SerializeField] TextMeshProUGUI input4_3;
    [SerializeField] TextMeshProUGUI input4_4;
    [SerializeField] TextMeshProUGUI input4_5;
    List<TextMeshProUGUI> inputs4 = new List<TextMeshProUGUI>();

    [SerializeField] SelectionScriptableObject dialogueOption;
    static int numberOfInputStatic;
    [SerializeField] GameObject optionPrefab;
    [SerializeField] BuffManager player;

    [SerializeField] GameObject optionBox1;
    [SerializeField] GameObject optionBox2;
    [SerializeField] GameObject optionBox3;
    [SerializeField] GameObject optionBox4;

    List<int> catpachaList = new List<int> { 4, 9, 10, 16, 17, 18 };
    List<int> upgradeList = new List<int> { 5, 12, 19, 21 };
    List<int> skillList = new List<int> { 11, 20 };
    int countCaptcha = 0;
    int countUpgarde = 0;
    int countSkill = 0;


    int inputNum1;
    int inputNum2;
    int inputNum3;
    int inputNum4;

    int countInput = -1;
    int playerInput;
    int optionNumber;

    bool isFirstInput = true;
    bool canInput = false;
    bool finish = false;
    static bool haveClear = true;
    [SerializeField] static int mode = 0; // mode0 = scritable object mode | mode1 = captcha mode | mode2 = upgrade | mode3 = oneTimeSkill
    int lineTypingOn = 0;
    int difficulty = 1;
    int countWin = 0;
    const int dialogueOptionHeight = 150;

    void Start()
    {
        optionPrefab.SetActive(false);
        optionBox1.SetActive(false);
        optionBox2.SetActive(false);
        optionBox3.SetActive(false);
        optionBox4.SetActive(false);

        numberOfInputStatic = dialogueOption == null ? 0 : dialogueOption.numberOfInput;

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

        inputs4.Add(input4_1);
        inputs4.Add(input4_2);
        inputs4.Add(input4_3);
        inputs4.Add(input4_4);
        inputs4.Add(input4_5);

        recieveNewOption();
    }


    void Inputs(int number)
    {
        playerInput = number;
        countInput++;
        compareInput(playerInput);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canInput)          {Inputs(1);}
        else if (Input.GetKeyDown(KeyCode.DownArrow) && canInput)   {Inputs(2);}
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canInput)   {Inputs(3);}
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canInput)  {Inputs(4);}
        else if (Input.GetKeyDown(KeyCode.Alpha1))                  { mode = 0; }
        else if (Input.GetKeyDown(KeyCode.Alpha2))                  { mode = 1; }
        else if (Input.GetKeyDown(KeyCode.R))                       {SceneManager.LoadScene(0);}
        else if (Input.GetKeyDown(KeyCode.Space))                   { recieveNewOption(); }


        if (!optionPrefab.activeSelf)  {canInput = false;}
        else                           {canInput = true;}


        if (countWin >= 2 && difficulty <= 2)
        {
            difficulty++;
            countWin = 0;
        }


        Debug.Log("###### " + dialogueOption.numberOfInput);

        if (dialogueOption.numberOfInput >= 1) { optionBox4.SetActive(true); };
        if (dialogueOption.numberOfInput >= 2) { optionBox3.SetActive(true); };
        if (dialogueOption.numberOfInput >= 3) { optionBox2.SetActive(true); };
        if (dialogueOption.numberOfInput >= 4) { optionBox1.SetActive(true); };

        if (optionPrefab.activeSelf && !haveClear)
        {
            recieveNewOption();
            haveClear = true;
        }
    }


    // update new option
    public void recieveNewOption()
    {
        isFirstInput = true;
        lineTypingOn = 0;
        countInput = -1;

        option1.text = "";
        option2.text = "";
        option3.text = "";
        option4.text = "";
        option1.color = new Color (0.455f, 0.980f, 0.992f, 1f);
        option2.color = new Color (0.455f, 0.980f, 0.992f, 1f);
        option3.color = new Color (0.455f, 0.980f, 0.992f, 1f);
        option4.color = new Color (0.455f, 0.980f, 0.992f, 1f);

        for (int i = 0; i < 5; i++)
        {
            inputs1[i].text = "";
            inputs2[i].text = "";
            inputs3[i].text = "";
            inputs4[i].text = "";
            inputs1[i].color = new Color (0.455f, 0.980f, 0.992f, 1f);
            inputs2[i].color = new Color (0.455f, 0.980f, 0.992f, 1f);
            inputs3[i].color = new Color (0.455f, 0.980f, 0.992f, 1f);
            inputs4[i].color = new Color (0.455f, 0.980f, 0.992f, 1f);
        }


        if (mode == 0)
        {
            optionNumber = dialogueOption.numberOfInput;
            recieveRegularOption();
        }
        else if (mode == 2)
        {
            optionNumber = 3;
            option2.text = "Enhance shooting!!!";
            option3.text = "Enhance damage!!!";
            option4.text = "Shoot more colum!!!";
            inputs2[0].text = convertToText(2);
            inputs3[0].text = convertToText(1);
            inputs4[0].text = convertToText(4);
            createRandomInputs(difficulty, 3);
        }
        else
        {
            if (mode == 1)
            {
                option1.text = "1";
                option2.text = "2";
                option3.text = "3";
                option4.text = "skip";
            }
            if (mode == 3)
            {
                option1.text = "Activate bomb!!!";
                option2.text = "Recover health!!!";
                option3.text = "Activate laser!!!";
                option4.text = "Activate homing!!!";
            }
            optionNumber = 4;
            inputs1[0].text = convertToText(1);
            inputs2[0].text = convertToText(2);
            inputs3[0].text = convertToText(3);
            inputs4[0].text = convertToText(4);
            createRandomInputs(difficulty, 4);
        }
    }

    void recieveRegularOption()
    {
        // put option text into the text area
        option1.text = dialogueOption.GetOption1();
        option2.text = dialogueOption.GetOption2();
        option3.text = dialogueOption.GetOption3();
        option4.text = dialogueOption.GetOption4();

        inputNum1 = dialogueOption.numOfInput1;
        inputNum2 = dialogueOption.numOfInput2;
        inputNum3 = dialogueOption.numOfInput3;
        inputNum4 = dialogueOption.numOfInput4;

        // put player input and convert it into arrows
        for (int i = 0; i < 5; i++)
        {
            inputs1[i].text = convertToText(dialogueOption.GetInput1(i));
            inputs2[i].text = convertToText(dialogueOption.GetInput2(i));
            inputs3[i].text = convertToText(dialogueOption.GetInput3(i));
            inputs4[i].text = convertToText(dialogueOption.GetInput4(i));
        }
    }

    void createRandomInputs(int difficulty, int optionNum)
    {
        if (optionNum == 4)
        {
            for (int i = 1; i <= difficulty + 1; i++)
            {
                inputs1[i].text = convertToText(UnityEngine.Random.Range(1, 5));
                inputs2[i].text = convertToText(UnityEngine.Random.Range(1, 5));
                inputs3[i].text = convertToText(UnityEngine.Random.Range(1, 5));
                inputs4[i].text = convertToText(UnityEngine.Random.Range(1, 5));
            }

            inputNum1 = difficulty + 2;
            inputNum2 = difficulty + 2;
            inputNum3 = difficulty + 2;
            inputNum4 = difficulty + 2;
        }
        if (optionNum == 3)
        {
            for (int i = 1; i <= difficulty + 1; i++)
            {
                inputs2[i].text = convertToText(UnityEngine.Random.Range(1, 5));
                inputs3[i].text = convertToText(UnityEngine.Random.Range(1, 5));
                inputs4[i].text = convertToText(UnityEngine.Random.Range(1, 5));
            }

            inputNum2 = difficulty + 2;
            inputNum3 = difficulty + 2;
            inputNum4 = difficulty + 2;
        }
    }





    string convertToText(int input)
    {
        if (input == 1)        {return "��";}
        else if (input == 2)   {return "��";}
        else if (input == 3)   {return "��";}
        else if (input == 4)   {return "��";}
        else                   {return "";}
    }
    int convertToInt(string input)
    {
        if      (input == "��") { return 1; }
        else if (input == "��") { return 2; }
        else if (input == "��") { return 3; }
        else if (input == "��") { return 4; }
        else                   { return 0; }
    }

    // compare player input with arrow directions
    void compareInput(int input)
    {
        if (input == convertToInt(inputs1[countInput].text) && (isFirstInput || lineTypingOn == 1))
        {
            campare(1, inputs1[countInput]);
        }
        else if (input == convertToInt(inputs2[countInput].text) && (isFirstInput || lineTypingOn == 2))
        {
            campare(2, inputs2[countInput]);
        }
        else if (input == convertToInt(inputs3[countInput].text) && (isFirstInput || lineTypingOn == 3))
        {
            campare(3, inputs3[countInput]);
        }
        else if (input == convertToInt(inputs4[countInput].text) && (isFirstInput || lineTypingOn == 4))
        {
            campare(4, inputs4[countInput]);
        }
        else
        {
            AudioManager.Instance.PlaySFX("inputIncorrect");
            recieveNewOption();
        }

        //Debug.Log("### " + lineTypingOn + " " + countInput + " " + (inputNum1 - 1));

        if (lineTypingOn == 1 && countInput == inputNum1 - 1)
        {
            inputSuccess(0);
        }
        else  if (lineTypingOn == 2 && countInput == inputNum2 - 1)
        {
            inputSuccess(1);
        }
        else if (lineTypingOn == 3 && countInput == inputNum3 - 1)
        {
            inputSuccess(2);
        }
        else if (lineTypingOn == 4 && countInput == inputNum4 - 1)
        {
            inputSuccess(3);
        } 
        else {return;}

        // TODO resize message container
        messageContainerRect.sizeDelta = new Vector2(messageContainerRect.sizeDelta.x,
                messageContainerRect.sizeDelta.y - dialogueOption.numberOfInput * dialogueOptionHeight);
    }


    void campare(int line, TextMeshProUGUI arrowText)
    {
        AudioManager.Instance.PlaySFX("inputCorrect");
        isFirstInput = false;
        lineTypingOn = line;
        arrowText.text = "<b>" + arrowText.text + "</b>";
        arrowText.color = Color.yellow;
    }
    
    void inputSuccess(int next)
    {
        AudioManager.Instance.PlaySFX("inputSuccess");
        if (mode == 0)
        {
            if (lineTypingOn == 1)
            {
                dialogueRunner.StartDialogue(dialogueOption.GetNode1());
            }
            else if (lineTypingOn == 2)
            {
                dialogueRunner.StartDialogue(dialogueOption.GetNode2());
            }
            else if (lineTypingOn == 3)
            {
                dialogueRunner.StartDialogue(dialogueOption.GetNode3());
            }
            else if (lineTypingOn == 4)
            {
                dialogueRunner.StartDialogue(dialogueOption.GetNode4());
            }
            dialogueOption = dialogueOption.GetSelection(next);
            numberOfInputStatic = dialogueOption.numberOfInput;
            finish = true;
        }
        if (mode == 1)
        {
            if (lineTypingOn == YarnTextMessageCommands.captchaAnswer || YarnTextMessageCommands.captchaValue == 9)
            {
                dialogueRunner.StartDialogue(catpachaList[countCaptcha].ToString());
                countCaptcha++;
                countWin++;
                finish = true;
            }
            else
            {
                dialogueRunner.StartDialogue("captchaIncorrect");
                recieveNewOption();
            }
        }
        if (mode == 2)
        {
            dialogueRunner.StartDialogue(skillList[countSkill].ToString());
            countSkill++;
            if (lineTypingOn == 2) { player.BuffShootingSpd(); }
            if (lineTypingOn == 3) { player.BuffShootingDmg(); }
            if (lineTypingOn == 4) { player.BuffShootingColumn(); }
            finish = true;
        }
        if (mode == 3)
        {
            dialogueRunner.StartDialogue(upgradeList[countUpgarde].ToString());
            countUpgarde++;
            if (lineTypingOn == 1) { player.ActivateBomb(); }
            if (lineTypingOn == 2) { player.RecoverHealth(); }
            if (lineTypingOn == 3) { player.ActivateLaser(); }
            if (lineTypingOn == 4) { player.ActivateHoming(); }
            finish = true;
        }

        if (finish)
        {
            optionBox1.SetActive(false);
            optionBox2.SetActive(false);
            optionBox3.SetActive(false);
            optionBox4.SetActive(false);
            optionPrefab.SetActive(false);
            finish = false;
        }
    }




    [YarnCommand("setOptionActive")]
    public static void ShowSelection(int newMode)
    {
        var dialogOption = GameObject.Find("/Dialogue/Canvas/MessageScrollBox/Viewport/Dialogue Option");
        dialogOption.SetActive(true);

        mode = newMode;
        haveClear = false; 

        // dialogOption.transform.SetSiblingIndex(dialogOption.transform.parent.transform.childCount + 1);

        // TODO resize messageContainer based on number of dialogue options
        var msgContent = GameObject.Find("/Dialogue/Canvas/MessageScrollBox/Viewport/MessageContent");
        RectTransform messageContainer = msgContent.GetComponent<RectTransform>();

        messageContainer.sizeDelta = new Vector2(messageContainer.sizeDelta.x,
                messageContainer.sizeDelta.y + numberOfInputStatic * dialogueOptionHeight);
    }
}





















//void resetField()
//{
//    isFirstInput = true;
//    lineTypingOn = 0;
//    countInput = -1;
//    for (int i = 0; i < 5; i++)
//    {
//        inputs1[i].color = Color.white;
//        inputs2[i].color = Color.white;
//        inputs3[i].color = Color.white;
//        inputs4[i].color = Color.white;
//        inputs1[i].text = convertToText(dialogueOption.GetInput1(i));
//        inputs2[i].text = convertToText(dialogueOption.GetInput2(i));
//        inputs3[i].text = convertToText(dialogueOption.GetInput3(i));
//        inputs4[i].text = convertToText(dialogueOption.GetInput4(i));

//        option1.text = dialogueOption.GetOption1();
//        option2.text = dialogueOption.GetOption2();
//        option3.text = dialogueOption.GetOption3();
//        option4.text = dialogueOption.GetOption4();
//        option1.color = Color.white;
//        option2.color = Color.white;
//        option3.color = Color.white;
//        option4.color = Color.white;
//    }
//}
