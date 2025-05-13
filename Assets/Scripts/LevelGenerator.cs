using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float wallWidth = 20f;
    public float wallHeight = 100f;
    public float wallThickness = 0.5f;


    public Material platformMaterial;
    public float platformDepth = 1f;
    public float platformWidth = 2f;
    public float platformThickness = 0.3f;

    public float heightDifference = 10f;

    public Material floorMaterial;

    public float initialSpeed = 0.1f;
    public float acceleration = 0.5f;

    private float currentSpeed;

    private float lastPlatformHeight;


    void Start()
    {
        currentSpeed = initialSpeed;
        lastPlatformHeight = heightDifference;
        GenerateWalls();
        GeneratePlatforms();
        AddFloor(this.gameObject, new Vector3(wallWidth, 0.1f, wallWidth));
    }

    void Update()
    {
        transform.position += Vector3.up * currentSpeed * Time.deltaTime;
        currentSpeed += acceleration * Time.deltaTime;
        Debug.Log(lastPlatformHeight);
        if (Mathf.FloorToInt(wallHeight) + transform.position.y - lastPlatformHeight >= heightDifference)
        {
            GeneratePlatforms();
        }
    }

    void GenerateWalls()
    {
        CreateWall(new Vector3(0, wallHeight / 2f, wallWidth / 2f), new Vector3(wallWidth, wallHeight, wallThickness));
        CreateWall(new Vector3(0, wallHeight / 2f, -wallWidth / 2f), new Vector3(wallWidth, wallHeight, wallThickness));
        CreateWall(new Vector3(-wallWidth / 2f, wallHeight / 2f, 0), new Vector3(wallThickness, wallHeight, wallWidth));
        CreateWall(new Vector3(wallWidth / 2f, wallHeight / 2f, 0), new Vector3(wallThickness, wallHeight, wallWidth));
    }

    void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = position;
        wall.transform.localScale = scale;
        wall.transform.parent = this.transform; // optional: keep hierarchy clean
        wall.name = "Wall";
    }

    void GeneratePlatforms()
    {
        for (float platformHeight = lastPlatformHeight; platformHeight < Mathf.FloorToInt(wallHeight) + transform.position.y; platformHeight += heightDifference)
        {
            lastPlatformHeight = platformHeight;
            float y = platformHeight;


            int wallIndex = Random.Range(0, 4); // 0 = Front, 1 = Back, 2 = Left, 3 = Right
            float halfWidth = wallWidth / 2f;
            float halfThickness = wallThickness / 2f;

            switch (wallIndex)
            {
                case 0: // Front (Z+), random X
                    CreatePlatform(
                        new Vector3(Random.Range(-halfWidth + platformWidth / 2f, halfWidth - platformWidth / 2f), y, halfWidth - halfThickness - platformDepth / 2f),
                        new Vector3(platformWidth, platformThickness, platformDepth)
                    );
                    break;

                case 1: // Back (Z-), random X
                    CreatePlatform(
                        new Vector3(Random.Range(-halfWidth + platformWidth / 2f, halfWidth - platformWidth / 2f), y, -halfWidth + halfThickness + platformDepth / 2f),
                        new Vector3(platformWidth, platformThickness, platformDepth)
                    );
                    break;

                case 2: // Left (X-), random Z
                    CreatePlatform(
                        new Vector3(-halfWidth + halfThickness + platformDepth / 2f, y, Random.Range(-halfWidth + platformWidth / 2f, halfWidth - platformWidth / 2f)),
                        new Vector3(platformDepth, platformThickness, platformWidth)
                    );
                    break;

                case 3: // Right (X+), random Z
                    CreatePlatform(
                        new Vector3(halfWidth - halfThickness - platformDepth / 2f, y, Random.Range(-halfWidth + platformWidth / 2f, halfWidth - platformWidth / 2f)),
                        new Vector3(platformDepth, platformThickness, platformWidth)
                    );
                    break;
            }

        }
        lastPlatformHeight += heightDifference;
    }

    void CreatePlatform(Vector3 position, Vector3 scale)
    {
        GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        platform.transform.position = position;
        platform.transform.localScale = scale;
        platform.name = "Platform";

        Rigidbody rb = platform.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        if (platformMaterial != null)
        {
            Renderer renderer = platform.GetComponent<Renderer>();
            renderer.material = platformMaterial;
        }
    }

    void AddFloor(GameObject parent, Vector3 size)
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
        floor.transform.parent = parent.transform;
        floor.transform.localScale = size;
        floor.transform.localPosition = new Vector3(0, -size.y / 2f, 0);

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
}
