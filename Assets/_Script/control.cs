using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;//引用此命名空间是用于数据的写入与读取
using System.Text; //引用这个命名空间是用于接下来用可变的字符串的
using UnityEngine;
using UnityEngine.UI;

public class control : MonoBehaviour
{
    public GameObject Cards;
    private GameObject[] test;
    public GameObject TheShowCard;
    private ArrayList cardData = new ArrayList();
    private int leftTimes;
    private int times;
    public void OnStartButton()
    {
        Load();
        Cards = GameObject.Find("Cards");
        test = Resources.LoadAll("Prefabs/Cards").Cast<GameObject>()
     .ToArray();
        // Debug.Log(Resources.LoadAll("Prefabs/Cards")) ;
        Debug.Log(Cards.transform.localPosition);
        RectTransform rt = (RectTransform)Cards.transform;

        float width = rt.rect.width;
        float height = rt.rect.height;
        Debug.Log(width);
        Debug.Log(height);
        Debug.Log(test.Length);
        float positionXS = width / (test.Length + 1);
        Debug.Log(positionXS);
        for (int i = 0; i < test.Length; i++)
        {
            var t = test[i];
            Debug.Log(t.name);
            cardData.Add(t.name);
            GameObject pfb = Instantiate(t);
            pfb.transform.SetParent(Cards.transform);
            // pfb.transform.localPosition = Vector3.zero;
            pfb.transform.localRotation = Quaternion.identity;
            pfb.transform.localScale = Vector3.one;
            pfb.transform.localPosition = new Vector3(-width / 2 + positionXS * (i + 1), 0, 0);
            RectTransform pfbRt = (RectTransform)pfb.transform;
            float scale = pfbRt.rect.width / pfbRt.rect.height;
            pfbRt.sizeDelta = new Vector2(width / (test.Length + 1), width / (test.Length) / scale);
            Debug.Log(pfbRt.sizeDelta);

        }
    }

    public int Choose(float[] Probs)
    {
        float total = 0;
        foreach (float elem in Probs)
        {
            total += elem;

        }
        float randomPoint = Random.value * total;
        for (int i = 0; i < Probs.Length; i++)
        {
            if (randomPoint < Probs[i])
                return i;
            else
                randomPoint -= Probs[i];
        }
        return 0;
    }



    public void Reload()
    {
        leftTimes += 5;
        GameObject timesParent = GameObject.Find("次数");
        timesParent.transform.Find("times").GetComponent<Text>().text = leftTimes.ToString();
        Debug.Log(leftTimes);

    }
    public void ShowCard()
    {
        if (leftTimes > 0)
        {
            leftTimes -= 1;
            GameObject timesParent = GameObject.Find("次数");
            timesParent.transform.Find("times").GetComponent<Text>().text = leftTimes.ToString();

            float[] probability = new float[] { 20, 10, 10, 10, 10, 5 };
            int i = Choose(probability);
            Debug.Log(cardData[i]);
            GameObject canvas = GameObject.Find("Canvas");
            TheShowCard = canvas.transform.Find("TheShowCard").gameObject;
            TheShowCard.SetActive(true);
            TheShowCard.transform.GetComponent<Image>().sprite = Resources.Load("Images/" + cardData[i], typeof(Sprite)) as Sprite;
            times += 1;
            Save();
        }



    }

    public void Save()
    {
        // StringBuilder sb = new StringBuilder();//声明一个可变字符串
        // for (int i = 0; i<10; i++)
        // {
        //     //循环给字符串拼接字符
        //     sb.append(i + '|');
        // }
        // //写文件 文件名为save.text
        // //这里的FileMode.create是创建这个文件,如果文件名存在则覆盖重新创建
        // FileStream fs = new FileStream(Application.dataPath + "/save.txt", FileMode.Create);
        // //存储时时二进制,所以这里需要把我们的字符串转成二进制
        // byte[] bytes = new UTF8Encoding().GetBytes(sb.ToString());
        // fs.Write(bytes, 0, bytes.Length);
        // //每次读取文件后都要记得关闭文件
        // fs.Close();
        string path = Application.dataPath + "/Data/save.txt";
        // 文件流创建一个文本文件
        FileStream file = new FileStream(path, FileMode.Create);
        //得到字符串的UTF8 数据流
        byte[] bts = System.Text.Encoding.UTF8.GetBytes(times.ToString());
        // 文件写入数据流
        file.Write(bts, 0, bts.Length);
        if (file != null)
        {
            //清空缓存
            file.Flush();
            // 关闭流
            file.Close();
            //销毁资源
            file.Dispose();
        }


    }

    //读取
    public void Load()
    {
        Debug.Log(Application.dataPath + "/Data/save.txt");
        if (File.Exists(Application.dataPath + "/Data/save.txt"))
        {
            // //FileMode.Open打开路径下的save.text文件
            FileStream fs = new FileStream(Application.dataPath + "/Data/save.txt", FileMode.Open);
            Debug.Log(fs);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            string str = UTF8Encoding.UTF8.GetString(bytes);
            Debug.Log(str);     //第二种方法添加txt文本


            // byte[] bytes = new byte[10];
            // fs.Read(bytes, 0, bytes.Length);
            // //将读取到的二进制转换成字符串
            // string s = new UTF8Encoding().GetString(bytes);
            // //将字符串按照'|'进行分割得到字符串数组
            // string[] itemIds = s.Split('|');
            // for (int i = 0; i < itemIds.Length; i++)
            // {
            //    Debug.Log(itemIds[i]);
            // }
        }
        else
        {
            times = 0;
            leftTimes = 0;
            Debug.Log("hhhhha");
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
