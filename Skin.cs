using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stonker
{
    public class Skin
    {
        public string name;
        public string collection;
        public int rarity;
        public float minFloat = 0.0f;
        public float maxFloat = 1.0f;
        public SkinPrices priceList;


        public Skin()
        {
            priceList = new SkinPrices();
        }

        public Skin(string line)
        {
            string[] values = line.Split(',');
            name = values[0];
            rarity = Int32.Parse(values[1]);
            minFloat = float.Parse(values[2]);
            maxFloat = float.Parse(values[3]);
            collection = values[4];
            priceList = new SkinPrices();
        }

        public string GetSaveString()
        {
            string result = name;
            result += "," + rarity;
            result += "," + minFloat.ToString("0.00");
            result += "," + maxFloat.ToString("0.00");
            result += "," + collection;
            result += "," + priceList.GetString();
            return result;
        }

        public static string IntToCondition(int cond)
        {
            if (cond == 0) return "(Factory New)";
            if (cond == 1) return "(Minimal Wear)";
            if (cond == 2) return "(Field-Tested)";
            if (cond == 3) return "(Well-Worn)";
            if (cond == 4) return "(Battle-Scarred)";
            return "NULL";
        }

        public static int ConditionToInt(string cond)
        {
            if (cond == "(Factory New)") return 0;
            if (cond == "(Minimal Wear)") return 1;
            if (cond == "(Field-Tested)") return 2;
            if (cond == "(Well-Worn)") return 3;
            if (cond == "(Battle-Scarred)") return 4;
            return -1;
        }

        public float GetPriceFromFloat(float xf, bool qstat = false) => priceList.c[FloatToCondition(xf) + (qstat ? 0 : 5)];

        public float GetOutcomeFloat(float xf) => (maxFloat - minFloat) * xf + minFloat;

        public string GetNameFromCondition(float condition, int qstat = 0)
        {
            string qname = "";
            if (qstat == 1) qname += "StatTrak™ ";
            if (qstat == 2) qname += "Souvenir ";
            qname += name + " " + IntToCondition(FloatToCondition(condition));
            return qname;
        }

        public static int FloatToCondition(float condition)
        {
            if (condition < 0.07f) return 0;
            if (condition < 0.15f) return 1;
            if (condition < 0.38f) return 2;
            if (condition < 0.45f) return 3;
            if (condition < 1.00f) return 4;
            return -1;
        }
    }
    public class SkinPrices
    {
        public float[] c { get; set; }
        public int stat { get; set; }
        public SkinPrices()
        {
            c = new float[10];
            for (int i = 0; i < 10; i++) c[i] = -1;
            stat = 1;
        }

        public string GetString()
        {
            string result = "" + stat + ",{" + c[0];

            for (int i = 1; i < 10; i++) result += "#" + c[i];

            return result + "}";
        }
    }
}
