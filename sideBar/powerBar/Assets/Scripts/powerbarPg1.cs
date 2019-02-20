using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System;
using System.Timers;

using UnityEngine.SceneManagement;

public class powerbarPg1 : MonoBehaviour
{
    //https://answers.unity.com/questions/1302087/how-to-restart-application.html
    public RawImage dia;

    public Sprite spr;
    //Unity elements
    public Canvas mainCanvas;
    public Sprite red;
    public Sprite vannilla;

    public Image min;

    public Texture[] arr;

    //DATA
    public int players = 4;

    public bool play = ascoltoplayer.checkgreen;
    void Start()
    {
        pb1 = new barElement(mainCanvas.transform.Find("red bar pg1").gameObject.GetComponent<Image>(), mainCanvas.transform.Find("green bar pg1").gameObject.GetComponent<Image>(), mainCanvas.transform.Find("freccetta pg1").gameObject.GetComponent<Image>());
        pb1.canPlay = ascoltoplayer.checkgreen;
    }

    public bool arrowin = false; //arrow in green zone
    public bool readyToStart = true; //game ready to start
    barElement pb1;

    bool[] colorIn = { false, false, false, false };
    bool allow = true;
    int points = 0;

    bool anim = false;
    public Texture2D[] frames;
    public int fps = 20;
    int counter = 0;
    public RawImage minatoreCa;
    bool can = true;
    
    void Update()
    {

        if (anim)
        {
            int index = (int)(Time.time * fps) % frames.Length;
            minatoreCa.texture = frames[index];
            counter++;
            if (counter == 41)
            {
                counter = 0;
                minatoreCa.texture = frames[18];
                anim = false;
            }
        }
        if (allow)
        {
            if (ascoltoplayer.checkgreen)
                mainCanvas.transform.Find("green bar pg1").gameObject.GetComponent<Image>().enabled = pb1.Active;
            else
            {
                mainCanvas.transform.Find("red bar pg1").gameObject.GetComponent<Image>().enabled = false;
                mainCanvas.transform.Find("back bar pg1").gameObject.GetComponent<Image>().enabled = false;
                mainCanvas.transform.Find("green bar pg1").gameObject.GetComponent<Image>().enabled = false;
                mainCanvas.transform.Find("freccetta pg1").gameObject.GetComponent<Image>().enabled = false;

                minatoreCa.enabled = false;
                dia.enabled = false;
                mainCanvas.transform.Find("txtverde").gameObject.GetComponent<Text>().enabled = false;


            }

            mainCanvas.transform.Find("red bar pg1").gameObject.GetComponent<Image>().sprite = red;
            if (ascoltoplayer.checkgreen)
                mainCanvas.transform.Find("green bar pg1").gameObject.GetComponent<Image>().enabled = true;

            if (pb1.Active)
            {
                mainCanvas.transform.Find("freccetta pg1").gameObject.GetComponent<Image>().rectTransform.localPosition = new Vector3(pb1.getNow(), mainCanvas.transform.Find("freccetta pg1").gameObject.GetComponent<Image>().rectTransform.anchoredPosition.y);
                pb1.pos = pb1.getNow();
            }
        }
        else
        {
            mainCanvas.transform.Find("green bar pg1").gameObject.GetComponent<Image>().enabled = false;
            mainCanvas.transform.Find("red bar pg1").gameObject.GetComponent<Image>().sprite = vannilla;
        }
        if (Input.GetButtonDown("q") && can) //COLOR 1
        {
            if (colorIn[0])
                funct();

        }
        if (Input.GetButtonDown("w") && can) //COLOR 2
        {
            if (colorIn[1])
                funct();

        }
        if (Input.GetButtonDown("e") && can) //COLOR 3
        {
            if (colorIn[2])
                funct();

        }
        if (Input.GetButtonDown("r") && can) //COLOR 4
        {
            if (colorIn[3])
                funct();

        }

        if (Input.GetButtonDown("Buzz 2") && can) //RED BUTTON
        {
            if (!allow)
                return;
            pb1.iNow = mainCanvas.transform.Find("freccetta pg1").gameObject.GetComponent<Image>().rectTransform.localPosition.x;
            if (pb1.check())
            {
                allow = false;
                //arrow in
                System.Random rnd = new System.Random();
                int val = rnd.Next(0, 4);
                colorIn[val] = true;
                dia.texture = arr[val];

                anim = true;
                //
                //
                //ANIMAZIONE ROTTURA + VISIONE PIETRA
                //
                //



            }
        }
        if (Input.GetButtonDown("LB") && can)
        {
            allow = true;
        }

    }
    //IEnumerator aspettaavvio()
    //{
    //    yield return new WaitForSeconds(3);

