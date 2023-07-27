using RPGCharacterAnims.Actions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject menu;
    
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
        Task.text = "任務找到寶劍";
        if (menu == null)
        {
            menu = GameObject.Find("menu");
            menu.SetActive(!menu.activeInHierarchy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Gameover()
    {
        yield return new WaitForSeconds(3f);
        Task.text = "你殺死了";
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
    {   IdleEvent idle = IdleEvent.Instance();
        SceneManager.LoadScene("testbomb");
        if (idle.Hp == 0)
        {
            idle.ModifyHp(10);
        }
        gameover.SetActive(false);
        replaybutton.SetActive(false);
        IdleEvent.noMove = false;
        idle.wea = false;
        idle.weapon.SetActive(idle.wea);
        idle.startPosition(1);
        if (idle.animator.GetCurrentAnimatorStateInfo(0).IsName("dead")|| idle.animator.GetCurrentAnimatorStateInfo(0).IsName("weaponidle"))
        {
            
            idle.animator.SetBool("weapon", false);
        }
        Start();

    }
    public void NextLevel()
    {
        IdleEvent idle = IdleEvent.Instance();
        Task.text = "第二關開始"+"\n殺掉所有敵人";
        SceneManager.LoadScene("testbomb2");
        idle.wea = true;
        idle.weapon.SetActive(idle.wea);
        idle.startPosition(2);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        
        menu.SetActive(!menu.activeInHierarchy);
    }

}
