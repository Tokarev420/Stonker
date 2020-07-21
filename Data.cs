using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;

namespace Stonker
{
    public class Data
    {
        public List<Skin> skins;

        public Data()
        {
            skins = new List<Skin>();
        }

        public void LoadSkins()
        {
            string skinsPath = Application.StartupPath + @"\base.csv";
            string[] lines = File.ReadAllLines(skinsPath);
        }

    }
}
