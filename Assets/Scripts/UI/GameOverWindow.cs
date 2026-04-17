using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStartLabel;
    public TextMeshProUGUI leftStartValue;
    public TextMeshProUGUI rightStartLabel;
    public TextMeshProUGUI rightStartValue;

    public TextMeshProUGUI scoreValue;

    public Button nextButton;

    private int length = 6;
    private Coroutine coPrintStats;
    private string initText;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);
        coPrintStats = null;
        initText = scoreValue.text;
    }

    public override void Open()
    {
        base.Open();
        if (coPrintStats != null)
        {
            StopCoroutine(coPrintStats);
            coPrintStats = null;
        }
        TextReset();
        coPrintStats = StartCoroutine(CoPrintStats());
    }

    private void TextReset()
    {
        leftStartLabel.text = "";
        leftStartValue.text = "";
        rightStartLabel.text = "";
        rightStartValue.text = "";
        scoreValue.text = initText;
    }

    private IEnumerator CoPrintStats()
    {
        int total = 0;
        int i = 0;
        for (; i < length / 2; i++)
        {
            int value = Random.Range(10, 10000);
            yield return new WaitForSeconds(1f);
            leftStartLabel.text += $"STAT {i}\n";
            leftStartValue.text += $"{value:D4}\n";
            total += value;
        }
        for (; i < length; i++)
        {
            int value = Random.Range(10, 10000);
            yield return new WaitForSeconds(1f);
            rightStartLabel.text += $"STAT {i}\n";
            rightStartValue.text += $"{value:D4}\n";
            total += value;
        }
        yield return new WaitForSeconds(1f);

        float elapsed = 0f;
        int current = 0;

        while(elapsed < 3f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 3f;
            current = Mathf.RoundToInt(Mathf.Lerp(0, total, t));
            scoreValue.text = $"{current:D9}";
            yield return null;
        }

        scoreValue.text = $"{total:D9}";
        coPrintStats = null;
    }

    public override void Close()
    {
        if (coPrintStats != null)
        {
            StopCoroutine(coPrintStats);
            coPrintStats = null;
        }
        base.Close();
    }

    public void OnNext()
    {
        windowManager.Open(0);   
    }
}
