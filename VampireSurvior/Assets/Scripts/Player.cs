using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime; //가로입력
        float v = Input.GetAxis("Vertical") * speed * Time.deltaTime; //세로입력

        transform.position += new Vector3(-v, 0, h);
    }
}
