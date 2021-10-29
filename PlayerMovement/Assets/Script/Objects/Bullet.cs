using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isFire;
    Vector3 Direction;
    [SerializeField]
    float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            transform.Translate(Direction * Time.deltaTime * speed);
        }
    }

    public void Fire(Vector3 dir)
    {
        Direction = dir;
        isFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bullet>() == null)
        {
            Destroy(gameObject);
        }
    }
}
