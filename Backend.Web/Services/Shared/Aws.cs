using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.SimpleEmail.Model;
using OfficeOpenXml;
using MimeKit;
using Backend.Application.DataTransferObjects.Shared;


namespace Backend.Web.Services.Shared
{
    public class Aws
    {
        public BodyBuilder GetMessageBody(string html, string filename, dynamic content)
        {
            var body = new BodyBuilder()
            {
                HtmlBody = html,
                TextBody = "",
            };
            body.Attachments.Add(filename, (byte[])content);
            return body;
        }

        public MimeMessage GetMessage(string from, string to, string subject, string html, string filename, dynamic content)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Backend by ILI.DIGITAL", from));
            message.To.Add(new MailboxAddress(string.Empty, to));
            message.Subject = subject;
            message.Body = GetMessageBody(html, filename, content).ToMessageBody();
            return message;
        }

        public MemoryStream GetMessageStream(string from, string to, string subject, string html, string filename, dynamic content)
        {
            var stream = new MemoryStream();
            GetMessage(from, to, subject, html, filename, content).WriteTo(stream);
            return stream;
        }
        public async Task<bool> SendEmail(string from, string to, string subject, string html)
        {
            try
            {
                //using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1))
                //"AKIA22TKTPNXLFQJQQW2", "T8Crs4UUcidVtvfkgaVARNIrSkT51H1/xgWeTYz4",
                using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.EUCentral1))
                {
                    var emailRequest = new SendEmailRequest()
                    {
                        Source = from,
                        Destination = new Destination(),
                        Message = new Message()
                    };
                    emailRequest.Destination.ToAddresses.Add(to);
                    emailRequest.Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Html = new Content(html)
                        }
                    };
                    var responce = await client.SendEmailAsync(emailRequest);
                    //|| responce.MessageId == nul
                    if (responce == null )
                    { return false; }
                    else
                    {
                        return true;
                    }
                }
                //return true;
            }
            catch (Exception e)
            {
                throw new Exception("send email operation failed.", e);
            }
        }
        public bool SendEmailRawMessage(string from, string to, string subject, string html, string filename, dynamic content)
        {
            try
            {
                using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1))
                {
                    var sendRequest = new SendRawEmailRequest { RawMessage = new RawMessage(GetMessageStream(from, to, subject, html, filename, content)) };
                    try
                    {
                        var response = client.SendRawEmailAsync(sendRequest);
                        //Console.WriteLine("The email was sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("send email message operation failed.", e);
            }
        }
        public async Task<string> ReadImageData(string fileName)
        {
            try
            {
                using (var client = new AmazonS3Client(RegionEndpoint.USEast2))
                {
                    var request = new GetObjectRequest
                    {
                        BucketName = AWSS3Model.BitbucketName,
                        Key = fileName
                    };

                    using (var getObjectResponse = await client.GetObjectAsync(request))
                    {
                        using (var responseStream = getObjectResponse.ResponseStream)
                        {
                            var stream = new MemoryStream();
                            await responseStream.CopyToAsync(stream);
                            stream.Position = 0;
                            byte[] bytes = stream.ToArray();
                            string base64String = Convert.ToBase64String(bytes);
                            var types = GetMimeTypes();
                            var ext = Path.GetExtension(fileName).ToLowerInvariant();
                            var type = types[ext];
                            string base64 = "data:" + types[ext] + ";base64," + base64String;
                            return base64;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Read object operation failed.", exception);
            }
        }

        public async Task<bool> UploadImage(IFormFile file, string filePath)
        {
            using (var client = new AmazonS3Client(RegionEndpoint.EUCentral1))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    try
                    {
                        file.CopyTo(newMemoryStream);

                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = newMemoryStream,
                            Key = filePath,
                            BucketName = AWSS3Model.BitbucketName,
                            CannedACL = S3CannedACL.PublicRead
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        await fileTransferUtility.UploadAsync(uploadRequest);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
              
                }

            }
            return true;
        }

        public async Task<bool> DeleteImage(string filePath)
        {
            using (var client = new AmazonS3Client(RegionEndpoint.USEast2))
            {
                DeleteObjectRequest request = new DeleteObjectRequest
                {
                    BucketName = AWSS3Model.BitbucketName,
                    Key = filePath
                };
                await client.DeleteObjectAsync(request);

            }
            return true;
        }

        public async Task<bool> UploadFile(IFormFile file, string filePath)
        {
            using (var client = new AmazonS3Client(RegionEndpoint.EUCentral1))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = filePath,
                        BucketName = AWSS3Model.BitbucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }

            }
            return true;
        }

        public async Task<bool> UploadFileTempSim(MemoryStream file, string filePath)
        {
            using (var client = new AmazonS3Client(RegionEndpoint.USEast2))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = filePath,
                        BucketName = AWSS3Model.BitbucketName,
                        CannedACL = S3CannedACL.BucketOwnerFullControl
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }

            }
            return true;
        }

        public async Task<Stream> ReadObjectData(string filePath)
        {
            try
            {
                using (var client = new AmazonS3Client(RegionEndpoint.EUCentral1))
                {
                    var request = new GetObjectRequest
                    {
                        BucketName = AWSS3Model.BitbucketName,
                        Key = filePath,
                    };

                    using (var getObjectResponse = await client.GetObjectAsync(request))
                    {
                        using (var responseStream = getObjectResponse.ResponseStream)
                        {
                            var stream = new MemoryStream();
                            await responseStream.CopyToAsync(stream);
                            stream.Position = 0;
                            return stream;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Read object operation failed.", exception);
            }
        }

        public Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public async Task<dynamic> ReadS3ExcelAsync(string filePath)
        {
            try
            {
                dynamic[] worksheets = new dynamic[10];
                dynamic T_Rise = 0;
                dynamic Overload = 0;
                using (var client = new AmazonS3Client(RegionEndpoint.USEast2))
                {
                    var request = new GetObjectRequest
                    {
                        BucketName = AWSS3Model.BitbucketName,
                        Key = filePath,
                    };
                    using (var getObjectResponse = await client.GetObjectAsync(request))
                    {
                        using (var package = new ExcelPackage(getObjectResponse.ResponseStream))
                        {
                            var workBook = package.Workbook;
                            if (workBook != null)
                            {
                                if (workBook.Worksheets.Count > 0)
                                {
                                    worksheets[0] = workBook.Worksheets["Derating"].Cells[3, 2, 31, 2].Value;
                                    worksheets[1] = workBook.Worksheets["Derating"].Cells[3, 3, 31, 3].Value;
                                    worksheets[2] = workBook.Worksheets["Derating"].Cells[3, 5, 4, 5].Value;
                                    worksheets[3] = workBook.Worksheets["Derating"].Cells[3, 6, 4, 6].Value;
                                    worksheets[4] = workBook.Worksheets["T_Rise"].Cells[3, 2, 13, 2].Value;
                                    worksheets[5] = workBook.Worksheets["T_Rise"].Cells[3, 3, 13, 3].Value;
                                    worksheets[6] = workBook.Worksheets["Overload"].Cells[4, 3, 46, 3].Value;
                                    worksheets[7] = workBook.Worksheets["Overload"].Cells[4, 9, 46, 9].Value;
                                    worksheets[8] = workBook.Worksheets["Overload"].Cells[4, 1, 46, 1].Value;
                                    worksheets[9] = workBook.Worksheets["Overload"].Cells[4, 9, 46, 9].Value;
                                }
                                else
                                {
                                    int hello = workBook.Worksheets.Count;

                                }
                            }
                            return worksheets;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Read object operation failed.", exception);
            }
        }
        public async Task<dynamic> ReadS3ExcelAsyncGraph(string filePath)
        {
            try
            {
                dynamic[] worksheets = new dynamic[10];
                dynamic Tabelle1 = 0;
                dynamic Tabelle2 = 0;
                using (var client = new AmazonS3Client(RegionEndpoint.USEast2))
                {
                    var request = new GetObjectRequest
                    {
                        BucketName = AWSS3Model.BitbucketName,
                        Key = filePath,
                    };
                    using (var getObjectResponse = await client.GetObjectAsync(request))
                    {
                        using (var package = new ExcelPackage(getObjectResponse.ResponseStream))
                        {
                            var workBook = package.Workbook;
                            if (workBook != null)
                            {
                                if (workBook.Worksheets.Count > 0)
                                {
                                    worksheets[0] = workBook.Worksheets["Tabelle2"].Cells[2, 2, 10700, 2].Value;
                                    worksheets[1] = workBook.Worksheets["Tabelle2"].Cells[2, 6, 10700, 6].Value;

                                }
                                else
                                {
                                    int hello = workBook.Worksheets.Count;

                                }
                            }
                            return worksheets;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Read object operation failed.", exception);
            }
        }
    }
}
