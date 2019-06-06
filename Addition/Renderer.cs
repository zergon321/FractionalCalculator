using System;
using System.Collections.Generic;
using System.Text;

namespace Addition
{
    class Renderer
    {
        public void RenderView(View view) => Console.WriteLine(String.Join('\n', view));
    }
}
