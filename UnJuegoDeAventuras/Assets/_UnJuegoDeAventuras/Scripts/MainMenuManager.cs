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

    // Método Start se ejecuta al inicio del objeto.
    void Start()
    {
        // Añade un listener al botón de salida para detectar cuando se presiona.
        exitButton.onClick.AddListener(OnExitButtonPressed);

        // Añade un listener al botón de inicio para detectar cuando se presiona.
        startButton.onClick.AddListener(OnStartButtonPressed);
    }

    // Método llamado cuando se presiona el botón de inicio.
    private void OnStartButtonPressed()
    {
        // Reproduce un sonido usando el SoundManager y el enum CustomSounds.
        SoundManager.Instance.PlaySound(CustomSounds.uiButton);

        // Carga la escena del juego.
        SceneManager.LoadScene(gameScene);
    }

    // Método llamado cuando se presiona el botón de salida.
    private void OnExitButtonPressed()
    {
        // Reproduce un sonido usando el SoundManager y el enum CustomSounds.
        SoundManager.Instance.PlaySound(CustomSounds.uiButton);

        // Cierra la aplicación (esto solo funciona en compilaciones de ejecutables).
        Application.Quit();
    }
}
