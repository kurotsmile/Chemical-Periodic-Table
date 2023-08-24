using Carrot;
using UnityEngine;
using UnityEngine.UI;

public class App : MonoBehaviour
{
    public Carrot.Carrot carrot;
    public P_view_info panel_info;
    public ScrollRect ScrollRect_main;
    public GridLayoutGroup gridLayout_main;
    public GameObject panel_search;
    public Button obj_button_zoom_out;
    public Button obj_button_zoom_in;
    public P_Element[] p;
    public InputField inp_search;
    public GameObject[] p_none;
    public GameObject obj_menu_main;
    public GameObject obj_menu_pin;
    public GameObject obj_btn_main_pin;
    public GameObject obj_btn_main_remove_ads;

    private float size_cell_x = 48f;
    public AudioClip sound_click_clip;
    public AudioSource sound_bk_music;

    void Start()
    {
        this.carrot.Load_Carrot(this.check_exit_app);
        this.carrot.game.load_bk_music(this.sound_bk_music);
        this.carrot.change_sound_click(this.sound_click_clip);

        this.panel_info.gameObject.SetActive(false);
        this.panel_search.SetActive(false);
        this.check_resize_cell();

        for (int i = 0; i < this.p.Length; i++)
        {
            if (PlayerPrefs.GetInt("pin_" + i, 0) == 1)
                this.p[i].is_pin = true;
            else
                this.p[i].is_pin = false;
            this.p[i].index_p = i;
            this.p[i].txt_ntk.text = this.p[i].s_Atomic_Weight;
            this.p[i].txt_am_dien.text = this.p[i].s_Electronegativity;
        }

        this.check_list_pin();
    }

    private void check_exit_app()
    {
        if (this.panel_info.gameObject.activeInHierarchy)
        {
            this.close_info();
            this.carrot.set_no_check_exit_app();
        }else if (this.panel_search.activeInHierarchy)
        {
            this.close_search();
            this.carrot.set_no_check_exit_app();
        }
    }

    public void show_info(P_Element p)
    {
        this.carrot.ads.show_ads_Interstitial();
        this.panel_info.view_info(p);
        this.play_sound();
    }

    public void close_info()
    {
        this.check_list_pin();
        this.panel_info.gameObject.SetActive(false);
        this.play_sound();
    }

    private void check_list_pin()
    {
        int count_pin = 0;
        for (int i = 0; i < this.p.Length; i++)
        {
            this.p[i].obj_pin.SetActive(this.p[i].is_pin);
            if (this.p[i].is_pin) count_pin++;
        }

        if (count_pin == 0) this.obj_btn_main_pin.SetActive(false); else this.obj_btn_main_pin.SetActive(true);
    }

    public void btn_zoom_in()
    {
        this.size_cell_x += 5f;
       this.gridLayout_main.cellSize = new Vector2(this.size_cell_x, this.size_cell_x);
        this.check_resize_cell();
        this.play_sound();
    }

    public void btn_zoom_out()
    {
        this.size_cell_x -= 5f;
        this.gridLayout_main.cellSize = new Vector2(this.size_cell_x, this.size_cell_x);
        this.check_resize_cell();
        this.play_sound();
    }

    private void check_resize_cell()
    {
        if (this.size_cell_x <= 48f)
            this.obj_button_zoom_out.interactable = false;
        else
            this.obj_button_zoom_out.interactable = true;

        if (this.size_cell_x >= 120f)
            this.obj_button_zoom_in.interactable = false;
        else
            this.obj_button_zoom_in.interactable = true;

        for(int i = 0; i < this.p.Length; i++)
        {
            if (this.size_cell_x > 80f)
            {
                this.p[i].txt_ntk.gameObject.SetActive(true);
                this.p[i].txt_electron.gameObject.SetActive(true);
                this.p[i].txt_am_dien.gameObject.SetActive(true);
                if(this.size_cell_x<100) this.p[i].txt_key.fontSize = 30;
                else this.p[i].txt_key.fontSize = 40;
            }
            else
            {
                this.p[i].txt_ntk.gameObject.SetActive(false);
                this.p[i].txt_electron.gameObject.SetActive(false);
                this.p[i].txt_am_dien.gameObject.SetActive(false);
                this.p[i].txt_key.fontSize = 20;
            }
        }
    }


    public void show_search()
    {
        this.play_sound();
        this.panel_search.SetActive(true);
    }

    public void close_search()
    {
        this.act_p_none(true);
        this.panel_search.SetActive(false);
        for (int i = 0; i < this.p.Length; i++) this.p[i].gameObject.SetActive(true);
        this.play_sound();
    }

    public void btn_search_done()
    {
        this.ScrollRect_main.normalizedPosition = new Vector2(-1f,-1f);
        this.act_p_none(false);
        for (int i = 0; i < this.p.Length; i++)
        {
            if (this.p[i].txt_key.text.ToLower().Contains(this.inp_search.text.ToLower()))
                this.p[i].gameObject.SetActive(true);
            else
                this.p[i].gameObject.SetActive(false);
        }
        this.play_sound();
    }

    private void act_p_none(bool is_act)
    {
        for (int i = 0; i < this.p_none.Length; i++) this.p_none[i].SetActive(is_act);
    }

    public void play_sound()
    {
        this.carrot.play_sound_click();
    }

    public void show_list_pin()
    {
        this.ScrollRect_main.normalizedPosition = new Vector2(-1f, -1f);
        this.panel_info.gameObject.SetActive(false);
        for (int i = 0; i < this.p.Length; i++)if(this.p[i].is_pin==false)this.p[i].gameObject.SetActive(false);
        for (int i = 0; i < this.p_none.Length; i++) this.p_none[i].gameObject.SetActive(false);
        this.obj_menu_main.SetActive(false);
        this.obj_menu_pin.SetActive(true);
        this.play_sound();
    }

    public void close_list_pin()
    {
        this.ScrollRect_main.normalizedPosition = new Vector2(-1f, -1f);
        for (int i = 0; i < this.p.Length; i++) this.p[i].gameObject.SetActive(true);
        for (int i = 0; i < this.p_none.Length; i++) this.p_none[i].gameObject.SetActive(true);
        this.obj_menu_main.SetActive(true);
        this.obj_menu_pin.SetActive(false);
        this.play_sound();
    }

    public void delete_list_pin()
    {
        for (int i = 0; i < this.p.Length; i++) if (this.p[i].is_pin) { PlayerPrefs.DeleteKey("pin_" + this.p[i].index_p);this.p[i].is_pin = false; }
        this.check_list_pin();
        this.close_list_pin();
    }

    public void app_rate()
    {
        this.play_sound();
        this.carrot.show_rate();
    }

    public void btn_buy_removeAds()
    {
        this.carrot.buy_inapp_removeads();
    }

    public void show_setting()
    {
       this.carrot.Create_Setting();
    }

    public void app_share()
    {
        this.play_sound();
        this.carrot.show_share();
    }

    public void app_list_carrot()
    {
        this.play_sound();
        this.carrot.show_list_carrot_app();
    }

    public void restore_in_app()
    {
        this.play_sound();
        this.carrot.shop.restore_product();
    }
}
