using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public UnityEngine.Color color;
    public UnityEngine.Color instanceColor;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public float MaxHp;
    public float CurrentHp;
    public Image Image;
    Rigidbody2D rb;
    float x;
    float y;
    float ThreshHold;
    void Start()
    {
        CurrentHp = MaxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        instanceColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    void Update()
    {
        
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }
    }
   
    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (x * speed, y * speed);
       

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Animal")
        {
            if(Time.time > ThreshHold)
            {
                Animal a = collision.gameObject.GetComponent<Animal>();
                a.Damage();
                ThreshHold = Time.time + 1;
            }
            

        }
        
    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log("Sa");
        CurrentHp--;
        StartCoroutine(ColorChange());
        float barFill = CurrentHp / MaxHp;
        barFill = Mathf.Clamp(barFill, 0f, Image.rectTransform.localScale.z);
        Image.rectTransform.localScale = new Vector3(barFill, Image.rectTransform.localScale.y, Image.rectTransform.localScale.z);
    }
    IEnumerator ColorChange()
    {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = instanceColor;



    }
}
