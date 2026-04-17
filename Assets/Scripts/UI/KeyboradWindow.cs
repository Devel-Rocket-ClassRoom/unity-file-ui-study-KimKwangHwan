using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboradWindow : GenericWindow
{
    public TextMeshProUGUI nameText;
    // private string inputName;
    private int timer = 0;
    private float blinkInterval = 0.5f;
    private float lastBlink;
    private int maxInputName = 12;

    private readonly StringBuilder sb = new StringBuilder();

    public override void Open()
    {
        base.Open();
        sb.Clear();
        //inputName = "_";
        // nameText.set = sb.ToString();
        nameText.SetText(sb);
        lastBlink = Time.time;
    }

    public override void Close()
    {
        base.Close();
    }

    private void Update()
    {
        if (lastBlink + blinkInterval < Time.time)
        {
            lastBlink = Time.time;
            nameText.text = $"{sb}{(timer++ % 2 == 0 ? string.Empty : "_")}";
        }
    }

    public void OnKeyboardButtonClick(string alpha)
    {
        if (sb.Length < maxInputName)
        {
            // inputName += alpha;
            sb.Append(alpha);
            nameText.SetText(sb + "_");
        }
    }

    public void OnDeleteButtonClick()
    {
        if (sb.Length > 0)
        {
            sb.Length -= 1;
            // inputName = inputName.Substring(0, inputName.Length - 1);
            nameText.SetText(sb + "_");
        }
    }

    public void OnCancelButtonClick()
    {
        sb.Clear();
        nameText.SetText(sb + "_");
    }

    public void OnAcceptButtonClick()
    {
        windowManager.Open(0);
    }
}
