using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI Score1;
    [SerializeField] public TextMeshProUGUI Score2;

    void Update() {
        Score1.text = CollectObjects.Score.ToString();
        if(CollectObjects.GetKey){
            Score2.text = 1.ToString();
        }
    }
}