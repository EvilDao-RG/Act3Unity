using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CarDataManager : MonoBehaviour
{
    [SerializeField] private Car[] cars;
    private GameObject[] carsGO;
    [SerializeField] private CarSO[] carsSO;
    private IEnumerator enumerator;

    public static CarDataManager Instance{
        get;
        private set;
    }

    void Awake() {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        carsGO = new GameObject[cars.Length];
        for (int i = 0; i < cars.Length; i++){
            carsGO[i] = CarPoolManager.Instance.Activate(Vector3.zero);
        }
    }

    public void placeCars(CarList carList){
        for (int i = 0; i < cars.Length; i++){
            carsGO[i].transform.position = new Vector3(carList.cars[i].x, carList.cars[i].y, carList.cars[i].z);
            carsGO[i].GetComponent<CarBuilder>().UpdateCar(carsSO[UnityEngine.Random.Range(0, cars.Length -1)]);
        }
    }

    public void listenWithArgs(CarList cars){
        placeCars(cars);
    }
}
