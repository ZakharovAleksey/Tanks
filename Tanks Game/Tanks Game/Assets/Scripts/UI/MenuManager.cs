using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameManager m_GameManager;

    public GameObject m_MainMenu;
    public Canvas m_QuitMenu;
    public Canvas m_EndGameMenu;
    public GameObject m_PauseMenu;

    public Text m_EndGameMessage;

    public bool m_IsGameStarting { get; set; }
    public bool m_IsGamePlaying { get; set; }
    public bool m_IsGameEnding { get; set; }
    public bool m_isGameOnPause { get; set; }

    private void Start()
    {
        m_IsGameStarting = false;
        m_IsGamePlaying = false;
        m_IsGameEnding = false;

        m_isGameOnPause = false;

        // Изначаально только Главное меню активно
        m_MainMenu.gameObject.SetActive(true);
        m_PauseMenu.gameObject.SetActive(false);
        m_QuitMenu.gameObject.SetActive(false);
        m_EndGameMenu.gameObject.SetActive(false);
    }

    #region Main Menu button actions

    public void OnPlayBTNClick()
    {
        m_IsGameStarting = true;

        m_MainMenu.gameObject.SetActive(false);
        m_QuitMenu.gameObject.SetActive(false);
    }

    public void OnQuitBTNClick()
    {
        m_MainMenu.gameObject.SetActive(false);
        m_EndGameMenu.gameObject.SetActive(false);
        m_PauseMenu.gameObject.SetActive(false);

        m_QuitMenu.gameObject.SetActive(true);   
    }

    #endregion

    #region Quit Menu button actions

    public void QuitMenuYesAnswer()
    {
        Application.Quit();
    }

    public void QuitMenuNoAnswer()
    {
        if (m_IsGameStarting && m_isGameOnPause)    // Если попытка выхода была в меню паузы
        {
            // Возвращаемся к меню игры пауза
            m_PauseMenu.gameObject.SetActive(true);
            m_QuitMenu.gameObject.SetActive(false);
        }
        else if (m_IsGameStarting)   // Если попытка выхода была при выигрыше игры
        {
            // Возврящаемся к пост-игровому меню игры
            m_EndGameMenu.gameObject.SetActive(true);
            m_QuitMenu.gameObject.SetActive(false);
        }
        else // Если попытка выхода была до начала игры в Главном меню
        {
            // Возвращаемся в новое меню
            m_MainMenu.gameObject.SetActive(true);
            m_QuitMenu.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Win Menu

    public void SetEndGameMenu()
    {
        // Текст сообщающий цвет игрока одержавшего победу
        m_EndGameMessage.text = m_GameManager.EndGameMessage();

        m_EndGameMenu.gameObject.SetActive(true);
        m_MainMenu.gameObject.SetActive(false);
    }

    public void OnRestartBTNClick()
    {
        m_MainMenu.gameObject.SetActive(false);
        m_EndGameMenu.gameObject.SetActive(false);

        m_GameManager.RestartGame();
    }

    #endregion


    #region PauseMenu

    public void OnPauseBTNClick()
    {
        // Поставим timeScale равный нулю чтобы имитировать паузу
        Time.timeScale = 0;

        m_PauseMenu.gameObject.SetActive(true);
        m_isGameOnPause = true;
    }

    public void OnResumeBTNClick()
    {
        // Поставим timeScale равный нулю чтобы вернуться к нормальному воспроизведению
        Time.timeScale = 1;

        m_PauseMenu.gameObject.SetActive(false);
        m_isGameOnPause = false;
    }

    public void OnMainMenuBTNClick()
    {
        Application.LoadLevel(Application.loadedLevel);

        m_IsGameStarting = false;
        m_IsGamePlaying = false;
        m_IsGameEnding = false;

        m_isGameOnPause = false;
    }

    #endregion

}