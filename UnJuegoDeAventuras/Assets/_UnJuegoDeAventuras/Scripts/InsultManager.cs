using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class InsultManager : MonoBehaviour
{
    // Declaración de una matriz de objetos de tipo Insult
    public Insult[] insults;

    // Declaración de un delegado personalizado
    public delegate void OnInsultLoadDelegate();

    // Declaración de una variable que almacenará referencias a métodos que sigan el formato del delegado definido arriba
    public OnInsultLoadDelegate onInsultsLoaded;

    // Este método se ejecuta cuando el objeto al que está adjunta la clase se inicializa
    void Start()
    {
        // Carga un archivo de recursos llamado "insults" como TextAsset
        TextAsset textAsset = Resources.Load("insults") as TextAsset;

        // Obtiene el contenido de texto del TextAsset
        string json = textAsset.text;

        // Deserializa el JSON en un objeto de tipo InsultContainer
        InsultContainer container = JsonUtility.FromJson<InsultContainer>(json);

        // Asigna el arreglo de Insults desde el contenedor al arreglo de insults en esta clase
        insults = container.insults;

        // Verifica si hay métodos registrados en onInsultsLoaded y, si es así, los ejecuta
        if (onInsultsLoaded != null)
            onInsultsLoaded();
    }
}

// Esta es una clase serializable que contiene una matriz de objetos de tipo Insult
[System.Serializable]
public class InsultContainer
{
    public Insult[] insults;
}
