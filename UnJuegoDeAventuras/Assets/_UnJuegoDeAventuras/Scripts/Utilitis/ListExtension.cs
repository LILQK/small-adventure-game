using System;
using System.Collections;
using System.Collections.Generic;

public static class ListExtension
{
    private static Random rand = new Random(); // Se crea una instancia de la clase Random para generar números aleatorios.

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count; // Se obtiene el número de elementos en la lista.
        while (n > 1) // Mientras haya más de un elemento en la lista.
        {
            n--; // Se decrementa el contador.
            int k = rand.Next(n + 1); // Se genera un número aleatorio entre 0 y n (incluyendo n).
            T value = list[k]; // Se guarda el valor del elemento en la posición k.
            list[k] = list[n]; // Se intercambia el elemento en la posición k con el último elemento de la lista.
            list[n] = value; // Se coloca el valor guardado en la última posición de la lista.
        }
    }
}
