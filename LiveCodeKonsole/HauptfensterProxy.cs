using System;
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
using System.Threading;
using LiveCodeKonsole.Properties;

namespace LiveCodeKonsole
{
    public partial class HauptfensterProxy : Form, ICompilerCheckedRunner, IProzessreaktion
    {        
        string dateiNameKlasse = Path.GetTempPath() + "MeineKlasse.cs";
        Compiler compiler;
        Settings settings = Settings.Default;

        public HauptfensterProxy()
        {
            InitializeComponent();
            Trace.TraceInformation("Started. Using File " + dateiNameKlasse);

            compiler = new Compiler();

            QuelltextLaden();
        }

        private void thread_KompilierenUndAusführen()
        {
            string sQuelltextAlt = "";
            string sQuelltextNeu = tbQuelltext.Text;

            while(this.Visible)
            {
                if (!sQuelltextNeu.Equals(sQuelltextAlt))
                {
                    QuelltextKompilierenUndAusfuehren();
                }
                sQuelltextAlt = sQuelltextNeu;
                sQuelltextNeu = tbQuelltext.Text;
                                
                Thread.Sleep(settings.compileWaitMs);
            }

            Trace.TraceInformation("compilation thread finished.");
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

        private void QuelltextSichern()
        {
            StreamWriter writer = new StreamWriter(dateiNameKlasse);
            writer.WriteLine(tbQuelltext.Text);
            writer.Close();
        }

        private void QuelltextKompilierenUndAusfuehren()
        {
            compiler.CompileSource(tbQuelltext.Text, Path.GetTempPath() + DateTime.Now.Ticks + "-MeineKlasse.exe");
            compiler.CheckCompilerResultsAndRun(this);
        }

        public void Ausfuehren()
        {
            BewachterProzess prozess = new BewachterProzess(compiler.GetPathToAssembly());
            setTextBoxTextThreadSafe(tbAusgabe, prozess.Start(this));
        }

        private void HauptfensterProxy_Shown(object sender, EventArgs e)
        {
            tbQuelltext.Focus();

            // Startet den Compilerthread nachdem das Fenster angezeigt wird.
            // Er läuft, solange das Fesnter sichtbar ist.
            ThreadStart tsKompilieren = new ThreadStart(thread_KompilierenUndAusführen);
            Thread thread = new Thread(tsKompilieren);
            thread.Start();
            Trace.TraceInformation("starting compilation thread.");
        }

        public void FehlerAnzeigen(string sFehler)
        {
            setTextBoxTextThreadSafe(tbFehler, sFehler);
            setTextBoxBackColorThreadSafe(tbQuelltext, settings.errorColor);
        }

        public void KeineCompilerfehlerAnzeigen()
        {
            setTextBoxTextThreadSafe(tbFehler, "");
            setTextBoxBackColorThreadSafe(tbQuelltext, settings.okColor);
        }

        /// <summary>
        /// Setzt den Text einer Textbox. Die Methode kann aus einem anderen Thread 
        /// aufgerufen werden und übergibt die Anweisung dem GUI-Thread.
        /// </summary>        
        private void setTextBoxTextThreadSafe(TextBox tb, string text)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                tb.Text = text;
            });
        }

        /// <summary>
        /// Setzt die Hintegrundfarbe einer Textbox. Die Methode kann aus einem anderen Thread 
        /// aufgerufen werden und übergibt die Anweisung dem GUI-Thread.
        /// </summary>        
        private void setTextBoxBackColorThreadSafe(TextBox tb, Color color)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                tb.BackColor = color;
            });
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
