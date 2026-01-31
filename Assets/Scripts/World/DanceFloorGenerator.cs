using System;
using ggj26;
using ggj26.Event;
using MyBox;
using Supyrb;
using UnityEngine;

public class DanceFloorGenerator : MonoBehaviour
{
    [SerializeField] private GameObject danceTile;
    [SerializeField] private int danceFloorWidth = 6;
    [SerializeField] private int danceFloorHeight = 4;
    [SerializeField] private float tileSeparation = 1.0f;

    private GameObject[][] danceTiles;
    private int currentColor = 0;

    private void Start()
    {
        Signals.Get<OnBeatEvent>().AddListener(ChangeFloorColors);
        
        
        CreateDanceFloor();
        ChangeFloorColors();
    }

    [ButtonMethod]
    public void CreateDanceFloor()
    {
        CleanFloor();
        danceTiles = new GameObject[danceFloorWidth][];
        for (int i = 0; i < danceFloorWidth; i++)
        {
            danceTiles[i] = new GameObject[danceFloorHeight];
            for(int j = 0; j < danceFloorHeight; j++)
            {
                danceTiles[i][j] = Instantiate(danceTile, transform);
                danceTiles[i][j].transform.position = transform.position + new Vector3(i, j, 0) * tileSeparation - new Vector3 (danceFloorWidth*tileSeparation/2, danceFloorHeight * tileSeparation / 2, 0);
            }
        }
    }

    [ButtonMethod]
    public void CleanFloor()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private void OnDestroy()
    {
        Signals.Get<OnBeatEvent>().RemoveListener(ChangeFloorColors);
    }

    [ButtonMethod]
    private void ChangeFloorColors()
    {
        Color color1 = RhythmManager.Instance.Config.Colors[currentColor];
        Color color2 = RhythmManager.Instance.Config.Colors[currentColor + 1 == RhythmManager.Instance.Config.Colors.Length ? 0 : currentColor + 1];

        for (int i = 0; i<danceFloorWidth; i++)
        {
            for (int j = 0; j<danceFloorHeight; j++)
            {
                if ((i+j)%2==0)
                {
                    danceTiles[i][j].GetComponent<SpriteRenderer>().color = color1;  
                }
                else 
                {
                    danceTiles[i][j].GetComponent<SpriteRenderer>().color = color2;
                }
            }
        }

        currentColor = currentColor + 1 == RhythmManager.Instance.Config.Colors.Length ? 0 : currentColor + 1;
    }

}
