﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_Database.Factory.Database
{
    public class Seeder
    {
        public static void Initialize()
        {
            RoleSeeder.Seed();
            TestUserSeeder.Seed();
        }
    }
}
