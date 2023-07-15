using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    // Start is called before the first frame update
    private float hp;
    private Material smaterial;
    public Object destroyEffect;
    void Start()
    {
        hp = 100f;
        smaterial = GetComponent<Renderer>().material;
    }
    public void Damage(float minus)
    {

        hp -= minus;
        Debug.Log(hp);
        if (hp <= 0)
        {
            hp = 0;
            GameObject gEffect = Instantiate(destroyEffect) as GameObject;
           gEffect.transform.position = transform.position;
            main.Instance().RemoveEnemy(this.gameObject);
            //Destroy(gameObject);
        }

        if (hp > 50)
        {
            smaterial.color = new Color(0f, 1.0f, 0f);
        }
        else
        {
            smaterial.color = new Color(1.0f, 0.0f, 0.0f);
        }

    }
    // Update is called once per frame

}
