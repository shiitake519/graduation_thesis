using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class mark : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    public ray r;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        csvFile = Resources.Load("moji7") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = float.Parse(csvDatas[r.num][3*r.count + 2]);
        float y = float.Parse(csvDatas[r.num][3*r.count + 3]);
        Vector3 vec = new Vector3(x,y,10.0f);
        Vector3 scr = cam.ScreenToWorldPoint(vec);
        this.gameObject.transform.position = new Vector3(scr.x,scr.y,scr.z);
        if(r.count > int.Parse(csvDatas[r.num][1])-1){
            this.gameObject.SetActive (false);
        }
        //if(r.count == 0){
        //    this.gameObject.SetActive (true);
        //}
    }
}
