using UnityEngine;
using Yarn.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public static class YarnTextMessageCommands
{
    public static int captchaValue;
    public static int captchaAnswer = 0;
    public static List<int> captchaList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};


    [YarnCommand("textmessage")]
    public static IEnumerator ShowTextMessage(string sender, string message)
    {
        var manager = GameObject.FindObjectOfType<TextMessageCommand>();
        manager.SpawnMessage(sender, message);
        AudioManager.Instance.PlaySFX("message");
        yield return new WaitForSeconds(2f);
    }

    [YarnCommand("textmessageimage")]
    public static IEnumerator ShowTextMessageImage(string sender, string message, string filename)
    {
        var manager = GameObject.FindObjectOfType<TextMessageCommand>();
        manager.SpawnImageMessage(sender, message, filename);

        yield return new WaitForSeconds(2f);
    }

    [YarnCommand("textmessagecaptcha")]
    public static IEnumerator ShowTextMessageCaptcha(string sender, string message)
    {
        captchaValue = captchaList[Random.Range(0, captchaList.Count)];
        captchaList.Remove(captchaValue);
        var manager = GameObject.FindObjectOfType<TextMessageCommand>();
        manager.SpawnImageMessage(sender, message, "Captchas/" + captchaValue);

        if (captchaValue == 1 || captchaValue == 3 || captchaValue == 4 || captchaValue == 5 || captchaValue == 6)
        {
            captchaAnswer = 4;
        }
        else if (captchaValue == 2)
        {
            captchaAnswer = 1;
        }
        else if (captchaValue == 7)
        {
            captchaAnswer = 3;
        }
        else if (captchaValue == 8 || captchaValue == 9)
        {
            captchaAnswer = 2;
        }

            yield return new WaitForSeconds(2f);
    }

    [YarnCommand("setdifficulty")]
    public static void SetShooterDifficulty(string diff) {
        var manager = GameObject.FindObjectOfType<ShooterManager>();
        manager.SetShooterState(diff);
    }
    
    [YarnCommand("increasediff")]
    public static void IncreaseDifficulty(float amt) {
        var manager = GameObject.FindObjectOfType<ShooterManager>();
        manager.IncreaseDifficulty(amt);
    }

    [YarnCommand("completegame")]
    public static void CompleteGame() {
        var manager = GameObject.FindObjectOfType<ShooterManager>();
        manager.GameSuccess();
    }

}
