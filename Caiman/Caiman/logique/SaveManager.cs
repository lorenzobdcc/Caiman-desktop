using Caiman.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.logique
{
    public class SaveManager
    {
        public List<FileInfo> lst_save = new List<FileInfo>();
        public List<String> lst_saveTimeOld = new List<string>();
        public List<String> lst_saveTimeNow = new List<string>();
        public string savePath;
        public bool isLocalFile;
        private int initialCounterFile = 0;
        public string destinationPath;
        public CallAPI callAPI = new CallAPI();
        EmulatorsManager emulatorsManager;
        public SaveManager(string savePathp, string destinationPathp, bool isLocalFilep, EmulatorsManager emulatorsManagerp)
        {
            savePath = savePathp;
            isLocalFile = isLocalFilep;
            destinationPath = destinationPathp;
            emulatorsManager = emulatorsManagerp;
            initialCounterFile = CountFileInFolder();
            if (isLocalFile == false)
            {
               // MoveSaveFileFromUserFolderToEmulatorSaveFolder();
            }
        }


        public SaveManager()
        {

        }

        public void ScanFolder()
        {
            lst_save.Clear();
            DirectoryInfo d = new DirectoryInfo(savePath);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles(); //Getting Text files
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
            if (initialCounterFile != CountFileInFolder())
            {
                if (isLocalFile)
                {
                    initialCounterFile = CountFileInFolder();
                    MoveAllFileToUserFolder();
                }
            }
            foreach (FileInfo save in lst_save)
            {

                save.Refresh();

                lst_saveTimeNow.Add(save.LastWriteTime.Ticks.ToString());
                if (lst_saveTimeOld.Count() > 0)
                {
                    try
                    {
                        if (lst_saveTimeOld[counter] != lst_saveTimeNow[counter])
                        {
                            if (lst_saveTimeOld[counter] != "")
                            {
                                if (isLocalFile)
                                {
                                    MoveFileToUserFolder(save);
                                }
                                else
                                {
                                    UploadSave();
                                }
                            }

                        }
                        counter++;
                    }
                    catch { }
                }


            }
        }
        public void MoveFileToUserFolder(FileInfo save)
        {
            File.Copy(save.FullName, Path.Combine(destinationPath, save.Name), true);

        }
        public void MoveAllFileToUserFolder()
        {
            DirectoryInfo d = new DirectoryInfo(savePath);
            FileInfo[] Files = d.GetFiles(); 
            foreach (FileInfo file in Files)
            {
                try
                {

                    File.Copy(file.FullName, Path.Combine(destinationPath, file.Name), true);
                }
                catch (Exception)
                {
                }
            }
        }
        public void UploadSave()
        {

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var savePathZipDolpin = "";
            var savePathZipPCSX2 = "";
            var savePath = Path.Combine(appDataPath, @"Caiman\users\" + emulatorsManager.user.username + @"\Save\");

            savePathZipDolpin = Path.Combine(appDataPath, @"Caiman\users\" +  emulatorsManager.user.username+ @"\Save\GamecubeWii\");
            savePathZipPCSX2 = Path.Combine(appDataPath, @"Caiman\users\" + emulatorsManager.user.username + @"\Save\Playstation2\");

            ZipFile.CreateFromDirectory(savePathZipPCSX2, savePath + "tempPCSX2.zip");
            ZipFile.CreateFromDirectory(savePathZipDolpin, savePath + "tempDolphin.zip");

            callAPI.UploadSave(1, emulatorsManager.user.id, emulatorsManager.user.apitoken, savePath + "tempDolphin.zip");
            callAPI.UploadSave(2,emulatorsManager.user.id,emulatorsManager.user.apitoken, savePath + "tempPCSX2.zip");

            File.Delete(savePath + "tempPCSX2.zip");
            File.Delete(savePath + "tempDolphin.zip");
            
        }

        private int CountFileInFolder()
        {
            DirectoryInfo myDir = new System.IO.DirectoryInfo(this.savePath);
            return myDir.GetFiles().Length;
        }

        public void MoveSaveFileFromUserFolderToEmulatorSaveFolder()
        {
            DirectoryInfo destinationDir = new DirectoryInfo(destinationPath);

            foreach (FileInfo file in destinationDir.GetFiles())
            {
                file.Delete();
            }

            DirectoryInfo d = new DirectoryInfo(savePath);
            FileInfo[] Files = d.GetFiles();
            foreach (FileInfo file in Files)
            {
                File.Copy(file.FullName, Path.Combine(destinationPath, file.Name), true);
            }
        }

    }
}
