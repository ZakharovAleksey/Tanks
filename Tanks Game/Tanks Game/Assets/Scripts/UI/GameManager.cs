using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 1;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 1f;
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

    private bool m_ISGameInProcess = false;
    


    private void Start()
    {
        // Create the delays so they only have to be made once.
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        m_SpawnObjectManger = m_Background.GetComponent<SpawnObjects>();

        SpawnAllTanks();
        SetCameraTargets();
    }

    private void Update()
    {

        if (!MenuManager.m_IsGameStarted && !MenuManager.m_IsGameProceed)
        {
            DisableTankControl();
            m_CameraControl.SetStartPositionAndSize();

            MenuManager.m_IsGameFinished = false;
        }

        // If game is started but not in process yet - initilize game
        if (MenuManager.m_IsGameStarted && !MenuManager.m_IsGameProceed)
        {
            StartCoroutine(GameLoop());

            MenuManager.m_IsGameStarted = false;
            MenuManager.m_IsGameProceed = true;
        }
        
        if (MenuManager.m_IsGameFinished)
        {
            m_MenuManager.SetWinMenu();

            DisableTankControl();
            m_CameraControl.SetStartPositionAndSize();
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


    public void StartNewGame()
    {
        
        for (int i = 0; i < m_Tanks.Length; ++i)
            m_Tanks[i].m_Wins = 0;
        
        m_RoundNumber = 0;

        MenuManager.m_IsGameStarted = true;
        MenuManager.m_IsGameProceed = false;

        MenuManager.m_IsGameFinished = false;

        Update();
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());
        
        if (m_GameWinner != null)
        {
            MenuManager.m_IsGameFinished = true;
            //Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        print("Start " + m_RoundNumber);
        ResetAllTanks();
        DisableTankControl();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;

        yield return m_StartWait;
    }

    private IEnumerator RoundPlaying()
    {
        print("Play " + m_RoundNumber);

        m_SpawnObjectManger.Set();
        EnableTankControl();

        m_MessageText.text = string.Empty;

        while (!OneTankLeft())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        print("END " + m_RoundNumber);

        DisableTankControl();
        m_SpawnObjectManger.Remove();

        m_RoundWinner = null;

        m_RoundWinner = GetRoundWinner();

        if (m_RoundWinner != null)
            m_RoundWinner.m_Wins++;


        m_GameWinner = GetGameWinner();

        // Get a message based on the scores and whether or not there is a game winner and display it.
        string message = EndRoundMessage();

        m_MessageText.text = message;


        yield return m_EndWait;
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

    // Returns a string message to display at the end of each round.
    private string EndRoundMessage()
    {
        // Default message
        string message = "";

        if (m_GameWinner == null)
        {
            message = "DRAWN!";

            if (m_RoundWinner != null)
                message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

            message += "\n\n\n\n";

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
            }
        }

        return message;
    }

    public string EndGameMessage()
    {
        string winMessage = "";

        if (m_GameWinner != null)
            winMessage = m_GameWinner.m_ColoredPlayerText + "\n WINS THE GAME!";

        return winMessage;
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