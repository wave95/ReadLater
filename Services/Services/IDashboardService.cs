﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IDashboardService
    {
        public DashboardModel GetDashboardData();
    }
}
