using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TextParseCount
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Программа считает одинаковые слова в тексте и записывает их количество в файл в директории exe.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = null;

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
            }

            List<string> words = new List<string>();
            List<int> counts = new List<int>();
            string[] linewords;

            if (fileName != null)
            {
                StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Windows-1251"));
                string line = sr.ReadLine();
                while (line != null)
                {
                    linewords = line.Split(' ');
                    for (int i =0; i < linewords.Length ; i++)
                    {
                        if (!words.Contains(linewords[i]))
                        {
                            words.Add(linewords[i]);
                            counts.Add(1);
                        }
                        else
                        {
                            counts[words.FindIndex(str => str == linewords[i])]++;
                        }

                    }
                    line = sr.ReadLine();
                }
            }

            for (int i = 0; i < counts.Count; i++)
            {
                for (int j = 0; j < counts.Count - 1; j++)
                {
                    if (counts[j] > counts[j + 1])
                    {
                        int z = counts[j];
                        string s = words[j];
                        counts[j] = counts[j + 1];
                        words[j] = words[j + 1];
                        counts[j + 1] = z;
                        words[j + 1] = s;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory +@"\\words.txt", false, Encoding.GetEncoding("Windows-1251")))
            {
                for (int i = 0; i < words.Count; i++)
                {
                    sw.WriteLine(words[i]+" | "+counts[i]);
                }
            }
        }
    }
}
