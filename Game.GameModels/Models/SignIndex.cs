using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameModels.Models
{
    public class SignIndex
    {
        public int i { get; set; }

        public int j { get; set; }

        public SignEnum sign { get; set; }

        public SignIndex() { }

        public SignIndex(int i, int j, SignEnum signEnum)
        {
            this.i = i;
            this.j = j;
            sign = signEnum;
        }
    }
}
