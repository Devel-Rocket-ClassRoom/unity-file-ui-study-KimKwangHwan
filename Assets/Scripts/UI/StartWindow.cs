using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow
{
    public Button continueButton;
    public Button newGameButton;
    public Button optionButton;

    public bool canContinue;

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinue);
        newGameButton.onClick.AddListener(OnNewGame);
        optionButton.onClick.AddListener(OnOption);
    }
    public override void Open()
    {
        continueButton.gameObject.SetActive(canContinue);

        if (!canContinue)
        {
            firstSelected = newGameButton.gameObject;
        }
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnContinue()
    {
        //Debug.Log("Continue");
        windowManager.Open(1);
    }

    public void OnNewGame()
    {
        Debug.Log("New Game");
    }

    public void OnOption()
    {
        Debug.Log("Option");
    }
}
