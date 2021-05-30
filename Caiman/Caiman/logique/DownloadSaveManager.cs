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




        public void StartDownload()
        {
            foreach (DownloadSave downloadSave in lst_download)
            {
                downloadSave.StartDownload();
            }

        }


        public void CreateDownload(int idEmulator,int idUser, string apiKey)
        {

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var savePath = "";


            switch (idEmulator)
            {
                case 1:
                     savePath = Path.Combine(appDataPath, @"Caiman\users\" + user.username + @"\Save\");
                    break;
                case 2:
                    savePath = Path.Combine(appDataPath, @"Caiman\users\" + user.username + @"\Save\");
                    break;
                default:
                    break;
            }
            if (lst_download == null)
            {
                lst_download = new List<DownloadSave>();
            }
            lst_download.Add(new DownloadSave(savePath, idEmulator,user.id, apiKey,user.username,this));
        }


    }
}
