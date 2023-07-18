using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    public GameObject lanternaAcesa;
    public GameObject lanternaApagada;

    public GameObject martelo;
    public GameObject machado;
    public GameObject peDeCabra;
    public GameObject noWeapon;

    public GameObject uso3;
    public GameObject uso2;
    public GameObject uso1;

    public GameObject chave1;

    public GameObject chave2;

    // Start is called before the first frame update
    void Start()
    {
        delChave1();
        delChave2();
        delUso3();
        delUso2();
        delUso1();
        delMachado();
        delMartelo();
        delPeDeCabra();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lanternaLigaDesliga()
    {
        if (lanternaAcesa.activeSelf)
        {
            lanternaAcesa.SetActive(false);
            lanternaApagada.SetActive(true);
        }
        else
        {
            lanternaApagada.SetActive(false);
            lanternaAcesa.SetActive(true);
        }
    }

    public void setChave1()
    {
        chave1.SetActive(true);
    }

    public void setChave2()
    {
        chave2.SetActive(true);
    }

    public void delChave1()
    {
        chave1.SetActive(false);
    }

    public void delChave2()
    {
        chave2.SetActive(false);
    }
    public void setMachado()
    {
        machado.SetActive(true);
        noWeapon.SetActive(false);
        setUsos();
    }

    public void delMachado()
    {
        machado.SetActive(false);
        noWeapon.SetActive(true);
    }

    public void setMartelo()
    {
        martelo.SetActive(true);
        noWeapon.SetActive(false);
        setUsos();
    }

    public void delMartelo()
    {
        martelo.SetActive(false);
        noWeapon.SetActive(true);
    }

    public void setPeDeCabra()
    {
        peDeCabra.SetActive(true);
        noWeapon.SetActive(false);
        setUsos();
    }

    public void delPeDeCabra()
    {
        peDeCabra.SetActive(false);
        noWeapon.SetActive(true);
    }

    public void setUsos()
    {
        uso3.SetActive(true);
        uso2.SetActive(true);
        uso1.SetActive(true);
    }

    public void delUso3()
    {
        uso3.SetActive(false);
    }
    public void delUso2()
    {
        uso2.SetActive(false);
    }
    public void delUso1()
    {
        uso1.SetActive(false);
    }
}
