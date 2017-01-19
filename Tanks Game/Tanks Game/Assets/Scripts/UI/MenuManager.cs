using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameManager m_GameManager;

    public GameObject m_MainMenu;
    public Canvas m_QuitMenu;
    public Canvas m_EndGameMenu;

    public Text m_EndGameMessage;

    public bool m_IsGameStarting { get; set; }
    public bool m_IsGamePlaying { get; set; }
    public bool m_IsGameEnding { get; set; }


    private void Start()
    {
        m_IsGameStarting = false;
        m_IsGamePlaying = false;
        m_IsGameEnding = false;

        // Изначаально только Главное меню активно
        m_MainMenu.gameObject.SetActive(true);
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
        if (m_IsGameStarting)   // При выходе после того как закончился раунд
        {
            // Возврящаемся к пост-игровому меню игры
            m_EndGameMenu.gameObject.SetActive(true);
            m_QuitMenu.gameObject.SetActive(false);
        }
        else // При выходе до начала игры
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

    public void OnRepeatClick()
    {
        m_MainMenu.gameObject.SetActive(false);
        m_EndGameMenu.gameObject.SetActive(false);

        m_GameManager.RestartGame();
    }

    #endregion


}