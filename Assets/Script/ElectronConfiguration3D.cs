using UnityEngine;

public class ElectronConfiguration3D : MonoBehaviour
{
    [Header("Object Main")]
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

    private void Start()
    {
        DrawElectronConfiguration("[Ar] 3d10 4s2 4p2");
    }

    public void DrawElectronConfiguration(string configuration)
    {
        GameObject nucleus = new GameObject("Nucleus");
        nucleus.transform.parent = this.transform;

        AddProtonsAndNeutrons(nucleus.transform);

        string[] shells = ParseConfiguration(configuration);

        float currentRadius = radiusIncrement;
        foreach (string shell in shells)
        {
            int electrons = GetElectronCount(shell);
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

            currentRadius += radiusIncrement; // Tăng bán kính cho lớp tiếp theo
        }
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

    private string[] ParseConfiguration(string configuration)
    {
        configuration = configuration.Replace("[Ar]", "").Trim();
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
        // Thêm LineRenderer để vẽ quỹ đạo
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = orbitColor;
        lineRenderer.endColor = orbitColor;

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