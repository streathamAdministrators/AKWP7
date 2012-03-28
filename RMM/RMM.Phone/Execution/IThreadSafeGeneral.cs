using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Phone.Execution
{
  public interface IThreadSafeGeneral
    {
      void ExecuteSafeDispatcher(params Action[] actionsAExecuter);
    }
}
