using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Caiman.logique
{
    public class DownloadManager
    {
        List<Download> lst_download = new List<Download>();
        Download activeDownload;

        public DownloadManager()
        {
            
        }

        public int GetPercentageActiveDownload()
        {
            int percentage = 0;
            if (activeDownload != null)
            {
                percentage = activeDownload.percentage;
            }
            return percentage;
        }

        public void StartDownload()
        {
            if (lst_download.Count >0)
            {
                lst_download[0].StartDownload();
            }
        }

        public void CreateDownload(int idGame, string apiKey)
        {
            lst_download.Add(new Download("",idGame, apiKey));
        }
    }
}
