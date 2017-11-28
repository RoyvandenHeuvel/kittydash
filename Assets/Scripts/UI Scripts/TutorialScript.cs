using CnControls;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    public List<GameObject> Tutorials;
    private Queue<GameObject> _tutorials;
    private GameObject _currentTutorial;

    private void Start()
    {
        _tutorials = new Queue<GameObject>(Tutorials);
        _currentTutorial = GameObject.Instantiate(_tutorials.Dequeue());
    }

    private void Update()
    {
        if (CnInputManager.GetButtonUp("Jump"))
        {
            NextTutorial();
        }
    }

    public void NextTutorial()
    {
        if (_tutorials.Count > 0)
        {
            var peek = _tutorials.Peek();
            if (peek != null)
            {
                GameObject.Destroy(_currentTutorial);
                _currentTutorial = GameObject.Instantiate(_tutorials.Dequeue());
            }
            else
            {
                KittyDashSceneManager.LoadScene("Scene_Ingame");
            }
        }
        else
        {
            KittyDashSceneManager.LoadScene("Scene_Ingame");
        }
    }
}