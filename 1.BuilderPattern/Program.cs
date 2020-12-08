using System;
using System.Collections.Generic;
using System.Text;

namespace _1.BuilderPattern
{
    //Create a product class - HTMLElement
    public class HTMLElement
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public List<HTMLElement> Elements = new List<HTMLElement>();
    }

    public class HTMLBuilder
    {
        private const int indentSize = 2;
        private HTMLElement rootElement;

        public HTMLBuilder(string rootName)
        {
            rootElement = new HTMLElement();
            rootElement.Name = rootName;
        }

        public HTMLBuilder AddChild(string name, string text)
        {
            rootElement.Elements.Add(new HTMLElement()
            {
                Name = name,
                Text = text
            });

            return this;
        }

        public string ToStringImpl(HTMLElement root, int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            if(!string.IsNullOrEmpty(root.Name))
                sb.AppendLine($"{i}<{root.Name}>");

            if (!string.IsNullOrEmpty(root.Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(root.Text);
            }

            foreach (var e in root.Elements)
            {
                sb.Append(ToStringImpl(e, string.IsNullOrEmpty(root.Name) ? indent : indent + 1));
            }

            if (!string.IsNullOrEmpty(root.Name))
                sb.AppendLine($"{i}</{root.Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(rootElement, 0);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //Without a Builder
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<html><p>");
            sb.Append(hello);
            sb.Append("</p></html>");
            Console.WriteLine(sb);
            sb.Clear();

            var words = new[] { "hello", "world" };
            sb.Append("<html><ul>");
            foreach(var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul></html>");
            Console.WriteLine(sb);

            //With an HTML Builder
            var pBuilder = new HTMLBuilder("");
            pBuilder.AddChild("p", "hello world");
            Console.WriteLine(pBuilder.ToString());

            var ulBuilder = new HTMLBuilder("ul");
            ulBuilder.AddChild("li", "hello").AddChild("li", "world");
            Console.WriteLine(ulBuilder.ToString());
        }
    }
}
