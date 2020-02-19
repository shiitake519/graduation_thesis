using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ray : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    Texture2D drawTexture;
    Color[] buffer;
    Color[] copybuf;
    bool trueflag = false;
    bool diffflag = false;
    bool owaritrue = false;
    bool owaridiff = false;
    public int count;
    public float distance3 = 0.0f;
    public UDPClient udpclient;
    //public Text text;
    public int num = 1;
    public Vector3 posnum;
    Color[] hakushi;
    public switchbutton s;
    public Toggle toggle;
    bool tensetu_flag = false;
    public int fcount = 0;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        //テクスチャ事前処理
        Texture2D mainTexture = (Texture2D)GetComponent<Renderer>().material.mainTexture;
        Color[] pixels = mainTexture.GetPixels();

        copybuf = new Color[pixels.Length];
        buffer = new Color[pixels.Length];
        hakushi = new Color[pixels.Length];
        pixels.CopyTo(buffer, 0);
        pixels.CopyTo(copybuf, 0);
        pixels.CopyTo(hakushi, 0);

        drawTexture = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
        drawTexture.filterMode = FilterMode.Point;

        //csv処理
        csvFile = Resources.Load("moji8") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }
        count = 0;
    }

    public void Draw(Vector2 p)
    {
        for (int x = 0; x < 256; x++)
        {
            for (int y = 0; y < 256; y++)
            {
                if ((p - new Vector2(x, y)).magnitude < 7)
                {
                    buffer.SetValue(Color.black, x + 256 * y);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool clickflag = false;
        bool dragflag = false;
        bool upflag = false;
        bool touchflag = false;
        Vector3 clickpos = new Vector3(0,0,0);
        Vector3 dragpos = new Vector3(0,0,0);
        Vector3 uppos = new Vector3(0,0,0);
        //string str1 = csvDatas[num][(3 * count) + (5*3)+4];
        //Debug.Log(str1);
        //float x0 = float.Parse(csvDatas[num][3*count + 2]);
        //float y0 = float.Parse(csvDatas[num][3*count + 3]);
        //posnum = new Vector3(x0, y0, 10.0f);

        if(s.onbutton){
            hakushi.CopyTo(buffer, 0);
            hakushi.CopyTo(copybuf, 0);
        }

        if (Input.GetMouseButtonDown(0))//クリックした瞬間
        {
            clickpos = Input.mousePosition;
            clickpos.z = 10.0f;
            clickflag = true;
        }

        if (Input.GetMouseButton(0))
        {
            dragpos = Input.mousePosition;
            dragpos.z = 10.0f;
            dragflag = true;
        }

        if (Input.GetMouseButtonUp(0))//マウスが話されたとき
        {
            uppos = Input.mousePosition;
            uppos.z = 10.0f;
            upflag = true;
        }

        if (Input.touchCount > 0)
        {
            // タッチ情報の取得
            Touch touch = Input.GetTouch(0);
            clickpos = touch.position;
            dragpos = touch.position;
            uppos = touch.position;
            clickpos.z = 10.0f;
            dragpos.z = 10.0f;
            uppos.z = 10.0f;
            touchflag = true;
            
            if (touch.phase == TouchPhase.Began)
            {
                clickflag = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                upflag = true;
                touchflag = false;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                dragflag = true;
            }
        }



        if (clickflag/*Input.GetMouseButtonDown(0)*/)//クリックした瞬間
        {
            Vector3 s_position = Input.mousePosition;
            s_position.z = 10.0f;

            float x = float.Parse(csvDatas[num][3*count + 2]);
            float y = float.Parse(csvDatas[num][3*count + 3]);

            Vector3 posB = new Vector3(x, y, 10.0f);

            float distance = Vector3.Distance(clickpos/*s_position*/, posB);

            if (distance < 50)
            {
                trueflag = true;
                //Debug.Log("距離"+distance);
                //text.text = "距離=" + distance;
            }
            else
            {
                diffflag = true;
                //Debug.Log("距離" + distance);
                //text.text = "距離=" + distance;
            }
            clickflag = false;
        }

        bool flag = false;
        if (dragflag/*Input.GetMouseButton(0)*/) { 
            //レイの設定
            //Ray rays = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if(touchflag){
            //Ray rays = Camera.main.ScreenPointToRay(touch.position);//ここがうまく動作するかどうか
            Touch touch2 = Input.GetTouch(0);
            //}
            Ray rays = Camera.main.ScreenPointToRay(touch2.position);//ここがうまく動作するかどうか
            //if(Input.touchPressureSupported){
                //Debug.Log ("3Dタッチ使えるよ！");
              //  string message =  touch2.position.x.ToString();
              //  //text.text = "座標" + message +"," + touch2.position.y.ToString();
              //  text.text = "圧力=" + Input.touches[0].pressure;
            //}
            RaycastHit hits;
            //Vector3 d_position = Input.mousePosition;
            //d_position.z = 10.0f;
            Vector3 touchpos = touch2.position;
            touchpos.z = 10.0f;

            if (Physics.Raycast(rays, out hits, 100.0f))
            {
                if (hits.transform.name != "Plane")
                {
                    if (trueflag)//正しい描き始めであれば
                    {
                        flag = true;
                        int n = int.Parse(csvDatas[num][1]);
                        float x3 = float.Parse(csvDatas[num][(3 * count) + (n*3)+2]);
                        float y3 = float.Parse(csvDatas[num][(3 * count) + (n*3)+3]);
                        Vector3 posD = new Vector3(x3, y3, 10.0f);
                        //Vector3 touchpos = 
                        distance3 = Vector3.Distance(touchpos/*d_position*/, posD);

                        float x4 = float.Parse(csvDatas[num][(2*fcount) + 32]);
                        float y4 = float.Parse(csvDatas[num][(2*fcount) + 33]);
                        Vector3 posE = new Vector3(x4, y4, 10.0f);
                        float distance4 = Vector3.Distance(touchpos/*s_position*/, posE);

                        //udpclient.send(distance3);
                        //Debug.Log("0");
                        //serialHandler.Write("a\n");
                        if(distance3 < 70){
                            //if(!toggle.isOn){
                                string str = csvDatas[num][(3 * count) + (n*3)+4];
                                if(str == "止め"){
                                    udpclient.send("c");
                                }else if(str == "跳ね"){
                                    udpclient.send("d");
                                }else if(str == "左はらい"){
                                    udpclient.send("e");
                                }else if(str == "右はらい"){
                                    udpclient.send("g");
                                }
                            //}else{
                            //    udpclient.send("a");
                            //}
                        }else{
                            //ここに転折の座標に来たら送る文字を変える
                            if(distance4 < 50 && !tensetu_flag){
                                tensetu_flag = true;
                            }

                            if(tensetu_flag && distance4 > 50){
                                  fcount++;
                                  tensetu_flag = false;
                            }
                            if(tensetu_flag){
                                udpclient.send("h");
                            }
                            else{
                                udpclient.send("a");
                            }
                        }
                    }
                    //Debug.Log("hit");
                }
                else
                {
                    //Debug.Log("90");
                    //serialHandler.Write("b\n");
                    //if(toggle.isOn){
                        udpclient.send("b");
                    //}
                }

            }
                //ヒットしたすべてのオブジェクト情報を取得
                foreach (RaycastHit hit in Physics.RaycastAll(rays))
            {
                //ヒットしたオブジェクトの名前

                if(hit.transform.name == "Plane")
                {
                     if (flag) {//正しい書き始めでドラッグ中
                        Draw(hit.textureCoord * 256);
                        //drawTexture.SetPixels(buffer);
                        //drawTexture.Apply();
                        //GetComponent<Renderer>().material.mainTexture = drawTexture;
                        //終点近くのフラグが立っているならの条件をたて文字を送る終点特徴に合わせてそのelseに普通の筆の感覚を出すための文字を送る
                        //上の部分のレイのところに書いた方がいいかも
                    }

                }
                else
                {
                    //Debug.Log(hit.transform.name);
                }
                //drawTexture.SetPixels(buffer);
                //drawTexture.Apply();
                //GetComponent<Renderer>().material.mainTexture = drawTexture;
            }
            
        }
        drawTexture.SetPixels(buffer);
        drawTexture.Apply();
        GetComponent<Renderer>().material.mainTexture = drawTexture;

        if (upflag/*Input.GetMouseButtonUp(0)*/)//マウスが話されたとき
        {
            Vector3 o_position = Input.mousePosition;
            o_position.z = 10.0f;
            int m = int.Parse(csvDatas[num][1]);
            //int m = int.Parse(csvDatas[1][1]);

            float x2 = float.Parse(csvDatas[num][(3 * count) + (m*3)+2]);
            float y2 = float.Parse(csvDatas[num][(3 * count) + (m*3)+3]);

            Vector3 posC = new Vector3(x2, y2, 10.0f);

            float distance2 = Vector3.Distance(uppos/*o_position*/, posC);

            if (distance2 < 30)
            {
                owaritrue = true;
                //Debug.Log("距離" + distance2);
                //text.text = "距離=" + distance2;
            }
            else
            {
                owaridiff = true;
                //text.text = "距離=" + distance2;
            }

            if (trueflag && owaritrue)//正しい描き始めの時(ここを描き終わりに変える)正しい描き始めかつ正しい描き終わりの時がベストか
            {
                if (count < int.Parse(csvDatas[num][1])) {
                    count++;
                }
                trueflag = false;
                //if (count != 0) {
                    buffer.CopyTo(copybuf, 0);
                    //ここのときにpixelの状態をコピーしておく
                //}

            }
            else
            {
                trueflag = false;
                //if (count != 0)
                //{
                    copybuf.CopyTo(buffer, 0);
                    //間違った順序なら前の状態に描写を戻す
                //}
            }

            if (diffflag)//間違った描き始めの時(ここを描き終わりのフラグに変える)間違った描き終わりのときに書いたものがリセットされたらいいかも
            {
                diffflag = false;
            }
            /*
            if (owaridiff)//終点が異なっていたら
            {

            }*/
            //diffflagとtrueflagを戻すのは離した時に条件なしで戻していいか
            //print("いま左ボタンが離された");
            udpclient.send("f");
            owaritrue = false;
            owaridiff = false;
            upflag = false;
            dragflag = false;
        }
        
    }
}
