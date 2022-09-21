using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float yRangeMin;
    [SerializeField] private float yRangeMax;
    [SerializeField] private int cameraDistance;
    [SerializeField] private float percentPlatformStandart;
    [SerializeField] private float percentItemsOnPlatform;
    [SerializeField] private float precentMonster;
    
    private Transform cameraTransform;
    private float lastSpawnY;
    private float rangeIncreaser;
    
    public static PlatformSpawner PlatformSpawnerScript;

    public bool isSpawnFinishBlock = false; 
    
    [SerializeField] private GameObject[] platformPrefab;
    [SerializeField] private GameObject[] itemPrefab;
    [SerializeField] private GameObject[] monsterPrefab;
    [SerializeField] private GameObject finishPrefab;
    
    // Ссылка на слайдер выполнения заданий для понимания, когда спавнить финишный блок
    [SerializeField] private Slider _taskSlider;
    
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        if (PlatformSpawnerScript == null) PlatformSpawnerScript = this;
        lastSpawnY = -3.5f;
        SpawnStartBlock();
    }

    public void SpawnStartBlock()
    {
        GameObject platform;
        for (uint i = 0; i < 15; i++)
        {
            platform = Instantiate(platformPrefab[0]);
            platform.transform.position = new Vector3(Random.Range(minX, maxX), lastSpawnY + Random.Range(yRangeMin, yRangeMax), 0);
            lastSpawnY = platform.transform.position.y;
        }
    }

    public void SpawnFinishBlock()
    {
        GameObject platform;
        for (uint i = 0; i < 10; i++)
        {
            platform = Instantiate(platformPrefab[0]);
            platform.transform.position = new Vector3(Random.Range(minX, maxX), lastSpawnY + Random.Range(yRangeMin, yRangeMax), 0);
            lastSpawnY = platform.transform.position.y;
        }

        platform = Instantiate(finishPrefab);
        platform.transform.position = new Vector3(0, lastSpawnY + 2.5f, 0);
        lastSpawnY = platform.transform.position.y;
    }

    private void Update()
    {
        if (cameraTransform.position.y + cameraDistance > lastSpawnY)
        {
            // Создаём платформы, итемы и монстров только в том случае, если уровень ещё не пройден до конца,
            // в противном случае создаём финишный блок
            if (_taskSlider.value < _taskSlider.maxValue)
            {
                GameObject platform;
                int index = 0;
            
                // Создание обичной платформы
                if (Random.value < percentPlatformStandart) platform = Instantiate(platformPrefab[0]);
                else
                {
                    index = Random.Range(1, platformPrefab.Length);
                    platform = Instantiate(platformPrefab[index]);
                }
                platform.transform.position = new Vector3(Random.Range(minX, maxX), lastSpawnY + Random.Range(yRangeMin, yRangeMax), 0);
                lastSpawnY = platform.transform.position.y;
            
                // Создание обычной платформы рядом с гнилой и взрывающейся (на всякий случай)
                if (index == 1 || index == 6)
                {
                    Vector3 newPos = new Vector3(Random.Range(minX, maxX), platform.transform.position.y + 0.5f, 0);
                    lastSpawnY = newPos.y;
                    Instantiate(platformPrefab[0], newPos, Quaternion.identity);
                }
            
                // Создание итема на обычной платформе
                if (index == 0)
                {
                    if (Random.value < percentItemsOnPlatform)
                    {
                        float itemDeltaY = 0.5f;
                        int itemIndex = Random.Range(0, 7);
                        
                        // Не генерируем пропеллер и ракетный ранец если пройдено 50% уровня
                        float precentLevelComplate = _taskSlider.maxValue * 50f / 100f;
                        if (itemIndex == 2 || itemIndex == 3)
                        {
                            if (_taskSlider.value > precentLevelComplate) itemIndex = 4;
                        }
                        
                        switch (itemIndex)
                        {
                            case 0: itemDeltaY = 0.4f; break;
                            case 1: itemDeltaY = 0.3f; break;
                            case 2: itemDeltaY = 0.3f; break;
                            case 3: itemDeltaY = 0.5f; break;
                            case 4: itemDeltaY = 0.4f; break;
                            case 5: itemDeltaY = 0.4f; break;
                            case 6: itemDeltaY = 0.4f; break;
                        }
                        
                        GameObject item = Instantiate(itemPrefab[itemIndex]);
                        item.transform.position = new Vector3(platform.transform.position.x,  platform.transform.position.y + itemDeltaY, 0f);
                    }
                }
            
                // Создание монстров
                if (Random.value < precentMonster)
                {
                    int monsterIndex = Random.Range(0, 2);
                    GameObject monster = Instantiate(monsterPrefab[monsterIndex]);
                    Vector3 newPos = new Vector3(Random.Range(minX, maxX), platform.transform.position.y + 1f, 0);
                    monster.transform.position = newPos;
                    lastSpawnY = newPos.y;
                }
            }
            else
            {
                // Чтобы конечный блок не спавнился дважды - проверяем достиг ли его игрок
                if(!isSpawnFinishBlock) SpawnFinishBlock();
                isSpawnFinishBlock = true;
            }
        }
    }
}
