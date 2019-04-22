﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniRestaurant.IO
{
    public class Writer : IWriter
    {
        private StringBuilder sb;

        public Writer()
        {
            this.sb = new StringBuilder();
        }

        public void Flush()
        {
            Console.WriteLine(sb.ToString().Trim());
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
