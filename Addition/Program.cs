using System;

namespace Addition
{
    class Program
    {
        static void Main(string[] args)
        {
            Renderer renderer = new Renderer();
            ViewBuilder builder = new ViewBuilder();

            if (args.Length < 2)
            {
                Console.WriteLine("Not enough arguments");

                return;
            }

            var first = double.Parse(args[0]);
            var second = double.Parse(args[1]);

            var model = new Model(first, second);
            var view = builder.BuildViewByModel(model);

            renderer.RenderView(view);
        }
    }
}
