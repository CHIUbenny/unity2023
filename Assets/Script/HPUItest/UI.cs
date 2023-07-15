using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;
    public Image playerHpbar;
    public Slider musicSlider;
    public Slider audioSlider;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this; 
    }
    void Start()
    {
        this.RefreshSettingUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MinusHpBtnClick()
    {
        PlayerCtrl.instance.Damage(10);
    }
    public void UpdatePlayerUI(CPlayerData data)
    {
        float some = data.Hp/data.MaxHp;
        if (some < 0)
        {
            some = 0;
        }else if (some >1) 
        {
            some = 1;
        }
        playerHpbar.fillAmount = some;
    }
    public void ToggleGroupChange(ToggleGroup tg)
    {
        Debug.Log("Toggle¸s²Õ:"+tg.name);
        foreach (Toggle t in tg.ActiveToggles()) 
        {
            Debug.Log(t.name + ":" + t.isOn);
        }
    }
    public void RefreshSettingUI()
    {
       XXmain m = XXmain.instance;
        //musicSlider.onValueChanged.RemoveListener(delegate { onSlideMusicVChanged(); });
        musicSlider.value = m.GetMusicVolume();
        //musicSlider.onValueChanged.AddListener(delegate { onSlideMusicVChanged(); });

        //audioSlider.onValueChanged.RemoveListener(delegate { onSlideAudioVChanged(); });
        audioSlider.value = m.GetAudioVolume();
        //audioSlider.onValueChanged.AddListener(delegate { onSlideAudioVChanged(); });

    }

    public void onSlideMusicVChanged()
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAA");
        XXmain.instance.SetMusicVolume(musicSlider.value);
    }

    public void onSlideAudioVChanged()

    {
        Debug.Log("kkkk");
        XXmain.instance.SetAudioVolume(audioSlider.value);
    }
}
