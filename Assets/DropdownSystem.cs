using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSystem : MonoBehaviour
{

    public Dropdown m_Dropdown;

    void Start(){
        m_Dropdown.value = PlayerPrefs.GetInt("DROPDOWNDIFF");
        m_Dropdown = GetComponent<Dropdown>();
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });

    }

    void DropdownValueChanged(Dropdown dropdown){

        switch(dropdown.value){
            case 0: 
                PlayerPrefs.SetInt("DROPDOWNDIFF", 0);
                PlayerPrefs.SetFloat("speed", 0.3f);
                break;
            case 1: 
                PlayerPrefs.SetInt("DROPDOWNDIFF", 1);
                PlayerPrefs.SetFloat("speed", 0.1f);
                break;
            case 2: 
                PlayerPrefs.SetInt("DROPDOWNDIFF", 2);
                PlayerPrefs.SetFloat("speed", 0.05f);
                break;
        }
        PlayerPrefs.Save();
    }

}
