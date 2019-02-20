using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System;
using System.Timers;

public class powerbar_pg1 : MonoBehaviour
{

    //Unity elements
    public Canvas mainCanvas;

    //DATA
    int players = 4;

    void Start()
    {
        pb1 = new barElement(mainCanvas.transform.Find("red bar pg1").gameObject.GetComponent<Image>(), mainCanvas.transform.Find("green bar pg1").gameObject.GetComponent<Image>(), mainCanvas.transform.Find("freccetta pg1").gameObject.GetComponent<Image>());

    }

    public bool arrowin = false; //arrow in green zone
    public bool readyToStart = true; //game ready to start
    barElement pb1;


    void Update()
    {
        mainCanvas.transform.Find("green bar pg1").gameObject.GetComponent<Image>().enabled = pb1.Active;

        if (pb1.Active)
        {
            mainCanvas.transform.Find("freccetta pg1").gameObject.GetComponent<Image>().rectTransform.localPosition = new Vector3(pb1.getNow(), mainCanvas.transform.Find("freccetta pg1").gameObject.GetComponent<Image>().rectTransform.anchoredPosition.y);
            pb1.pos = pb1.getNow();
        }

        if (Input.GetButtonDown("Buzz 1 Rosso"))
        {
            pb1.iNow = mainCanvas.transform.Find("freccetta pg1").gameObject.GetComponent<Image>().rectTransform.localPosition.x;
            pb1.check();
        }

        if (Input.GetButtonDown("Buzz 2 Verde"))
        {
            pb1.Active = true;
        }

        if (Input.GetButtonDown("Buzz 2 Rosso"))
        {
            pb1.restart();
        }
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

        bool active = false;

        private float incrementor = 2; //starting increment rate
        public float minPerc = 0; //start of green zone
        public float maxPerc = 0; //end of green zone
        private bool positive = true; //arrow direction

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
            System.Random rnd = new System.Random();
            startPerc = rnd.Next(0, 80);
            float barPositionX = (float)((iMin + (greenBar.rectTransform.rect.size.x / 2)) + (startPerc * constPerc));
            greenBar.rectTransform.localPosition = new Vector3(barPositionX, greenBar.rectTransform.anchoredPosition.y, 0);

            minPerc = barPositionX - (float)35;
            maxPerc = barPositionX + (float)35;
        }

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
        public void check()
        {
            if (!active)
                return;
            active = false;
            tempPos = pos;
            if (tempPos > minPerc && tempPos < maxPerc)
            {
                t1.Enabled = true;
                t1.Start();
                setGreenBar();
                
            }
            else
            {
                if (incrementor < 16)
                    incrementor *= 2;
                active = true;
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

        public void close()
        {
            try
            {
                tMove.Abort();
                tMove.Join();
            }
            catch (Exception ex) { Debug.Log(ex.Message); }
        }

        public void restart()
        {
            setGreenBar();
            softReset();
            active = true;

        }

    }
}
