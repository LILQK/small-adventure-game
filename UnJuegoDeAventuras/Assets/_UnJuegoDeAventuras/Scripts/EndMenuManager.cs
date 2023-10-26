using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuManager : MonoBehaviour
{

    [SerializeField] Button gobackButton;
    [SerializeField] string MenuScene = "MainMenu";
    // Start is called before the first frame update
    void Start()
    {
        gobackButton.onClick.AddListener(OnGoBackButtonPressed);    
    }

    private void OnGoBackButtonPressed() {
        SceneManager.LoadScene(MenuScene);
    }
}
