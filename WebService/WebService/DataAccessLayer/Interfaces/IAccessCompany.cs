﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Model;

namespace WebService.DataAccessLayer.Interfaces
{
    public interface IAccessCompany
    {
        public Company FindByName(string name); 
    }
}