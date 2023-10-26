using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuManager : MonoBehaviour
{
    // Variable para almacenar el nombre de la escena del juego.
    public string gameScene;

    // Referencia a los botones en el Inspector de Unity.
    [SerializeField] Button exitButton;
    [SerializeField] Button startButton;

    // M�todo Start se ejecuta al inicio del objeto.
    void Start()
    {
        // A�ade un listener al bot�n de salida para detectar cuando se presiona.
        exitButton.onClick.AddListener(OnExitButtonPressed);

        // A�ade un listener al bot�n de inicio para detectar cuando se presiona.
        startButton.onClick.AddListener(OnStartButtonPressed);
    }

    // M�todo llamado cuando se presiona el bot�n de inicio.
    private void OnStartButtonPressed()
    {
        // Reproduce un sonido usando el SoundManager y el enum CustomSounds.
        SoundManager.Instance.PlaySound(CustomSounds.uiButton);

        // Carga la escena del juego.
        SceneManager.LoadScene(gameScene);
    }

    // M�todo llamado cuando se presiona el bot�n de salida.
    private void OnExitButtonPressed()
    {
        // Reproduce un sonido usando el SoundManager y el enum CustomSounds.
        SoundManager.Instance.PlaySound(CustomSounds.uiButton);

        // Cierra la aplicaci�n (esto solo funciona en compilaciones de ejecutables).
        Application.Quit();
    }
}
