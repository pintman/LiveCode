using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.IO;

namespace LiveCodeKonsole
{
    class BewachterProzess
    {
        Process process;

        public BewachterProzess(string sDateiname)
        {
            process = new Process();
            process.StartInfo.FileName = sDateiname;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
        }

        public string Start(IProzessreaktion prozessreaktion)
        {
            string sAusgabe = "";

            using (process)
            {
                process.Start();

                process.WaitForExit(1000);
                if (process.HasExited)
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        sAusgabe = reader.ReadToEnd();
                    }
                    prozessreaktion.ProzessErfolgreich();
                }
                else
                {
                    process.Kill();
                    process.WaitForExit(1000);
                    prozessreaktion.ProzessFehlgeschlagen("Endlosschleife");
                }

                KompilatLöschen();
                return sAusgabe;
            }
        }

        void KompilatLöschen()
        {
            try
            {
                File.Delete(process.StartInfo.FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Datei konnte nicht gelöscht werden. " + ex.Message);
            }
        }
    }

    public interface IProzessreaktion
    {
        void ProzessErfolgreich();
        void ProzessFehlgeschlagen(string sUrsache);
    }
}
