using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float speed = 30f;
    private float movement;
    public Text speedText;

    private int p1score;
    public Text p1scoretext;

    private int p2score;
    public Text p2scoretext;

    public Toggle MadOne;
    private bool MadMode;

    public SpriteRenderer ballColor;
    private float blclr = 1f;

    void Start()
    {
        //Задаю изначальное направление мяча
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 5);
    }

    //Функция для определения части ракетки, на которую пришелся удар
    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //После каждого удара мяч летит быстрее на 1
        speed++;

        //Меняю цвет после каждого удара
        ChangeBallColor();
        
        //Если мяч столкнулся с левым игроком, напрвляем мяч направо
        if (col.gameObject.name == "RacketLeft")
        {
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            Vector2 dir = new Vector2(1, y).normalized;

            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        //Если мяч столкнулся с левым игроком, напрвляем мяч налево
        if (col.gameObject.name == "RacketRight")
        {
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            Vector2 dir = new Vector2(-1, y).normalized;

            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        //Мяч коснулся правой стенки? Плюс очко левому
        if (col.gameObject.name == "WallRight"){
            p1score++;
            p1scoretext.text = p1score.ToString();

            //Если режим безумия выключен, мяч вылетает из середины
            if (MadMode == false) {
                GameObject.Find("Ball").transform.position = new Vector2(0, 0);
            }
        }

        //Мяч коснулся левой стенки? Плюс очко правому
        if (col.gameObject.name == "WallLeft"){
            p2score++;
            p2scoretext.text = p2score.ToString();

            //Если режим безумия выключен, мяч вылетает из середины
            if (MadMode == false) {
                GameObject.Find("Ball").transform.position = new Vector2(0, 0);
            }
        }

    }

    //Вывожу в тексте скорость мяча
   private void Update()
    {
        speedText.text = "Ball speed: " + speed.ToString();
        MadModeToggle();
    }

    //Проверяю влючен ли режим безумия
    public void MadModeToggle (){
        MadMode = MadOne.isOn ? true : false;

    }

  /* Взял откуда-то код, чтобы нормальное окно открывалось 
   * void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Screenmanager Resolution Width", 1024);
        PlayerPrefs.SetInt("Screenmanager Resolution Height", 768);
        PlayerPrefs.SetInt("Screenmanager Is Fullscreen mode", 0);
        Debug.Log("Application ending after " + Time.time + " seconds");
    } */

    // С каждым ударом цвет мяча становится насыщеннее
    void ChangeBallColor()
    {
        if (blclr > 0f) {
            blclr -= 0.01f;
            ballColor.color = new Color(blclr, blclr, 1, 1);
        }
    }

}

