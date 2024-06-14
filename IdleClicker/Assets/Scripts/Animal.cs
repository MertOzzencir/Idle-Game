using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Animal : MonoBehaviour
{
    public int CurrentHp;
    public int MaxHp;
    public int MoneyToGive;
    public float GiveDamage;
    public Color color;
    public Color instance;
    public SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public float xMovementSpeed = -5;
    float attackTimer;
    float speedMultiplier;
    public UnityEngine.UI.Image HealthBarFill;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        CurrentHp = MaxHp;
        
        instance = spriteRenderer.color;
    }


    public void Damage()
    {
        Debug.Log("Hit");
        CurrentHp--;
        StartCoroutine(ColorChange());
        MoveBack();
        float barFill = (float)CurrentHp / (float)MaxHp;
        barFill = Mathf.Clamp(barFill, 0f, HealthBarFill.rectTransform.localScale.z);
        HealthBarFill.rectTransform.localScale = new Vector3(barFill, HealthBarFill.rectTransform.localScale.y, HealthBarFill.rectTransform.localScale.z);

        if (CurrentHp <= 0)
        {
            Caught();
        }

    }
    private void Update()
    {
        Move();
    }
    void MoveBack()
    {
        if (transform.position.x < 7f)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

        }
    }

    private void Move()
    {
        speedMultiplier = 1.10f * AutoClickManager.instance.autoRoundUp.Count;
        rb.velocity = new Vector2(xMovementSpeed - speedMultiplier, 0);
    }

    private void Caught()
    {
        GameManager.Instance.AddMoney(MoneyToGive);
        AnimalManager.instance.ReplaceAnimal(gameObject);

    }
    IEnumerator ColorChange()
    {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = instance;



    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "LeftWall")
        {
            if(Time.time > attackTimer)
            {
                Player.instance.TakeDamage(GiveDamage);
                attackTimer = Time.time + 0.5f;

            }
        }
    }
}
