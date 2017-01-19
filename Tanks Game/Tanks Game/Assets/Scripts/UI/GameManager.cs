using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 5;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    public CameraController m_CameraControl;
    public Text m_MessageText;
    public GameObject m_TankPrefab;
    public TankManager[] m_Tanks;
    public GameObject m_Background;

    public MenuManager m_MenuManager;

    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private TankManager m_RoundWinner;
    private TankManager m_GameWinner;
    private SpawnObjects m_SpawnObjectManger;



    private void Start()
    {
        // Это задержки в начале и конце раунда соответственно
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        m_SpawnObjectManger = m_Background.GetComponent<SpawnObjects>();

        SpawnAllTanks();
        SetCameraTargets();
    }

    private void Update()
    {
        if (!m_MenuManager.m_IsGameStarting)    // Если игра еще не начата (мы находимся в основном меню)
        {
            DisableTankControl();
            m_CameraControl.SetStartPositionAndSize();

            // Говорим что теперь можно начинать игру
            m_MenuManager.m_IsGamePlaying = true;
        }
        else if  (m_MenuManager.m_IsGamePlaying)    // Если игра уже начата
        {
            StartCoroutine(GameLoop());

            // Чтобы не заходить в этот цикл каждый раз при Update!
            m_MenuManager.m_IsGamePlaying = false;
        }
        else if (m_MenuManager.m_IsGameEnding)     // Если по итогу сыгранных раундов есть победитель
        {
            DisableTankControl();
            m_CameraControl.SetStartPositionAndSize();

            // Перехордим в пост-игровое меню
            m_MenuManager.SetEndGameMenu();
            m_MenuManager.m_IsGameEnding = false;
        }
    }

    private void SpawnAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].m_Instance = Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();
        }
    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Tanks.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Tanks[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        PostEnding();
    }

    private IEnumerator RoundStarting()
    {
        ResetAllTanks();
        DisableTankControl();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;

        yield return m_StartWait;
    }

    private IEnumerator RoundPlaying()
    {
        EnableTankControl();
        m_SpawnObjectManger.Spawn();

        m_MessageText.text = string.Empty;

        while (!OneTankLeft())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {

        DisableTankControl();
        m_SpawnObjectManger.Remove();

        m_RoundWinner = GetRoundWinner();

        if (m_RoundWinner != null)
            m_RoundWinner.m_Wins++;

        // Get a message based on the scores and whether or not there is a game winner and display it.
        string message = EndRoundMessage();

        m_MessageText.text = message;

        yield return m_EndWait;
    }

    private void PostEnding()
    {
        // Стираем предыдущее сообщение о победе
        m_MessageText.text = "";

        // Определяем есть ли победитель
        m_GameWinner = GetGameWinner();

        if (m_GameWinner != null)
            m_MenuManager.m_IsGameEnding = true;
        else
            StartCoroutine(GameLoop());
    }

    public void RestartGame()
    {
        // Обнуляем количество выигранных игроками раундов  
        for (int i = 0; i < m_Tanks.Length; ++i)
            m_Tanks[i].m_Wins = 0;

        // Говорим что играем заново - первый раунд
        m_RoundNumber = 0;

        // Необходимо пропустить первый цыкл Update и перйти сразу к игре
        m_MenuManager.m_IsGamePlaying = true;
        m_MenuManager.m_IsGameEnding = false;

        Update();
    }



    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }

    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                return m_Tanks[i];
        }

        return null;
    }

    private TankManager GetGameWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
            {
                return m_Tanks[i];
            }
        }

        return null;
    }

    private string EndRoundMessage()
    {
        string message = "";

        if (m_GameWinner == null)
        {
            // Если убили друг друга
            message = "DRAWN!";

            if (m_RoundWinner != null)
                message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

            message += "\n\n\n\n\n";

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                message += m_Tanks[i].m_ColoredPlayerText + " : " + m_Tanks[i].m_Wins + " WINS\n";
            }
        }

        return message;
    }

    public string EndGameMessage()
    {
        string message = "";

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + "\n WINS THE GAME!";

        return message;
    }

    private void ResetAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].Reset();
        }
    }

    private void EnableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].EnableControl();
        }
    }

    private void DisableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].DisableControl();
        }
    }
}