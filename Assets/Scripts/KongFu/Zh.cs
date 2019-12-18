using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zh
{

        private string nm;
        private string type;
        private int ceng;
        private int rk;

        public Zh(string Nm, string Type, int Ceng, int Rk)
        {
            this.nm = Nm;
            this.type = Type;
            this.ceng = Ceng;
            this.rk = Rk;
        }
        public string Nm
        {
            get { return nm; }
        }
        public string Type
        {
            get { return type; }
        }
        public int Ceng
        {
            get { return ceng; }
        }
        public int Rk
        {
            get { return rk; }
        }




}
