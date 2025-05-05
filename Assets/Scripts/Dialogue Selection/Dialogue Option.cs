using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

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
    [SerializeField] GameObject optionPrefab;

    int inputNum1;
    int inputNum2;
    int inputNum3;
    int inputNum4;

    int countInput = -1;
    int playerInput;

    bool isFirstInput = true;
    bool canInput = false;
    [SerializeField] int mode = 0; // mode0 = scritable object mode | mode1 = captcha mode | mode2 = skill/powerUps mode
    int lineTypingOn = 0;
    int difficulty = 1;
    int countWin = 0;
    const int dialogueOptionHeight = 100;

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
        else if (Input.GetKeyDown(KeyCode.Alpha1)) { mode = 0; }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { mode = 1; }
        else if (Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene(0); }
        else if (Input.GetKeyDown(KeyCode.Space))                   {ShowSelection();}


        if (!optionPrefab.activeSelf)  {canInput = false;}
        else                           {canInput = true;}


        if (countWin >= 2 && difficulty <= 2)
        {
            difficulty++;
            countWin = 0;
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
        option1.color = Color.white;
        option2.color = Color.white;
        option3.color = Color.white;
        option4.color = Color.white;

        for (int i = 0; i < 5; i++)
        {
            inputs1[i].text = "";
            inputs2[i].text = "";
            inputs3[i].text = "";
            inputs4[i].text = "";
            inputs1[i].color = Color.white;
            inputs2[i].color = Color.white;
            inputs3[i].color = Color.white;
            inputs4[i].color = Color.white;
        }


        if (mode == 0)
        {
            recieveRegularOption();
        }
        else if (mode == 1)
        {
            option1.text = "1";
            option2.text = "2";
            option3.text = "3";
            option4.text = "skip";
            inputs1[0].text = convertToText(1);
            inputs2[0].text = convertToText(2);
            inputs3[0].text = convertToText(3);
            inputs4[0].text = convertToText(4);
            createRandomInputs(difficulty);
        }
        else if (mode == 2)
        {
            option1.text = "bomb";
            option2.text = "laser";
            option3.text = "bomb";
            option4.text = "laser";
            inputs1[0].text = convertToText(1);
            inputs2[0].text = convertToText(2);
            inputs3[0].text = convertToText(3);
            inputs4[0].text = convertToText(4);
            createRandomInputs(difficulty);
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

    void createRandomInputs(int difficulty)
    {
        for (int i = 1; i <= difficulty+1; i++)
        {
            inputs1[i].text = convertToText(UnityEngine.Random.Range(1,5));
            inputs2[i].text = convertToText(UnityEngine.Random.Range(1,5));
            inputs3[i].text = convertToText(UnityEngine.Random.Range(1,5));
            inputs4[i].text = convertToText(UnityEngine.Random.Range(1,5));
        }

        inputNum1 = difficulty + 2;
        inputNum2 = difficulty + 2;
        inputNum3 = difficulty + 2;
        inputNum4 = difficulty + 2;
    }





    string convertToText(int input)
    {
        if (input == 1)        {return "u";}
        else if (input == 2)   {return "d";}
        else if (input == 3)   {return "l";}
        else if (input == 4)   {return "r";}
        else                   {return "";}
    }
    int convertToInt(string input)
    {
        if (input == "u")      { return 1; }
        else if (input == "d") { return 2; }
        else if (input == "l") { return 3; }
        else if (input == "r") { return 4; }
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

        Debug.Log("### " + lineTypingOn + " " + countInput + " " + (inputNum1 - 1));

        if (lineTypingOn == 1 && countInput == inputNum1 - 1)
        {
            dialogueRunner.StartDialogue(dialogueOption.GetNode1());
            inputSuccess(0);
        }
        else  if (lineTypingOn == 2 && countInput == inputNum2 - 1)
        {
            dialogueRunner.StartDialogue(dialogueOption.GetNode2());
            inputSuccess(1);
        }
        else if (lineTypingOn == 3 && countInput == inputNum3 - 1)
        {
            dialogueRunner.StartDialogue(dialogueOption.GetNode3());
            inputSuccess(2);
        }
        else if (lineTypingOn == 4 && countInput == inputNum4 - 1)
        {
            dialogueRunner.StartDialogue(dialogueOption.GetNode4());
            inputSuccess(3);
        } 
        else {return;}

        // TODO resize message container
        messageContainerRect.sizeDelta = new Vector2(messageContainerRect.sizeDelta.x,
                messageContainerRect.sizeDelta.y - 3 * dialogueOptionHeight);
    }


    void campare(int line, TextMeshProUGUI arrowText)
    {
        AudioManager.Instance.PlaySFX("inputCorrect");
        isFirstInput = false;
        lineTypingOn = line;
        arrowText.text = "<b><u>" + arrowText.text + "</u></b>";
        arrowText.color = Color.yellow;
    }

    void inputSuccess(int next)
    {
        AudioManager.Instance.PlaySFX("inputSuccess");
        if (mode == 1 || mode == 2)
        {
            countWin++;

        }
        dialogueOption = dialogueOption.GetSelection(next);
        recieveNewOption();
        optionPrefab.SetActive(false);
    }




    [YarnCommand("setOptionActive")]
    public static void ShowSelection()
    {
        var dialogueOption = GameObject.Find("/Dialogue/Canvas/MessageScrollBox/Viewport/MessageContent/Dialogue Option");
        dialogueOption.SetActive(true);

        dialogueOption.transform.SetSiblingIndex(dialogueOption.transform.parent.transform.childCount + 1);

        // TODO resize messageContainer based on number of dialogue options (how do we check amount of options?)
        var msgCont = GameObject.Find("/Dialogue/Canvas/MessageScrollBox/Viewport/MessageContent");
        RectTransform messageContainer = msgCont.GetComponent<RectTransform>();

        messageContainer.sizeDelta = new Vector2(messageContainer.sizeDelta.x,
                messageContainer.sizeDelta.y + 3 * dialogueOptionHeight);
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
