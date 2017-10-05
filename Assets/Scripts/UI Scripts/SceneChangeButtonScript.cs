using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChangeButtonScript : MonoBehaviour
{
    private Button @Button;
    public string SceneName;

    void Start()
    {
        @Button = GetComponent<Button>();
        @Button.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
}
