using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Diagnostics;
using System.IO;

namespace PDFMerge
{
    public class Program
    {
        //public static readonly string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        public static readonly string inputFolder = Path.Combine(path, "InputFolder");
        public static readonly string outputFolder = Path.Combine(path, "OutputFolder");
        static void Main(string[] args)
        {
            string[] filePaths = Directory.GetFiles(inputFolder, "*.pdf",
                                         SearchOption.TopDirectoryOnly);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (PdfDocument output = new PdfDocument())
            {
                foreach (var fileName in filePaths)
                {
                    using (PdfDocument input = PdfReader.Open(fileName, PdfDocumentOpenMode.Import))
                    {
                        CopyPages(input, output);
                    }
                }

                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                output.Save(outputFolder + "\\output.pdf");
            }
        }

        static void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }
    }
}
