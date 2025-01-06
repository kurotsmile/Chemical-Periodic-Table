using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElectronConfiguration3D : MonoBehaviour
{
    [Header("Object Main")]
    public App app;
    public GameObject electronPrefab;
    public GameObject protonPrefab;
    public GameObject neutronPrefab;

    [Header("Config")]
    public float nucleusRadius = 0.5f;
    public int protonCount = 20;
    public int neutronCount = 20; 
    public float radiusIncrement = 1.5f;

    [Header("Ui")]
    public GameObject panel_3d;
    public Text txt_p_name;
    public Text txt_count_e;
    public Text txt_count_p;
    public Text txt_count_n;
    public Text txt_s_electrons;
    public Image img_mini_map_electrons;
    public TextMeshProUGUI txt_p_key;
    private int index_view_cur=0;

    public void On_load()
    {
        this.panel_3d.SetActive(false);
        this.app.camera_controller.enabled=false;
    }

    public void Btn_On_show(){
        this.app.camera_controller.enabled=true;
        this.app.carrot.play_sound_click();
        this.panel_3d.SetActive(true);
        this.app.panel_main.SetActive(false);
        this.app.panel_info.gameObject.SetActive(false);
        this.Update_p_info();
    }

    public void DrawElectronConfiguration(string configuration)
    {
        this.app.carrot.clear_contain(this.transform);
        GameObject nucleus = new GameObject("Nucleus");
        nucleus.transform.parent = this.transform;

        string[] shells = ParseConfiguration(configuration);

        float currentRadius = radiusIncrement;
        int count_e=0;
        foreach (string shell in shells)
        {
            int electrons = GetElectronCount(shell);
            count_e+=electrons;
            int maxElectronsInShell = GetMaxElectrons(shell);
            float angleIncrement = 360f / maxElectronsInShell;

            GameObject shellParent = new GameObject($"Shell_{currentRadius}");
            shellParent.transform.parent = this.transform;

            ElectronOrbit orbit = shellParent.AddComponent<ElectronOrbit>();
            orbit.radius = currentRadius; 
            orbit.orbitColor = Color.blue;

            for (int i = 0; i < electrons; i++)
            {
                float angle = i * angleIncrement * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * currentRadius;
                float z = Mathf.Sin(angle) * currentRadius;

                GameObject electron = Instantiate(electronPrefab, new Vector3(x, 0, z), Quaternion.identity, shellParent.transform);
                electron.AddComponent<ElectronMovement>();
                electron.name = $"Electron_{shell}_{i + 1}";
            }
            currentRadius += radiusIncrement;
        }
        this.protonCount=count_e;
        string[] parts = this.app.p[this.index_view_cur].s_Atomic_Weight.ToString().Split('.');
        int massNumber=int.Parse(parts[0]);
        this.neutronCount=(massNumber-(this.app.p[this.index_view_cur].index_p+1));
        this.txt_count_e.text="Electrons : "+count_e;
        this.txt_count_n.text="Neutrons : "+this.neutronCount;
        this.txt_count_p.text="Protons : "+this.protonCount;
        Debug.Log(this.app.p[this.index_view_cur].txt_name.text+" -> mass:"+massNumber+" -> pr:"+this.protonCount+" -> Nr:"+this.neutronCount);
        AddProtonsAndNeutrons(nucleus.transform);
    }

    private void AddProtonsAndNeutrons(Transform nucleusTransform)
    {
        for (int i = 0; i < protonCount; i++)
        {
            Vector3 position = Random.insideUnitSphere * nucleusRadius;
            GameObject proton = Instantiate(protonPrefab, position, Quaternion.identity, nucleusTransform);
            proton.AddComponent<NucleusOscillation>();
            proton.name = $"Proton_{i + 1}";
        }

        for (int i = 0; i < neutronCount; i++)
        {
            Vector3 position = Random.insideUnitSphere * nucleusRadius;
            GameObject neutron = Instantiate(neutronPrefab, position, Quaternion.identity, nucleusTransform);
            neutron.AddComponent<NucleusOscillation>();
            neutron.name = $"Neutron_{i + 1}";
        }
    }

    public string RemoveAfterOr(string input)
    {
        int orIndex = input.IndexOf(" or ");
        if (orIndex >= 0)
        {
            return input.Substring(0, orIndex).Trim();
        }
        return input.Trim();
    }

    private string[] ParseConfiguration(string configuration)
    {
        configuration = configuration.Replace("[Ar]", "").Trim();
        configuration = configuration.Replace("[He]", "").Trim();
        configuration = configuration.Replace("[Ne]", "").Trim();
        configuration = configuration.Replace("[Rn]", "").Trim();
        configuration = configuration.Replace("[Kr]", "").Trim();
        configuration = configuration.Replace("[Xe]", "").Trim();
        configuration=RemoveAfterOr(configuration);
        return configuration.Split(' ');
    }

    private int GetElectronCount(string shell)
    {
        string count = System.Text.RegularExpressions.Regex.Match(shell, @"\d+$").Value;
        return int.Parse(count);
    }

    private int GetMaxElectrons(string shell)
    {
        if (shell.Contains("s")) return 2;
        if (shell.Contains("p")) return 6;
        if (shell.Contains("d")) return 10;
        if (shell.Contains("f")) return 14;
        return 0;
    }

    public void On_Closer(){
        this.app.carrot.clear_contain(this.transform);
        this.app.carrot.play_sound_click();
        this.panel_3d.SetActive(false);
        this.app.panel_main.SetActive(true);
        this.app.camera_controller.transform.rotation = Quaternion.identity;
    }

    public void On_Next(){
        this.app.carrot.play_sound_click();
        this.index_view_cur++;
        if(this.index_view_cur>=this.app.p.Length) this.index_view_cur=0;
        this.Update_p_info();
    }

    public void On_Prev(){
        this.app.carrot.play_sound_click();
        this.index_view_cur--;
        if(this.index_view_cur<0) this.index_view_cur=this.app.p.Length-1;
        this.Update_p_info();
    }

    public void On_zoom_in(){
        this.app.carrot.play_sound_click();
        this.app.camera_controller.distance--;
    }

    public void On_zoom_out(){
        this.app.carrot.play_sound_click();
        this.app.camera_controller.distance++;
    }

    public void On_Detail(){
        this.app.panel_info.view_info(this.app.p[this.index_view_cur]);
        this.app.carrot.clear_contain(this.transform);
        this.app.carrot.play_sound_click();
        this.panel_3d.SetActive(false);
        this.app.panel_main.SetActive(false);
        this.app.camera_controller.transform.rotation = Quaternion.identity;
    }

    public void Set_index_view(int index){
        this.index_view_cur=index;
    }

    private void Update_p_info(){
        this.txt_s_electrons.text=this.app.p[this.index_view_cur].txt_electron.text;
        this.txt_p_name.text=(this.app.p[this.index_view_cur].index_p+1)+". "+this.app.p[this.index_view_cur].txt_name.text;
        this.txt_p_key.text=this.app.p[this.index_view_cur].txt_key.text;
        this.DrawElectronConfiguration(this.app.p[index_view_cur].txt_electron.text);
        this.img_mini_map_electrons.sprite=this.app.p[this.index_view_cur].img_electron;
        Debug.Log(this.app.p[index_view_cur].txt_electron.text);
    }
}

