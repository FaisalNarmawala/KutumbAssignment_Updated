using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;

public class LoginManager : MonoBehaviour
{
    
    public TextMeshProUGUI alertDisplay;
    public TMP_InputField emailId,password;


    private void Start()
    {
        alertDisplay.gameObject.SetActive(false);
    }

    public void TapOnLogin_Button()
    {
        bool loginSuccess = IsValidEmail(emailId.text);

        if (loginSuccess)
        {
            Goto_CharacterAnim();
        }
        else
        {
            WrongUser();
        }
    }

    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password.text.ToString()))
            return false;

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    private void Goto_CharacterAnim()
    {
        SceneManager.LoadScene("CharacterAnim_Humanoid");
    }

    private void WrongUser()
    {
        alertDisplay.gameObject.SetActive(true);
        alertDisplay.text = "Invalid login";
    }
}
