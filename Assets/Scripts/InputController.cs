using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.InputSystem;
using XInputDotNetPure;
using TMPro;

public class InputController : MonoBehaviour
{
    private Vector2 lastMousePosition;
    public bool gamepadOn = false;

    public static InputController instance = null;

    private Gamepad gamepad;

    private string pcIconsPath = "Icons/pc"; // Nome da pasta 1
    private string gamepadIconsPath = "Icons/gamepad"; // Nome da pasta 2
    private Dictionary<string, Sprite> pcIcons = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> gamepadIcons = new Dictionary<string, Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        lastMousePosition = Mouse.current.position.ReadValue();
        LoadImages(pcIconsPath, pcIcons);
        LoadImages(gamepadIconsPath, gamepadIcons);
        gamepad = Gamepad.current;
    }

    public void Vibrate(float duration)
    {
        if (gamepad != null)
        {
            // Definir a vibra��o nos motores esquerdo e direito
            gamepad = Gamepad.current;
            gamepad.SetMotorSpeeds(50, 50);
            GamePad.SetVibration(0, 50, 50);
            Invoke("StopVibration", duration);
        }
    }
    private void StopVibration()
    {
        if (gamepad != null)
        {
            // Parar a vibra��o definindo a intensidade dos motores para zero
            gamepad.SetMotorSpeeds(0, 0);
            GamePad.SetVibration(0, 0, 0);
        }
    }

    private void detectarEntrada()
    {
        //Image btnDinamico = GameObject.Find("imgDinamica").GetComponent<Image>();

        bool mouseMove = false;
        Vector2 currentMousePosition = new Vector2(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y);
        if (currentMousePosition != lastMousePosition)
        {
            if (lastMousePosition != Vector2.zero)
            {
                mouseMove = true;
            }
            lastMousePosition = Mouse.current.position.ReadValue();
        }

        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed || mouseMove)
        {
            //btnDinamico.sprite = keyboard;
            if (gamepadOn == true)
            {
                gamepadOn = false;
                Cursor.visible = true;
            }
        }

        if (Gamepad.current != null)
        {
            foreach (InputControl control in Gamepad.current.allControls)
            {
                if (control.IsPressed())
                {
                    //btnDinamico.sprite = gamepad;
                    //return true;
                    if (gamepadOn == false)
                    {
                        gamepadOn = true;
                        Cursor.visible = false;
                    }
                }
            }
        }
    }

    private void LoadImages(string path, Dictionary<string, Sprite> dicionario)
    {
        string folderPath = Path.Combine(Application.streamingAssetsPath, path);
        string[] imagePaths = Directory.GetFiles(folderPath); // Obter os caminhos completos de todas as imagens na pasta

        foreach (string imagePath in imagePaths)
        {
            string imageName = Path.GetFileNameWithoutExtension(imagePath); // Nome do arquivo sem a extens�o
            Sprite sprite = LoadSprite(imagePath); // Carregar o sprite da imagem

            if (sprite != null)
            {
                dicionario.Add(imageName, sprite); // Adicionar o sprite ao dicion�rio com a chave correspondente
            }
        }
    }

    private Sprite LoadSprite(string path)
    {
        byte[] bytes = File.ReadAllBytes(path); // Ler os bytes da imagem
        Texture2D texture = new Texture2D(2, 2); // Criar uma nova textura
        bool loadSuccess = texture.LoadImage(bytes); // Carregar a imagem na textura

        if (loadSuccess)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f); // Criar e retornar o sprite
        }

        return null;
    }

    private void toggleIcons(Dictionary<string, Sprite> dicionario)
    {
        GameObject[] dynamicIcons = GameObject.FindGameObjectsWithTag("dynamicIcon");
        Image image;
        foreach (GameObject dynamicIcon in dynamicIcons)
        {
            image = dynamicIcon.GetComponent<Image>();
            string spriteName = dynamicIcon.name;
            //UI
            if (image != null)
            {
                image.sprite = dicionario[spriteName];
            }
            //In game
            else
            {
                SpriteRenderer spriteRenderer = dynamicIcon.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = dicionario[spriteName];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        detectarEntrada();
        //Debug.Log("Entrada de gamepad: "+gamepadOn);


        //muda icones da HUD pra icones de gamepad
        if (gamepadOn)
        {
            toggleIcons(gamepadIcons);
        }
        //muda icones da HUD pra icones de PC
        else
        {
            toggleIcons(pcIcons);
        }
    }
}
