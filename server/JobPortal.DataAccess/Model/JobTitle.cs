using JobPortal.DataAccess.Helpers;
using JobPortal.DataAccess.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DataAccess.Model
{
    [Table(name: DbHelpers.JobTitleTableName, Schema = DbHelpers.SchemaName)]
    class JobTitle: NamedModelBase
    {
    }
}
