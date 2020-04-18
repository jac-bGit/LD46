using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIBehaviour : MonoBehaviour
{
    [SerializeField] private Text _speedText;

    [SerializeField] private PlayerBehaviour _playerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Restart();

        _speedText.text = _playerBehaviour.CurrentSpeed.ToString();
    }

    void Restart(){
        if(Input.GetKeyDown(KeyCode.R))
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
