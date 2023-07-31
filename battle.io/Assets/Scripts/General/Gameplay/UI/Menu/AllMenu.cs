using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AllMenu : MonoBehaviour
{
    XmlDocument xDoc = new XmlDocument();
    XmlReader xReader;
    Setts[] S = { new Setts("Master",0), new Setts("Music", 0) , new Setts("Effects", 0) };
    protected  void Awake()
    {
        try
        {
            xDoc.Load("Settings.xml");
            for (int i = 0; i < S.Length; i++)
            {
                S[i].value = LoadXML(S[i].name);
                Debug.Log("Loaded: " + S[i].name + " = " + S[i].value);
            }
        }
        catch
        {
            XmlWriter writer = XmlWriter.Create("Settings.xml");
            writer.WriteStartElement("Setts");

            for (int i = 0; i < S.Length; i++)
            {
                writer.WriteStartElement("Own");
                writer.WriteAttributeString("name", S[i].name);

                writer.WriteElementString("Nested", "0");
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();
            xDoc.Load("Settings.xml");
        }
    }

    public void Play()
    {
        SaveAll();
        
    }

    public void Exit()
    {
        SaveAll();
        Application.Quit();
    }

    public void SaveAll()
    {
        for (int i = 0; i < S.Length; i++)
        {
            SaveXML(S[i].name, S[i].value);
        }
    }

    public TextMeshProUGUI[] text;

    public void SetMasterValue(Slider slider)
    {
        S[0].value = slider.value;
        text[0].text = ((int)(slider.value * 100)).ToString();
    }

    public void SetMusicValue(Slider slider)
    {
        S[1].value = slider.value;
        text[1].text = ((int)(slider.value * 100)).ToString();
    }

    public void SetEffectsValue(Slider slider)
    {
        S[2].value = slider.value;
        text[2].text = ((int)(slider.value * 100)).ToString();
    }

    public float LoadXML(string s)
    {
        // получим корневой элемент
        XmlElement xRoot = xDoc.DocumentElement;
        if (xRoot != null)
        {
            // обход всех узлов в корневом элементе
            foreach (XmlElement xnode in xRoot)
            {
                if(xnode.Attributes.GetNamedItem("name").Value == s)
                {
                    return float.Parse(xnode.FirstChild.InnerText);
                }
            }
        }
        return 0;
    }

    public void SaveXML(string s,float x)
    {
        XmlElement xRoot = xDoc.DocumentElement;
        if (xRoot != null)
        {
            // обход всех узлов в корневом элементе
            foreach (XmlElement xnode in xRoot)
            {
                if (xnode.Attributes.GetNamedItem("name").Value == s)
                {
                    xnode.FirstChild.InnerText = x.ToString();
                    Debug.Log("Saved: " + xnode.Attributes.GetNamedItem("name").Value + " = " + xnode.FirstChild.InnerText);
                }
            }
        }
        xDoc.Save(new FileInfo("Settings.xml").FullName);
    }
}

class Setts
{
    public string name;
    public float value;

    public Setts(string n,float v)
    {
        name = n;
        value = v;
    }
}