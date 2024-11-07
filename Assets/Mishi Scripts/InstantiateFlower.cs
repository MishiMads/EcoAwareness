using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InstantiateFlower : MonoBehaviour
{
    public GameObject SeedPrefab1;
    
    // Start is called before the first frame update
    public void InstantiateFlowerSeeds()
    {
        Instantiate(SeedPrefab1, transform.position, quaternion.identity);
    }
}
