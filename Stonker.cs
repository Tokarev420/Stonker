using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stonker
{
    public partial class Stonker : Form
    {

        string output = Application.StartupPath + @"\Outcomes\Prices";
        string[] exclude = { "Sticker", "Graffiti", "Knife", "Pin", "Tag", "Tool", "Key", "Pass", "Music", "Gloves", "Container", "Agent", "Patch" };
        string url = @"http://steamcommunity.com/market/search/render/?count=100&sort_column=name&sort_dir=asc&appid=730&norender=1&search_descriptions=0&start=";
        List<string> lines;

        int reqsMade = 10000;
        int startReqs = 10000;
        int targetReqs = 14856;
        //int targetReqs = 14856;

        public Stonker()
        {
            InitializeComponent();
        }

        private void Stonker_Load(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
            lines = new List<string>();
            startReqs = reqsMade;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool written = false;
            int qn = 0;
            while (reqsMade < targetReqs)
            {
                qn++;
                DateTime beginning = DateTime.Now;
                try
                {
                    string qStr = WebReq.GetResponse(url + reqsMade);
                    Request data = JsonSerializer.Deserialize<Request>(qStr);
                    qStr = "";

                    int target = data.results.Count;
                    for (int i = 0; i < target; i++)
                    {
                        int it = 0;
                        while (it < 13)
                        {
                            if (data.results[i].hash_name.Contains(exclude[it++]))
                            {
                                it = -1;
                                break;
                            }
                        }

                        lines.Add(data.results[i].hash_name + "," + data.results[i].sale_price_text + "," + data.results[i].sell_price_text);

                        if (it > 0)
                        {
                        }
                    }

                    reqsMade += 100;
                    //MessageBox.Show("REQS: " + reqsMade);
                    File.WriteAllLines(output+qn+ ".csv", lines);
                    worker.ReportProgress(min(100,(reqsMade - startReqs + 100) * 100 / (targetReqs - startReqs)));
                    int offset = (int)(DateTime.Now - beginning).TotalMilliseconds;
                    Thread.Sleep(30000 - offset);
                }
                catch (Exception ex)
                {
                    if(!written)
                    {
                        written = true; 
                    }
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("REQS: " + reqsMade);
                }

            }

            File.WriteAllLines(output+ ".csv", lines);
        }

        int min(int a, int b)
        {
            return a > b ? b : a;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\intervals.txt";
            string[] lines = File.ReadAllLines(path);
            int totalReqs = 0;
            foreach(string q in lines)
            {
                string[] splitted = q.Split(',');
                totalReqs += Int32.Parse(splitted[1]) - Int32.Parse(splitted[0]);
            }
            MessageBox.Show("REQS: " + totalReqs + " DIFFERENT: " + lines.Length);
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }
    }
}
