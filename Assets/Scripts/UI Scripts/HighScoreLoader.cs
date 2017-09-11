using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighScoreLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(HighScoreUtilities.GetScores(GetComponent<Text>()));
    }
}