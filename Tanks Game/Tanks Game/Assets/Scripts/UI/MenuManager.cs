using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameManager m_GameManager;
    public GameObject m_MainMenu;


    public Text m_GameNameText;

    public Text m_PlayText;
    public Text m_QuitText;

    public Canvas m_QuitMenu;
    private Button m_QuitMenuQuitBTN;
    private Button m_QuitMenuPlayBTN;

    
    public Canvas m_WinMenu;
    public Text m_WinText;
    private Button m_WinMenuRestartBTN;
    private Button m_WinMenuQuiteBTN;

    static public bool m_IsGameStarted = false;
    static public bool m_IsGameProceed = false;
    static public bool m_IsGameFinished = false;

    private void Start()
    {
        if (m_MainMenu.transform.FindChild("PlayBTN"))
        {
            print("LOL");
        }

        m_PlayText.gameObject.SetActive(true);
        m_QuitText.gameObject.SetActive(true);

        m_QuitMenuQuitBTN = m_QuitText.GetComponent<Button>();
        m_QuitMenuPlayBTN = m_PlayText.GetComponent<Button>();


        m_QuitMenu.gameObject.SetActive(false);
        m_WinMenu.gameObject.SetActive(false);
    }

    #region Main Menu

    public void OnPlayBTNClick()
    {
        m_IsGameStarted = true;

        m_GameNameText.gameObject.SetActive(false);
        m_QuitMenuQuitBTN.gameObject.SetActive(false);
        m_QuitMenuPlayBTN.gameObject.SetActive(false);

        m_QuitMenu.gameObject.SetActive(false);
    }

    public void OnQuitBTNClick()
    {
        m_GameNameText.gameObject.SetActive(false);

        m_QuitMenuQuitBTN.gameObject.SetActive(false);
        m_QuitMenuPlayBTN.gameObject.SetActive(false);
        m_WinMenu.gameObject.SetActive(false);

        m_QuitMenu.gameObject.SetActive(true);
        
    }

    #endregion

    #region Quit Menu

    public void QuitMenuYesAnswer()
    {
        print("QUIT GAME!");
        Application.Quit();
    }

    public void QuitMenuNoAnswer()
    {
        m_GameNameText.gameObject.SetActive(true);
        m_QuitMenuQuitBTN.gameObject.SetActive(true);
        m_QuitMenuPlayBTN.gameObject.SetActive(true);

        m_QuitMenu.gameObject.SetActive(false);
    }

    #endregion

    #region Win Menu

    public void SetWinMenu()
    {
        m_WinText.text = m_GameManager.EndGameMessage();

        m_QuitMenuQuitBTN.gameObject.SetActive(false);
        m_QuitMenuPlayBTN.gameObject.SetActive(false);

        m_WinMenu.gameObject.SetActive(true);
    }

    public void OnRepeatClick()
    {
        m_QuitMenuQuitBTN.gameObject.SetActive(false);
        m_QuitMenuPlayBTN.gameObject.SetActive(false);

        m_QuitMenu.gameObject.SetActive(false);
        m_WinMenu.gameObject.SetActive(false);

        m_GameManager.StartNewGame();
    }

    #endregion


}