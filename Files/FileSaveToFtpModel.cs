namespace GenericFileService.Files
{
    public class FileSaveToFtpModel
    {
        public FileSaveToFtpModel(string ftpAddress, string userName, string password)
        {
            FtpAddress = ftpAddress;
            UserName = userName;
            Password = password;
        }
        public string FtpAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
