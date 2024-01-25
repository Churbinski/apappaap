using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class Player : MonoBehaviour
{
    //Текущее здоровье игрока
    private int health = 10;
 
    //Число собранных монет
    private int coins;
 
    //Префаб огненного шара и параметр Transform точки атаки
    public GameObject fireballPrefab;
    public Transform attackPoint;
 
 
   //Компонент, отвечающий за проигрывание звуков
    public AudioSource audioSource;
 
    //Звуковой файл, содержащий звуковой эффект нанесения урона
    public AudioClip damageSound;
 
    //Метод, обрабатывающий нанесённый урон
    public void TakeDamage(int damage)
    {
        health -= damage;
 
        //Если здоровье ещё есть, то проигрывается звук нанесения урона
        if(health > 0)
        {
            audioSource.PlayOneShot(damageSound);
            //print("Здоровье игрока: " + health);
        }
        //Если здоровья нет, то перезапускается текущая сцена
        else
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex);
        }
    }
 
    //Метод, увеличивающий число монеток
    public void CollectCoins()
    {
        coins++;
        print("Собранные монетки: " + coins);
    }
 
 
    void Update()
    {
 
        //Если игрок кликает левой кнопкой мыши, то создаётся огненный шар
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(fireballPrefab, attackPoint.position, attackPoint.rotation);
        }
 
    }
}
 













Platformer.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Platform : MonoBehaviour
{
    //Скорость движения платформы
    public float speed;
    //Направление движения платформы
    public Vector3 direction;
    //Состояние платформы: активна / не активна
    public bool isActive;
 
 
    //Обновление движения платформы каждый кадр
    void Update()
    {
 
        if (isActive)
        {
 
            transform.position += direction * speed * Time.deltaTime;
        }
    }
 
 
    //Столкновение платформы с двумя типами объектов
    void OnTriggerEnter(Collider other) {
        /*Если платформа достигает точки остановки, то она меняет направление
        своего движения*/
        if (other.tag == "PlatformStop")
        {
            direction *= -1;
        }
        //Если платформы коснулся игрок, то она активируется
        if(other.tag == "Player")
        {
            isActive = true;
        }
    }
 
    void OnTriggerExit(Collider other) {
       //Если с платформы ушёл игрок, то она дезактивируется
        if (other.tag == "Player")
        {
            isActive = false;
        }
    } 
 
}




Timer.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class Timer : MonoBehaviour
{
    public int minutes;
    public float seconds;
 
    // Цикл обновления составляет примерно 0.01 секунды
    void Update()
    {
        seconds -= Time.deltaTime;
 
        if (seconds <= 0)
        {
            if (minutes > 0)
            {
                seconds += 59;
 
                minutes--;
            }
            else
            {
                // Если таймер остановился, перезагружаем текущую сцену
                int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}
