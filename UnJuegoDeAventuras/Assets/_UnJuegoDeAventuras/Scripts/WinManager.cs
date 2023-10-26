using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinManager : MonoBehaviour
{
    // Nombre de la escena a la que se debe cargar cuando el juego termina
    public string EndScene;

    // Texto que se mostrará en la pantalla de victoria
    string winText;

    // Referencia al objeto de texto en la interfaz de usuario
    TextMeshProUGUI winTextUi;

    // Instancia estática del WinManager para asegurar que solo haya uno en el juego
    public static WinManager Instance { get; private set; }

    // Se ejecuta cuando el objeto se inicializa
    private void Awake()
    {
        // Verificar si ya existe una instancia de WinManager
        if (Instance != null && Instance != this)
        {
            // Si hay una instancia previa, destruir este objeto
            Destroy(this);
        }
        else
        {
            // Si no hay una instancia previa, establecer esta como la instancia activa
            Instance = this;
        }
    }

    // Se ejecuta al inicio del juego
    private void Start()
    {
        // No destruir este objeto cuando se carga una nueva escena
        DontDestroyOnLoad(this);

        // Suscribirse al evento que se dispara cuando se carga una nueva escena
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    // Se ejecuta cuando se carga una nueva escena
    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        // Verificar si la escena cargada es la escena final
        if (arg0.name == EndScene)
        {
            // Encontrar el objeto de texto de victoria en la escena
            winTextUi = GameObject.FindGameObjectWithTag("wintext").GetComponent<TextMeshProUGUI>();
            // Verificar si se encontró el objeto de texto
            if (winTextUi != null)
            {
                // Establecer el texto de victoria
                winTextUi.text = winText;
            }
        }
    }

    // Método para manejar la condición de victoria
    public void OnWin(Winner winner)
    {
        // Determinar el texto de victoria según el ganador
        if (winner == Winner.Player)
        {
            winText = "You win!";
        }
        else
        {
            winText = "You lost, good luck next time";
        }

        // Cargar la escena final
        SceneManager.LoadScene(EndScene);
    }
}

// Enumeración que define los posibles ganadores
public enum Winner
{
    Player,
    Ai
}
