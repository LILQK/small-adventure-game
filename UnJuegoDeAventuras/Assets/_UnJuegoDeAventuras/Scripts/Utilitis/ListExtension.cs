using System;
using System.Collections;
using System.Collections.Generic;

public static class ListExtension
{
    private static Random rand = new Random(); // Se crea una instancia de la clase Random para generar n�meros aleatorios.

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count; // Se obtiene el n�mero de elementos en la lista.
        while (n > 1) // Mientras haya m�s de un elemento en la lista.
        {
            n--; // Se decrementa el contador.
            int k = rand.Next(n + 1); // Se genera un n�mero aleatorio entre 0 y n (incluyendo n).
            T value = list[k]; // Se guarda el valor del elemento en la posici�n k.
            list[k] = list[n]; // Se intercambia el elemento en la posici�n k con el �ltimo elemento de la lista.
            list[n] = value; // Se coloca el valor guardado en la �ltima posici�n de la lista.
        }
    }
}
