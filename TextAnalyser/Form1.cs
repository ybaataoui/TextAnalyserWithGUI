using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TextAnalyser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            string fileName = openFileDialog2.FileName;
            //string readFile = File.ReadAllText(fileName);
            this.textBox1.Text = fileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string fileName = openFileDialog2.FileName;
            String content = "";
            try
            {
                String path = openFileDialog2.FileName; ;
                content = File.ReadAllText(path, Encoding.UTF8);

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            String newContent1 = Between(content, "START", "END");

            var words = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);

            countWordsInFile(newContent1, words);
            foreach (var item in words.OrderByDescending(x => x.Value).Take(20))
            {
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
                
            }

            //Console.WriteLine("*****Created by Youssef*****");
            Dictionary<string, int> second = new Dictionary<string, int>();

            foreach (var item in words.OrderByDescending(x => x.Value).Take(20))
            {
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
                second.Add(item.Key, item.Value); // copy the 20 values ordered from dictionary words to dictionary second.

            }
            this.textAnalyse.Lines = second.Select(x => x.Key + " : " + x.Value).ToArray(); // Display the result into the textBox


        }

        //This a funtion that returns just the text we want 
        public static string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        // This a function that count word's frequency in a string and return a dictionary that hold words with thier frequancy. 
        private static Dictionary<string, int> countWordsInFile(string str, Dictionary<string, int> words)
        {
            var content = str;

            var wordPattern = new System.Text.RegularExpressions.Regex(@"\w+");

            foreach (Match match in wordPattern.Matches(content))
            {
                if (!words.ContainsKey(match.Value))
                    words.Add(match.Value, 1);
                else
                    words[match.Value]++;
            }

            return words;
        }
    }
}
