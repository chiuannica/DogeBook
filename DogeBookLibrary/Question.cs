using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogeBookLibrary
{
    public class Question
    {
        public int SecurityQuestionId { get; set; }
        public int UserId { get; set; }
        public String SecurityQuestion { get; set; }
        public String Answer { get; set; }

        public Question() { }

    }
}
