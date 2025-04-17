using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "DialogueOption")]
public class SelectionScriptableObject : ScriptableObject
{
    [SerializeField] string option1;
    public int numOfInput1;
    [SerializeField] List<int> input1;
    
    [SerializeField] string option2;
    public int numOfInput2;
    [SerializeField] List<int> input2;
    
    [SerializeField] string option3;
    public int numOfInput3;
    [SerializeField] List<int> input3;
    
    [SerializeField] SelectionScriptableObject[] nextOptions;
    public int numberOfInput;

    public string GetOption1()
    {
        return option1;
    }
    public string GetOption2()
    {
        return option2;
    }
    public string GetOption3()
    {
        return option3;
    }


    public int GetInput1(int number)
    {
        return input1[number];
    }
    public int GetInput2(int number)
    {
        return input2[number];
    }
    public int GetInput3(int number)
    {
        return input3[number];
    }

    public SelectionScriptableObject GetSelection(int index)
    {
        return nextOptions[index];
    }
}
