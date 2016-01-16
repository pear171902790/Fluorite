using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fluorite.MobileSite.Data;
using NUnit.Framework;

namespace Fluorite.MobileSite.UnitTest
{
    [TestFixture]
    public class TestEF
    {
        [Test]
        public void ReadData()
        {
            var a = new DB().Articles.SingleOrDefault(x => x.Id == new Guid("2BE80030-EB7D-41BE-BFE9-BA49699C9EA6"));
            var s = a.Seller;
            var b = 0;
        }
    }
}