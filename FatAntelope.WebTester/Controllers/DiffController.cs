using FatAntelope.Writers;
using Microsoft.Web.XmlTransform;
using System;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace FatAntelope.WebTester.Controllers
{
	[RequireHttps]
	public class DiffController : Controller
	{
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Diff(string sourceXml, string targetXml)
		{
			try
			{
                var doc1 = new XmlDocument();
                doc1.LoadXml(sourceXml);

                var doc2 = new XmlDocument();
                doc2.LoadXml(targetXml);

                var tree1 = new XTree(doc1);
                var tree2 = new XTree(doc2);
                XDiff.Diff(tree1, tree2);

                var writer = new XdtDiffWriter();
                var patch = writer.GetDiff(tree2);


                return Content(Beautify(patch), "text/xml");
			}
			catch (Exception e)
			{
                return ErrorXml(e.Message);
			}
		}

        public string Beautify(XmlDocument doc)
        {
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = Environment.NewLine,
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }

		private ContentResult ErrorXml(string errorMessage)
		{
			return Content(new XDocument(new XElement("error", errorMessage)).ToString(), "text/xml");
		}
	}
}
