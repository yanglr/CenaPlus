﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using mshtml;
using CenaPlus.Server.Bll;
using CenaPlus.Server.Dal;
namespace CenaPlus.Server.ServerMode
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private CenaPlusServerHost host;
        public Home()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            if (host == null)
            {
                host = new CenaPlusServerHost(9999,"Orz Yuno Server");
                host.Open();
                btnTest.Content = "Click to stop";
            }
            else
            {
                host.Close();
                host = null;
                btnTest.Content = "Click to start";
            }
        }

        private void btnToHTML_Click(object sender, RoutedEventArgs e)
        {
            var range = new TextRange(txtRich.Document.ContentStart, txtRich.Document.ContentEnd);
            using (System.IO.MemoryStream mem = new System.IO.MemoryStream())
            {
                range.Save(mem,DataFormats.Xaml);
                MessageBox.Show(Encoding.UTF8.GetString(mem.ToArray()));
            }
            IHTMLDocument2 doc = browser.Document as IHTMLDocument2;
            doc.designMode = "On";

            //MessageBox.Show(browser.Document.GetType().ToString());
        }
    }
}
