using Microsoft.Win32;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Security.Cryptography.Pkcs;
using System.Windows.Controls;
using System.Windows;

namespace Final_Project
{
    public class ExportToPDF
    {
        public static void exportToPDF(List<Resume> resumes)
        {
            //Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Test PDF Document";

            //Create an empty page
            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.Letter;

            //Get an XGraphics Object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //Create a font
            XFont fontTitle = new XFont("Verdana", 22, XFontStyleEx.BoldItalic);

            XFont fontSubtitle = new XFont("Times New Roman", 18, XFontStyleEx.Bold);

            XFont fontBody = new XFont("Arial", 12, XFontStyleEx.Regular);

            //Create a text formatter
            XTextFormatter tf = new XTextFormatter(gfx);

            //Color the page
            XRect rect = new XRect(0, 0, page.Width, page.Height);
            gfx.DrawRectangle(XBrushes.BlanchedAlmond, rect);

            //Title
            rect = new XRect(0, 10, page.Width - 20, 50);
            tf.Alignment = XParagraphAlignment.Center;
            string title = "RESUMES LIST";
            tf.DrawString(title, fontTitle, XBrushes.Red, rect);

            //Add texts
            string text = "";
            foreach (Resume resume in resumes)
            {
                text += String.Format("{0}\t {1}\t {2} \t, {3}\t, {4} year-old\nContact Information: {5}\nWork Experience: {6}\nEducation: {7}\nHobbies: {8}\nReferences: {9}",
                resume.Id, resume.FirstName, resume.LastName, resume.Gender, resume.Age, resume.ContactInfo, resume.Experience, resume.Education, resume.Hobbies, resume.References);
            }
            rect = new XRect(10, 220, page.Width - 20, 220);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(text, fontBody, XBrushes.Black, rect,
            XStringFormats.TopLeft);
            const string filename = "Resumes.pdf";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files(*.pdf)|*.pdf|All files(*.*)|*.*";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath
                (Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Title = filename;
            saveFileDialog.OverwritePrompt = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                document.Save(saveFileDialog.FileName);
            }
        }
    }
}
