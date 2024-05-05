using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMergeUtility
{
    internal class PdfMergeService
    {
        public void Merge(FileInfo front, FileInfo back, FileInfo? targetFile)
        {
            using (PdfDocument pdfFront = PdfReader.Open(front.FullName, PdfDocumentOpenMode.Import))
            using (PdfDocument pdfBack = PdfReader.Open(back.FullName, PdfDocumentOpenMode.Import))
            using (PdfDocument outPdf = new PdfDocument())
            {
                var pageCounts = pdfFront.PageCount + pdfBack.PageCount;

                if (pdfFront.PageCount != pdfBack.PageCount)
                    throw new InvalidOperationException($"Failed to merge pdfs from {front.Name} and {back.Name}. Page counts must match.");

                for (int i = 0; i < pageCounts; i++)
                {
                    if (i % 2 == 0)
                    {
                        // append front (take from front)
                        AppendPage(pdfFront, i / 2, outPdf);
                    }
                    else
                    {
                        // append back (take from back)
                        AppendPage(pdfBack, i / 2, outPdf);

                    }
                }

                if (targetFile is null)
                {
                    outPdf.Save(Path.Combine(front.Directory.FullName, front.Name.Replace(front.Extension, "-new.pdf")));
                } else
                {
                    outPdf.Save(Path.Combine(targetFile.Directory.FullName, targetFile.Name.Replace(targetFile.Extension, ".pdf")));
                }
            }

            //front.Delete();
            //back.Delete();
        }

        private void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }

        private void AppendPage(PdfDocument source, int pageNumber, PdfDocument target)
        {
            target.AddPage(source.Pages[pageNumber]);
        }
    }
}
