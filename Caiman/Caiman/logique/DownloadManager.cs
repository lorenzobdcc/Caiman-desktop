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
        CallAPI callAPI = new CallAPI();

        public DownloadManager()
        {

            
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
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
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
            if (lst_download.Count >0)
            {
                lst_download[0].StartDownload();
            }
            lst_activeDownload.Add(lst_download[0]);
            lst_download.RemoveAt(0);
        }

        public void CreateDownload(int idGame, string apiKey)
        {
            lst_download.Add(new Download(@"C:\Caiman\"+callAPI.CallFolderNameGame(idGame)+@"\", idGame, apiKey, callAPI.CallFileNameGame(idGame)));
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
