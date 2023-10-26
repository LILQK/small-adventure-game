using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    // Referencia al componente de texto
    private TMP_Text text;
    // Texto completo que se va a escribir
    private string fullText;
    // Tiempo entre cada letra
    public float timeBetweenLetters = 0.1f;

    // Texto actual que se ha escrito hasta el momento
    private string currentText = "";
    // Tiempo en el que se escribi� la �ltima letra
    private float lastLetterTime = 0f;
    // �ndice de la letra actual
    private int index = 0;

    void Start()
    {
        // Obtiene el componente de texto asociado al objeto
        text = GetComponent<TMP_Text>();
        // Inicializa el texto como vac�o
        text.text = "";
    }

    void Update()
    {
        // Verifica si no hay texto completo, en cuyo caso no hace nada
        if (fullText == null) return;

        // Verifica si a�n no se han escrito todas las letras
        if (index < fullText.Length)
        {
            // Verifica si ha pasado suficiente tiempo desde la �ltima letra escrita
            if (Time.time - lastLetterTime > timeBetweenLetters)
            {
                // Agrega la siguiente letra al texto actual
                currentText += fullText[index];
                // Actualiza el texto en el objeto
                text.text = currentText;
                // Incrementa el �ndice y actualiza el tiempo de la �ltima letra escrita
                index++;
                lastLetterTime = Time.time;
            }
        }
    }

    public void WriteText(string newText)
    {
        // Establece el nuevo texto a escribir
        fullText = newText;
        // Resetea el texto actual
        currentText = "";
        // Resetea el �ndice de la letra actual
        index = 0;
        // Resetea el tiempo de la �ltima letra escrita al tiempo actual
        lastLetterTime = Time.time;
    }
}
