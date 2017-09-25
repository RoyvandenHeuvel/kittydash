using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNameScript : MonoBehaviour
{
    public InputField InputField;

    public void Start()
    {
        //Adds a listener to the main input field and invokes a method when the value changes.
        if (!string.IsNullOrEmpty(GameManager.Instance.GameData.PlayerName))
        {
            InputField.text = GameManager.Instance.GameData.PlayerName;
        }
        InputField.onEndEdit.AddListener(delegate { ValueChanged(); });
    }

    // Invoked when the value of the text field changes.
    public void ValueChanged()
    {
        GameManager.Instance.GameData.PlayerName = InputField.text;
        GameManager.Instance.PostHighScore(GameManager.Instance.GameData.Coins);
        GameManager.Instance.GameData.Coins = 0;
        SceneManager.LoadScene("scene_highscore");
    }
}