public class ElectronMovement : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float orbitRadius;
    private Vector3 centerPoint;

    private void Start()
    {
        centerPoint = transform.parent.position;
    }

    private void Update()
    {
        transform.RotateAround(centerPoint, Vector3.up, rotationSpeed * Time.deltaTime);
        float oscillation = Mathf.Sin(Time.time * rotationSpeed * 0.1f) * 0.1f;
        transform.localPosition = new Vector3(transform.localPosition.x, oscillation, transform.localPosition.z);
    }
}

public class ShellMovement : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float orbitRadius;
    private Vector3 centerPoint;
    private Vector2 directvie_rand;
    private void Start()
    {
        centerPoint = transform.parent.position;
        if(Random.Range(0,2)>=1)
            directvie_rand=Vector3.right;
        else
            directvie_rand=Vector3.left;
    }

    private void Update()
    {
        transform.RotateAround(centerPoint, directvie_rand, rotationSpeed * Time.deltaTime);
        float oscillation = Mathf.Sin(Time.time * rotationSpeed * 0.1f) * 0.1f;
        transform.localPosition = new Vector3(transform.localPosition.x, oscillation, transform.localPosition.z);
    }
}

public class NucleusOscillation : MonoBehaviour
{
    public float oscillationAmplitude = 0.1f;
    public float oscillationSpeed = 1.0f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
        oscillationAmplitude=Random.Range(0.01f,0.2f);
        oscillationSpeed=Random.Range(0.5f,1.5f);
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * oscillationSpeed) * oscillationAmplitude;
        transform.localPosition = initialPosition + new Vector3(0, offset, 0);
    }
}

public class ElectronOrbit : MonoBehaviour
{
    public int segments = 100;
    public float radius = 1.0f;
    public Color orbitColor = Color.white;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        float glow = Mathf.PingPong(Time.time, 1);
        lineRenderer.startColor = Color.Lerp(Color.cyan, Color.magenta, glow);
        lineRenderer.endColor = Color.Lerp(Color.magenta, Color.cyan, glow);

        lineRenderer.positionCount = segments + 1;
        lineRenderer.loop = true;
        CreateOrbit();
    }

    void CreateOrbit()
    {
        Vector3[] positions = new Vector3[segments + 1];

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            positions[i] = new Vector3(x, 0, z);
        }
        lineRenderer.SetPositions(positions);
    }
}