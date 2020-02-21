using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    Animation anim;
    SteeringWheel wheel;
    public float playerSpeed = 2;
    public int rotationspeed = 50;
    Rigidbody2D snakeRigidbody;
    PlayerConfig playerConfig;

    private void Start()
    {
        anim = transform.GetComponentInChildren<Animation>();
        snakeRigidbody = GetComponent<Rigidbody2D>();
        wheel = FindObjectOfType<SteeringWheel>();
    }
    public void SetPlayerConfig(PlayerConfig playerConfig)
    {
        this.playerConfig = playerConfig;
        playerSpeed = playerConfig.playerSpeed;
        rotationspeed = playerConfig.playerRotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("Slither");
        if (wheel.GetWheelValue() != 0)
        {
            gameObject.transform.Rotate(Vector3.forward * rotationspeed * Time.deltaTime * -wheel.GetWheelValue());
        }
        //else {
            
        //}
        //if (wheel.GetWheelValue() > 0)
        //{
        //    anim.Play("turnRight");
        //}
        //else if(wheel.GetWheelValue()<0) { anim.Play("turnLeft"); }
    }
    private void FixedUpdate()
    {
        snakeRigidbody.velocity = (transform.up * (playerSpeed));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "tile") 
        {
            GameManager.Instance.RemoveTile(other.gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        float normalAngel;
       // Debug.Log(transform.rotation.eulerAngles);
        switch (collision.gameObject.name)
        {
            case "top":
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, (180- transform.rotation.eulerAngles.z)));
                break;
            case "right":
                normalAngel = transform.rotation.eulerAngles.z - 180;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z - (2 * normalAngel)));
                break;
            case "bottom":
                normalAngel =180 - transform.rotation.eulerAngles.z;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, normalAngel));
                break;
            case "left":
                normalAngel = 180 - transform.rotation.eulerAngles.z;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180+normalAngel));
                break;
        }
        if (collision.gameObject.tag == "food")
        {
            UIManager.Instance.UpdateScore();
            GameManager.Instance.ResetFoodClock();
        }

    }
}