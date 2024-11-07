using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scalechange : MonoBehaviour
{
    public GameObject plant;
    // Start is called before the first frame update

    private Vector3 scale;
    void Start()
    {
        scale = new Vector3(0.1f,0.1f,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    private void ScaleUp()
    {
        plant.transform.localScale += scale;
    }
    
}
