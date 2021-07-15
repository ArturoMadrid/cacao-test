using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.cacao
{
    public class Student
    {
        static int _base = 5;

        public int id { get; set; }
        public string name { get; set; }
        public int origin_score { get; set; }
        public int final_score { get; set; }

        public int GetNearestMult()
        {
            int next = origin_score + Math.Abs((origin_score % _base) - _base);

            if (origin_score < 38)
                return origin_score;
            else if ((next - origin_score) < 3)
                return next;
            else
                return origin_score;

        }
    }
}
