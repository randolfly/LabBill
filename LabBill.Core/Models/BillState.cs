using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBill.Core.Models;

/// <summary>
/// The state of bill, is it finished
/// Note that only Reimbursable bills can be NotFinished
/// The default type of NonReimbursable is Finished
/// </summary>
public enum BillState
{
    Finished,
    NotFinished
}