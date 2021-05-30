using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Caiman.database;
using System.IO;

namespace Caiman.logique
{
    public class DownloadManager
    {
        public List<Download> lst_download = new List<Download>();
        public List<Download> lst_activeDownload = new List<Download>();
        public List<Download> lst_finishDownload = new List<Download>();
        private List<Download> lst_allDonwload;
        CallAPI callAPI = new CallAPI();
        public EmulatorsManager emulatorsManager;

        public List<Download> Lst_allDonwload { get {
                lst_download.Clear();
                lst_download.AddRange(lst_download);
                lst_download.AddRange(lst_activeDownload);
                lst_download.AddRange(lst_finishDownload);
                return lst_allDonwload;
            }   set => lst_allDonwload = value; }

        public DownloadManager(EmulatorsManager emulatorsManagerp)
        {
            emulatorsManager = emulatorsManagerp;
        }
        public void DeleteGame(int idGame)
        {
            string path = @"C:\Caiman\" + callAPI.CallFolderNameGame(idGame) + @"\";
            string filename = callAPI.CallFileNameGame(idGame);
            if (!IsFileinUse(new FileInfo(path + filename)))
            {
                if (File.Exists(path + filename))
                {

                    File.Delete(path + filename);
                    emulatorsManager.gamesListConfigFile.DeleteValue(idGame.ToString());
                }
            }

        }

        private bool IsFileinUse(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {

                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }

        public void StartDownload()
        {
            if (lst_activeDownload.Count > 0)
            {
                lst_activeDownload[0].StartDownload();
            }
            else
            {
                NextDownload();
            }

        }
        public void NextDownload()
        {
            if (lst_activeDownload.Count >0)
            {
                lst_finishDownload.Add(lst_activeDownload[0]);
                lst_activeDownload.RemoveAt(0);
            }

            if (lst_download.Count() == 1)
            {
                lst_activeDownload.Add(lst_download[0]);
                lst_download.RemoveAt(0);
                StartDownload();
            }
            
            
        }

        public void CreateDownload(int idGame, string apiKey)
        {
            lst_download.Add(new Download(@"C:\Caiman\"+callAPI.CallFolderNameGame(idGame)+@"\", idGame, apiKey, callAPI.CallFileNameGame(idGame),this));
        }

        public bool CheckIfDownloadIsActive(int idGame)
        {
            bool isActive = false;
            foreach (var download in lst_activeDownload)
            {
                if (download.idGame == idGame)
                {
                    if (download.percentage != 100)
                    {
                        isActive = true;
                    }
                }
            }
            return isActive;
        }
    }
}
