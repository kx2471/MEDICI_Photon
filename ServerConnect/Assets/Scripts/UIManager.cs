using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    void Awake()
    {
        instance = this;    
    }
    public Text armotext;
    public void UpdateArmoText(int mag_count, int remain_armo)
    {
        armotext.text = "" + mag_count + "/" + remain_armo;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
