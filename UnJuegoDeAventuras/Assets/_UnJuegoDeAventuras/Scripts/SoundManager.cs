using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta es la clase principal que maneja los sonidos
public class SoundManager : MonoBehaviour
{
    // Variables para almacenar los diferentes sonidos
    [SerializeField] AudioClip getHitSound;  // Sonido cuando el personaje es golpeado
    [SerializeField] AudioClip hitSound;     // Sonido cuando el personaje realiza un golpe
    [SerializeField] AudioClip swordsSound;  // Sonido de espadas
    [SerializeField] AudioClip uiButtonSelectSound; // Sonido de botones de interfaz de usuario
    [SerializeField] AudioClip dieSound;     // Sonido cuando el personaje muere
    [SerializeField] AudioClip gameStart;    // Sonido al iniciar el juego
    [SerializeField] AudioClip gameEnd;      // Sonido al finalizar el juego

    private AudioSource _audioSource; // Componente que reproduce los sonidos

    public static SoundManager Instance; // Instancia est�tica de SoundManager

    // Awake se llama antes de Start y se utiliza para inicializaciones
    private void Awake()
    {
        // Verifica si ya hay una instancia existente de SoundManager
        if (Instance != null && Instance != this)
        {
            // Si existe, destruye esta instancia
            Destroy(this.gameObject);
        }
        else
        {
            // Si no existe, asigna esta instancia a la variable est�tica
            Instance = this;
        }

        // No destruye este objeto al cargar una nueva escena
        DontDestroyOnLoad(this.gameObject);
    }

    // Start se llama antes del primer frame de la ejecuci�n
    void Start()
    {
        // Obtiene el componente AudioSource del objeto actual
        _audioSource = GetComponent<AudioSource>();
    }

    // M�todo para reproducir un sonido espec�fico
    public void PlaySound(CustomSounds sound)
    {
        AudioClip clip = null;
        // Seg�n el tipo de sonido proporcionado, asigna el AudioClip correspondiente
        switch (sound)
        {
            case CustomSounds.getHit:
                clip = getHitSound;
                break;
            case CustomSounds.hit:
                clip = hitSound;
                break;
            case CustomSounds.sword:
                clip = swordsSound;
                break;
            case CustomSounds.dieSound:
                clip = dieSound;
                break;
            case CustomSounds.uiButton:
                clip = uiButtonSelectSound;
                break;
            case CustomSounds.end:
                clip = gameEnd;
                break;
            case CustomSounds.start:
                clip = gameStart;
                break;
        }
        // Reproduce el sonido una vez
        if (clip != null) {
            _audioSource.PlayOneShot(clip);
        }
    }
}

// Enumeraci�n para los diferentes tipos de sonidos
public enum CustomSounds
{
    getHit,    // Sonido cuando el personaje es golpeado
    hit,       // Sonido cuando el personaje realiza un golpe
    sword,     // Sonido de espadas
    uiButton,  // Sonido de botones de interfaz de usuario
    dieSound,  // Sonido cuando el personaje muere
    start,     // Sonido al iniciar el juego
    end        // Sonido al finalizar el juego
}
