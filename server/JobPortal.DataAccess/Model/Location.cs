﻿using JobPortal.DataAccess.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DataAccess.Model
{
    [Table(name: DbHelpers.LocationTableName, Schema = DbHelpers.SchemaName)]
    class Location: ModelBase
    {
        public string Address { get; set; }
    }
}
