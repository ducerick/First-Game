using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerStackColor : MonoBehaviour
{
    public static GameControllerStackColor Instance;
    [SerializeField] Text textScore;
    private int _score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int valueIn)
    {
        _score += valueIn;
        textScore.text = _score.ToString();
    }
}
