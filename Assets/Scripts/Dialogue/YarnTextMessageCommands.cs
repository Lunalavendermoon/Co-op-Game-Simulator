using UnityEngine;
using Yarn.Unity;
using System.Collections;

public static class YarnTextMessageCommands
{
    public static int captchaValue;

    [YarnCommand("textmessage")]
    public static IEnumerator ShowTextMessage(string sender, string message)
    {
        var manager = GameObject.FindObjectOfType<TextMessageCommand>();
        manager.SpawnMessage(sender, message);
        AudioManager.Instance.PlaySFX("message");
        yield return new WaitForSeconds(0.5f);
    }

    [YarnCommand("textmessageimage")]
    public static IEnumerator ShowTextMessageImage(string sender, string message, string filename)
    {
        var manager = GameObject.FindObjectOfType<TextMessageCommand>();
        manager.SpawnImageMessage(sender, message, filename);

        yield return new WaitForSeconds(0.5f);
    }

    [YarnCommand("textmessagecaptcha")]
    public static IEnumerator ShowTextMessageCaptcha(string sender, string message)
    {
        captchaValue = Random.Range(1,11);
        var manager = GameObject.FindObjectOfType<TextMessageCommand>();
        manager.SpawnImageMessage(sender, message, "Captchas/图片" + captchaValue);

        yield return new WaitForSeconds(0.5f);
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

}
