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
                labMessage.Text = "�п�J�ݭn���X���X���ƶq.";
            }
        }

        private void GeneratePhoneNumbers(int numberOfPhoneNumbers) 
        {
            btnRunGenerate.Enabled = false;
            labMessage.Text = "�еy��...���X��";
            //���Y
            string prefixFilePath = "���X���Y.txt";

            //�������X
            string recordFilePath = "�w�ͦ����X�O����.txt";
            
            //�ɶ��W
            string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            //Excel�W��
            string excelFilePath = $"�ͦ����X_{timeStamp}.xlsx";

            //Ū�����Y���X
            var prefixes = LoadPrefixes(prefixFilePath);

            //Ū�����g���X�����X
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
                        labMessage.Text = "�L�k���X��h���X.";
                        return;
                    }

                }
                workbook.SaveAs(excelFilePath);
                labMessage.Text = $"�Ҧ����X���X�w�g�x�s�� {excelFilePath}.";
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
                // �Ыعw�]�e���ɮ�
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