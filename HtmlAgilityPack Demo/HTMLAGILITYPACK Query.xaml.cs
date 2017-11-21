using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using EngineLayer;

namespace MetaMorpheusGUI
{
    /// <summary>
    /// Interaction logic for ReleaseNotes.xaml
    /// </summary>
    public partial class ReleaseNotes : Window
    {
        public ReleaseNotes()
        {
            InitializeComponent();
            HtmlWeb gitPage = new HtmlWeb();
            HtmlDocument doc = gitPage.Load("https://github.com/smith-chem-wisc/MetaMorpheus/releases");
            HtmlNode testClass = doc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'release-title')]");
            HtmlNode testNotes = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'release-body commit open')]").SelectSingleNode("//div[contains(@class,'markdown-body')]");
            //GlobalEngineLevelSettings.MetaMorpheusVersion.Contains("DEBUG")+"");
            printVersionNotes();
        }

        public int parser(string VersionNode)
        {
            string pattern = @"\d\.\d\.(\d+)";
            return Int32.Parse(Regex.Match(VersionNode, pattern).Groups[1].Value);
        }

        public void printVersionNotes()
        {
            bool debugVersion = true;
            int integerVersion=0;
            if (!debugVersion)
            {
                integerVersion = 220;//parser(GlobalEngineLevelSettings.NewestVersion);
            }

            HtmlWeb gitPage = new HtmlWeb();
            HtmlDocument doc = gitPage.Load("https://github.com/smith-chem-wisc/MetaMorpheus/releases");
            HtmlNode[] Releases = doc.DocumentNode.SelectNodes("//div[contains(@class,'release-body commit open')]").ToArray();//.SelectSingleNode("//div[contains(@class,'markdown-body')]");
            foreach(HtmlNode hNode in Releases)
            {
                string currV = hNode.SelectSingleNode(".//h1[contains(@class, 'release-title')]").InnerText;
                if (!debugVersion && parser(currV)<=integerVersion)
                {
                    break;
                }
                releaseNotes.Text += currV;
                releaseNotes.Text += hNode.SelectSingleNode(".//div[contains(@class,'markdown-body')]").InnerText;
            }
        }
    }
}
