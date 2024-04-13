using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button diffButton;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        diffButton = GetComponent<Button>();
        diffButton.onClick.AddListener(SetDifficulty);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void SetDifficulty()
    {
        Debug.Log(gameObject.name + " was clicked");
        gameManager.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
