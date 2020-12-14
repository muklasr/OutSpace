using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objectToFollow;

    public float left = 0f;
    public float speed = 2.0f;
    public bool lerp = true;
    bool isUp = false;
    bool isUp2 = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        if (objectToFollow.transform.position.x > 200 || objectToFollow.transform.position.x > 3716)
        {
            position.y = this.transform.position.y;
            if (objectToFollow.transform.position.x > 483 && !isUp)
            {
                isUp = true;
                Debug.Log("Munggah");
                position.y += 52;
            } else if(objectToFollow.transform.position.x < 483 && isUp)
            {
                isUp = false;
                Debug.Log("Medun");
                position.y -= 52;
            }
            if (objectToFollow.transform.position.x > 3216 && isUp)
            {
                isUp = false;
                Debug.Log("Medun");
                position.y -= 52;
            }
            else if (objectToFollow.transform.position.x < 3216 && !isUp)
            {
                isUp = true;
                Debug.Log("Munggah");
                position.y += 52;
            }

            if (lerp)
            {
                position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x+left, interpolation);
            }
            else
            {
                position.x = objectToFollow.transform.position.x - 100;
            }
            this.transform.position = position;
        }
    }
}
