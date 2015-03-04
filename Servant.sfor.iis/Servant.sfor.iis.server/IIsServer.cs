using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Microsoft.Web.Administration;

namespace Servant.sfor.iis.server
{
    public class IIsServer
    {
        ServerManager iisManager = null;
        public IIsServer()
        {
            iisManager = ServerManager.OpenRemote("192.168.7.60");
        }
        public SiteCollection SelectAllSite()
        {
            //ServerManager iisManager = new ServerManager();
            //ServiceController sc = new ServiceController("iisadmin");
            SiteCollection sitecol = iisManager.Sites;
            return sitecol;
        }

        public bool CreateSite(string sitename, string physicalPaht, int port)
        {
            Site site = iisManager.Sites.Add(sitename, physicalPaht, port);
            site.ServerAutoStart = true;
            iisManager.CommitChanges();
            return true;
        }


    }
}
