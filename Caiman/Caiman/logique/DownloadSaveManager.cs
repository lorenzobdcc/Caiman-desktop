/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Class used to manage the download of save file
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Caiman.database;
using System.IO;
using Caiman.models;

namespace Caiman.logique
{
    public class DownloadSaveManager
    {
        public List<DownloadSave> lst_download = new List<DownloadSave>();
        private List<Download> lst_allDonwload;
        public User user;

        public List<Download> Lst_allDonwload { get {
                lst_download.Clear();
                lst_download.AddRange(lst_download);
                return lst_allDonwload;
            }   set => lst_allDonwload = value; }

        public DownloadSaveManager(User userp)
        {
            user = userp;
        }
        public DownloadSaveManager()
        {
        }



        /// <summary>
        /// Start the download od the saves 
        /// </summary>
        public void StartDownload()
        {
            foreach (DownloadSave downloadSave in lst_download)
            {
                downloadSave.StartDownload();
            }

        }

        /// <summary>
        /// Create a download and add it to the list
        /// </summary>
        /// <param name="idEmulator"></param>
        /// <param name="idUser"></param>
        /// <param name="apiKey"></param>
        public void CreateDownload(int idEmulator, string apiKey)
        {

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var savePath = Path.Combine(appDataPath, @"Caiman\users\" + user.username + @"\Save\");


            if (lst_download == null)
            {
                lst_download = new List<DownloadSave>();
            }
            lst_download.Add(new DownloadSave(savePath, idEmulator,user.id, apiKey,user.username,this));
        }


    }
}
