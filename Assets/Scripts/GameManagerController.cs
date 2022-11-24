using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public Text coinsText;
    public Text materialesText;
    
    private int score;
    private int lives;
    public int balas;
    private int materiales;
    
    public PlayerController _player;
    
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        
        lives = 3;
        score = 0;
        balas = _player.cantidadBalas;
        materiales = 0;

        PrintInScreenLives();
        PrintInScreenScore();
        PrintInScreenCoins();
        PrintInScreenMateriales();
        LoadGame();
    }


       public void SaveGame(){
        var filePath = Application.persistentDataPath + "/t4_10.dat";
        FileStream file;

        if(File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else    
            file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = score;
        data.Balas = balas;
        data.Lives = lives;
        data.Materiales = materiales;

        

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file,data);
        file.Close();

    }

    public void LoadGame(){
            var filePath = Application.persistentDataPath + "/t4_10.dat";
        FileStream file;

        if(File.Exists(filePath)){
            file = File.OpenRead(filePath);
        }
        else    {
            Debug.LogError("No se encontreo archivo");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData) bf.Deserialize(file);
        file.Close();

        //utilizar los datos guardados
      


        materiales = data.Materiales;
        score = data.Score;
        balas = data.Balas;
        lives = data.Lives;
        
        PrintInScreenCoins();
        PrintInScreenScore();
        PrintInScreenMateriales();
        PrintInScreenLives();
    }





    public int Coins()
    {
        return balas;
    }

    public int Score()
    {
        return score;
    }

    public int Lives()
    {
        return lives;
    }
    
    public int Materiales()
    {
        return materiales;
    }
    
    public void ganarMaterial(int material)
    {
        materiales += material;

        PrintInScreenMateriales();
    }
    

    public void perderBala(int bala)
    {
        this.balas-=1;

        PrintInScreenCoins();
    }

    public void ganarBala(int bala)
    {
        balas += bala;

        PrintInScreenCoins();
    }
   


    public void GanarPuntos(int puntos)
    {
        score += puntos;
        PrintInScreenScore();
    }

    public void PerderVida(int vidas)
    {
        lives -= vidas;
        PrintInScreenLives();
    }

    public void PrintInScreenCoins()
    {
        coinsText.text = "Balas : " + balas;
    }

    public void PrintInScreenScore()
    {
        scoreText.text = "Puntaje: " + score;
    }

    public void PrintInScreenLives()
    {
        livesText.text = "Vidas: " + lives;
    }

    public void PrintInScreenMateriales()
    {
        materialesText.text = "Materiales: " + materiales;
    }
}
