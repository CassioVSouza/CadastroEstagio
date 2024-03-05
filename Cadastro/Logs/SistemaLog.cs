namespace Cadastro.Logs
{
    public class SistemaLog : ISistemaLog
    {
        public void EscreverLog(string message) //Função pra gravar possíveis erros em um txt
        {
            using(StreamWriter writer = new StreamWriter(GetLogPath()))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        private string GetLogPath()
        {
            string directory = "Logs";
            string fileName = "Log.txt";
            string filePath = Path.Combine(directory, fileName);

            if(File.Exists(filePath))
            {
                if(Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.Create(filePath).Close();
            }

            return filePath;
        }
    }
}
