using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesInitializer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            if (Instantiater.zones == false && this.spriteRenderer.enabled == true) Instantiater.zones = true;
            if (Instantiater.zones == true && this.spriteRenderer.enabled == false) Instantiater.zones = false;


            if (Instantiater.zones )
            {
                this.spriteRenderer.enabled = false;
                Instantiater.zones = false;
                print(Instantiater.zones);
            }
            else
            {

                this.spriteRenderer.enabled = true;
                Instantiater.zones = true;
                print(Instantiater.zones);

            }
        }
        /*
        if (Input.GetKey("p"))
        {

            if (Instantiater.pause == true) Instantiater.pause = false; else Instantiater.pause = true;
            print(" Pause " + Instantiater.pause);

        }
        */
    }
}
