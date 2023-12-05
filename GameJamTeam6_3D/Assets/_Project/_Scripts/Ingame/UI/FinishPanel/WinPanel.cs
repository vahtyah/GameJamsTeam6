using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    
    private void Start()
    {
        continueButton.onClick.AddListener(ContinueButtonOnClick);
    }

    private void ContinueButtonOnClick()
    {
        gameObject.SetActive(false);    
    }
}