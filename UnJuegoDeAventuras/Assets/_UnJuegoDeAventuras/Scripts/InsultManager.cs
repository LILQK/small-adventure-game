using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class InsultManager : MonoBehaviour
{
    // Declaraci�n de una matriz de objetos de tipo Insult
    public Insult[] insults;

    // Declaraci�n de un delegado personalizado
    public delegate void OnInsultLoadDelegate();

    // Declaraci�n de una variable que almacenar� referencias a m�todos que sigan el formato del delegado definido arriba
    public OnInsultLoadDelegate onInsultsLoaded;

    // Este m�todo se ejecuta cuando el objeto al que est� adjunta la clase se inicializa
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

        // Verifica si hay m�todos registrados en onInsultsLoaded y, si es as�, los ejecuta
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
