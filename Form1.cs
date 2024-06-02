using ClosedXML.Excel;

namespace PhoneNumberGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRunGenerate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtNumberCount.Text, out int numberOfPhoneNumbers))
            {
                GeneratePhoneNumbers(numberOfPhoneNumbers);
            }
            else
            {
                labMessage.Text = "請輸入需要產出號碼之數量.";
            }
        }

        private void GeneratePhoneNumbers(int numberOfPhoneNumbers) 
        {
            btnRunGenerate.Enabled = false;
            labMessage.Text = "請稍後...產出中";
            //標頭
            string prefixFilePath = "號碼標頭.txt";

            //紀錄號碼
            string recordFilePath = "已生成號碼記錄檔.txt";
            
            //時間戳
            string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            //Excel名稱
            string excelFilePath = $"生成號碼_{timeStamp}.xlsx";

            //讀取標頭號碼
            var prefixes = LoadPrefixes(prefixFilePath);

            //讀取曾經產出之號碼
            var generatedNumbers = LoadGeneratedNumbers(recordFilePath);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("PhoneNumbers");

                int rowIndex = 1;
                int batchSize = 1000;
                while (numberOfPhoneNumbers > 0)
                {
                    int currentBatchSize = Math.Min(batchSize, numberOfPhoneNumbers);
                    List<string> newNumbers;

                    if (chkSequential.Checked)
                    {
                        newNumbers = GenerateSequentialPhoneNumbers(prefixes, generatedNumbers, currentBatchSize);
                    }
                    else
                    {
                        newNumbers = GenerateUniquePhoneNumbers(prefixes, generatedNumbers, currentBatchSize);
                    }
                    if (newNumbers.Count > 0)
                    {
                        SaveGeneratedNumbers(recordFilePath, newNumbers);
                        WriteNumbersToWorksheet(worksheet, newNumbers, ref rowIndex);
                        numberOfPhoneNumbers -= newNumbers.Count;
                    }
                    else
                    {
                        labMessage.Text = "無法產出更多號碼.";
                        return;
                    }

                }
                workbook.SaveAs(excelFilePath);
                labMessage.Text = $"所有產出號碼已經儲存至 {excelFilePath}.";
                btnRunGenerate.Enabled = true;
            }
        }
        private List<string> GenerateSequentialPhoneNumbers(List<string> prefixes, HashSet<string> generatedNumbers, int count)
        {
            List<string> newNumbers = new List<string>();
            int prefixIndex = 0;

            while (newNumbers.Count < count)
            {
                string prefix = prefixes[prefixIndex];
                prefixIndex = (prefixIndex + 1) % prefixes.Count;
                string suffix = new Random().Next(0, 1000000).ToString("D6");
                string newNumber = prefix + suffix;

                if (!generatedNumbers.Contains(newNumber))
                {
                    generatedNumbers.Add(newNumber);
                    newNumbers.Add(newNumber);
                }
            }

            return newNumbers;
        }
        private void WriteNumbersToWorksheet(IXLWorksheet worksheet, List<string> phoneNumbers, ref int rowIndex)
        {
            foreach (var number in phoneNumbers)
            {
                worksheet.Cell(rowIndex, 1).Value = number;
                rowIndex++;
            }
        }
        private void SaveGeneratedNumbers(string filePath, List<string> numbers)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                foreach (var number in numbers)
                {
                    writer.WriteLine(number);
                }
            }
        }
        private List<string> GenerateUniquePhoneNumbers(List<string> prefixes, HashSet<string> generatedNumbers, int count)
        {
            Random random = new Random();
            const int maxAttempts = 100000;
            List<string> newNumbers = new List<string>();

            for (int attempt = 0; attempt < maxAttempts && newNumbers.Count < count; attempt++)
            {
                string prefix = prefixes[random.Next(prefixes.Count)];
                string suffix = random.Next(0, 1000000).ToString("D6");
                string newNumber = prefix + suffix;

                if (!generatedNumbers.Contains(newNumber))
                {
                    generatedNumbers.Add(newNumber);
                    newNumbers.Add(newNumber);
                }
            }

            return newNumbers;
        }
        private List<string> LoadPrefixes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                // 創建預設前綴檔案
                var defaultPrefixes = new List<string>
        {
            "0900",
            "0911",
            "0922",
            "0933",
            "0944",
            "0955",
            "0966",
            "0977",
            "0988",
            "0999"
        };

                File.WriteAllLines(filePath, defaultPrefixes);
                return defaultPrefixes;
            }

            var prefixes = File.ReadAllLines(filePath)
                               .Select(line => line.Trim())
                               .Where(line => !string.IsNullOrEmpty(line))
                               .ToList();

            return prefixes;
        }
        private HashSet<string> LoadGeneratedNumbers(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new HashSet<string>();
            }

            var numbers = File.ReadAllLines(filePath)
                              .Select(line => line.Trim())
                              .Where(line => !string.IsNullOrEmpty(line))
                              .ToHashSet();

            return numbers;
        }
    }
}