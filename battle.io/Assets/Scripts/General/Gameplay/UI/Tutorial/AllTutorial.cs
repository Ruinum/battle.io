using System.Collections;
using UnityEngine;
using System.Xml;
using DG.Tweening;
using Ruinum.Core;
using UnityEngine.UI;
using System.IO;

public class AllTutorial : Executable
{
    public float TimeToFade;
    [Space]
    public Image[] images;

    XmlDocument xDoc = new XmlDocument();
    public override void Start()
    {
        xDoc.Load("Settings.xml");
        if (xDoc.DocumentElement.Attributes.GetNamedItem("tutorial").Value == "1") Destroy(gameObject);
        NextIMG_w();
        base.Start();
    }

    [SerializeField]private int _i = 0;
    public override void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Return)) NextIMG();
    }

    public void NextIMG()
    {
        if (_i == images.Length) { StartCoroutine(TutorialPass());  return; }
        images[_i-1].DOFade(0, TimeToFade);
        images[_i].DOFade(1, TimeToFade);
        _i++;
    }
    public void NextIMG_w()
    {
        images[_i].DOFade(1, TimeToFade);
        _i++;
    }
    public IEnumerator TutorialPass()
    {
        yield return new WaitForSeconds(TimeToFade);
        images[_i-1].DOFade(0, TimeToFade);
        yield return new WaitForSeconds(TimeToFade);
        xDoc.DocumentElement.Attributes.GetNamedItem("tutorial").Value = "1";
        xDoc.Save(new FileInfo("Settings.xml").FullName);
        Destroy(gameObject);
    }
}
