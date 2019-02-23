using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantUpdatesDisplay : MonoBehaviour
{
    public static int plantHealth = 0;
    public static int plantFruits = 0;

    Text plant;

    // Start is called before the first frame update
    void Start()
    {
        plant = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        plant.text = "Moisture level: " + plantHealth + "\n There are " + plantFruits + " fruits on the plant.";
    }
}
