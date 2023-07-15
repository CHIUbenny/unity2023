using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanger : MonoBehaviour
{
    public static UImanger Instance;

    public GameObject replaybutton;
    public GameObject gameover;
    public Text Task;
    public Image hpBer;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
                DontDestroyOnLoad(gameObject);
            
           
        } else { Destroy(gameObject); }
    }
    void Start()
    {
        Task.text = "���ȧ���_�C";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Gameover()
    {
        yield return new WaitForSeconds(3f);
        Task.text = "�A�Q�����F";
        gameover.SetActive(true);
        replaybutton.SetActive(true);

    }
    public void UpdateHpBar()
    {
        float hpmass=IdleEvent.Instance().Hp/ IdleEvent.Instance(). MaxHp;
        if (hpmass < 0 ) 
        {
            hpmass = 0;
        }else if (hpmass > 1 )
        {
            hpmass = 1;
        }
        hpBer.fillAmount = hpmass;
    }
    public void Replay()
    {
        SceneManager.LoadScene("testbomb");
       IdleEvent.Instance().ModifyHp(10);
        gameover.SetActive(false);
        replaybutton.SetActive(false);
        IdleEvent.Instance().startPosition();
        IdleEvent.noMove = false;
        Start();

    }
    public void NextLevel()
    {
       
        Task.text = "�ĤG���}�l";
        SceneManager.LoadScene("testbomb2");

    }
    public void OnQuit()
    {
        Application.Quit();
    }

}
