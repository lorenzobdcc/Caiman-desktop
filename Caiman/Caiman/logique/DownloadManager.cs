/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Class used to manage all the download
 */
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
        private const string PATH_TO_CAIMAN_FOLDER_GAMES = @"C:\Caiman\";
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

        /// <summary>
        /// Delete a game form the disk of the user
        /// </summary>
        /// <param name="idGame"></param>
        public void DeleteGame(int idGame)
        {
            string path = PATH_TO_CAIMAN_FOLDER_GAMES + callAPI.CallFolderNameGame(idGame) + @"\";
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
        /// <summary>
        /// Check if file is in use
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Start the download
        /// </summary>
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
        /// <summary>
        /// Move the started download to the finished list and start the next download
        /// </summary>
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
        /// <summary>
        /// Create a download and add it to the download list
        /// </summary>
        /// <param name="idGame"></param>
        /// <param name="apiKey"></param>
        public void CreateDownload(int idGame, string apiKey)
        {
            lst_download.Add(new Download(PATH_TO_CAIMAN_FOLDER_GAMES + callAPI.CallFolderNameGame(idGame)+@"\", idGame, apiKey, callAPI.CallFileNameGame(idGame),this));
        }

        /// <summary>
        /// Check if a download is alredy active
        /// </summary>
        /// <param name="idGame"></param>
        /// <returns></returns>
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
