using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Addition
{
    class View : IEnumerable<string>
    {
        private IList<string> lines;

        public string this[int index] => lines[index];

        public View()
        {
            lines = new List<string>();
        }

        public void AddLine(string line) => lines.Add(line);

        public IEnumerator<string> GetEnumerator() => lines.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
