using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PersonalBestDisplayScript : MonoBehaviour
{
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = string.Format("{0}", GameManager.Instance.SaveData.PersonalBest);
    }
}
