using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Web.Administration;
using Servant.sfor.iis.server;

namespace Servant.sfor.iis.units
{
    [TestClass]
    public class UnitTest1
    {
        IIsServer iisServer = new IIsServer();
        [TestMethod]
        public void SelectAllSite()
        {
            SiteCollection col = iisServer.SelectAllSite();
        }



    }
}
