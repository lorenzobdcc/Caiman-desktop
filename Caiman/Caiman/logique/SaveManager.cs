using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.logique
{
    class SaveManager
    {
        public List<FileInfo> lst_save = new List<FileInfo>();
        public List<String> lst_saveTimeOld = new List<string>();
        public List<String> lst_saveTimeNow = new List<string>();
        public string savePath;
        public SaveManager(string savePathp)
        { 
            savePath = savePathp;
        }
        public SaveManager()
        {

        }

        public void ScanFolder()
        {
            lst_save.Clear();
            DirectoryInfo d = new DirectoryInfo(savePath);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles(); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {
                lst_save.Add(file);
            }
            ScanDateFile();
        }

        private void ScanDateFile()
        {
            int counter = 0;
            lst_saveTimeOld.Clear();
            lst_saveTimeOld.AddRange(lst_saveTimeNow);
            lst_saveTimeNow.Clear();
            
            foreach (FileInfo save in lst_save)
            {
                try
                {
                    save.Refresh();
                    //lst_saveTimeNow.Add(GetMd5File(save));
                    lst_saveTimeNow.Add(save.LastWriteTime.Ticks.ToString());
                        if (lst_saveTimeOld[counter] != lst_saveTimeNow[counter])
                        {
                            if (lst_saveTimeOld[counter] != "")
                            {
                                string lol = "lll";
                            }

                        }
                        counter++;
                    
                }
                catch
                {
                    
                }
                
            }
        }
        private string GetMd5File(FileInfo file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file.FullName))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

    }
}
