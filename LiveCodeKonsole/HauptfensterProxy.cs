﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

using System.IO;
using System.Diagnostics;

namespace LiveCodeKonsole
{
    public partial class HauptfensterProxy : Form, ICompilerCheckedRunner, IProzessreaktion
    {        
        string dateiNameKlasse = Path.GetTempPath() + "MeineKlasse.cs";
        bool bAktualisierungLaeuft;
        Compiler compiler;

        public HauptfensterProxy()
        {
            InitializeComponent();
            Trace.TraceInformation("Started. Using File " + dateiNameKlasse);
            
            bAktualisierungLaeuft = false;

            compiler = new Compiler();

            QuelltextLaden();
            QuelltextKompilierenUndAusfuehren();            
        }

        private void QuelltextLaden()
        {
            try
            {
                StreamReader reader = new StreamReader(dateiNameKlasse);
                String s = reader.ReadToEnd();
                reader.Close();

                tbQuelltext.Text = s;
            }
            catch (FileNotFoundException)
            {
                // nothing to be loaded
            }
        }

        private void tbQuelltext_TextChanged(object sender, EventArgs e)
        {
            QuelltextKompilierenUndAusfuehren();
        }

        private void QuelltextSichern()
        {
            StreamWriter writer = new StreamWriter(dateiNameKlasse);
            writer.WriteLine(tbQuelltext.Text);
            writer.Close();
        }

        private void QuelltextKompilierenUndAusfuehren()
        {
            if (bAktualisierungLaeuft)
                return;
            bAktualisierungLaeuft = true;

            compiler.CompileSource(tbQuelltext.Text, Path.GetTempPath() + DateTime.Now.Ticks + "-MeineKlasse.exe");
            compiler.CheckCompilerResultsAndRun(this);

            bAktualisierungLaeuft = false;
        }

        public void Ausfuehren()
        {
            BewachterProzess prozess = new BewachterProzess(compiler.GetPathToAssembly());
            tbAusgabe.Text = prozess.Start(this);
        }

        private void HauptfensterProxy_Shown(object sender, EventArgs e)
        {
            tbQuelltext.Focus();
        }

        public void FehlerAnzeigen(string sFehler)
        {
            tbFehler.Text = sFehler;
            tbQuelltext.BackColor = Color.OrangeRed;
        }

        public void KeineCompilerfehlerAnzeigen()
        {
            tbFehler.Text = "";
            tbQuelltext.BackColor = Color.White;
        }

        public void ProzessErfolgreich()
        {
            QuelltextSichern();
        }

        public void ProzessFehlgeschlagen(string sUrsache)
        {
            FehlerAnzeigen(sUrsache);
        }
    }    
}
