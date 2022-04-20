using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yup.MoneyExchange.Domain.AggregatesModel.Base;

public abstract class AuditoriaEntity
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool EsEliminado { get; set; }

    public void SetCreateAudit(DateTime registredDate, Guid registredBy)
    {
        this.CreatedDate = registredDate;
        this.CreatedBy = registredBy;
    }

    public void SetUpdateAudit(DateTime registredDate, Guid registredBy)
    {
        this.UpdatedDate = registredDate;
        this.UpdatedBy = registredBy;
    }
}
