using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Data
{
    public class RmmConfiguration
    {
        public static void Initialize()
        {
            using(RmmDataContext datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
            {
                if (datacontext.DatabaseExists() == false)
                {
                    datacontext.CreateDatabase();
                    datacontext.SubmitChanges();
                }
            }
        }
    }
}
