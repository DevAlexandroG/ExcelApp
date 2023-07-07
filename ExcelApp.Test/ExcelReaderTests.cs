using ExcelApp.Data;
using ExcelApp.Interfaces;
using ExcelApp.Services;
using ExcelApp.Test;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace ExcelApp.Tests
{
    [TestFixture]
    public class ExcelReaderTests
    {
        private ExcelReader _excelReader;
        private MockExcelDataFilter _mockExcelDataFilter;
        private List<ProductInfo> _expectedItems = new();

        [SetUp]
        public void SetUp()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _mockExcelDataFilter = new MockExcelDataFilter();
            _excelReader = new ExcelReader(new ProductInfoFactory());
            _expectedItems = TestDataUtility.CreateExpectedItems();
        }

        [Test]
        public async Task ReadExcelFileAsync_ValidFile_ReturnsCorrectItems()
        {
            var file = CreateTestFile(_expectedItems);
            var result = await _excelReader.ReadExcelFileAsync(file, _mockExcelDataFilter);
            Assert.That(_expectedItems.OrderBy(x => x.Id), Is.EquivalentTo(result.OrderBy(x => x.Id))
                .Using(new MyItemEqualityComparer()));
        }

        [Test]
        public async Task ReadExcelFileAsync_ValidFile_ReturnsUnCorrectItems()
        {
            var file = CreateTestFile(_expectedItems);
            var result = await _excelReader.ReadExcelFileAsync(file, _mockExcelDataFilter);
            _expectedItems[0].Cipher = "failed";
            Assert.That(_expectedItems.OrderBy(x => x.Id), !Is.EquivalentTo(result.OrderBy(x => x.Id))
                .Using(new MyItemEqualityComparer()));
        }

        private IFormFile CreateTestFile(List<ProductInfo> exceptedItems)
        {
            var fileStream = new MemoryStream();
            using (var package = new ExcelPackage(fileStream))
            {
                ExcelWorksheet? worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "Name";
                worksheet.Cells["C1"].Value = "Cipher";
                worksheet.Cells["D1"].Value = "EffectiveDateFrom";
                worksheet.Cells["E1"].Value = "EffectiveDateBy";

                SetData(exceptedItems, worksheet);
                package.Save();
            }


            var content = "Fake File";
            var fileName = "test.xlsx";
            var writer = new StreamWriter(fileStream);
            writer.Write(content);
            writer.Flush();
            fileStream.Position = 0;
            IFormFile file = new FormFile(fileStream, 0, fileStream.Length, "id_from_form", fileName);

            return file;
        }

        private void SetData(List<ProductInfo> myItems, ExcelWorksheet worksheet)
        {
            int cell = 2;
            foreach (var item in myItems)
            {
                worksheet.Cells[$"A{cell}"].Value = item.Id;
                worksheet.Cells[$"B{cell}"].Value = item.Name;
                worksheet.Cells[$"C{cell}"].Value = item.Cipher;
                worksheet.Cells[$"D{cell}"].Value = item.EffectiveDateFrom.ToString();
                worksheet.Cells[$"E{cell}"].Value = item.EffectiveDateBy.ToString();
                cell++;
            }
        }

        private class MockExcelDataFilter : IExcelDataFilter
        {
            public bool IsDataValid(ProductInfo productInfo)
            {
                return true;
            }
        }
    }
}