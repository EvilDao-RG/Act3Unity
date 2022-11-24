using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;

[Serializable]
public class RequestWithArgs : UnityEvent<CarList>{}

public class NetworkManager : MonoBehaviour
{

    public static NetworkManager Instance{
        get;
        private set;
    }

    public string backendURL = "http://127.0.0.1:5000/";
    public RequestWithArgs requestWithArgs;
    private IEnumerator enumerator;


    private void Awake() {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void Start()
    {
        enumerator = UpdatePositions(CarPoolManager.Instance._poolSize);
        Coroutine coroutine = StartCoroutine(enumerator);
    }

    IEnumerator UpdatePositions(int poolSize){
        while(true){
            CarList listaCarros = new CarList();
            listaCarros.cars = new Car[poolSize];
            yield return new WaitForSeconds(1);

            for(int i = 0; i < poolSize; i++){
                listaCarros.cars[i] = new Car();
                listaCarros.cars[i].id = i;
                listaCarros.cars[i].x = UnityEngine.Random.Range(0f, 10f);
                listaCarros.cars[i].y = UnityEngine.Random.Range(0f, 10f);
                listaCarros.cars[i].z = UnityEngine.Random.Range(0f, 10f);
            }

            requestWithArgs?.Invoke(listaCarros);

        }
    }
}
