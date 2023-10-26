using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InsultUI : MonoBehaviour
{
    [SerializeField] RectTransform answersPanel;
    [SerializeField] RectTransform insultsPanel;
    [SerializeField] InsultManager insultManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject textPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Verifica si insultManager no es nulo y suscribe el método OnInsultLoadedCallback al evento onInsultsLoaded
        if (insultManager != null)
            insultManager.onInsultsLoaded += OnInsultLoadedCallback;

        // Verifica si gameManager no es nulo y suscribe los métodos correspondientes a los eventos
        if (gameManager != null)
        {
            gameManager.playerInsultRoundStarted += OnPlayerInsultRound;
            gameManager.playerResponseRoundStarted += OnPlayerResponse;
            gameManager.onGameEnded += OnGameEnded;
        }

        // Desactiva los paneles de respuestas e insultos al inicio
        answersPanel.gameObject.SetActive(false);
        insultsPanel.gameObject.SetActive(false);
    }

    private void OnGameEnded() {
        answersPanel.gameObject.SetActive(false);
        insultsPanel.gameObject.SetActive(false);
    }
    private void OnPlayerResponse()
    {
        Debug.Log("Activo panel de respuestas");
        // Activa el panel de respuestas y desactiva el panel de insultos
        answersPanel.gameObject.SetActive(true);
        insultsPanel.gameObject.SetActive(false);
    }

    private void OnPlayerInsultRound()
    {
        Debug.Log("Activo panel de insultos");
        // Activa el panel de insultos y desactiva el panel de respuestas
        answersPanel.gameObject.SetActive(false);
        insultsPanel.gameObject.SetActive(true);
    }

    private void OnInsultLoadedCallback()
    {
        // Convierte la lista de insultos a una lista y la mezcla (shuffle)
        List<Insult> insults = insultManager.insults.ToList();
        insults.Shuffle();

        // Para cada insulto en la lista
        foreach (Insult _insult in insults)
        {
            // Instancia un nuevo TextMeshProUGUI a partir del prefab en el panel de insultos
            TextMeshProUGUI insultText = Instantiate(textPrefab, insultsPanel).GetComponent<TextMeshProUGUI>();

            // Añade un listener para el evento de clic en el botón asociado al insulto
            insultText.GetComponent<Button>().onClick.AddListener(delegate {
                OnInsultSelected(_insult._Insult);
            });

            // Establece el texto del TextMeshProUGUI al insulto
            insultText.text = _insult._Insult;
        }

        // Mezcla nuevamente la lista de insultos
        insults.Shuffle();

        // Para cada insulto en la lista mezclada
        foreach (Insult _insult in insults)
        {
            // Instancia un nuevo TextMeshProUGUI a partir del prefab en el panel de respuestas
            TextMeshProUGUI responseText = Instantiate(textPrefab, answersPanel).GetComponent<TextMeshProUGUI>();

            // Añade un listener para el evento de clic en el botón asociado a la respuesta correcta
            responseText.GetComponent<Button>().onClick.AddListener(delegate {
                OnAnserSelected(_insult._CorrectAnswer);
            });

            // Establece el texto del TextMeshProUGUI a la respuesta correcta
            responseText.text = _insult._CorrectAnswer;
        }
    }

    private void OnAnserSelected(string answer)
    {
        Debug.Log(answer);
        // Llama al método SelectAnswer del gameManager con la respuesta seleccionada
        gameManager.SelectAnswer(answer);
    }

    private void OnInsultSelected(string insult)
    {
        Debug.Log(insult);
        // Llama al método SelectInsult del gameManager con el insulto seleccionado
        gameManager.SelectInsult(insult);
    }

    private void OnDisable()
    {
        // Desuscribe los métodos de los eventos para evitar posibles errores
        insultManager.onInsultsLoaded -= OnInsultLoadedCallback;
        gameManager.playerInsultRoundStarted -= OnPlayerInsultRound;
        gameManager.playerResponseRoundStarted -= OnPlayerResponse;
    }
}
