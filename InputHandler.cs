using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public int fertilizerCost;
    public int plantCost;
    public int plantMoisture;
    public int maxFruits;
    public int maxExtraFruits;
    public int currentMoney;

    int currentPlantMoisture;
    int currentFruit;

    public bool isPlayerTurn;
    bool fertilizerUsed;
    int weatherControl;

    void Start()
    {
        currentPlantMoisture = 0;
        currentMoney = 0;
        isPlayerTurn = true;
        int w = Random.Range(1, 3);
        WeatherControl.weatherControl = w;
        if (w == 1)
        {
            currentPlantMoisture++;
            Debug.Log("It rained. The plant's current moisture level is " + currentPlantMoisture.ToString());
        }
        else if (w == 2)
        {
            currentPlantMoisture--;
            Debug.Log("It is sunny. The plant's current moisture level is " + currentPlantMoisture.ToString());
        }
        else if (w == 3)
        {
            Debug.Log("It is a normal day. The plant's current moisture level is " + currentPlantMoisture.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerTurn)
        {
            if (!fertilizerUsed)
            {
                int w = Random.Range(1, 4);
                WeatherControl.weatherControl = w;
                if (w == 1)
                {
                    currentPlantMoisture++;
                    Debug.Log("It rained.");
                }
                if (w == 2)
                {
                    currentPlantMoisture--;
                    Debug.Log("It is sunny.");
                }
                else
                {
                    Debug.Log("It is a normal day.");
                }
            }

            if (!fertilizerUsed)
            {
                if (currentPlantMoisture > 3 || currentPlantMoisture < -3)
                {
                    Debug.Log("Game over!");
                    currentMoney = 0;
                    currentPlantMoisture = 0;
                    currentFruit = 0;
                }
                if (currentFruit == 0)
                {
                    currentFruit = Random.Range(0, maxFruits);
                }
                else if (currentFruit < maxFruits)
                {
                    currentFruit += Random.Range(0, maxExtraFruits - Mathf.Abs(currentPlantMoisture));
                }
            }
            Debug.Log("You have $" + currentMoney.ToString());
            Debug.Log("There are " + currentFruit.ToString() + " fruits on the tree.");
            Debug.Log("The plant's current moisture level is " + currentPlantMoisture.ToString());
            Debug.Log("What will you do?");
            fertilizerUsed = false;
            isPlayerTurn = true;
        }

            PlantUpdatesDisplay.plantFruits = currentFruit;
            PlantUpdatesDisplay.plantHealth = currentPlantMoisture;
            moneyScript.moneyValue = currentMoney;

        //on the CPU turn, CPU will select weather - rain, sun, or ntrl. ntrl will have no effect on moisture; rain+1, sun-1.
        //if there is no fruit, CPU will randomly generate an amount of fruit from 0-maxFruits.
        //if there is fruit, CPU will randomly select whether or not to generate a fruit. maxExtraFruits should be < maxFruits. 
        //rand(0, maxExtraFruits - abs(currentPlantMoisture))
    }

    public void waterPlant()
    {
        currentPlantMoisture++;
        isPlayerTurn = false;
    }
    public void sunPlant()
    {
        currentPlantMoisture--;
        isPlayerTurn = false;
    }
    public void fertilizePlant()
    {
        if (currentMoney >= fertilizerCost)
        {
            currentMoney -= fertilizerCost;
            currentPlantMoisture = 0;
            currentFruit += maxFruits;
            isPlayerTurn = false;
            fertilizerUsed = true;
        }
    }
    public void pickFruits()
    {
        currentMoney += plantCost * currentFruit;
        currentFruit = 0;
        isPlayerTurn = false;
    }
}
