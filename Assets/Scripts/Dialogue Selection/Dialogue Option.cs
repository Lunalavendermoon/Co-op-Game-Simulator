using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class DialogueOption : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public RectTransform messageContainerRect;
    public RectTransform messageViewportRect;
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
    int inputNum1, inputNum2, inputNum3, inputNum4;
    int countInput = -1;
    int playerInput;
    int optionNumber;

    bool isFirstInput = true;
    bool canInput = false;
    bool finish = false;
    static bool haveClear = true;
    [SerializeField] static int mode = 0; // mode0 = scritable object mode | mode1 = captcha mode | mode2 = upgrade | mode3 = buff
    int lineTypingOn = 0;
    int difficulty = 1;
    int countWin = 0;
    const int dialogueOptionHeight = 90;

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

        else if (mode == 1)
        {
            option1.text = "1";
            option2.text = "2";
            option3.text = "3";
            option4.text = "skip";

            optionNumber = 4;
            inputs1[0].text = convertToText(1);
            inputs2[0].text = convertToText(2);
            inputs3[0].text = convertToText(3);
            inputs4[0].text = convertToText(4);
            createRandomInputs(difficulty, 4);

        }
        else
        {
            if (mode == 2)
            {
                option2.text = "Enhance shooting!!!";
                option3.text = "Enhance damage!!!";
                option4.text = "Shoot more colum!!!";
            }
            if (mode == 3)
            {
                option2.text = "Recover health!!!";
                option3.text = "Activate laser!!!";
                option4.text = "Activate homing!!!";
            }
            optionNumber = 3;
            inputs2[0].text = convertToText(2);
            inputs3[0].text = convertToText(1);
            inputs4[0].text = convertToText(4);
            createRandomInputs(difficulty, 3);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canInput) { Inputs(1); }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && canInput) { Inputs(2); }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canInput) { Inputs(3); }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canInput) { Inputs(4); }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) { mode = 0; }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { mode = 1; }
        else if (Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene(0); }
        else if (Input.GetKeyDown(KeyCode.Space)) { recieveNewOption(); }


        if (!optionPrefab.activeSelf) { canInput = false; }
        else { canInput = true; }


        if (countWin >= 2 && difficulty <= 2)
        {
            difficulty++;
            countWin = 0;
        }




        if (optionPrefab.activeSelf && !haveClear)
        {
            recieveNewOption();
            haveClear = true;
            if (optionNumber >= 1) { optionBox4.SetActive(true); }
            if (optionNumber >= 2) { optionBox3.SetActive(true); }
            if (optionNumber >= 3) { optionBox2.SetActive(true); }
            if (optionNumber >= 4) { optionBox1.SetActive(true); }
        }
        numberOfInputStatic = optionNumber;
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

            inputNum1 = inputNum2 = inputNum3 = inputNum4 = difficulty + 2;
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
        if (input == 1)        {return "U";}
        else if (input == 2)   {return "D";}
        else if (input == 3)   {return "L";}
        else if (input == 4)   {return "R";}
        else                   {return "";}
    }
    int convertToInt(string input)
    {
        if      (input == "U") { return 1; }
        else if (input == "D") { return 2; }
        else if (input == "L") { return 3; }
        else if (input == "R") { return 4; }
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
            Debug.Log("dialogue!");

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
            finish = true;
        }
        if (mode == 1)
        {
            Debug.Log("captcha!");
            Debug.Log(YarnTextMessageCommands.captchaAnswer);
            if (lineTypingOn == YarnTextMessageCommands.captchaAnswer || YarnTextMessageCommands.captchaValue == 10)
            {
                Debug.Log("captcha correct!");
                dialogueRunner.StartDialogue(catpachaList[countCaptcha].ToString());
                countCaptcha++;
                countWin++;
                finish = true;
            }
            else
            {
                Debug.Log("captcha incorrect!");

                dialogueRunner.StartDialogue("captchaIncorrect");
                recieveNewOption();
            }
        }
        if (mode == 2)
        {
            Debug.Log("upgrade!");

            dialogueRunner.StartDialogue(upgradeList[countUpgarde].ToString());
            Debug.Log("starting dialogue with index " + countUpgarde + "and number: " + upgradeList[countUpgarde].ToString());
            countUpgarde++;
            if (lineTypingOn == 2) { player.BuffShootingSpd(); }
            if (lineTypingOn == 3) { player.BuffShootingDmg(); }
            if (lineTypingOn == 4) { player.BuffShootingColumn(); }
            finish = true;
        }
        if (mode == 3)
        {
            Debug.Log("buff!");

            dialogueRunner.StartDialogue(skillList[countSkill].ToString());
            Debug.Log("starting dialogue with index " + countSkill + "and number: " + skillList[countSkill].ToString());
            countSkill++;
            if (lineTypingOn == 1) { player.ActivateBomb(); }
            if (lineTypingOn == 2) { player.RecoverHealth(); }
            if (lineTypingOn == 3) { player.ActivateLaser(); }
            if (lineTypingOn == 4) { player.ActivateHoming(); }
            finish = true;
        }

        if (finish)
        {
            Debug.Log("Hide option Before: " + messageViewportRect.sizeDelta);

            messageViewportRect.sizeDelta = new Vector2(messageViewportRect.sizeDelta.x,
                    messageViewportRect.sizeDelta.y + numberOfInputStatic * dialogueOptionHeight);

            Debug.Log("Hide option After: " + messageViewportRect.sizeDelta);
        

            optionPrefab.SetActive(false);
            optionBox1.SetActive(false);
            optionBox2.SetActive(false);
            optionBox3.SetActive(false);
            optionBox4.SetActive(false);
            finish = false;
        }
    }

    [YarnCommand("setOptionActive")]
    public static void ShowSelection(int newMode)
    {
        var dialogOption = GameObject.Find("/Dialogue/Canvas/Dialogue Option");
        dialogOption.SetActive(true);

        mode = newMode;
        haveClear = false;
        
        var viewPort = GameObject.Find("/Dialogue/Canvas/MessageScrollBox/Viewport");
        RectTransform vp = viewPort.GetComponent<RectTransform>();

        switch (mode) {
            // case 0:
            //     break;
            case 1:
                numberOfInputStatic = 4;
                break;
            case 2:
                numberOfInputStatic = 3;
                break;
            case 3:
                numberOfInputStatic = 3;
                break;
            default:
                numberOfInputStatic = 1;
                break;
        }

        Debug.Log("Show option Before: " + vp.sizeDelta);

        vp.sizeDelta = new Vector2(vp.sizeDelta.x, vp.sizeDelta.y - numberOfInputStatic * dialogueOptionHeight);

        Debug.Log("Show option After: " + vp.sizeDelta);
    }
}
