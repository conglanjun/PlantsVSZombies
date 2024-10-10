using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance{ get; private set;}
    public List<Plant> plantPrefabList;
    private Plant currentPlant;
    private void Awake() {
        Instance = this;
    }

    public void AddPlant(PlantType plantType)
    {
        Plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab == null)
        {
            print("植物不存在！");
            return;
        }
        currentPlant = GameObject.Instantiate(plantPrefab);
    }

    private Plant GetPlantPrefab(PlantType plantType)
    {
        foreach (Plant plant in plantPrefabList)
        {
            if (plant.plantType == plantType)
            {
                return plant;
            }
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        if (currentPlant == null) return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        currentPlant.transform.position = mouseWorldPosition;
    }
}
