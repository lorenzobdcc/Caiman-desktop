/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to manage the download of the save file
 */
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
        }


        public SaveManager()
        {

        }
        /// <summary>
        /// Scan to fold to get the files in it
        /// </summary>
        public void ScanFolder()
        {
            lst_save.Clear();
            DirectoryInfo d = new DirectoryInfo(savePath);
            FileInfo[] Files = d.GetFiles(); 
            foreach (FileInfo file in Files)
            {
                lst_save.Add(file);
            }
            ScanDateFile();
        }
        /// <summary>
        /// Scan the file to know the last time they have been modified
        /// </summary>
        private void ScanDateFile()
        {
            int counter = 0;
            lst_saveTimeOld.Clear();
            lst_saveTimeOld.AddRange(lst_saveTimeNow);
            lst_saveTimeNow.Clear();
            //if a file has been added or delete sync the folder
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
                            //if the file has benn modified since the last time move or sync the folder
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
        /// <summary>
        /// Move the local file to the appdata folder
        /// </summary>
        /// <param name="save"></param>
        public void MoveFileToUserFolder(FileInfo save)
        {
            File.Copy(save.FullName, Path.Combine(destinationPath, save.Name), true);

        }
        /// <summary>
        /// Move all the appdata file to the emulator folder
        /// </summary>
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
        /// <summary>
        /// Zip the save of the user and send it to the Bunker by the API
        /// </summary>
        public void UploadSave()
        {

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var savePathZipDolpin = "";
            var savePathZipPCSX2 = "";
            var savePath = Path.Combine(appDataPath, @"Caiman\users\" + emulatorsManager.user.username + @"\Save\");

            savePathZipDolpin = Path.Combine(appDataPath, @"Caiman\users\" +  emulatorsManager.user.username+ @"\Save\GamecubeWii\");
            savePathZipPCSX2 = Path.Combine(appDataPath, @"Caiman\users\" + emulatorsManager.user.username + @"\Save\Playstation2\");
            //zip the save of the emulators
            ZipFile.CreateFromDirectory(savePathZipPCSX2, savePath + "tempPCSX2.zip");
            ZipFile.CreateFromDirectory(savePathZipDolpin, savePath + "tempDolphin.zip");
            //call the api to create a copy of the save to the Bunker
            callAPI.UploadSave(1, emulatorsManager.user.id, emulatorsManager.user.apitoken, savePath + "tempDolphin.zip");
            callAPI.UploadSave(2,emulatorsManager.user.id,emulatorsManager.user.apitoken, savePath + "tempPCSX2.zip");
            //delete the temporary zip file
            File.Delete(savePath + "tempPCSX2.zip");
            File.Delete(savePath + "tempDolphin.zip");
            
        }

        /// <summary>
        /// Count the file in the folder
        /// </summary>
        /// <returns></returns>
        private int CountFileInFolder()
        {
            DirectoryInfo myDir = new System.IO.DirectoryInfo(this.savePath);
            return myDir.GetFiles().Length;
        }

        /// <summary>
        /// Move the file off the appdata folder to the emulators folder
        /// </summary>
        public void MoveSaveFileFromUserFolderToEmulatorSaveFolder()
        {
            DirectoryInfo destinationDir = new DirectoryInfo(destinationPath);
            //before send it to the folder whe need to erase the curent save files
            foreach (FileInfo file in destinationDir.GetFiles())
            {
                file.Delete();
            }

            //copy the file to the emulators save folders
            DirectoryInfo d = new DirectoryInfo(savePath);
            FileInfo[] Files = d.GetFiles();
            foreach (FileInfo file in Files)
            {
                File.Copy(file.FullName, Path.Combine(destinationPath, file.Name), true);
            }
        }

    }
}
