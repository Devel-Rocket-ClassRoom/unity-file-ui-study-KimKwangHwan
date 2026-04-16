using UnityEngine;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour
{
    public GameObject firstSelected;

    protected WindowManager windowManager;

    public void Init(WindowManager mgr)
    {
        windowManager = mgr;
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelected);
        // UI Canvas 생성되면 나오는 이벤트 시스템
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
