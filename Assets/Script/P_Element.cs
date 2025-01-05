using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Element : MonoBehaviour
{
    public int index_p;
    public bool is_pin;

    public Text txt_key;
    public Text txt_stt;
    public Text txt_ntk;
    public Text txt_am_dien;
    public Text txt_name;
    
    public Sprite img_electron;
    public Sprite img_spectral;
    public Sprite img_Crystal_structure=null;

    public GameObject obj_pin;

    [Header("Hydrogen")]
    public Text txt_electron;
    public string s_Electron_per_shell;
    public string s_Atomic_Weight;
    public string s_Element_category;
    public string s_color;
    public string s_other_name;

    [Header("Physical properties")]
    public string s_Melting_point;
    public string s_Boiling_point;
    public string s_Density;
    public string s_Speed_of_Sound;

    [Header("Atomic properties")]
    public string s_Oxidation_states;
    public string s_Electronegativity;
    public string s_Ionization_energies;
    public string s_Atomic_radius;
    public string s_Covalent_radius;
    public string s_Van_der_Waals_radius;

    [Header("Identifiers")]
    public string s_CAS_Number;
    public string s_EC_Number;
    public string s_MDL_Number;
    public string s_Beilstein_Number;
    public string s_SMILES_Identifier;
    public string s_InChI_Identifier;
    public string s_InChI_Key;
    public string s_PubChem_CID;
    public string s_ChemSpider_ID;

    [Header("History")]
    public string s_Discovery;
    public string s_Discovery_Date;
    public string s_Named_by;
    public string s_First_Isolation;
    
    public void click()
    {
        GameObject.Find("App").GetComponent<App>().show_info(this);
    }
}
