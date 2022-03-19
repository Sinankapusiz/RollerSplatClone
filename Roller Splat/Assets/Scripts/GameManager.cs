using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] _grounds;
    public float _groundNumbers;
    private int _currentLevel;
    public Text downLevel, upLevel;
    void Start()
    {
        /*_grounds = GameObject.FindGameObjectsWithTag("Ground");
        Debug.Log("Parça Sayısı : "+_grounds.Length);
        _groundNumbers = _grounds.Length;
        _currentLevel = SceneManager.GetActiveScene().buildIndex;*/
        ParcaSayisiniBul();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LeveLUpdate() {
        SceneManager.LoadScene(_currentLevel + 1);
    }
    public void ParcaSayisiniBul()
    {
        _grounds = GameObject.FindGameObjectsWithTag("Ground");
        Debug.Log("Parça Sayısı : " + _grounds.Length);
        _groundNumbers = _grounds.Length;
        _currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

}
