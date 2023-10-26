# Un Juego de aventuras por Ethan Navarro

## Contenido

1. [Introducción](#introducción)
2. [Scripts](#scripts-explicados)
3. [Uso](#uso)
4. [Recursos Externos](#recursos-externos)
5. [Créditos](#créditos)
6. [Licencia](#licencia)

## Introducción

El juego que podreis descargar se basa en la famosa mecanica del juego Secret Of Monkey Island, esta desarrollado en Unity 2022.3.10. La mecanica principal es la pelea de insultos entre dos jugadores(jugador y maquina). El jugador debera intentar adivinar la respuesta correcta del insulto que le lance la maquina para poder lanzar  el insultos. Cada vez que alguien acierta una respuesta la suma un punto y al llegar a 3 ese jugador gana.

### Scripts Explicados:

1. **GameManager.cs**
   - Gestiona el flujo del juego, incluyendo el inicio, la carga de insultos, la lógica de lucha, y el final del juego. 
   - Controla las puntuaciones, los temporizadores y las animaciones de los personajes.
   - Administra los estados del juego (inicio, insultos, respuestas, lucha, etc.).
   
2. **Insult.cs**
   - Clase serializable que define un par de datos: un insulto y su respuesta correcta.

3. **InsultManager.cs**
   - Carga y administra los insultos utilizados en el juego desde un archivo JSON.
   - Contiene un delegado personalizado para notificar cuando los insultos están cargados.

4. **InsultUI.cs**
   - Maneja la interfaz de usuario durante el juego, mostrando insultos y respuestas disponibles.
   - Permite a los jugadores seleccionar una respuesta o insulto.

5. **TypeWriter.cs**
   - Simula el efecto de una máquina de escribir para mostrar texto gradualmente en la pantalla.
   - Utilizado para la presentación de insultos y respuestas en el juego.

6. **WinManager.cs**
   - Gestiona el flujo del juego después de que se determina un ganador.
   - Cambia a una escena de victoria y muestra el texto correspondiente.

7. **SoundManager.cs**
   - Controla la reproducción de diferentes sonidos en el juego (golpes, espadas, etc.).
   - Asocia sonidos específicos a eventos y acciones del juego.

8. **ListExtension.cs**
   - Extensión para mezclar aleatoriamente los elementos de una lista.

9. **EndMenuManager.cs**
   - Gestiona el menú de fin de juego, permitiendo al jugador regresar al menú principal.



## Uso

A continuacion dejo un video del funcionamiento del juego:
- [Ver video](https://www.youtube.com/watch?v=qtgQOft0M-0)

O tambien se puede jugar en navegador desde el siguiente enlace (algunas extensiones pueden interferir con el funcionamiento):
- [Jugar](www.ethannavarro.site/juegos/unjuegodeaventuras/index.html)

## Recursos Externos

- [Sonidos del Asset Store](https://assetstore.unity.com/packages/audio/sound-fx/game-music-stingers-and-ui-sfx-2-pack-112051)
- [Sonido de Espadas en FreeSound](https://freesound.org/people/PorkMuncher/sounds/263595/)
- [Personaje del Enemigo en el Asset Store](https://assetstore.unity.com/packages/2d/characters/medieval-warrior-pack-2-174788)
![Personaje](https://assetstorev1-prd-cdn.unity3d.com/key-image/36c4ab57-3392-43ef-a0ac-ae177dba9d33.webp){: width="200px"}
- [Personaje del Jugador en el Asset Store](https://assetstore.unity.com/packages/2d/characters/martial-hero-170422)![Personaje jugable](https://assetstorev1-prd-cdn.unity3d.com/key-image/c6a0fd1e-57f7-45df-b18b-1331791f06c4.webp){: width="200px"}
- [Sprites del Entorno en el Asset Store](https://assetstore.unity.com/packages/2d/characters/gothicvania-town-101407)
![Entorno](https://assetstorev1-prd-cdn.unity3d.com/key-image/4bc799f3-ac24-4168-8c32-0bf4a7c092a7.webp){: width="200px"}
- [Fuente de Texto en Google Fonts](https://fonts.google.com/specimen/VT323)

---

### Créditos

#### Recursos Utilizados:

- **Sonidos del Asset Store**
  - [Game Music Stingers and UI SFX 2 Pack](https://assetstore.unity.com/packages/audio/sound-fx/game-music-stingers-and-ui-sfx-2-pack-112051) por [WOW SOUND](https://assetstore.unity.com/publishers/19233).

- **Sonido de Espadas**
  - [Espadas - Sonido de FreeSound.org](https://freesound.org/people/PorkMuncher/sounds/263595/) por [PorkMuncher](https://freesound.org/people/PorkMuncher/).

- **Personaje del Enemigo**
  - [Medieval Warrior Pack 2](https://assetstore.unity.com/packages/2d/characters/medieval-warrior-pack-2-174788) por [Luzio Melo](https://assetstore.unity.com/publishers/34852).

- **Personaje del Jugador**
  - [Martial Hero](https://assetstore.unity.com/packages/2d/characters/martial-hero-170422) por [Luiz Melo](https://assetstore.unity.com/publishers/34852).

- **Sprites del Entorno**
  - [Gothicvania Town](https://assetstore.unity.com/packages/2d/characters/gothicvania-town-101407) por [Ansimuz](https://assetstore.unity.com/publishers/18720).

- **Fuente Utilizada**
  - [VT323](https://fonts.google.com/specimen/VT323) de Google Fonts.

---

## Licencia

Este proyecto está bajo la Licencia MIT - consulte el archivo [LICENSE](https://gitlab.com/ethanavarro/pec1-ethan-navarro/-/blob/main/LICENSE) para obtener más detalles.
---
