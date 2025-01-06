using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_view_info : MonoBehaviour
{
    public Image img_Block_Color;
    public Text txt_p_key;
    public Text txt_p_name;
    public Text txt_p_stt;
    public Image img_Electron_view;
    public GameObject p_Spectral_lines;
    public Image img_Spectral_lines;
    public GameObject p_Crystal_structure;
    public Image img_Crystal_structure;
    public ScrollRect scrollrect_view;
    public App app;
    public Transform arean_info_left_Landscape;
    public Transform arean_info_right_Landscape;
    public Transform arean_info_portrait;
    public Transform arean_info_Landscapet;
    public GameObject obj_show_pin;
    public Image img_pin;
    public Sprite sp_pin;
    public Sprite sp_unpin;

    private List<GameObject> list_obj_info_left=new List<GameObject>();
    private List<GameObject> list_obj_info_right=new List<GameObject>();

    private P_Element p_cur;

    [Header("Hydrogen")]
    public Text txt_i_Electron;
    public Text txt_i_Electron_per_shell;
    public GameObject p_Atomic_Weight;
    public Text txt_Atomic_Weight;
    public GameObject p_Element_category;
    public Text txt_Element_category;
    public GameObject p_other_name;
    public Text txt_other_name;
    public GameObject p_color;
    public Text txt_color;

    [Header("Physical properties")]
    public GameObject p_Melting_point;
    public Text txt_i_Melting_point;
    public GameObject p_Boiling_point;
    public Text txt_i_Boiling_point;
    public GameObject p_Density;
    public Text txt_i_Density;
    public GameObject p_Speed_of_Sound;
    public Text txt_i_Speed_of_Sound;

    [Header("Atomic properties")]
    public Text txt_i_Oxidation_states;
    public GameObject p_Electronegativity;
    public Text txt_i_Electronegativity;
    public Text txt_i_Ionization_energies;
    public GameObject panel_Atomic_radius;
    public Text txt_i_Atomic_radius;
    public GameObject p_Covalent_radius;
    public Text txt_i_Covalent_radius;
    public GameObject p_Van_der_Waals_radius;
    public Text txt_i_Van_der_Waals_radius;

    [Header("Identifiers")]
    public Text txt_CAS_Number;
    public GameObject p_EC_Number;
    public Text txt_EC_Number;
    public GameObject p_MDL_Number;
    public Text txt_MDL_Number;
    public GameObject p_Beilstein_Number;
    public Text txt_Beilstein_Number;
    public GameObject p_SMILES_Identifier;
    public Text txt_SMILES_Identifier;
    public GameObject p_InChI_Identifier;
    public Text txt_InChI_Identifier;
    public GameObject p_InChI_Key;
    public Text txt_InChI_Key;
    public GameObject p_PubChem_CID;
    public Text txt_PubChem_CID;
    public GameObject p_ChemSpider_ID;
    public Text txt_ChemSpider_ID;

    [Header("History")]
    public GameObject p_Discovery;
    public Text txt_i_Discovery;
    public GameObject p_Discovery_Date;
    public Text txt_i_Discovery_Date;
    public GameObject p_Named_by;
    public Text txt_i_Named_by;
    public GameObject p_First_Isolation;
    public Text txt_i_First_Isolation;

    private void Start()
    {
        foreach (Transform chid in this.arean_info_left_Landscape) this.list_obj_info_left.Add(chid.gameObject);
        foreach (Transform chid in this.arean_info_right_Landscape) this.list_obj_info_right.Add(chid.gameObject);
        this.show_body_info_by_roate();
    }

    public void view_info(P_Element p)
    {
        this.p_cur = p;
        this.img_Block_Color.color = p.GetComponent<Image>().color;
        this.txt_p_stt.text = p.txt_stt.text;
        this.txt_p_key.text = p.txt_key.text;
        this.txt_p_name.text = p.txt_name.text;

        this.img_Electron_view.sprite = p.img_electron;
        this.img_Spectral_lines.sprite = p.img_spectral; if (p.img_spectral != null) this.p_Spectral_lines.SetActive(true); else this.p_Spectral_lines.SetActive(false);
        this.img_Crystal_structure.sprite = p.img_Crystal_structure;if (p.img_Crystal_structure!=null) this.p_Crystal_structure.SetActive(true); else this.p_Crystal_structure.SetActive(false);

        this.txt_i_Electron.text = p.txt_electron.text;
        this.txt_i_Electron_per_shell.text = p.s_Electron_per_shell;
        this.txt_Atomic_Weight.text = p.s_Atomic_Weight; if (p.s_Atomic_Weight.Trim() != "") this.p_Atomic_Weight.SetActive(true); else this.p_Atomic_Weight.SetActive(false);
        this.txt_other_name.text = p.s_other_name; if (p.s_other_name.Trim() != "") this.p_other_name.SetActive(true); else this.p_other_name.SetActive(false);
        this.txt_Element_category.text = p.s_Element_category; if(p.s_Element_category.Trim() != "") this.p_Element_category.SetActive(true); else this.p_Element_category.SetActive(false);
        this.txt_color.text = p.s_color; if(p.s_color.Trim() != "") this.p_color.SetActive(true); else this.p_color.SetActive(false);

        this.txt_i_Melting_point.text = p.s_Melting_point; if(p.s_Melting_point.Trim() != "") this.p_Melting_point.SetActive(true); else this.p_Melting_point.SetActive(false);
        this.txt_i_Boiling_point.text = p.s_Boiling_point; if (p.s_Boiling_point.Trim() != "") this.p_Boiling_point.SetActive(true); else this.p_Boiling_point.SetActive(false);
        this.txt_i_Density.text = p.s_Density; if (p.s_Density.Trim() != "") this.p_Density.SetActive(true); else this.p_Density.SetActive(false);
        this.txt_i_Speed_of_Sound.text = p.s_Speed_of_Sound; if (p.s_Speed_of_Sound.Trim() != "") this.p_Speed_of_Sound.SetActive(true); else this.p_Speed_of_Sound.SetActive(false);

        this.txt_i_Oxidation_states.text = p.s_Oxidation_states;
        this.txt_i_Electronegativity.text = p.s_Electronegativity; if(p.s_Electronegativity.Trim() != "") this.p_Electronegativity.SetActive(true); else this.p_Electronegativity.SetActive(false);
        this.txt_i_Ionization_energies.text = p.s_Ionization_energies;
        this.txt_i_Atomic_radius.text = p.s_Atomic_radius; if (p.s_Atomic_radius.Trim() != "") this.panel_Atomic_radius.SetActive(true); else this.panel_Atomic_radius.SetActive(false);
        this.txt_i_Covalent_radius.text = p.s_Covalent_radius; if (p.s_Covalent_radius.Trim() != "") this.p_Covalent_radius.SetActive(true); else this.p_Covalent_radius.SetActive(false);
        this.txt_i_Van_der_Waals_radius.text = p.s_Van_der_Waals_radius; if(p.s_Van_der_Waals_radius.Trim() != "") this.p_Van_der_Waals_radius.SetActive(true); else this.p_Van_der_Waals_radius.SetActive(false);

        this.txt_CAS_Number.text = p.s_CAS_Number;
        this.txt_EC_Number.text = p.s_EC_Number; if (p.s_EC_Number.Trim() != "") this.p_EC_Number.SetActive(true); else this.p_EC_Number.SetActive(false);
        this.txt_MDL_Number.text = p.s_MDL_Number; if (p.s_MDL_Number.Trim() != "") this.p_MDL_Number.SetActive(true); else this.p_MDL_Number.SetActive(false);
        this.txt_Beilstein_Number.text = p.s_Beilstein_Number; if (p.s_Beilstein_Number.Trim() != "") this.p_Beilstein_Number.SetActive(true); else this.p_Beilstein_Number.SetActive(false);
        this.txt_SMILES_Identifier.text = p.s_SMILES_Identifier; if (p.s_SMILES_Identifier.Trim() != "") this.p_SMILES_Identifier.SetActive(true); else this.p_SMILES_Identifier.SetActive(false);
        this.txt_InChI_Identifier.text = p.s_InChI_Identifier; if (p.s_InChI_Identifier.Trim() != "") this.p_InChI_Identifier.SetActive(true); else this.p_InChI_Identifier.SetActive(false);
        this.txt_InChI_Key.text = p.s_InChI_Key; if(p.s_InChI_Key.Trim() != "") this.p_InChI_Key.SetActive(true); else this.p_InChI_Key.SetActive(false);
        this.txt_PubChem_CID.text = p.s_PubChem_CID; if (p.s_PubChem_CID.Trim() != "") this.p_PubChem_CID.SetActive(true); else this.p_PubChem_CID.SetActive(false);
        this.txt_ChemSpider_ID.text = p.s_ChemSpider_ID; if(p.s_ChemSpider_ID.Trim() != "") this.p_ChemSpider_ID.SetActive(true); else this.p_ChemSpider_ID.SetActive(false);

        this.txt_i_Discovery.text = p.s_Discovery;
        this.txt_i_Named_by.text = p.s_Named_by; if(p.s_Named_by.Trim() != "") this.p_Named_by.SetActive(true); else this.p_Named_by.SetActive(false);
        this.txt_i_Discovery_Date.text = p.s_Discovery_Date; if (p.s_Discovery_Date.Trim() != "") this.p_Discovery_Date.SetActive(true); else this.p_Discovery_Date.SetActive(false);
        this.txt_i_First_Isolation.text = p.s_First_Isolation; if (p.s_First_Isolation.Trim() != "") this.p_First_Isolation.SetActive(true); else this.p_First_Isolation.SetActive(false);

        this.scrollrect_view.normalizedPosition = new Vector2(1f, 1f);
        this.obj_show_pin.SetActive(this.p_cur.is_pin);
        this.check_status_pin();
        this.gameObject.SetActive(true);
    }

    public void check_change_rotate_device() {
        this.app.carrot.delay_function(0.5f, show_body_info_by_roate);
    }

    private void show_body_info_by_roate()
    {
        if (this.arean_info_portrait.gameObject.activeInHierarchy)
        {
            for (int i = 0; i < this.list_obj_info_left.Count; i++) this.list_obj_info_left[i].transform.SetParent(this.arean_info_portrait);
            for (int i = 0; i < this.list_obj_info_right.Count; i++) this.list_obj_info_right[i].transform.SetParent(this.arean_info_portrait);
            this.scrollrect_view.content = this.arean_info_portrait.GetComponent<RectTransform>();
        }
        else
        {
            for (int i = 0; i < this.list_obj_info_left.Count; i++) this.list_obj_info_left[i].transform.SetParent(this.arean_info_left_Landscape);
            for (int i = 0; i < this.list_obj_info_right.Count; i++) this.list_obj_info_right[i].transform.SetParent(this.arean_info_right_Landscape);
            this.scrollrect_view.content = this.arean_info_Landscapet.GetComponent<RectTransform>();
        }
    }

    public void btn_search_wikipedia()
    {
        this.app.play_sound();
        Application.OpenURL("https://en.wikipedia.org/w/index.php?search=" + this.txt_p_name.text);
    }

    public void btn_search_youtube()
    {
        this.app.play_sound();
        Application.OpenURL("https://www.youtube.com/results?search_query=" + this.txt_p_name.text);
    }

    public void btn_search_google()
    {
        this.app.play_sound();
        Application.OpenURL("https://www.google.com/search?q="+this.txt_p_name.text);
    }

    public void btn_next()
    {
        this.app.ads.On_show_interstitial();
        int index_p_view = this.p_cur.index_p+1;
        if (index_p_view >= 118) index_p_view = 0;
        this.view_info(this.app.p[index_p_view]);
        this.app.play_sound();
    }

    public void btn_prev()
    {
        this.app.ads.On_show_interstitial();
        int index_p_view = this.p_cur.index_p-1;
        if (index_p_view < 0) index_p_view = 117;
        this.view_info(this.app.p[index_p_view]);
        this.app.play_sound();
    }

    public void btn_pin()
    {
        if (this.p_cur.is_pin)
        {
            PlayerPrefs.SetInt("pin_" + this.p_cur.index_p, 0);
            this.p_cur.is_pin = false;
        }
        else
        {
            PlayerPrefs.SetInt("pin_" + this.p_cur.index_p, 1);
            this.p_cur.is_pin = true;
            this.app.carrot.play_vibrate();
        }
        this.check_status_pin();
        this.app.play_sound();
        this.app.carrot.play_vibrate();
    }

    private void check_status_pin()
    {
        if (this.p_cur.is_pin)
        {
            this.obj_show_pin.SetActive(true);
            this.img_pin.sprite = this.sp_unpin;
        }
        else
        {
            this.obj_show_pin.SetActive(false);
            this.img_pin.sprite = this.sp_pin;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) this.btn_next();
        if (Input.GetKeyDown(KeyCode.DownArrow)) this.btn_prev();
        if (Input.GetKeyDown(KeyCode.RightArrow)) this.btn_next();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) this.btn_prev();
    }

    public void play_or_stop_anim_Electron()
    {
        if (this.img_Electron_view.GetComponent<Animator>().enabled)
        {
            this.img_Electron_view.GetComponent<Animator>().enabled = false;
            this.img_Electron_view.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else
            this.img_Electron_view.GetComponent<Animator>().enabled = true;

        this.app.play_sound();
    }
}
