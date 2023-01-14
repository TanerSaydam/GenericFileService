# Dependency

This library was created by .Net 7.0

## Install

```bash
dotnet add package GenericFileService --version 1.0.0
```

## Usage
Parameters
```Cshar


```

If you want to save file in server
```Csharp
string filePath = "./Files/";

string fileName = FileService.FileSaveToServer(file,filePath);
```

If you want to save file in Ftp
```Csharp
FileSaveToFtpModel fileSaveToFtpModel = new("ftp.ftpadresiniz.com","userName", "password");

string fileName = FileService.FileSaveToFtp(file,fileSaveToFtpModel);
```

If you want to save file in Database (byte[])
```Csharp
byte[] fileByeArray = FileService.FileConvertByteArrayToDatabase(file);
```

If you want to delete file in server
```Csharp
string path = "./Files/" + "FileName";

FileService.FileDeleteToServer(path);
```

If you want to delete file in ftp
```Csharp
FileSaveToFtpModel fileSaveToFtpModel = new("ftp.ftpadresiniz.com","userName", "password");
string path = "./Files/" + "FileName";

FileService.FileDeleteToFtp(path,fileSaveToFtpModel);
```

## Method and Class
```Csharp
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
```


```Csharp
public static class FileService
    {
        public static string FileSaveToServer(IFormFile file, string filePath)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;
            string path = filePath + fileName;
            using (var stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public static string FileSaveToFtp(IFormFile file, FileSaveToFtpModel fileSaveToFtpModel)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(
                $"{fileSaveToFtpModel.FtpAddress}{fileName}");
            request.Credentials = new NetworkCredential(
                fileSaveToFtpModel.UserName,
                fileSaveToFtpModel.Password);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream ftpStream = request.GetRequestStream())
            {
                file.CopyTo(ftpStream);
            }

            return fileName;
        }

        public static byte[] FileConvertByteArrayToDatabase(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                string fileString = Convert.ToBase64String(fileBytes);
                return fileBytes;
            }
        }

        public static void FileDeleteToServer(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception)
            {
            }
        }

        public static void FileDeleteToFtp(string path, FileSaveToFtpModel fileSaveToFtpModel)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(
                    $"{fileSaveToFtpModel}{path}");
                request.Credentials = new NetworkCredential(
                    fileSaveToFtpModel.UserName,
                    fileSaveToFtpModel.Password);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
            }
        }
    }
```