    //    connetti.NewGameBtn(0);
    //}
    void funct()
    {
        minatoreCa.texture = frames[0];
        points++;
        if (points == 10)
        {
            mainCanvas.transform.Find("txtverde").gameObject.GetComponent<Text>().text = "You win!";
            allow = false;
            anim = false;
            can = false;
        }
        else
            mainCanvas.transform.Find("txtverde").gameObject.GetComponent<Text>().text = points.ToString();

        colorIn[0] = false;
        colorIn[1] = false;
        colorIn[2] = false;
        colorIn[3] = false;
        allow = true;

    }

    public class barElement
    {
        private Image redBar;
        private Image greenBar;
        private Image pointer;

        private Text tex;
        //min and max of the bar
        private float iMin = 0;
        private float iMax = 0;
        public float iNow = 0; //arrw atm

        private int startPerc = 80;
        private double constPerc = 3.5; //one percent is equal to ..

        bool active = true;

        private float incrementor = 2; //starting increment rate
        public float minPerc = 0; //start of green zone
        public float maxPerc = 0; //end of green zone
        private bool positive = true; //arrow direction

        public Canvas canv;

        Thread tMove;

        System.Timers.Timer t1 = new System.Timers.Timer();


        void tClick(object source, ElapsedEventArgs e)
        {
            t1.Stop();
            t1.Enabled = false;
            softReset();
            active = true;
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        void setGreenBar()
        {

            if (!canPlay)
                greenBar.enabled = false;

            System.Random rnd = new System.Random();
            startPerc = rnd.Next(5, 75);
            float barPositionX = (float)((iMin + (greenBar.rectTransform.rect.size.x / 2)) + (startPerc * constPerc));
            greenBar.rectTransform.localPosition = new Vector3(barPositionX, greenBar.rectTransform.anchoredPosition.y, 0);

            minPerc = barPositionX - (float)35;
            maxPerc = barPositionX + (float)35;
        }

        public bool canPlay;

        public barElement(Image redBar, Image greenBar, Image pointer)
        {

            this.redBar = redBar;
            this.greenBar = greenBar;
            this.pointer = pointer;

            iMin = redBar.rectTransform.anchoredPosition.x - (redBar.rectTransform.rect.size.x / 2);
            iMax = redBar.rectTransform.anchoredPosition.x + (redBar.rectTransform.rect.size.x / 2);
            iNow = iMin;
            setGreenBar();

            tMove = new Thread(new ThreadStart(move));
            tMove.IsBackground = true;
            tMove.Start();

            t1.Interval = 2000;
            t1.Elapsed += new ElapsedEventHandler(tClick);
            t1.Enabled = true;
        }

        public float getNow()
        {
            return iNow;
        }

        public void move()
        {
            while (true)
            {
                if (Active)
                {
                    //end reached
                    if (iNow + incrementor >= iMax)//right
                        positive = false;
                    else if (iNow - incrementor <= iMin)//left
                        positive = true;
                    if (positive && active) { iNow += incrementor; }
                    else { iNow -= incrementor; }
                }

                Thread.Sleep(5);//DEFAULT SPEED
            }
        }
        public bool cheking = false;
        public float pos = 0;
        public float tempPos = 0;
        public bool check()
        {
            active = false;
            tempPos = pos;
            if (tempPos > minPerc && tempPos < maxPerc)
            {
                
                softReset();
                setGreenBar();
                active = true;
                return true;
            }
            else
            {
                if (incrementor < 16)
                    incrementor *= 2;
                active = true;
                return false;
            }
        }
        
        public void start()
        {
            tMove.Start();
        }

        void softReset()
        {
            incrementor = 2;
            iNow = iMin;
            positive = true;
        }
    }
}
