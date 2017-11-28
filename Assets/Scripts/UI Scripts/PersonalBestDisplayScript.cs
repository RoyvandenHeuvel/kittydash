using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PersonalBestDisplayScript : MonoBehaviour
{
    private Text _text;

    private string _initialText;

    void Start()
    {
        _text = GetComponent<Text>();
        _initialText = _text.text;
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = string.Format(_initialText, GameManager.Instance.SaveData.PersonalBest);
    }
}
