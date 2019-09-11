﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDbCassandra
{
    class User
    {
        public int user_id { get; set; }
        public String user_name { get; set; }
        public String user_bcity { get; set; }

        public String test { get; set; }

        public User(int user_id, String user_name, String user_bcity, string test = null)
        {
            this.user_id = user_id;
            this.user_name = user_name;
            this.user_bcity = user_bcity;
            this.test = test;
        }

        public override String ToString()
        {
            return String.Format(" {0} | {1} | {2} ", user_id, user_name, user_bcity);
        }

    }
}
