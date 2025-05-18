using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //walls
    public Material wallMaterial;
    public float wallWidth = 20f;
    public float wallHeight = 20f;
    public float wallThickness = 0.5f;
    public int wallsLevel = 5;

    //platforms
    public Material platformMaterial;
    public float platformDepth = 1f;
    public float platformWidth = 2f;
    public float platformThickness = 0.3f;


    public Material floorMaterial;

    public float initialSpeed = 0.1f;
    public float acceleration = 0.5f;

    private float currentSpeed;

    private float lastPlatformHeight;



    public float spawnInterval = 2f;
    private float timer = 0f;

    private bool startSpawning = false;
    void Start()
    {
        BoxCollider bc = gameObject.AddComponent<BoxCollider>();
        bc.size = new Vector3(wallWidth, 1, wallWidth);
        bc.isTrigger = true;
        for (int i = 0; i < wallsLevel; i++)
        {
            GenerateWalls(i * wallHeight);
        }
    }

    void Update()
    {
        if (startSpawning)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                timer = 0f;
                GenerateWalls(wallsLevel * wallHeight);
                wallsLevel++;
            }
        }
    }

    private void GenerateWalls(float yOffset)
    {
        CreateWall(new Vector3(transform.position.x, transform.position.y + yOffset + wallHeight / 2f, wallWidth / 2f + transform.position.z), new Vector3(wallWidth, wallHeight, wallThickness));
        CreateWall(new Vector3(transform.position.x, transform.position.y + yOffset + wallHeight / 2f, -wallWidth / 2f + transform.position.z), new Vector3(wallWidth, wallHeight, wallThickness));
        CreateWall(new Vector3(-wallWidth / 2f + transform.position.x, transform.position.y + yOffset + wallHeight / 2f, transform.position.z), new Vector3(wallThickness, wallHeight, wallWidth));
        CreateWall(new Vector3(wallWidth / 2f + transform.position.x, transform.position.y + yOffset + wallHeight / 2f, transform.position.z), new Vector3(wallThickness, wallHeight, wallWidth));
        GeneratePlatformOnWall(yOffset);
    }

    private void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = position;
        wall.transform.localScale = scale;
        wall.transform.parent = this.transform; // optional: keep hierarchy clean
        wall.name = "Wall";
        Rigidbody rb = wall.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        if (wallMaterial != null)
        {
            Renderer renderer = wall.GetComponent<Renderer>();
            renderer.material = wallMaterial;
        }
    }

    private void GeneratePlatformOnWall(float y)
    {
        int wallIndex = Random.Range(0, 4); // 0 = Front, 1 = Back, 2 = Left, 3 = Right
        float halfWidth = wallWidth / 2f;
        float halfThickness = wallThickness / 2f;

        switch (wallIndex)
        {
            case 0: // Front (Z+), random X
                CreatePlatform(
                    new Vector3(Random.Range(transform.position.x - halfWidth + platformWidth / 2f, transform.position.x + halfWidth - platformWidth / 2f), y, transform.position.z + halfWidth - halfThickness - platformDepth / 2f),
                    new Vector3(platformWidth, platformThickness, platformDepth)
                );
                break;

            case 1: // Back (Z-), random X
                CreatePlatform(
                    new Vector3(Random.Range(transform.position.x - halfWidth + platformWidth / 2f, transform.position.x + halfWidth - platformWidth / 2f), y, transform.position.z - halfWidth + halfThickness + platformDepth / 2f),
                    new Vector3(platformWidth, platformThickness, platformDepth)
                );
                break;

            case 2: // Left (X-), random Z
                CreatePlatform(
                    new Vector3(transform.position.x - halfWidth + halfThickness + platformDepth / 2f, y, Random.Range(transform.position.z - halfWidth + platformWidth / 2f, transform.position.z + halfWidth - platformWidth / 2f)),
                    new Vector3(platformDepth, platformThickness, platformWidth)
                );
                break;

            case 3: // Right (X+), random Z
                CreatePlatform(
                    new Vector3(transform.position.x + halfWidth - halfThickness - platformDepth / 2f, y, Random.Range(transform.position.z - halfWidth + platformWidth / 2f, transform.position.z + halfWidth - platformWidth / 2f)),
                    new Vector3(platformDepth, platformThickness, platformWidth)
                );
                break;
        }
    }

    private void CreatePlatform(Vector3 position, Vector3 scale)
    {
        GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        platform.transform.position = position;
        platform.transform.localScale = scale;
        platform.name = "Platform";

        Rigidbody rb = platform.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        // BoxCollider collider = platform.AddComponent<BoxCollider>();
        // collider.size = scale;
        // collider.isTrigger = true;

        platform.AddComponent<Platform>();


        if (platformMaterial != null)
        {
            Renderer renderer = platform.GetComponent<Renderer>();
            renderer.material = platformMaterial;
        }
    }

    private void AddFloor(GameObject parent, Vector3 size)
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
        floor.tag = "lava";
        floor.transform.parent = parent.transform;
        floor.transform.localScale = size;
        floor.transform.localPosition = new Vector3(0, -wallHeight, 0);

        floor.AddComponent<Floor>();

        BoxCollider collider = floor.AddComponent<BoxCollider>();
        collider.size = size;
        collider.isTrigger = true;

        if (floorMaterial != null)
        {
            Renderer renderer = floor.GetComponent<Renderer>();
            renderer.material = floorMaterial;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startSpawning = true;
            AddFloor(this.gameObject, new Vector3(wallWidth, 0.1f, wallWidth));
            BoxCollider bc = GetComponent<BoxCollider>();
            bc.enabled = false;
            Debug.Log("aaaaaaaaaaaaa");
        }

    }
}
