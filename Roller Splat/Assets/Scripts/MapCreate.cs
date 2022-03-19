using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCreate : MonoBehaviour
{
    [SerializeField]
    public Texture2D[] images;
    private Texture2D image;
    [SerializeField]
    private GameObject MapWalls;
    [SerializeField]
    private GameObject MapGrounds;
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private GameObject ground;
    public int levelCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        MapImageReadAndCreate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MapImageReadAndCreate()
    {
        int rastgeleSayi = 0;
        //rastgeleSayi = Random.Range(0, images.Length);
        image = images[levelCount];
        
        //images[rastgeleSayi] = null;
        Color[] pix = image.GetPixels();

        Vector3[] spawnPositions = new Vector3[pix.Length];
        Vector3 startingSpawnPosition = new Vector3(-Mathf.Round(image.width/2),0, -Mathf.Round(image.height / 2));
        Vector3 currentSpawnPos = startingSpawnPosition;

        int counter = 0;

        for (int i = 0; i < image.height; i++)
        {
            for (int j = 0; j < image.width; j++)
            {
                spawnPositions[counter] = currentSpawnPos;
                counter++;
                currentSpawnPos.x++;
            }
            currentSpawnPos.x = startingSpawnPosition.x;
            currentSpawnPos.z++;
        }

        counter = 0;
        int wallCounter = 0;
        int groundCounter = 0;
        foreach (Vector3 pos in spawnPositions)
        {
            Color c = pix[counter];

            if (c.Equals(Color.white))
            {
                GameObject gameObject;
                gameObject = Instantiate(ground, pos, Quaternion.identity);
                gameObject.name = "Ground_" + groundCounter;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
                gameObject.transform.position = new Vector3(pos.x,-0.5f,pos.z);
                gameObject.transform.SetParent(MapGrounds.transform);
                groundCounter++;
            }
            else if (c.Equals(Color.black))
            {
                GameObject gameObject;
                gameObject = Instantiate(wall, pos, Quaternion.identity);
                gameObject.name = "Wall_" + wallCounter;
                gameObject.transform.SetParent(MapWalls.transform);
                wallCounter++;
            }
            counter++;
        }
        
    }


}
