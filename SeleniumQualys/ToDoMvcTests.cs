using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumQualys
{
    [TestFixture]
    public class ToDoMvcTests
    {
        [Test]
        public void WithQualys()
        {
            var qualys = new QualysWrapper(DriverFactory.CreateChromeWithQualys(DriverFactory.DefaultChromeOptions.AddDownloadPath(AppDomain.CurrentDomain.BaseDirectory)));
            qualys.OpenExtension();
            qualys.StartCapture("test1");
            // do something here
            qualys.StopCapture("test1_recording");
        }
    }
}
