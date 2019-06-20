using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handler : MonoBehaviour
{
    public GameObject Question;
    public GameObject[] Choice;
    public GameObject Times;
    public GameObject[] ChoiceBack;
    public GameObject nextB;
    public Sprite pressed;
    public Sprite notPressed;
    public Sprite wrong;
    public Sprite right;
    public Sprite nextpressed;
    public AudioSource slow;
    public AudioSource fast;
    public AudioSource correct, incorrect;

    private float DT = 0.0f;
    private int answer;
    private int select = -1;
    private bool next = false;

    public void nextQ()
    {
        if (DT <= 0.0f)
        {
            select = -1;
            lightup();
            next = true;
        }        
    }

    public void button1()
    {
        if(DT > 0.0f)
        {
            if (select == 1)
            {
                result();
                DT = 0.0f;
                return;
            }
            select = 1;
            lightup();
        }
    }
    public void button2()
    {
        if (DT > 0.0f)
        {
            if (select == 2)
            {
                result();
                DT = 0.0f;
                return;
            }
            select = 2;
            lightup();
        }
    }
    public void button3()
    {
        if (DT > 0.0f)
        {
            if (select == 3)
            {
                result();
                DT = 0.0f;
                return;
            }
            select = 3;
            lightup();
        }
    }
    public void button4()
    {
        if (DT > 0.0f)
        {
            if (select == 4)
            {
                result();
                DT = 0.0f;
                return;
            }
            select = 4;
            lightup();
        }
    }

    void lightup()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == (select - 1))
            {
                ChoiceBack[i].GetComponent<Image>().overrideSprite = pressed;
            }
            else
            {
                ChoiceBack[i].GetComponent<Image>().overrideSprite = notPressed;
            }
        }
    }

    public void ReadText()
    {
        TextAsset asset = Resources.Load("TEST") as TextAsset;
        string str = asset.text;
        char[] sepe = new char[] { '\n' };
        string[] lines = str.Split(sepe);
        int ran = Random.Range(0, lines.Length);
        {
            if (lines == null) return;
            else
            {
                char[] seps = new char[] { ':' };
                string[] StringList = lines[ran].Split(seps);
                {
                    Question.GetComponent<TMPro.TextMeshProUGUI>().text = StringList[0];
                    Choice[0].GetComponent<TMPro.TextMeshProUGUI>().text = StringList[1];
                    Choice[1].GetComponent<TMPro.TextMeshProUGUI>().text = StringList[2];
                    Choice[2].GetComponent<TMPro.TextMeshProUGUI>().text = StringList[3];
                    Choice[3].GetComponent<TMPro.TextMeshProUGUI>().text = StringList[4];
                    string a = StringList[5];
                    answer = int.Parse(a);
                }
            }
        }
    }

    void Update()
    {
        if (next)
        {
            next = false;
            DT = 10.0f; 
            ReadText();
            
        }
        if (next == false && DT > 0.0f)
        {
            DT -= Time.deltaTime;
            if (DT >= 3.0f)
            {
                Times.GetComponent<TMPro.TextMeshProUGUI>().text = "Time Remaining : " + DT;
                if(!slow.isPlaying)
                {
                    slow.Play();
                }
            }
            else if (DT >= 0.0f)
            {
                Times.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=#ff0000>" + "Time Remaining : " + DT + "</color>";
                if(!fast.isPlaying)
                {
                    slow.Stop();
                    fast.Play();
                }
            }
            else if (DT < 0.0f)
            {
                result();
                DT = 0.0f;                
            }
        }
    }

    void result()
    {
        slow.Stop();
        fast.Stop();

        if (select == answer)
        {
            Times.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=#009900>" + "정답" + "</color>";
            ChoiceBack[answer - 1].GetComponent<Image>().overrideSprite = right;
            correct.Play();
        }

        if (select != answer)
        {
            Times.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=#ff0000>" + "오답" + "</color>";
            ChoiceBack[answer - 1].GetComponent<Image>().overrideSprite = right;
            if (select != -1)
            {
                ChoiceBack[select - 1].GetComponent<Image>().overrideSprite = wrong;
            }
            incorrect.Play();
        }
    }
}