using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables publicas accesibles desde el inspector
    public int MaxScore;
    public GameState _GameState;
    public string currentInsult = string.Empty;

    [SerializeField] InsultManager insultManager;

    //Delegados y eventos para eventos especificos del juego
    public delegate void OnPlayerInsultRoundStarted();
    public OnPlayerInsultRoundStarted playerInsultRoundStarted;

    public delegate void OnPlayerResponseRoundStarted();
    public OnPlayerInsultRoundStarted playerResponseRoundStarted;

    public delegate void OnGameEnded();
    public OnGameEnded onGameEnded;
    // Variables para puntuaciones y temporizadores

    public int playerScore;
    public int aiScore;

    //Referencias a los textos de jugadores
    public TypeWriter playerText;
    public TypeWriter enemyText;

    public float fightTime;
    private float fightTimer;

    public float endTime;
    private float endTimer;

    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator enemyAnimator;

    private Winner gameWiner;

    // Método que se ejecuta al inicio del juego

    void Start()
    {
        SoundManager.Instance.PlaySound(CustomSounds.start);
        playerScore = 0;
        aiScore = 0;

        fightTimer = fightTime;

        insultManager.onInsultsLoaded += OnInsultsLoadCallback;
    }

    private void OnDestroy()
    {
        insultManager.onInsultsLoaded -= OnInsultsLoadCallback;
    }

    // Método que se ejecuta cuando se cargan los insultos

    private void OnInsultsLoadCallback()
    {
        int r = UnityEngine.Random.Range(0, 2);
        SetNextState(r == 0 ? GameState.PlayerInsults : GameState.AiInsults);
    }

    // Método que se ejecuta en cada frame del juego

    void Update()
    {
        GameStateMachine();
    }

    // Máquina de estados que maneja el flujo del juego

    private void GameStateMachine() {
        switch (_GameState)
        {
            case GameState.Fight:
                FightState();
                break;
            case GameState.End:
                EndWaitTime();
                break;
        }
    }

    //Bucle de final de juego
    private void EndWaitTime() {
 

        if (endTimer < endTime)
        {
            endTimer += Time.deltaTime; //Mientras el tiempo no haya transcurrido seguimos sumando tiempo
        }
        else {
            WinManager.Instance.OnWin(gameWiner); //Al acabar el tiempo ejecutamos la funcion de final de juego
        }
    }

    private void FightState()
    {
        fightTimer -= Time.deltaTime; // Mientras peleen restamos tiempo

        //Activamos las animaciones de combate
        playerAnimator.SetTrigger("attack");
        enemyAnimator.SetTrigger("attack");
        if (fightTimer <= 0)
        {
            fightTimer = 0;
        }
    }
    private void SetNextState(GameState _state) {

        _GameState = _state;
        switch (_state)
        {
            case GameState.AiRespond:
                StartAiRespond();
                break;
            case GameState.AiInsults:
                StartAiInsults();
                break;
            case GameState.PlayerRespond:
                StartPlayerRespond();
                Debug.Log("Player responds");
                break;
            case GameState.PlayerInsults:
                StartPlayerInsult();
                Debug.Log("Player Insults");
                break;
            case GameState.Fight:
                fightTimer = fightTime; //Reiniciamos el temporizador
                //Borramos los textos de los jugadores
                playerText.WriteText(" ");
                enemyText.WriteText(" ");
                break;
            case GameState.End:
                StartEndTime();
                break;
        }

    }

    private void StartEndTime() {
        //Activamos sonido de fin de juego
        SoundManager.Instance.PlaySound(CustomSounds.end);

        if (gameWiner == Winner.Player)
        {
            enemyAnimator.SetBool("isDead", true);//Animacion de muerte del enemigo

            SoundManager.Instance.PlaySound(CustomSounds.dieSound);
        }
        else
        {
            //Animacion y sonido de muerte del jugador
            SoundManager.Instance.PlaySound(CustomSounds.dieSound);
            playerAnimator.SetBool("isDead", true);
        }
        
    }
    private void StartPlayerInsult()
    {
        //Cuando empiece el turno del jugador llamamos al delegado
        if (playerInsultRoundStarted != null)
            playerInsultRoundStarted();
        
    }

    private void StartPlayerRespond()
    {
        //Cuando empiece el turno del jugador llamamos al delegado

        if (playerResponseRoundStarted != null)
            playerResponseRoundStarted();
    }

    private void StartAiInsults()
    {
        //Elegimos un insulto aleatorio y lo mostramos en pantalla
        currentInsult = SelectRandomInsult();
        enemyText.WriteText(currentInsult);
        StartCoroutine(WaitForPlayerResponse());
    }

    IEnumerator WaitForPlayerResponse() {
        //Esperamos y pasamos el turno de responder al jugador
        yield return new WaitForSeconds(2f);

        SetNextState(GameState.PlayerRespond);
    }

    private void StartAiRespond()
    {
        //Elegimos una respuesta aleatoria y la escribimos
        string response = SelectRandomAnswer();
        enemyText.WriteText(response);

        StartCoroutine(SelectEnemyAnswerAndFight(response));
    }

    IEnumerator SelectEnemyAnswerAndFight(string answer)
    {
        //Esperamos para que se acabe de mostrar el texto y empezamos a pelear
        yield return new WaitForSeconds(4f);

        SetNextState(GameState.Fight);

        SoundManager.Instance.PlaySound(CustomSounds.sword);

        //Esperamos a acabar de pelear y comprobamos si la respuesta era correcta
        yield return new WaitForSeconds(5f);


        if (CheckCorrectAnswer(answer, currentInsult))
        {
            playerAnimator.SetTrigger("gethit");
            SoundManager.Instance.PlaySound(CustomSounds.getHit);
            yield return new WaitForSeconds(2f);
            aiScore++;
            if (aiScore < MaxScore)
            {
                SetNextState(GameState.AiInsults);
            }
            else {
                gameWiner = Winner.Ai;
                SetNextState(GameState.End);
            }
        }else
        {
            enemyAnimator.SetTrigger("gethit");
            SoundManager.Instance.PlaySound(CustomSounds.hit);
            yield return new WaitForSeconds(2f);
            playerScore++;
            if (playerScore < MaxScore)
            {
                SetNextState(GameState.PlayerInsults);
            }
            else
            {
                gameWiner = Winner.Player;
                SetNextState(GameState.End);
            }
        }
    }

    public void SelectAnswer(string answer) {

        playerText.WriteText(answer);
        StartCoroutine(SelectAnswerAndFight(answer));
    }


    IEnumerator SelectAnswerAndFight(string answer) {
        //Esperamos a que se muestre el texto y iniciamos la pelea
        yield return new WaitForSeconds(4f);

        SetNextState(GameState.Fight);

        SoundManager.Instance.PlaySound(CustomSounds.sword);

        yield return new WaitForSeconds(5f);
        //Esperamos a acabar de pelear y comprobamos si la respuesta era correcta


        if (CheckCorrectAnswer(answer, currentInsult))
        {
            enemyAnimator.SetTrigger("gethit");

            SoundManager.Instance.PlaySound(CustomSounds.hit);
            yield return new WaitForSeconds(2f);
            playerScore++;
            if (playerScore < MaxScore)
            {
                SetNextState(GameState.PlayerInsults);
            }
            else {
                gameWiner = Winner.Player;
                SetNextState(GameState.End);
            }
        }
        else
        {
            playerAnimator.SetTrigger("gethit");
            SoundManager.Instance.PlaySound(CustomSounds.getHit);
            yield return new WaitForSeconds(2f);
            aiScore++;
            if (aiScore < MaxScore)
            {
                SetNextState(GameState.AiInsults);
            }
            else
            {
                gameWiner = Winner.Ai;
                SetNextState(GameState.End);
            }
        }
    }
    public void SelectInsult(string insult) {

        playerText.WriteText(insult);
        currentInsult = insult;

        StartCoroutine(WaitForResponse());
    }

    IEnumerator WaitForResponse() {

        yield return new WaitForSeconds(5f);

        
        if (_GameState == GameState.PlayerInsults)
        {
            SetNextState(GameState.AiRespond);
        }
        else if (_GameState == GameState.AiInsults)
        {
            SetNextState(GameState.PlayerRespond);
        }
    }

    //Funcion para seleccionar insultos aleatorios
    private string SelectRandomInsult() {

        string insult = string.Empty;

        Insult[] insults = insultManager.insults; //Recopilamos los insultos

        int r = UnityEngine.Random.Range(0, insults.Length - 1);

        insult = insults[r]._Insult; //Elegimos uno al azar

        return insult;
    }

    //Funcion para seleccionar respuestas aleatoris
    private string SelectRandomAnswer()
    {

        string insult = string.Empty;

        Insult[] insults = insultManager.insults;

        int r = UnityEngine.Random.Range(0, insults.Length);

        insult = insults[r]._CorrectAnswer;

        return insult;
    }

    //Funcion para comprobar si la respuestas son correctas
    private bool CheckCorrectAnswer(string answer, string currentInsult)
    {
        foreach (Insult insult in insultManager.insults) //Iteramos en cada insulto que haya guardado
        {
            if (insult._Insult.Equals(currentInsult, StringComparison.OrdinalIgnoreCase))//Buscamos el insulto correspondiente a nuestra respuesta
            {
                return insult._CorrectAnswer.Equals(answer, StringComparison.OrdinalIgnoreCase);//Comprobamos si la respuesta es la misma que la respuesta correcta
            }
        }

        return false;
    }

}

//Enum para los estados de juego
public enum GameState {
    AiInsults,
    PlayerRespond,
    PlayerInsults,
    AiRespond,
    Fight,
    Wait,
    End
}
